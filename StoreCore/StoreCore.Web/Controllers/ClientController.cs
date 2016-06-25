using System.Web.Mvc;
using System.Linq;
using PagedList;
using StoreCore.Services.Client;
using StoreCore.Services.Client.Interfaces;
using StoreCore.Web.Models;

namespace StoreCore.Web.Controllers
{
    public class ClientController : Controller
    {
        readonly IClientService _clientService = new ClientService();
        readonly int pageSize = 20;

        // GET: Client
        public ActionResult GetPhysicals(int? page)
        {
            int pageNumber = (page ?? 1);

            var model = _clientService.GetAllPhysicalClients();
            return View(model.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult GetLegals(int? page)
        {
            int pageNumber = (page ?? 1);

            var model = _clientService.GetAllLegalClients();
            return View(model.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult AddPhysicalClient()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPhysicalClient(PhysicalViewModel client)
        {
            if (string.IsNullOrWhiteSpace(client.Name))
            {
                ModelState.AddModelError("Name", "Please fill out in the form");
            }

            if (ModelState.IsValid)
            {
                var resClient = _clientService.AddClient(2, client.Name);

                if (!resClient.Success)
                    return View("Error");

                var resPhysical = _clientService.AddPhysicalEntity(new Model.Client.PhysicalEntity()
                {
                    ClientId = (int)resClient.Data["id"],
                    Name = client.Name,
                    PassportNumber = client.Passport,
                    DateOfBirth = client.DateOfBirth,
                    Address = new Model.Address.Address()
                    {
                        Country = client.Country,
                        City = client.City,
                        Street = client.Street,
                        HomeNumber = client.Home
                    }
                });

                if (!resPhysical.Success)
                    return View("Error");

                return RedirectToAction("GetPhysicals");
            }
            else
            {
                return View(client);
            }
        }

        public ActionResult AddLegalClient()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddLegalClient(LegalViewModel client)
        {
            if (string.IsNullOrWhiteSpace(client.Name))
            {
                ModelState.AddModelError("Name", "Please fill out in the form");
            }

            if (client.TIN <= 0 || client.TIN > 999999999999)
            {
                ModelState.AddModelError("TIN", "Please fill out in the form(maximum 12 symbol)");
            }

            if (ModelState.IsValid)
            {
                var resClient = _clientService.AddClient(1, client.Name);

                if (!resClient.Success)
                    return View("Error");

                var resPhysical = _clientService.AddLegalEntity(new Model.Client.LegalEntity()
                {
                    ClientId = (int)resClient.Data["id"],
                    Name = client.Name,
                    TIN = client.TIN,
                    Address = new Model.Address.Address()
                    {
                        Country = client.Country,
                        City = client.City,
                        Street = client.Street,
                        HomeNumber = client.Home
                    }
                });

                if (!resPhysical.Success)
                    return View("Error");

                return RedirectToAction("GetLegals");
            }
            else
            {
                return View(client);
            }
        }

        public ActionResult DeleteClient(int id)
        {
            var type = _clientService.ClientType(id);
            _clientService.Delete(id);

            if(type == 0 || type == 2)
                return RedirectToAction("GetPhysicals");

            return RedirectToAction("GetLegals");
        }

        public ActionResult UpdateLegalClient(int id)
        {
            var client = _clientService.GetLegalClient(id);
            var model = new LegalViewModel()
            {
                Name = client.Name,
                TIN = client.TIN,
                Country = client.Address.Country,
                City = client.Address.City,
                Street = client.Address.Street,
                Home = client.Address.HomeNumber
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateLegalClient(LegalViewModel client, int id)
        {
            if (string.IsNullOrEmpty(client.Name))
            {
                ModelState.AddModelError("Name", "Please fill out in the form!");
            }

            if (client.TIN <= 0 || client.TIN > 999999999999)
            {
                ModelState.AddModelError("TIN", "Please fill out in the form(maximum 12 symbol)");
            }

            if (ModelState.IsValid)
            {
                _clientService.UpdateLegalClient(new Model.Client.LegalEntity()
                {
                    ClientId = id,
                    Name = client.Name,
                    TIN = client.TIN,
                    Address = new Model.Address.Address()
                    {
                        Country = client.Country,
                        City = client.City,
                        Street = client.Street,
                        HomeNumber = client.Home
                    }
                });

                return RedirectToAction("GetLegals");
            }
            else
            {
                return View(client);
            }
        }

        public ActionResult UpdatePhysicalClient(int id)
        {
            var client = _clientService.GetPhysicalClient(id);
            var model = new PhysicalViewModel()
            {
                Name = client.Name,
                Passport = client.PassportNumber,
                DateOfBirth = client.DateOfBirth.Value,
                Country = client.Address.Country,
                City = client.Address.City,
                Street = client.Address.Street,
                Home = client.Address.HomeNumber
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdatePhysicalClient(PhysicalViewModel client, int id)
        {
            if (string.IsNullOrEmpty(client.Name))
            {
                ModelState.AddModelError("Name", "Please fill out in the form!");
            }

            if (ModelState.IsValid)
            {
                _clientService.UpdatePhysicalClient(new Model.Client.PhysicalEntity()
                {
                    ClientId = id,
                    Name = client.Name,
                    PassportNumber = client.Passport,
                    DateOfBirth = client.DateOfBirth,
                    Address = new Model.Address.Address()
                    {
                        Country = client.Country,
                        City = client.City,
                        Street = client.Street,
                        HomeNumber = client.Home
                    }
                });

                return RedirectToAction("GetPhysicals");
            }
            else
            {
                return View(client);
            }
        }
    }
}