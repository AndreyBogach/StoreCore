using System.Linq;
using System.Web.Mvc;
using PagedList;
using StoreCore.Services.Order;
using StoreCore.Services.Order.Interfaces;
using StoreCore.Web.Models;

namespace StoreCore.Web.Controllers
{
    public class OrderController : Controller
    {
        readonly IOrderService _orderService = new OrderService();
        readonly int pageSize = 20;

        // GET: Order
        public ActionResult GetOrders(int? page)
        {
            int pageNumber = (page ?? 1);

            var model = _orderService.GetOrders();
            return View(model.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult AddOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddOrder(OrderViewModel order)
        {
            #region Validation
            if (order.Client <= 0)
            {
                ModelState.AddModelError("Client", "The Client ID must be greater than zero");
            }

            if (order.Product <= 0)
            {
                ModelState.AddModelError("Product", "The Product ID must be greater than zero");
            }

            if (order.Count <= 0)
            {
                ModelState.AddModelError("Count", "The amount must be greater than zero");
            }
            #endregion

            if (ModelState.IsValid)
            {
                var res = _orderService.AddOrder(new Model.Order.Order()
                {
                    ClientId = order.Client,
                    ProductId = order.Product,
                    Count = order.Count
                });

                if(!res.Success)
                {
                    ViewBag.MessageResult = res.Errors.First(); // "ClientID or ProductID is invalid!"
                    return View(order);
                }

                return RedirectToAction("GetOrders");
            }
            else
            {
                return View(order);
            }
        }

        public ActionResult DeleteOrder(int id)
        {
            var res = _orderService.DeleteOrder(id);
            
            return RedirectToAction("GetOrders");
        }

        public ActionResult UpdateOrder(int id)
        {
            var order = _orderService.GetOrder(id);
            var model = new OrderViewModel()
            {
                ClientName = order.Client,
                Client = order.ClientId,
                Product = order.ProductId,
                Count = order.Count
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateOrder(OrderViewModel order, int id)
        {
            #region Validation
            if (order.Client <= 0)
            {
                ModelState.AddModelError("Client", "The Client ID must be greater than zero");
            }

            if (order.Product <= 0)
            {
                ModelState.AddModelError("Product", "The Product ID must be greater than zero");
            }

            if (order.Count <= 0)
            {
                ModelState.AddModelError("Count", "The amount must be greater than zero");
            }
            #endregion

            if (ModelState.IsValid)
            {
                var res = _orderService.UpdateOrder(new Model.Order.Order()
                {
                    Id = id,
                    ProductId = order.Product,
                    Count = order.Count
                });

                if (!res.Success)
                {
                    ViewBag.MessageResult = res.Errors.First();
                    return View(order);
                }

                return RedirectToAction("GetOrders");
            }
            else
            {
                return View(order);
            }
        }
    }
}