using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.ServicesImpl;
using MVC.Models;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing;
using MVC.Entity;

namespace MVC.Controllers
{
    public class AdministracionController : Controller
    {
        //todos los services que tienen mi logica
        PeliculaServiceImpl peliculaService = new PeliculaServiceImpl();
        sedeServiceImpl sedeService = new sedeServiceImpl();
        ReservaServiceImpl reservaServiceImpl = new ReservaServiceImpl();
        //Se agrega el service de Carteleras
        CarteleraServiceImpl carteleraService = new CarteleraServiceImpl();

        // GET: Administracion
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult inicio()
        {
            return View();
        }

        //Muesta en las vista las peliculas
        public ActionResult peliculas()
        {
            PeliculaModelAndView model = new PeliculaModelAndView();
            try
            {
                ViewBag.ErrorPelicula = "";

                model.listadoDePeliculas = peliculaService.getListadoDePeliculas();


                return View(model);
            }
            catch (Exception e)
            {
                ViewBag.ErrorPelicula = e.Message;
                model.listadoDePeliculas = new List<Peliculas>();
                return View(model);

            }


        }

        //El el formulario vacio para agregar la pelicula
        public ActionResult agregarPelicula()
        {
            PeliculaModelAndView model = new PeliculaModelAndView();
            // model.llenarListados();
            return View(model);
        }
        [HttpPost]
        public ActionResult agregarPelicula(PeliculaModelAndView model, HttpPostedFileBase imagenPelicula)
        {
            if (!ModelState.IsValid)
            {
                //model.llenarListados();
                return View(model);
            }
            else
            {
                try
                {
                    ViewBag.ErrorPelicula = "";
                    Entity.Peliculas peliculaAAgregar = new Entity.Peliculas();
                    //Imagenes
                    //Obtengo la ruta absoluta
                    string appDataFolder = Server.MapPath("~/ImagenesDelServidor/");
                    string savePath = appDataFolder;
                    string fileName = imagenPelicula.FileName;
                    string pathToCheck = savePath + fileName;
                    string tempfileName = "";
                    if (System.IO.File.Exists(pathToCheck))
                    {
                        int counter = 2;
                        while (System.IO.File.Exists(pathToCheck))
                        {
                            // if a file with this name already exists,
                            // prefix the filename with a number.
                            tempfileName = counter.ToString() + fileName;
                            pathToCheck = savePath + tempfileName;
                            counter++;
                        }

                        fileName = tempfileName;
                    }
                    else
                    {
                        Console.WriteLine("Se guardo la imagen correctamente");
                    }

                    // Append the name of the file to upload to the path.
                    savePath += fileName;

                    // Call the SaveAs method to save the uploaded
                    // file to the specified directory.
                    imagenPelicula.SaveAs(savePath);



                    peliculaAAgregar.Nombre = model.nombrePeliculaModel;
                    peliculaAAgregar.Descripcion = model.descripcionPeliculaModel;
                    peliculaAAgregar.IdCalificacion = Convert.ToInt32(model.idCalificacionPeliculaModel);
                    peliculaAAgregar.Duracion = Convert.ToInt32(model.duracionPeliculaModel);
                    peliculaAAgregar.IdGenero = Convert.ToInt32(model.idgeneroPeliculaModel);
                    peliculaAAgregar.Imagen = fileName;
                    peliculaAAgregar.FechaCarga = DateTime.Now;




                    peliculaService.grabarUnaPelicula(peliculaAAgregar);
                    return RedirectToAction("peliculas");
                }
                catch (Exception e)
                {
                    ViewBag.ErrorPelicula = e.Message;
                    return View(model);
                }
            }

        }

        //sede

        //lista las sedes por request
        public ActionResult sedes()
        {

            if (!ModelState.IsValid)
            {
                return View("agregarSede");
            }
            else
            {
                SedeModelAndView model = new SedeModelAndView();
                try
                {
                    ViewBag.errorSede = "";
                    model.listadoDeSedesModel = sedeService.getListadoDeSedes();
                    return View(model);
                }
                catch (Exception e)
                {
                    ViewBag.errorSede = e.Message;
                    model.listadoDeSedesModel = new List<Sedes>();
                    return View(model);
                }
            }

        }

        //Envia al formulario de agregar sede
        //Este formulario lo uso para editar, y para agregar
        [HttpPost]
        public ActionResult agregarSede(SedeModelAndView model)
        {
            if (model.IdSede == 0)
            {

                model.nombreSedeModel = "Ingrese nombre";

                return View(new SedeModelAndView());
            }
            else
            {
                try
                {
                    Sedes sedeAEditar = sedeService.getSedePorId(Convert.ToInt32(model.IdSede));
                    model.nombreSedeModel = sedeAEditar.Nombre;
                    model.direccionSedeModel = sedeAEditar.Direccion;
                    model.precioEntradaGeneralModel = sedeAEditar.PrecioGeneral.ToString();
                }
                catch (Exception e)
                {
                    ViewBag.errorSede = "Error al modificar sede. Error traer sede a modificar";
                    RedirectToAction("sedes");
                }
                return View(model);
            }

        }

