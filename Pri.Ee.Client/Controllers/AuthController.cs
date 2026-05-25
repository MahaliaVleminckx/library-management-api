using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Pri.Ee.Client.Models;
using Pri.Ee.Client.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace Pri.Ee.Client.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        //Get login page
        public IActionResult Login()
        {
            return View();
        }

        //Post login from submit
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var token = await _authService.Login(username, password);

            if (string.IsNullOrEmpty(token))
            {
                ViewBag.Error = "Login failed";
                return View();
            }

            ////opslaan token
            HttpContext.Session.SetString("JWT", token);
            var handler = new JwtSecurityTokenHandler();
            //var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            //foreach (var claim in jwt.Claims )
            //{
            //    Console.WriteLine($"{claim.Type}:{claim.Value}");
            //}

            var role = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role ?? "User")
            };

            //var identity = new ClaimsIdentity(claims, "Cookies");
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            //await HttpContext.SignInAsync("Cookies", principal);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


            //redirect books
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var success = await _authService.Register(model);

            if (!success)
            {
                ViewBag.Error = "Register failed";
                    return View(model);
            }
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("JWT");
            return RedirectToAction("Login");
        }
    }
}
