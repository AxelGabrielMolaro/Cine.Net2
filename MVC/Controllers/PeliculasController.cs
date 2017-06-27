using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
using MVC.Entity;
using MVC.ServicesImpl;
using MVC.Herramientas;
using System.Text.RegularExpressions;

namespace MVC.Controllers
{
    public class PeliculasController : Controller
    {
        PeliculaServiceImpl peliculaService = new PeliculaServiceImpl();
        sedeServiceImpl sedeService = new sedeServiceImpl();
        VersionServiceImpl versionService = new VersionServiceImpl();
        ReservaServiceImpl reservaService = new ReservaServiceImpl();
        DocumentoServiceImpl documentoService = new DocumentoServiceImpl();
        // GET: Peliculas
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult reserva( HomeModelAndView homeModel)
        {
            ReservaModelAndView model = new ReservaModelAndView();
            model.paso = "1";
            model.idPeliculaReservaModel = homeModel.idPeliculaHome;
            model.listadoDeVersionesReservaModel = reservaService.getListadosDeVersionesParaReserva(Convert.ToInt32(homeModel.idPeliculaHome));
            return View(model);
        }


        [HttpPost]
        public ActionResult reservaPaso2(FormCollection formCol)
        {

            ReservaModelAndView model = new ReservaModelAndView();
            string idPeliculaFC = formCol["peliculaPaso1"];
            string idVersionFC = formCol["versionPaso1"];

            if (idVersionFC == "" || idVersionFC == null || idVersionFC == "0")
            {
               
                ViewBag.errorReserva = "Seleccione una versión por favor";
                model.listadoDeVersionesReservaModel = reservaService.getListadosDeVersionesParaReserva(Convert.ToInt32(idPeliculaFC));
                model.paso = "1";
                model.idPeliculaReservaModel = idPeliculaFC;
                return View("reserva", model);
            }
            else
            {           
                model.idPeliculaReservaModel = idPeliculaFC;
                model.idVersionReservaModel = idVersionFC;
                model.paso = "2";
                model.listadoDeSedesReservaModel = reservaService.getListadoDeSedesParaReserva(Convert.ToInt32(idPeliculaFC), Convert.ToInt32(idVersionFC));
                model.listadoDeFunciones = new List<FuncionModelAndView>();
                return View("reserva", model);
            }    
        }

        [HttpPost]
        public ActionResult reservaPaso3(FormCollection formCol)
        {

            ReservaModelAndView model = new ReservaModelAndView();
            string idVersionFC = formCol["versionPaso2"];
            string idPeliculaFC = formCol["peliculaPaso2"];
            string idSedeFC = formCol["sedePaso2"];
            if (idSedeFC == "" || idSedeFC == null || idSedeFC == "0")
            {
                ViewBag.errorReserva = "Seleccione una sede por favor";
                model.idPeliculaReservaModel = idPeliculaFC;
                model.idVersionReservaModel = idVersionFC;
                model.paso = "2";
                model.listadoDeSedesReservaModel = reservaService.getListadoDeSedesParaReserva(Convert.ToInt32(idPeliculaFC), Convert.ToInt32(idVersionFC));
                model.listadoDeFunciones = new List<FuncionModelAndView>();
                return View("reserva", model);
            }
            else
            {

                model.idPeliculaReservaModel = idPeliculaFC;
                model.idSedeReservaModel = idSedeFC;
                model.idVersionReservaModel = idVersionFC;
                model.paso = "3";
                model.listadoDeDiasReservaModel = reservaService.getListadoDeDiasParaReserva(Convert.ToInt32(idPeliculaFC), Convert.ToInt32(idVersionFC), Convert.ToInt32(idSedeFC));
                return View("reserva", model);

            }

        }


        [HttpPost]
        public ActionResult reservaPaso4(FormCollection formCol)
        {
            ReservaModelAndView model = new ReservaModelAndView();
            string idVersionFC = formCol["versionPaso3"];
            string idPeliculaFC = formCol["peliculaPaso3"];
            string idSedeFC = formCol["idSedePaso3"];
            string diaFC = formCol["diaPaso3"];
            if (diaFC == "" || diaFC == null || diaFC == "0")
            {
                ViewBag.errorReserva = "Seleccione una dia por favor";
                model.idPeliculaReservaModel = idPeliculaFC;
                model.idSedeReservaModel = idSedeFC;
                model.idVersionReservaModel = idVersionFC;
                model.paso = "3";
                model.listadoDeSedesReservaModel = new List<Sedes>();
                model.listadoDeDiasReservaModel = new List<string>();
                model.listadoDeDiasReservaModel = reservaService.getListadoDeDiasParaReserva(Convert.ToInt32(idPeliculaFC), Convert.ToInt32(idVersionFC), Convert.ToInt32(idSedeFC));
                model.listadoDeVersionesReservaModel = new List<Versiones>();
                model.listadoDeFunciones = new List<FuncionModelAndView>();
                return View("reserva", model);
            }
            else
            {


                model.idPeliculaReservaModel = idPeliculaFC;
                model.idSedeReservaModel = idSedeFC;
                model.idVersionReservaModel = idVersionFC;
                model.diaDeReservaReservaModel = diaFC;
                model.paso = "4";
                model.llenarListadoDeFunciones(Convert.ToInt32(idPeliculaFC), Convert.ToInt32(idSedeFC), Convert.ToInt32(idVersionFC));
            }


            return View("reserva", model);
        }

