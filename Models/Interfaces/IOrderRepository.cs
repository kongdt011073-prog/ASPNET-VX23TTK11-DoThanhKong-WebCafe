using SCoffee.Models.Domain;

namespace SCoffee.Models.Interfaces
{
    public interface IOrderRepository
    {
        void PlaceOrder(Order order);
    }
}