        //aca si es una sede nueva agrega si no edita , llama a los metodos correspondientes
        //ante la peticion por request
        [HttpPost]
        public ActionResult agregarSedePost(SedeModelAndView model)
        {

            if (!ModelState.IsValid)
            {
                return View("AgregarSede", model);
            }
            else
            {

                if (model.idSedeModel == null || model.idSedeModel == "0")//agrega
                {
                    Sedes sedeAAgregar = new Sedes();
                    sedeAAgregar.Nombre = model.nombreSedeModel;
                    sedeAAgregar.Direccion = model.direccionSedeModel;
                    sedeAAgregar.PrecioGeneral = Convert.ToInt32(model.precioEntradaGeneralModel);
                    try
                    {
                        ViewBag.errorSede = "";
                        sedeService.crearSede(sedeAAgregar);
                    }
                    catch (Exception e)
                    {
                        ViewBag.errorSede = "Error al agregar sede, por favor no lene los campos vacios.";
                        return View("agregarSede", model);

                    }

                    return RedirectToAction("sedes");
                }
                else
                { //Modifica uno ya existente
                    try
                    {
                        ViewBag.errorSede = "";
                        sedeService.modificarSedeorId(Convert.ToInt32(model.idSedeModel), model.nombreSedeModel, model.direccionSedeModel, model.precioEntradaGeneralModel);
                    }
                    catch (Exception e)
                    {
                        ViewBag.errorSede = e.Message;
                        return View("agregarSede", model);
                    }
                    return RedirectToAction("sedes");
                }

            }

        }


        //CARTELERAS 


        /* public ActionResult carteleras()
        {
            CarteleraModelAndView model = new CarteleraModelAndView();
            try
            {
                ViewBag.ErrorPelicula = ""; //Revisar las validaciones!

                model.listadoDeCarteleras = carteleraService.getListadoDeCarteleras();

                return View(model);
            }
            catch (Exception e)
            {
                ViewBag.ErrorPelicula = e.Message;
                model.listadoDeCarteleras = new List<Carteleras>();
                return View(model);
            }
        } */


        public ActionResult carteleras()
        {

            if (!ModelState.IsValid)
            {
                return View("agregarCartelera");
            }
            else
            {
                CarteleraModelAndView model = new CarteleraModelAndView();
                try
                {
                    model.listadoDeCarteleras = carteleraService.getListadoDeCarteleras();
                    return View(model);
                }
                catch (Exception e)
                {
                    ViewBag.ListaCartelerasVacia = e.Message; //Lanza la excepción en la vista.
                    model.listadoDeCarteleras = new List<Carteleras>();
                    return View(model);
                }
            }
        }


        [HttpPost]
        public ActionResult agregarCartelera(CarteleraModelAndView model)
        {
            if (model.IdCartelera == 0)
            {
                return View(new CarteleraModelAndView());
            }
            else
            {
                try
                {
                    Carteleras carteleraModificable = carteleraService.obtenerCarteleraPorId(model.IdCartelera);
                    model.idSedeCarteleraModel = carteleraModificable.IdSede.ToString();
                    model.idPeliculaCarteleraModel = carteleraModificable.IdPelicula.ToString();
                    model.horaInicioModel = carteleraModificable.HoraInicio.ToString();
                    model.fechaInicioModel = carteleraModificable.FechaInicio.ToString();
                    model.fechaFinModel = carteleraModificable.FechaFin.ToString();
                    model.numeroSalaModel = carteleraModificable.NumeroSala.ToString();
                    model.idVersionModel = carteleraModificable.IdVersion.ToString();
                    model.lunesModel = carteleraModificable.Lunes.ToString();
                    model.martesModel = carteleraModificable.Martes.ToString();
                    model.miercolesModel = carteleraModificable.Miercoles.ToString();
                    model.juevesModel = carteleraModificable.Jueves.ToString();
                    model.viernesModel = carteleraModificable.Viernes.ToString();
                    model.sabadoModel = carteleraModificable.Sabado.ToString();
                    model.domingoModel = carteleraModificable.Domingo.ToString();
                    model.fechaCargaModel = carteleraModificable.FechaCarga.ToString();
                }
                catch (Exception e)
                {
                    ViewBag.errorCartelera = ""; //REVISAR ERRORES!
                    RedirectToAction("carteleras");
                }
                return View(model);
            }

        }

