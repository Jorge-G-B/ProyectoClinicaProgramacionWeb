using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ClinicaModels;
using ProyectoClinica.Services;

namespace ProyectoClinica.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            IEnumerable<Usuario> users = await APIServices.GetUsers();
            Usuario userlogin = new Usuario();
            foreach (var user in users)
            {
                if (user.User1 == username)
                {
                    userlogin = user;
                }
            }

            if (username == userlogin.User1 && password == userlogin.Contraseña)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim("username", username));

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Usuario user)
        {
            user.Rol = 6;
            await APIServices.CreateUser(user);
            return RedirectToAction(nameof(Index));
        }
    }
}
