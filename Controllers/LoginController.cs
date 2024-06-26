﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sistema_servicio_tecnico.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace sistema_servicio_tecnico.Controllers
{
    public class LoginController : Controller
    {
        private readonly MercyDeveloperContext _context;

        public LoginController(MercyDeveloperContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(string correo, string contraseña)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);

            if (user != null && BCrypt.Net.BCrypt.Verify(contraseña, user.Contraseña))
            {
                // Crear claims para el usuario autenticado
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Correo),
                    // Agrega más claims según sea necesario para la autorización
                };

                // Crear identidad del usuario
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Crear principal de autenticación
                var principal = new ClaimsPrincipal(identity);

                // Iniciar sesión del usuario
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // Redirigir a la página deseada después de iniciar sesión correctamente
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Correo o contraseña inválidos.";
                return View("Index");
            }
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
        //creado PARA EL LOGOUT
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}