        //paso anterior antes de finalizar
        [HttpPost]
        public ActionResult reserva2(FormCollection formCol)
        {
          
            ReservaModelAndView model = new ReservaModelAndView();
            string idVersionFC = formCol["versionPaso4"];
            string idPeliculaFC = formCol["peliculaPaso4"];
            string idSedeFC = formCol["idSedePaso4"];
            string diaFC = formCol["diaPaso4"];
            string horarioFC = formCol["horario"];
            if (horarioFC == "" || horarioFC == null || horarioFC == "0")
            {
                ViewBag.errorReserva = "Seleccione un horario por favor";
                model.idPeliculaReservaModel = idPeliculaFC;
                model.idSedeReservaModel = idSedeFC;
                model.idVersionReservaModel = idVersionFC;
                model.diaDeReservaReservaModel = diaFC;
                model.paso = "4";
                model.llenarListadoDeFunciones(Convert.ToInt32(idPeliculaFC), Convert.ToInt32(idSedeFC), Convert.ToInt32(idVersionFC));
                return View("reserva", model);
            }
            else
            {
                model.idPeliculaReservaModel = idPeliculaFC;
                model.idSedeReservaModel = idSedeFC;
                model.idVersionReservaModel = idVersionFC;
                model.diaDeReservaReservaModel = diaFC;
                TimeSpan horario = TimeSpan.FromMinutes(Convert.ToInt32(horarioFC));
               
                model.horaDeFuncionReservaModel = (horario.Hours.ToString() + ":" + horario.Minutes.ToString());
                model.listadoDeTipoDocumentoReservaModel = documentoService.getListadoTipoDocumento();
                model.paso = "4";
                model.setearSedeYPeliculaYVersionParaReserva2(Convert.ToInt32(idPeliculaFC), Convert.ToInt32(idSedeFC), Convert.ToInt32(idVersionFC));
                return View(model);
            }

        }

       [HttpPost]
        public ActionResult finalizarReserva(ReservaModelAndView model)
        {
            if (!ModelState.IsValid)
            {
                model.listadoDeTipoDocumentoReservaModel = documentoService.getListadoTipoDocumento();
                model.pelicula = peliculaService.getPeliculaPorId(Convert.ToInt32(model.idPeliculaReservaModel));
                model.sede = sedeService.getSedePorId(Convert.ToInt32(model.idSedeReservaModel));
                model.version = versionService.getVersionPorId(Convert.ToInt32(model.idVersionReservaModel));
                return View("reserva2", model);
            }

            //casteo del dt
            string fechaString = Regex.Replace(model.diaDeReservaReservaModel, @"[^\d]", "");
            string dia = fechaString.Substring(0, 2);
            string mes = fechaString.Substring(2, fechaString.Length - 2);
            string horarioString = Regex.Replace(model.horaDeFuncionReservaModel, @"[^\d]", "");
            string hora = horarioString.Substring(0, 2);
            string minutos = horarioString.Substring(2, fechaString.Length - 2);
            DateTime fechaHorainicioDate = DateTime.Parse(dia + "-" + mes + "-" + DateTime.Now.Year + " " + hora + ":" + minutos + ":" + "00");

            //crea la reserva
            Reservas nuevaReserva = new Reservas();
            nuevaReserva.CantidadEntradas = Convert.ToInt32(model.cantidadDeEntradasPasoFinalReservaModel);
            nuevaReserva.Email = model.mailPasoFinalReservaModel;
            nuevaReserva.FechaCarga = DateTime.Now;
            nuevaReserva.FechaHoraInicio = fechaHorainicioDate;
            nuevaReserva.IdPelicula = Convert.ToInt32(model.idPeliculaReservaModel);
            nuevaReserva.IdSede = Convert.ToInt32(model.idSedeReservaModel);
            nuevaReserva.IdVersion = Convert.ToInt32(model.idVersionReservaModel);
            nuevaReserva.IdTipoDocumento = Convert.ToInt32(model.tipoDocumentoPasoFinalReservaModel);
            nuevaReserva.NumeroDocumento = model.numeroDocumentoPasoFinalReservaModel;
            reservaService.agregarReserva(nuevaReserva);
            
            return Redirect("/Home/Inicio");

        }
    }
}