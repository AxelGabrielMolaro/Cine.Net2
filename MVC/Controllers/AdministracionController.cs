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
        ReporteServiceImpl reporteService = new ReporteServiceImpl();

        // GET: Administracion
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult inicio()
        {
            ViewData["sessionString"] = System.Web.HttpContext.Current.Session["sessionString"] as String; //paso variable de sesión para validar si está logueado o no.
            if (ViewData["sessionString"] != null)
            {
                return View();
            }
            else
            {
                TempData["controlador"] = Request.RequestContext.RouteData.Values["controller"].ToString(); //acá estoy guardando el controller y el action de cada vista 
                TempData["accion"] = Request.RequestContext.RouteData.Values["action"].ToString(); //para redigir desde el login.
                return RedirectToAction("login", "Home");
            }
        }

        //Muesta en las vista las peliculas
        public ActionResult peliculas()
        {
            ViewData["sessionString"] = System.Web.HttpContext.Current.Session["sessionString"] as String;
            if (ViewData["sessionString"] != null)
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
            else
            {
                TempData["controlador"] = Request.RequestContext.RouteData.Values["controller"].ToString();
                TempData["accion"] = Request.RequestContext.RouteData.Values["action"].ToString();
                return RedirectToAction("login", "Home");
            }
        }

        //El el formulario vacio para agregar la pelicula
        public ActionResult agregarPelicula()
        {
            ViewData["sessionString"] = System.Web.HttpContext.Current.Session["sessionString"] as String;
            if (ViewData["sessionString"] != null)
            {
                PeliculaModelAndView model = new PeliculaModelAndView();
                // model.llenarListados();
                return View(model);
            }
            else
            {
                TempData["controlador"] = Request.RequestContext.RouteData.Values["controller"].ToString();
                TempData["accion"] = Request.RequestContext.RouteData.Values["action"].ToString();
                return RedirectToAction("login", "Home");
            }
        }
        [HttpPost]
        public ActionResult agregarPelicula(PeliculaModelAndView model) //A esta vista se tiene acceso por post, no es necesario validar acceso por url. 
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

                    //ruta de la carpeta
                    string appDataFolder = Server.MapPath("~/ImagenesDelServidor/");

                    string fileName = peliculaService.guardarUnaImagenEnUnCarpetaSeServidor(appDataFolder, model.imagenPeliculaModel);

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
                catch (FormatException e)
                {
                    ViewBag.ErrorPeliculaImagen = e.Message;
                    return View(model);
                }
                catch (Exception e)
                {
                    ViewBag.ErrorPelicula = e.Message;
                    return View(model);
                }

            }

        }

        /********** MODIFICAR PELÍCULA **********/
        //ESTOY TENIENDO PROBLEMAS PARA MODIFICAR, NO ME TOMA EL ID 
        //YA ESTÁ HECHO EL FORM Y TODOS LOS MÉTODOS, EL PROBLEMA ESTÁ EN EL CONTROLLER.
        public ActionResult modificarPelicula(int id, PeliculaModelAndView model)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                model.idPeliculaModel = id.ToString();
                return View(model);
            }

        }

        [HttpPost]
        public ActionResult modificarPelicula(PeliculaModelAndView model)
        {
            model.IdPelicula = Convert.ToInt32(model.idPeliculaModel);
            if (ModelState.IsValid)
            {
                string appDataFolder = Server.MapPath("~/ImagenesDelServidor/");
                string fileName = peliculaService.guardarUnaImagenEnUnCarpetaSeServidor(appDataFolder, model.imagenPeliculaModel);

                Peliculas peliAEditar = peliculaService.getPeliculaPorId(model.IdPelicula);
                model.nombrePeliculaModel = peliAEditar.Nombre;
                model.descripcionPeliculaModel = peliAEditar.Descripcion;
                model.idCalificacionPeliculaModel = Convert.ToString(peliAEditar.IdCalificacion);
                model.duracionPeliculaModel = Convert.ToString(TimeSpan.FromMinutes(peliAEditar.Duracion));
                model.idgeneroPeliculaModel = Convert.ToString(peliAEditar.IdGenero);
                // model.imagenPeliculaModel REVISAR CÓMO QUEDA LA IMG. (Si no se cambia, hay que conservar la anterior, y si se cambia, hay que eliminarla)

                //Fecha carga tiene que quedar igual, no se modifica. 

                peliculaService.modificarPelicula(Convert.ToInt32(model.idPeliculaModel), model.nombrePeliculaModel, model.descripcionPeliculaModel, model.idCalificacionPeliculaModel, model.duracionPeliculaModel, model.idgeneroPeliculaModel, Convert.ToString(model.imagenPeliculaModel));
                return RedirectToAction("peliculas");
            }
            else
            {
                ModelState.AddModelError("IdPelicula", "La película no existe");
                return View(model);
            }
        }

        /* * * * * * * * * * * * * * * * * * * */


        //sede

        //lista las sedes por request
        public ActionResult sedes()
        {
            ViewData["sessionString"] = System.Web.HttpContext.Current.Session["sessionString"] as String;
            if (ViewData["sessionString"] != null)
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
            else
            {
                TempData["controlador"] = Request.RequestContext.RouteData.Values["controller"].ToString();
                TempData["accion"] = Request.RequestContext.RouteData.Values["action"].ToString();
                return RedirectToAction("login", "Home");
            }
        }

        //Envia al formulario de agregar sede
        //Este formulario lo uso para editar, y para agregar
        [HttpPost]
        public ActionResult agregarSede(SedeModelAndView model) //A esta vista se tiene acceso por post, no es necesario validar acceso por url. 
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
                    model.precioEntradaGeneralModel = Convert.ToInt32(sedeAEditar.PrecioGeneral).ToString();
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
        public ActionResult agregarSedePost(SedeModelAndView model) //A esta vista se tiene acceso por post, no es necesario validar acceso por url. 
        {
            model.IdSede = Convert.ToInt32(model.idSedeModel);
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

                    return RedirectToAction("sedes", model);
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
        public ActionResult carteleras()
        {
            ViewData["sessionString"] = System.Web.HttpContext.Current.Session["sessionString"] as String;
            if (ViewData["sessionString"] != null)
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
                        /*model.listadoDeFunciones = new List<FuncionModelAndView>(); 
                        foreach (var c in model.listadoDeCarteleras)
                        {
                            model.llenarListadoDeFunciones(c.IdCartelera);
                        }*/
                        //VER FUNCIONES.

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
            else
            {
                TempData["controlador"] = Request.RequestContext.RouteData.Values["controller"].ToString();
                TempData["accion"] = Request.RequestContext.RouteData.Values["action"].ToString();
                return RedirectToAction("login", "Home");
            }
        }

        [HttpPost]
        public ActionResult agregarCartelera(CarteleraModelAndView model) //A esta vista se tiene acceso por post, no es necesario validar acceso por url. 
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
                    //esto de los dias no se para q se usa, por ahora no setea nada, pasa en todos los abms
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
                    ViewBag.errorCartelera = e.Message;
                    RedirectToAction("carteleras");
                }
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult agregarCarteleraPost(CarteleraModelAndView model) //A esta vista se tiene acceso por post, no es necesario validar acceso por url. 
        {
            model.IdCartelera = Convert.ToInt32(model.idCarteleraModel);
            if (!ModelState.IsValid)
            {
                return View("AgregarCartelera", model);
            }
            else
            {

                if (model.idCarteleraModel == null || model.idCarteleraModel == "0")
                {
                    Carteleras carteleraAAgregar = new Carteleras();

                    /* 
                     * carteleraAAgregar.IdCartelera = Convert.ToInt32(model.idCarteleraModel); 
                     COMENTÉ ESTA LÍNEA PORQUE OBVIAMENTE IBA A TIRAR UNA EXCEPCIÓN, 
                     ESTABA TOMANDO EL ID 0 DEL MODEL 
                     *  Marian
                     */


                    carteleraAAgregar.IdSede = Convert.ToInt32(model.idSedeCarteleraModel);
                    carteleraAAgregar.IdPelicula = Convert.ToInt32(model.idPeliculaCarteleraModel);
                    carteleraAAgregar.HoraInicio = Convert.ToInt32(model.horaInicioModel);
                    //carteleraAAgregar.FechaInicio = DateTime.Now;//ver esto mariana 
                    //carteleraAAgregar.FechaFin = DateTime.Now; 

                    //Por el momento lo pongo como string - Marian. 

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
                    model.listadoDeFunciones = new List<FuncionModelAndView>();
                    try
                    {
                        carteleraService.crearCartelera(carteleraAAgregar);
                    }
                    catch (Exception e)
                    {
                        ViewBag.ErrorAlAgregarCartelera = e.Message; //me esta entrando en esta excepcion
                                                                     //Ya lo arreglé - Marian.
                        return View("agregarCartelera", model);
                    }
                    TempData["CarteleraOK"] = "¡La cartelera se agregó correctamente!";
                    return RedirectToAction("carteleras");
                }
                else
                {
                    try
                    {
                        carteleraService.modificarCarteleraPorId(Convert.ToInt32(model.idCarteleraModel), model.idSedeCarteleraModel, model.idPeliculaCarteleraModel, model.horaInicioModel, model.fechaInicioModel, model.fechaFinModel, model.numeroSalaModel, model.idVersionModel, model.lunesModel, model.martesModel, model.miercolesModel, model.juevesModel, model.viernesModel, model.sabadoModel, model.domingoModel);
                    }
                    catch (Exception e)
                    {
                        ViewBag.errorCartelera = e.Message;
                        return View("agregarCartelera", model);
                    }
                    TempData["CarteleraOK"] = "¡La cartelera se modificó correctamente!";
                    return RedirectToAction("carteleras");
                }
            }
        }

        [HttpPost]
        public ActionResult eliminarCartelera(Carteleras cartelera) //A esta vista se tiene acceso por post, no es necesario validar acceso por url. 
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

        [HttpPost]
        public ActionResult eliminarTodasLasCarteleras() //A esta vista se tiene acceso por post, no es necesario validar acceso por url. 
        {
            try
            {
                List<Carteleras> lista = carteleraService.getListadoDeCarteleras();

                if (lista != null)
                {
                    carteleraService.eliminarTodasLasCarteleras();
                }
            }

            catch (Exception e)
            {
                ViewBag.ListaCartelerasVacia = e.Message;
            }
            TempData["CartelerasEliminadas"] = "Se eliminaron todas las carteleras";
            return RedirectToAction("carteleras");
        }


        /*********************************************************************************************/

        //reservas
        public ActionResult reportes()
        {
            ViewData["sessionString"] = System.Web.HttpContext.Current.Session["sessionString"] as String;
            if (ViewData["sessionString"] != null)
            {
                ReservaModelAndView model = new ReservaModelAndView();
                model.listadoDeReservasReporteModel = reservaServiceImpl.getListadoDeReservas();

                return View(model);
            }
            else
            {
                TempData["controlador"] = Request.RequestContext.RouteData.Values["controller"].ToString();
                TempData["accion"] = Request.RequestContext.RouteData.Values["action"].ToString();
                return RedirectToAction("login", "Home");
            }
        }

        [HttpPost]
        public ActionResult reportesFiltrar(ReservaModelAndView model) //A esta vista se tiene acceso por post, no es necesario validar acceso por url. 
        {
            model.listadoDeReservasReporteModel = reporteService.getListadoDeReservasConFilto(model.fechaDesdeReporteModel, model.fechaHastaReporteModel);
            return View("reportes", model);
        }
    }
}