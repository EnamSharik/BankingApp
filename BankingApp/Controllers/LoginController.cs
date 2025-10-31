using BankingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankingApp.Controllers
{
    public class LoginController : Controller
    {
        private Entities db = new Entities();

        // Dentro de LoginController
        public ActionResult Index()
        {
            // Opcional: Inicializar un modelo vacío si lo necesitas
            return View();
        }

        public ActionResult GoToRegister()
        {
            // 1. Establece TempData. Estará disponible solo para la siguiente solicitud (USUARIOs/Create)
            TempData["HideNavBar"] = true;

            // 2. Redirige a la acción Create del controlador USUARIOs
            return RedirectToAction("Create", "USUARIOs");
        }

        // POST: Login/LogOn
        [HttpPost] 
        public ActionResult logOn(LoginViewModel model)
        {
            string user = model.user;
            string pass = model.password;

            USUARIO usModel = db.USUARIO.Where(u => u.USUARIO1 == user).FirstOrDefault();
            if (usModel == null)
            {
                ViewBag.Message = "El usuario no fue encontrado en el sistema.";
                return View("Index", model); // Vuelve a la vista Index
            }

            if (usModel.CONTRASENA != model.password) // Uso 'model.password' ya que es el valor sin encriptar
            {
                ViewBag.Message = "Contraseña incorrecta. Inténtalo de nuevo.";
                return View("Index", model); // Vuelve a la vista Index, manteniendo el usuario si es posible
            }

            // Si todo es correcto
            // ... Lógica de inicio de sesión (Session, Authentication) ...
            return RedirectToAction("Index", "Home"); // Redirige a la página principal
        }
    }
}