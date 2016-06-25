using System;
using System.Collections.Generic;
using System.Linq;
using StoreCore.Data;
using StoreCore.Services.Product.Interfaces;

namespace StoreCore.Services.Product
{
    //record_state = 0 created item
    //record_state = 1 deleted item
    //record_state = 2 updated item
    public class ProductService : IProductService
    {
        public bool AddProduct(Model.Product.Product product)
        {
            using (var context = new StoreCoreContext())
            {
                if (product != null)
                {
                    var dbProduct = context.inf_product.Create();

                    dbProduct.name = product.Name;
                    dbProduct.count = product.Count;
                    dbProduct.price = product.Price;
                    dbProduct.creation_date = DateTime.UtcNow;
                    dbProduct.record_updated = DateTime.UtcNow;
                    dbProduct.record_state = 0;

                    context.inf_product.Add(dbProduct);

                    context.SaveChanges();

                    return true;
                }
                return false;
            }
        }

        public bool DeleteProduct(int product_id)
        {
            using (var context = new StoreCoreContext())
            {
                var dbProduct = context.inf_product.Find(product_id);

                if (dbProduct != null)
                {
                    dbProduct.record_updated = DateTime.UtcNow;
                    dbProduct.record_state = 1;

                    context.SaveChanges();

                    return true;
                }
                return false;
            }
        }

        public Model.Product.Product GetProduct(int product_id)
        {
            using (var context = new StoreCoreContext())
            {
                return context.inf_product.Where(w => w.id == product_id && w.record_state != 1)
                    .Select(s => new Model.Product.Product
                    {
                        Id = s.id,
                        Name = s.name,
                        Count = s.count,
                        Price = s.price,
                        CreationDate = s.creation_date
                    }).FirstOrDefault();
            }
        }

        public IEnumerable<Model.Product.Product> GetProducts()
        {
            using (var context = new StoreCoreContext())
            {
                return context.inf_product.Where(w => w.record_state != 1)
                    .OrderByDescending(o => o.creation_date)
                    .Select(s => new Model.Product.Product
                    {
                        Id = s.id,
                        Name = s.name,
                        Count = s.count,
                        Price = s.price,
                        CreationDate = s.creation_date
                    }).ToList();
            }
        }

        public bool UpdateProduct(Model.Product.Product product)
        {
            using (var context = new StoreCoreContext())
            {
                var dbProduct = context.inf_product.Find(product.Id);

                if (dbProduct != null)
                {
                    dbProduct.name = product.Name;
                    dbProduct.count = product.Count;
                    dbProduct.price = product.Price;
                    dbProduct.record_updated = DateTime.UtcNow;
                    dbProduct.record_state = 2;

                    context.SaveChanges();

                    return true;
                }
                return false;
            }
        }
    }
}
