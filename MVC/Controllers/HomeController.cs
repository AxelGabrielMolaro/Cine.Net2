using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.ServicesImpl;
using MVC.Entity;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        UsuarioServiceImpl usuarioService = new UsuarioServiceImpl();
        PeliculaServiceImpl peliculaService = new PeliculaServiceImpl();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult inicio()
        {
            HomeModelAndView model = new HomeModelAndView();
            model.listadoDePeliculasHome = peliculaService.getListadoDePeliculasHome();
            return View(model);
        }

        public ActionResult login()
        {
            ViewData["sessionString"] = System.Web.HttpContext.Current.Session["sessionString"] as String;
            if (ViewData["sessionString"] != null)
            {
                return Redirect("/Administracion/inicio"); //si hay una sesión activa, el botón "Administración" redirige al inicio de admin.
            }
            else
            {
                UsuarioModelAndView modeloLogin = new UsuarioModelAndView();
                return View(modeloLogin);
            }
        }

        //Si iNgresa mal los datos de el login, y no pasa las reglas de validacion se mantiene en la vista
        //Si el usuario que ingresa es incorrecto y se manda un msj de error
        //Si es correcto ingresa a el home del admin
        [HttpPost]
        public ActionResult login(UsuarioModelAndView model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                Usuarios usuarioLogin = usuarioService.login(model.nombreUsuarioModel, model.contraseñaUsuarioModel);
                System.Web.HttpContext.Current.Session["sessionString"] = model.idUsuarioModel; //guarda la variable de sesión. 
                return RedirectToAction(TempData["accion"].ToString(), TempData["controlador"].ToString()); //redirige a la última página que se accedió por url.
            }
            catch (Exception e)
            {
                ViewBag.errorLogin = e.Message;
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("inicio", "Home");
        }
    }
}