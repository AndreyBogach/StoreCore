using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreCore.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult ThrowError()
        {
            throw new Exception("This is a nasty exception...");
        }
    }
}