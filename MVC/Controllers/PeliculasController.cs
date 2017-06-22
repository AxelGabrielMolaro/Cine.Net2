using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
using MVC.Entity;
using MVC.ServicesImpl;
using MVC.Herramientas;

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
                model.horaDeFuncionReservaModel = horarioFC;
                model.listadoDeTipoDocumentoReservaModel = documentoService.getListadoTipoDocumento();
                model.paso = "4";
                model.setearSedeYPeliculaYVersionParaReserva2(Convert.ToInt32(idPeliculaFC), Convert.ToInt32(idSedeFC), Convert.ToInt32(idVersionFC));
                return View(model);
            }

        }

       [HttpPost]
        public ActionResult finalizarReserva(FormCollection formCol)
        {

            HerramientaMensajes herramientaMensajes = new HerramientaMensajes();

            ReservaModelAndView model = new ReservaModelAndView();
            //datos ocultos
            string idVersionFC = formCol["versionPaso5"];
            string idPeliculaFC = formCol["peliculaPaso5"];
            string idSedeFC = formCol["idSedePaso5"];
            string diaFC = formCol["diaPaso5"];
            string horarioFC = formCol["horarioPaso5"];
            //formulario de la vista
            string mailFC = formCol["mail"];
            string tipoDocumentoFC = formCol["tipoDocumento"];
            string numeroDocumentoFC = formCol["numeroDocumento"];
            string cantidadDeEntradasFC = formCol["cantidadDeEntradas"];

            if (HerramientasString.mailEsValido(mailFC) == false)
            {
                ViewBag.errorReserva = "Coloque un mail válido por favor";
                model.SetearValoresReservaModelAndViewFinal(idPeliculaFC, idSedeFC, idVersionFC, diaFC, horarioFC, "4", "", "");
                 return View("reserva2", model);
            }
            else if (HerramientasString.documentoEsValido(tipoDocumentoFC, numeroDocumentoFC) == false)
            {
                ViewBag.errorReserva = "Coloque un documento válido por favor";
                model.SetearValoresReservaModelAndViewFinal(idPeliculaFC, idSedeFC, idVersionFC, diaFC, horarioFC, "4", mailFC, "");
                return View("reserva2", model);
            }
            else if (HerramientasString.esNumero(numeroDocumentoFC) == false)
            {
                ViewBag.errorReserva = "Coloque un numero de documento de 8 dígitos por favor";
                model.SetearValoresReservaModelAndViewFinal(idPeliculaFC, idSedeFC, idVersionFC, diaFC, horarioFC, "4", mailFC, "");
                return View("reserva2", model);
            }
            else if (HerramientasString.esNumero(cantidadDeEntradasFC) == false)
            {
                ViewBag.errorReserva = "Elija la cantidad de entradas favor";
                model.SetearValoresReservaModelAndViewFinal(idPeliculaFC, idSedeFC, idVersionFC, diaFC, horarioFC, "4", mailFC, "");
                return View("reserva2", model);
            }


            model.idPeliculaReservaModel = idPeliculaFC;
            model.idSedeReservaModel = idSedeFC;
            model.idVersionReservaModel = idVersionFC;
            model.diaDeReservaReservaModel = diaFC;
            model.horaDeFuncionReservaModel = horarioFC;
            model.paso = "5";
            model.setearSedeYPeliculaYVersionParaReserva2(Convert.ToInt32(idPeliculaFC), Convert.ToInt32(idSedeFC), Convert.ToInt32(idVersionFC));

            //crea la reserva
            Reservas nuevaReserva = new Reservas();
            nuevaReserva.CantidadEntradas = Convert.ToInt32(cantidadDeEntradasFC);
            nuevaReserva.Email = mailFC;
            nuevaReserva.FechaCarga = DateTime.Now;
            nuevaReserva.FechaHoraInicio = DateTime.Now; //CAMBIAR ESTO ESTA MAL
            nuevaReserva.IdPelicula = Convert.ToInt32(idPeliculaFC);
            nuevaReserva.IdSede = Convert.ToInt32(idSedeFC);
            nuevaReserva.IdVersion = Convert.ToInt32(idVersionFC);
            nuevaReserva.IdTipoDocumento = Convert.ToInt32(tipoDocumentoFC);
            nuevaReserva.NumeroDocumento = numeroDocumentoFC;
            reservaService.agregarReserva(nuevaReserva);

            return Redirect("/Home/Inicio");

        }
    }
}