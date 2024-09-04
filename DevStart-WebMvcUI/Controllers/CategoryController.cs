using Azure;
using DevStart_Entity.Interfaces;
using DevStart_Entity.ViewModels;
using DevStart_Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevStart_WebMvcUI.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Index(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CategoryId = Guid.NewGuid(); //id üretilmiş oldu.
                await _categoryService.AddAsync(model);
                TempData["message1"] = true;
                TempData["message2"] = "Yeni Kategori Kaydedildi";
            }
            else
            {
                TempData["message1"] = false;
                TempData["message2"] = "Kategori Kaydedilemedi";
            }
            return View();
        }

        public async Task<IActionResult> Update(Guid CategoryId)
        {
            var category = await _categoryService.GetByIdAsync(CategoryId);
          return View(category);
        }


        [HttpPost]
        public async Task<IActionResult> Update(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryViewModel);
            }

            try
            {
                await _categoryService.UpdateAsync(categoryViewModel); // Asenkron güncellemeyi gerçekleştir
                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu: " + ex.Message);
                return View(categoryViewModel);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(Guid CategoryId)
        {
            await _categoryService.DeleteAsync(CategoryId);
            return RedirectToAction("Index");
        }
    }
}
