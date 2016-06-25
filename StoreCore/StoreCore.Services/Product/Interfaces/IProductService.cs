using System.Collections.Generic;

namespace StoreCore.Services.Product.Interfaces
{
    public interface IProductService
    {
        bool AddProduct(Model.Product.Product product);
        IEnumerable<Model.Product.Product> GetProducts();
        Model.Product.Product GetProduct(int product_id);
        bool DeleteProduct(int product_id);
        bool UpdateProduct(Model.Product.Product product);
    }
}
