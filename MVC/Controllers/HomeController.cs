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
            UsuarioModelAndView modeloLogin = new UsuarioModelAndView();
           return View(modeloLogin);
        }


        //Si igresa mal los datos de el login, y no pasa las reglas de validacion se mantiene en la vista
        //Si el usuario que ingresa es incorrecto y se manda un msj de error
        //Si es correcto ingresa a el home del admin
        [HttpPost]
        public ActionResult login(UsuarioModelAndView model)
        {
            ViewBag.errorLogin = "";
           
            if (!ModelState.IsValid)
            {
                return View(model);   
            }
            try
            {
                Usuarios usuarioLogin = usuarioService.login(model.nombreUsuarioModel, model.contraseñaUsuarioModel);
                return Redirect("/Administracion/inicio"); 
            }
            catch (Exception e)
            {
                ViewBag.errorLogin = e.Message;
                return View(model);

            }



            /* ViewBag.bienvenido = ("Bienvenido " + model.nombre);//no anda ver bien dps con la bdd
            return Redirect("/Administracion/inicio"); */
        }
    }
}