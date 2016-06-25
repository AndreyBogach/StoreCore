using System.Web.Mvc;
using PagedList;
using StoreCore.Services.Product;
using StoreCore.Services.Product.Interfaces;
using StoreCore.Web.Models;

namespace StoreCore.Web.Controllers
{
    public class ProductController : Controller
    {
        readonly IProductService _productService = new ProductService();
        readonly int pageSize = 20;

        // GET: Product
        public ActionResult GetProducts(int? page)
        {
            int pageNumber = (page ?? 1);

            var model = _productService.GetProducts();
            return View(model.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductViewModel product)
        {
            #region Validation
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                ModelState.AddModelError("Name", "Please fill out in the form");
            }

            if (product.Count <= 0)
            {
                ModelState.AddModelError("Count", "The amount must be greater than zero");
            }

            if (product.Price <= 0)
            {
                ModelState.AddModelError("Price", "The price must be greater than zero");
            }
            #endregion

            if (ModelState.IsValid)
            {
                var resProduct = _productService.AddProduct(new Model.Product.Product()
                {
                    Name = product.Name,
                    Count = product.Count,
                    Price = product.Price
                });

                return RedirectToAction("GetProducts");
            }
            else
            {
                return View(product);
            }
        }

        public ActionResult DeleteProduct(int id)
        {
            var res = _productService.DeleteProduct(id);

            return RedirectToAction("GetProducts");
        }

        public ActionResult UpdateProduct(int id)
        {
            var product = _productService.GetProduct(id);
            var model = new ProductViewModel()
            {
                Name = product.Name,
                Count = product.Count,
                Price = product.Price
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateProduct(ProductViewModel product, int id)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                ModelState.AddModelError("Name", "Please fill out in the form");
            }

            if (product.Count <= 0)
            {
                ModelState.AddModelError("Count", "The amount must be greater than zero");
            }

            if (product.Price <= 0)
            {
                ModelState.AddModelError("Price", "The price must be greater than zero");
            }

            if (ModelState.IsValid)
            {
                var res = _productService.UpdateProduct(new Model.Product.Product()
                {
                    Id = id,
                    Name = product.Name,
                    Count = product.Count,
                    Price = product.Price
                });

                return RedirectToAction("GetProducts");
            }
            else
            {
                return View(product);
            }
        }
    }
}