        [HttpPost]
        public ActionResult agregarCarteleraPost(CarteleraModelAndView model)
        {

            if (!ModelState.IsValid)
            {
                return View("AgregarCartelera", model);
            }
            else
            {

                if (model.idCarteleraModel == null || model.idCarteleraModel == "0")
                {
                    Carteleras carteleraAAgregar = new Carteleras();

                    carteleraAAgregar.IdCartelera = Convert.ToInt32(model.idCarteleraModel);
                    carteleraAAgregar.IdSede = Convert.ToInt32(model.idSedeCarteleraModel);
                    carteleraAAgregar.IdPelicula = Convert.ToInt32(model.idPeliculaCarteleraModel);
                    carteleraAAgregar.HoraInicio = Convert.ToInt32(model.horaInicioModel);
                    carteleraAAgregar.FechaInicio = DateTime.Now;//ver esto mariana
                    carteleraAAgregar.FechaFin = DateTime.Now;
                    carteleraAAgregar.NumeroSala = Convert.ToInt32(model.numeroSalaModel);
                    carteleraAAgregar.IdVersion = Convert.ToInt32(model.idVersionModel);
                    carteleraAAgregar.Lunes = Convert.ToBoolean(model.lunesModel);
                    carteleraAAgregar.Martes = Convert.ToBoolean(model.martesModel);
                    carteleraAAgregar.Miercoles = Convert.ToBoolean(model.miercolesModel);
                    carteleraAAgregar.Jueves = Convert.ToBoolean(model.juevesModel);
                    carteleraAAgregar.Viernes = Convert.ToBoolean(model.viernesModel);
                    carteleraAAgregar.Sabado = Convert.ToBoolean(model.sabadoModel);
                    carteleraAAgregar.Domingo = Convert.ToBoolean(model.domingoModel);
                    carteleraAAgregar.FechaCarga = DateTime.Now;
                    try
                    {
                        ViewBag.errorCartelera = "";
                        carteleraService.crearCartelera(carteleraAAgregar);
                    }
                    catch (Exception e)
                    {
                        ViewBag.ErrorAlAgregarCartelera = "No se pudo agregar la cartelera";
                        return View("agregarCartelera", model);
                    }
                    TempData["CarteleraAgregada"] = "¡La cartelera se agregó correctamente!";
                    return RedirectToAction("carteleras");
                }
                else
                {
                    try
                    {
                        ViewBag.errorCartelera = "";
                        carteleraService.modificarCarteleraPorId(Convert.ToInt32(model.idCarteleraModel), model.idSedeCarteleraModel, model.idPeliculaCarteleraModel, model.horaInicioModel, model.fechaInicioModel, model.fechaFinModel, model.numeroSalaModel, model.idVersionModel, model.lunesModel, model.martesModel, model.miercolesModel, model.juevesModel, model.viernesModel, model.sabadoModel, model.domingoModel, model.fechaCargaModel);
                    }
                    catch (Exception e)
                    {
                        ViewBag.errorCartelera = e.Message;
                        return View("agregarCartelera", model);
                    }
                    TempData["CarteleraAgregada"] = "¡La cartelera se agregó correctamente!";
                    return RedirectToAction("carteleras");
                }

            }

        }


        /*
        public ActionResult agregarCartelera()
        {
            CarteleraModelAndView model = new CarteleraModelAndView();
            return View(model);
        }
        [HttpPost]
        public ActionResult agregarCartelera(CarteleraModelAndView model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                Carteleras carteleraAAgregar = new Carteleras();

                carteleraAAgregar.IdCartelera = Convert.ToInt32(model.idCarteleraModel);
                carteleraAAgregar.IdSede = Convert.ToInt32(model.idSedeCarteleraModel);
                carteleraAAgregar.IdPelicula = Convert.ToInt32(model.idPeliculaCarteleraModel);
                carteleraAAgregar.HoraInicio = Convert.ToInt32(model.horaInicioModel);
                carteleraAAgregar.FechaInicio = Convert.ToDateTime(model.fechaInicioModel);
                carteleraAAgregar.FechaFin = Convert.ToDateTime(model.fechaFinModel);
                carteleraAAgregar.NumeroSala = Convert.ToInt32(model.numeroSalaModel);
                carteleraAAgregar.IdVersion = Convert.ToInt32(model.idVersionModel);
                carteleraAAgregar.Lunes = Convert.ToBoolean(model.lunesModel);
                carteleraAAgregar.Martes = Convert.ToBoolean(model.martesModel);
                carteleraAAgregar.Miercoles = Convert.ToBoolean(model.miercolesModel);
                carteleraAAgregar.Jueves = Convert.ToBoolean(model.juevesModel);
                carteleraAAgregar.Viernes = Convert.ToBoolean(model.viernesModel);
                carteleraAAgregar.Sabado = Convert.ToBoolean(model.sabadoModel);
                carteleraAAgregar.Domingo = Convert.ToBoolean(model.domingoModel);
                carteleraAAgregar.FechaCarga = DateTime.Now;

                carteleraService.grabarUnaCartelera(carteleraAAgregar);

                return RedirectToAction("carteleras");
            }
        }
        */

        [HttpPost]
        public ActionResult eliminarCartelera(Carteleras cartelera)
        {
            cartelera = carteleraService.obtenerCarteleraPorId(cartelera.IdCartelera);
            if (cartelera != null)
            {
                carteleraService.eliminarCartelerametodo(cartelera);
            }
            else
            {
                return View("eliminarCartelera");
            }
            TempData["CarteleraEliminada"] = "La cartelera fue eliminada";
            return RedirectToAction("carteleras");
        }

        /*********************************************************************************************/

        //reservas
        public ActionResult reportes()
        {
            ReservaModelAndView model = new ReservaModelAndView();
            model.listadoDeReservasReporteModel = reservaServiceImpl.getListadoDeReservas();

            return View(model);

        }
    }
}