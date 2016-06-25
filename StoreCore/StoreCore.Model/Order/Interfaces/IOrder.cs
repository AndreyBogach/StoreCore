using System;

namespace StoreCore.Model.Order.Interfaces
{
    public interface IOrder
    {
        int Id { get; }
        int ClientId { get; }
        int ProductId { get; }
        string Client { get; }
        string Product { get; }
        int Count { get; }
        decimal Amount { get; }
        decimal AmountTotal { get; }
        DateTime CreationDate { get; }
    }
}
