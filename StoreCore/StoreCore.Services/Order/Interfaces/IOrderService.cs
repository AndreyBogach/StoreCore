using System.Collections.Generic;
using StoreCore.Common;

namespace StoreCore.Services.Order.Interfaces
{
    public interface IOrderService
    {
        OperationResult AddOrder(Model.Order.Order order);
        IEnumerable<Model.Order.Order> GetOrders();
        Model.Order.Order GetOrder(int order_id);
        bool DeleteOrder(int order_id);
        OperationResult UpdateOrder(Model.Order.Order order);
    }
}
