
using Microsoft.EntityFrameworkCore;
using SCoffee.Data;
using SCoffee.Models.Domain;
using SCoffee.Models.Interfaces;

namespace SCoffee.Models.Services
{
    public class OrderRepository: IOrderRepository
    {
        private SCoffeeDbContext dbcontext;
        private IShoppingCartRepository shoppingCartRepository;
        public OrderRepository(SCoffeeDbContext dbcontext, IShoppingCartRepository shoppingCartRepository)
        {
            this.dbcontext = dbcontext;
            this.shoppingCartRepository = shoppingCartRepository;
        }
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
    }
}
