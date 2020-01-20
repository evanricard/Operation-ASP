using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OperationASP.Models;

namespace OperationASP.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        readonly UserManager<CoreUser> userManager;

        public HomeController(UserManager<CoreUser> userManager)
        {
            this.userManager = userManager;
        }
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "DESCRIPTOR";
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(loginModel.UserName);

                //Plaintext password check
                if(user !=null && await userManager.CheckPasswordAsync(user, loginModel.Password))
                {
                    var identity = new ClaimsIdentity("Cookies");
                        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName ));

                    await HttpContext.SignInAsync(scheme:(string)(CookieAuthenticationDefaults.AuthenticationScheme), new ClaimsPrincipal(identity));
                    return RedirectToAction("Index");
                    }

                //Don't inform the user of whatever point of data auth failed
                ModelState.AddModelError("", "Invalid UserName or Password");
            }

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.UserName);
                if(user == null)
                {
                    user = new CoreUser()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = model.UserName
                    };
                    
                    var result = await userManager.CreateAsync(user, model.Password);
                }

                return View("Success");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
