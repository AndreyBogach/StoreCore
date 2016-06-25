using System;

namespace StoreCore.Model.Product.Interfaces
{
    public interface IProduct
    {
        int Id { get; }
        string Name { get; }
        int Count { get; }
        decimal Price { get; }
        DateTime CreationDate { get; }
    }
}
