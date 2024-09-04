namespace DevStart_WebMvcUI.Models
{
    public class CartItem
    {
        public Guid CourseId { get; set; }
        public string CourseTitle { get; set; }
        public int CourseQuantity { get; set; }
        public decimal CoursePrice { get; set; }

        public static List<CartItem> AddToCart(List<CartItem> cart, CartItem cartItem)
        {
            var item = cart.Find(c => c.CourseId == cartItem.CourseId);
            if (item != null)
            {
                item.CourseQuantity += cartItem.CourseQuantity;
            }
            else
            {
                cart.Add(cartItem);
            }
            return cart;
        }

        public static List<CartItem> DeleteFromCart(List<CartItem> cart, Guid courseId)
        {
            cart.RemoveAll(c => c.CourseId == courseId);
            return cart;
        }

        public int TotalQuantity(List<CartItem> cart)
        {
            int total = cart.Sum(c => c.CourseQuantity);
            return total;
        }

        public decimal TotalPrice(List<CartItem> cart)
        {
            decimal total = cart.Sum(c => c.CourseQuantity * c.CoursePrice);
            return total;
        }
    }
}