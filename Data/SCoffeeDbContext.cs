using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SCoffee.Models.Domain;

namespace SCoffee.Data
{
    public class SCoffeeDbContext: IdentityDbContext
    {
        public SCoffeeDbContext(DbContextOptions<SCoffeeDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        //seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "Espresso",
                Price = 20,
                Detail = "Espresso là loại cà phê nguyên chất, được pha bằng cách ép nước nóng qua bột cà phê xay nhuyễn với áp suất cao. Nó có vị đậm đà, hơi đắng và có một lớp crema (bọt) màu nâu trên bề mặt.",
                ImageUrl = "https://insanelygoodrecipes.com/wp-content/uploads/2020/07/Cup-Of-Creamy-Coffee-1024x536.webp"
            },
            new Product
            {
                Id = 2,
                Name = "Americano",
                Price = 21,
                Detail = "Americano là Espresso pha loãng với nước nóng, giúp giảm độ đậm đặc và tạo ra hương vị nhẹ nhàng hơn, nhưng vẫn giữ được sự thơm ngon của cà phê nguyên chất.",
                ImageUrl = "https://insanelygoodrecipes.com/wp-content/uploads/2020/07/Cup-Of-Creamy-Coffee-1024x536.webp"
            },
            new Product
            {
                Id = 3,
                Name = "Cappuccino",
                Price = 15,
                Detail = "Một loại cà phê Ý gồm 3 phần bằng nhau: Espresso, sữa nóng và bọt sữa. Cappuccino có lớp bọt sữa dày và thường được rắc thêm bột cacao hoặc quế lên trên.",
                ImageUrl = "https://insanelygoodrecipes.com/wp-content/uploads/2020/07/Cup-Of-Creamy-Coffee-1024x536.webp"
            },
            new Product
            {
                Id = 4,
                Name = "Flat White",
                Price = 16,
                Detail = "Flat White có nguồn gốc từ Úc và New Zealand, tương tự Latte nhưng có tỉ lệ Espresso cao hơn và ít bọt sữa hơn, giúp giữ nguyên vị cà phê đậm đà.",
                ImageUrl = "https://insanelygoodrecipes.com/wp-content/uploads/2020/07/Cup-Of-Creamy-Coffee-1024x536.webp"
            },
            new Product
            {
                Id = 5,
                Name = "Latte",
                Price = 25,
                Detail = "Latte có thành phần tương tự Cappuccino nhưng chứa nhiều sữa nóng hơn, tạo ra vị cà phê nhẹ nhàng, béo ngậy, phù hợp với những ai thích cà phê ít đắng.",
                ImageUrl = "https://insanelygoodrecipes.com/wp-content/uploads/2020/07/Cup-Of-Creamy-Coffee-1024x536.webp"
            },
            new Product
            {
                Id = 6,
                Name = "Mocha",
                Price = 35,
                Detail = "Mocha là sự kết hợp giữa Espresso, sữa nóng và chocolate syrup, tạo ra một hương vị ngọt ngào, béo thơm và hấp dẫn cho những người thích sự hòa quyện giữa cà phê và chocolate.",
                ImageUrl = "https://insanelygoodrecipes.com/wp-content/uploads/2020/07/Cup-Of-Creamy-Coffee-1024x536.webp"
            },
            new Product
            {
                Id = 7,
                Name = "Macchiato",
                Price = 25,
                Detail = "Có hai loại Macchiato phổ biến:\r\n\r\nEspresso Macchiato: Một ly Espresso với một ít bọt sữa bên trên, giúp làm dịu vị đắng.\r\nCaramel Macchiato: Một biến thể phổ biến hơn, gồm sữa, Espresso, và caramel tạo nên hương vị ngọt ngào.",
                ImageUrl = "https://insanelygoodrecipes.com/wp-content/uploads/2020/07/Cup-Of-Creamy-Coffee-1024x536.webp"
            },
            new Product
            {
                Id = 8,
                Name = "Cold Brew",
                Price = 20,
                Detail = "Cold Brew được pha bằng cách ngâm cà phê xay trong nước lạnh từ 12–24 giờ, cho ra hương vị nhẹ nhàng, ít chua và rất mát lạnh.",
                ImageUrl = "https://insanelygoodrecipes.com/wp-content/uploads/2020/07/Cup-Of-Creamy-Coffee-1024x536.webp"
            }
            );
        }
    }
}
