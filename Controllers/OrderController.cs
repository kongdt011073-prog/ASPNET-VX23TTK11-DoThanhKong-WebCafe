using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCoffee.Data;
using SCoffee.Models.Domain;
using SCoffee.Models.Interfaces;

namespace SCoffee.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private SCoffeeDbContext dbcontext;
        private IOrderRepository orderRepository;
        private IShoppingCartRepository shoppingCartRepository;
        public OrderController(IOrderRepository oderRepository, IShoppingCartRepository shoppingCartRepossitory, SCoffeeDbContext dbcontext)
        {
            this.orderRepository = oderRepository;
            this.shoppingCartRepository = shoppingCartRepossitory;
            this.dbcontext = dbcontext;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void PlaceOrder(Order order)
        {
            var shoppingCartItems = shoppingCartRepository.GetAllShoppingCartItems();
            order.OrderDetails = new List<OrderDetail>();
            foreach (var item in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Quantity = item.Qty,
                    ProductId = item.Product.Id,
                    Price = item.Product.Price
                };
                order.OrderDetails.Add(orderDetail);
            }
            order.OrderPlaced = DateTime.Now;
            order.OrderTotal = shoppingCartRepository.GetShoppingCartTotal();
            dbcontext.Orders.Add(order);
            dbcontext.SaveChanges();
        }

        public IActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            orderRepository.PlaceOrder(order);
            shoppingCartRepository.ClearCart();
            HttpContext.Session.SetInt32("CartCount", 0);
            return RedirectToAction("CheckoutComplete");
        }

        public IActionResult CheckoutComplete()
        {
            return View();
        }
    }
}
