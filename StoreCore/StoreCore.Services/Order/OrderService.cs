using System;
using System.Collections.Generic;
using System.Linq;
using StoreCore.Common;
using StoreCore.Data;
using StoreCore.Services.Order.Interfaces;

namespace StoreCore.Services.Order
{
    //record_state = 0 created item
    //record_state = 1 deleted item
    //record_state = 2 updated item

    public class OrderService : IOrderService
    {
        public OperationResult AddOrder(Model.Order.Order order)
        {
            var result = OperationResult.CreateWithSuccess();

            using (var context = new StoreCoreContext())
            {
                var client = context.inf_client.FirstOrDefault(f => f.id == order.ClientId && f.record_state != 1);
                var product = context.inf_product.FirstOrDefault(f => f.id == order.ProductId && f.record_state != 1);

                if(client == null)
                    return OperationResult.CreateWithError("Client ID is invalid!");

                if (product == null)
                    return OperationResult.CreateWithError("Product ID is invalid!");

                if (product.count < order.Count)
                    return OperationResult.CreateWithError("Your order amount is not available!");

                if (order != null)
                {
                    var dbOrder = context.inf_order.Create();

                    dbOrder.client_id = order.ClientId;
                    dbOrder.product_id = order.ProductId;
                    dbOrder.count = order.Count;
                    dbOrder.amount = product.price;
                    dbOrder.creation_date = DateTime.UtcNow;
                    dbOrder.record_updated = DateTime.UtcNow;
                    dbOrder.record_state = 0;

                    context.inf_order.Add(dbOrder);
                    context.SaveChanges();

                    product.count = product.count - order.Count;
                    product.record_state = 2;
                    product.record_updated = DateTime.UtcNow;

                    context.SaveChanges();
                }
                else { return OperationResult.CreateWithError("Error"); }
            }
            return result;
        }

        public bool DeleteOrder(int order_id)
        {
            using (var context = new StoreCoreContext())
            {
                var dbOrder = context.inf_order.Find(order_id);

                if (dbOrder != null)
                {
                    var product = context.inf_product.FirstOrDefault(f => f.id == dbOrder.product_id && f.record_state != 1);

                    dbOrder.record_updated = DateTime.UtcNow;
                    dbOrder.record_state = 1;

                    context.SaveChanges();

                    product.count = product.count + dbOrder.count;
                    product.record_state = 2;
                    product.record_updated = DateTime.UtcNow;

                    context.SaveChanges();

                    return true;
                }
                return false;
            }
        }

        public IEnumerable<Model.Order.Order> GetOrders()
        {
            using (var context = new StoreCoreContext())
            {
                return context.inf_order.Where(w => w.record_state != 1)
                    .OrderByDescending(o => o.creation_date)
                    .Select(s => new Model.Order.Order
                    {
                        Id = s.id,
                        Client = s.inf_client.name,
                        Product = s.inf_product.name,
                        Amount = s.amount,
                        Count = s.count,
                        CreationDate = s.creation_date
                    }).ToList();
            }
        }

        public Model.Order.Order GetOrder(int order_id)
        {
            using (var context = new StoreCoreContext())
            {
                return context.inf_order.Where(w => w.id == order_id && w.record_state != 1)
                    .Select(s => new Model.Order.Order
                    {
                        Id = s.id,
                        Client = s.inf_client.name,
                        ClientId = s.client_id,
                        ProductId = s.product_id,
                        Amount = s.amount,
                        Count = s.count
                    }).FirstOrDefault();
            }
        }

        public OperationResult UpdateOrder(Model.Order.Order order)
        {
            var result = OperationResult.CreateWithSuccess();

            using (var context = new StoreCoreContext())
            {
                var dbProduct = context.inf_product.FirstOrDefault(f => f.id == order.ProductId && f.record_state != 1);
                var dbOrder = context.inf_order.FirstOrDefault(f => f.id == order.Id && f.record_state != 1);

                if (dbProduct == null)
                    return OperationResult.CreateWithError("Product ID is invalid!");

                if (dbProduct.count < order.Count)
                    return OperationResult.CreateWithError("Your order amount is not available!");

                if (dbOrder != null)
                {
                    var productId = dbOrder.product_id;
                    var prodAmount = dbOrder.count;

                    dbOrder.product_id = order.ProductId;
                    dbOrder.count = order.Count;
                    dbOrder.amount = dbProduct.price;
                    dbOrder.record_updated = DateTime.UtcNow;
                    dbOrder.record_state = 2;
                    
                    context.SaveChanges();

                    if(prodAmount < order.Count)
                    {
                        var res = order.Count - prodAmount;

                        dbProduct.count = dbProduct.count - res;
                        dbProduct.record_state = 2;
                        dbProduct.record_updated = DateTime.UtcNow;

                        context.SaveChanges();
                    }

                    if(prodAmount > order.Count)
                    {
                        var res1 = prodAmount - order.Count;

                        dbProduct.count = dbProduct.count + res1;
                        dbProduct.record_state = 2;
                        dbProduct.record_updated = DateTime.UtcNow;

                        context.SaveChanges();
                    }

                    if(dbOrder.product_id != productId)
                    {
                        var dbOldProduct = context.inf_product.FirstOrDefault(f => f.id == productId & f.record_state != 1);

                        if(dbOldProduct != null)
                        {
                            dbOldProduct.count = dbOldProduct.count + prodAmount;
                            dbOldProduct.record_state = 2;
                            dbOldProduct.record_updated = DateTime.UtcNow;

                            context.SaveChanges();

                            dbProduct.count = dbProduct.count - prodAmount;
                            dbProduct.record_state = 2;
                            dbProduct.record_updated = DateTime.UtcNow;

                            context.SaveChanges();
                        }
                    }
                }
                else { return OperationResult.CreateWithError("Error"); }
            }
            return result;
        }
    }
}
