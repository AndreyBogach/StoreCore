using System;
using StoreCore.Model.Order.Interfaces;

namespace StoreCore.Model.Order
{
    public class Order: IOrder
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public string Client { get; set; }
        public string Product { get; set; }
        public int Count { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountTotal
        {
            get { return Amount*Count; }
        }
        public DateTime CreationDate { get; set; }
    }
}
