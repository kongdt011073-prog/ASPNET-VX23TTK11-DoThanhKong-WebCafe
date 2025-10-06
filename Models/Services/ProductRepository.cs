
using Microsoft.EntityFrameworkCore;
using SCoffee.Data;
using SCoffee.Models.Domain;
using SCoffee.Models.Interfaces;

namespace SCoffee.Models.Services
{
    public class ProductRepository: IProductRepository
    {
        private SCoffeeDbContext dbContext;
        public ProductRepository(SCoffeeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<Product> ListProducts()
        {
            return dbContext.Products;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return dbContext.Products;
        }
        public Product? GetProductDetail(int id)
        {
            return dbContext.Products.FirstOrDefault(p => p.Id == id);
        }
        public IEnumerable<Product> GetTrendingProducts()
        {
            return dbContext.Products.Where(p => p.IsTrendingProduct);
        }
        public IEnumerable<Product> SearchProducts(string keyword)
        {
            return dbContext.Products
                            .Where(p => p.Name.Contains(keyword) || p.Detail.Contains(keyword))
                            .ToList();
        }

        public IEnumerable<Product> FilterProductsByPrice(decimal minPrice, decimal maxPrice)
        {
            return dbContext.Products
                            .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                            .ToList();
        }

        public Product GetProductById(int id)
        {
            return dbContext.Products.Find(id);
        }
        public void AddProduct(Product product)
        {
            dbContext.Products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            dbContext.Entry(product).State = EntityState.Modified;
        }

        public void DeleteProduct(int id)
        {
            var product = dbContext.Products.Find(id);
            if (product != null)
            {
                dbContext.Products.Remove(product);
                dbContext.SaveChanges();
            }
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
    }
}
