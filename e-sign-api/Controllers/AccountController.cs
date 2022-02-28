using e_sign_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace e_sign_api.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly AuthHelper _authHelper;

        public AccountController(AuthHelper authHelper)
        {
            _authHelper = authHelper;
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login(string redirectUrl = "/")
        {
            _authHelper.LoginUser();
            return Redirect(redirectUrl);
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            _authHelper.Logout();
            return RedirectToAction("/");
        }
    }
}
