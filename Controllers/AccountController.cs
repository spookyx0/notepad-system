using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    public class AccountController : Controller
    {
        // GET: AccountController
        public ActionResult Login()
        {
            return View();
        }

    }
}
