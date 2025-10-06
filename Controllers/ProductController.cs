using Microsoft.AspNetCore.Mvc;
using SCoffee.Models.Domain;
using SCoffee.Models.Interfaces;

namespace SCoffee.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository productRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductController(IProductRepository productRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.productRepository = productRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Shop()
        {
            return View(productRepository.GetAllProducts());
        }

        public IActionResult ListProduct()
        {
            return View(productRepository.ListProducts());
        }

        public IActionResult Search(string keyword)
        {
            var products = productRepository.SearchProducts(keyword);
            return View("Shop", products);
        }

        public IActionResult FilterByPrice(decimal minPrice, decimal maxPrice)
        {
            var products = productRepository.FilterProductsByPrice(minPrice, maxPrice);
            return View("Shop", products);
        }

        public IActionResult Detail(int id)
        {
            var product = productRepository.GetProductDetail(id);
            if (product != null)
            {
                return View(product);
            }
            return NotFound();
        }

        // add product
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Product product, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Lưu ảnh vào thư mục wwwroot/images
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(fileStream);
                    }

                    // Lưu tên file vào database
                    product.ImageUrl = uniqueFileName;
                }

                productRepository.AddProduct(product);
                productRepository.SaveChanges();
                return RedirectToAction("ListProduct");
            }
            return View(product);
        }

        //edit product
       public ActionResult Edit(int id)
        {
            var product = productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = productRepository.GetProductById(product.Id);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                existingProduct.Name = product.Name;
                existingProduct.Detail = product.Detail;
                existingProduct.Price = product.Price;
                existingProduct.IsTrendingProduct = product.IsTrendingProduct;

                if (imageFile != null && imageFile.Length > 0)
                {
                    // Xóa ảnh cũ nếu có
                    if (!string.IsNullOrEmpty(existingProduct.ImageUrl))
                    {
                        string oldImagePath = Path.Combine(webHostEnvironment.WebRootPath, "images", existingProduct.ImageUrl);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // Lưu ảnh mới
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(fileStream);
                    }

                    existingProduct.ImageUrl = uniqueFileName;
                }

                productRepository.UpdateProduct(existingProduct);
                productRepository.SaveChanges();
                return RedirectToAction("ListProduct");
            }
            return View(product);
        }


        //delete product
        // Hiển thị form xác nhận xóa
        public ActionResult Delete(int id)
        {
            var product = productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // Xóa sản phẩm
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var productToDelete = productRepository.GetProductById(id);
            if (productToDelete == null)
            {
                return NotFound();
            }

            productRepository.DeleteProduct(id);
            productRepository.SaveChanges();

            return RedirectToAction("ListProduct");
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var productToDelete = productRepository.GetProductById(id);
            if (productToDelete == null)
            {
                return NotFound();
            }

            // Xóa ảnh trong thư mục wwwroot/images nếu có
            if (!string.IsNullOrEmpty(productToDelete.ImageUrl))
            {
                string imagePath = Path.Combine(webHostEnvironment.WebRootPath, "images", productToDelete.ImageUrl);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            productRepository.DeleteProduct(id);
            productRepository.SaveChanges();

            return RedirectToAction("ListProduct");
        }

    }
}
