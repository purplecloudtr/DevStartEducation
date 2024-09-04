using DevStart_DataAccsess.Contexts;
using DevStart_Entity.Entities;
using DevStart_Entity.Interfaces;
using DevStart_Entity.ViewModels;
using DevStart_Service.Extensions;
using DevStart_Service.Services;
using DevStart_WebMvcUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DevStart_WebMvcUI.Controllers
{
    public class CartController : Controller
    {
        private readonly IRepository<Course> _courseRepo;
        private readonly ICourseSaleService _courseSaleService;
        private readonly ICourseSaleDetailService _courseSaleDetailService;

        public CartController(IRepository<Course> courseRepo, ICourseSaleService courseSaleService, ICourseSaleDetailService courseSaleDetailService)
        {
            _courseRepo = courseRepo;
            _courseSaleService = courseSaleService;
            _courseSaleDetailService = courseSaleDetailService;
        }

        List<CartItem> cart = new List<CartItem>(); //bu satır SEPET!!
        CartItem cartItem = new CartItem();
        public IActionResult Index()
        {
            var cart = GetCart(); // Session'dan sepeti alıyoruz.

            TempData["ToplamAdet"] = cartItem.TotalQuantity(cart);

            if (cartItem.TotalPrice(cart) > 0)
                TempData["ToplamTutar"] = cartItem.TotalPrice(cart);

            return View(cart);
        }

        public int GetCartAcount()
        {
            var cart = GetCart();
            return cartItem.TotalQuantity(cart);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Guid CourseId)
        {
            var course = await _courseRepo.GetByIdAsync(CourseId); // Sipariş edilecek ürünü buluyorum burada.

            var cart = GetCart(); // Sepetimi alıyorum
            int adet = 1;
            var cartItem = new CartItem
            {
                CourseId = course.CourseId,
                CourseTitle = course.CourseTitle,
                CourseQuantity = adet,
                CoursePrice = course.CoursePrice
            };

            cart = CartItem.AddToCart(cart, cartItem); // Yeni siparişi sepete ekliyoruz.

            SetCart(cart);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid courseId)
        {
            var cart = GetCart();
            cart = CartItem.DeleteFromCart(cart, courseId);
            SetCart(cart); // Session'a sepetin son halini kayıt ediyoruz.
            return RedirectToAction("Index");
        }

        public void SetCart(List<CartItem> sepet)
        {
            HttpContext.Session.SetJson("sepet", sepet); // Alışveriş sepetimizi session'a kayıt ediyoruz.
        }

        public List<CartItem> GetCart()
        {
            return HttpContext.Session.GetJson<List<CartItem>>("sepet") ?? new List<CartItem>();
        }

        public IActionResult DeleteCart()
        {
            HttpContext.Session.Remove("sepet"); // Sepeti boşalt
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var cartList =  HttpContext.Session.GetJson<List<CartItem>>("sepet") ?? new List<CartItem>();
            return View(cartList);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(int? x)
        {
            var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var cart = GetCart();
            if (!cart.Any())
            {
                return RedirectToAction("Index");
            }
            var courseSaleId = Guid.NewGuid();
            var courseSale = new CourseSaleViewModel
            {
                CourseSaleId = courseSaleId,
                CourseSaleDate = DateTime.Now,
                CourseSaleTotalPrice = cart.Sum(item => item.CoursePrice * item.CourseQuantity),
                CourseSaleState = true,
                UserId = userId
            };

            await _courseSaleService.AddAsync(courseSale);

            foreach (var item in cart)
            {
                var courseSaleDetail = new CourseSaleDetailViewModel
                {
                    CourseSaleDetailId = Guid.NewGuid(),
                    CourseSaleDetailQuantity = item.CourseQuantity,
                    CourseSaleDetailState = true,
                    CourseSaleId = courseSaleId,
                    CourseId = item.CourseId
                };

                await _courseSaleDetailService.AddAsync(courseSaleDetail);
            }


            DeleteCart();

            return RedirectToAction("Index","Home");
        }
    }


}