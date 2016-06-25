using System;
using StoreCore.Model.Product.Interfaces;

namespace StoreCore.Model.Product
{
    public class Product: IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
