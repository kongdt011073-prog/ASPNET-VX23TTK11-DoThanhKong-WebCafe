using SCoffee.Models.Domain;

namespace SCoffee.Models.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> ListProducts();
        IEnumerable<Product> GetTrendingProducts();
        IEnumerable<Product> SearchProducts(string keyword);
        IEnumerable<Product> FilterProductsByPrice(decimal minPrice, decimal maxPrice);
        Product GetProductDetail(int id);
        Product GetProductById(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        void SaveChanges();
    }
}
