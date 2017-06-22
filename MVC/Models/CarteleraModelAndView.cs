using MVC.Entity;
using MVC.ServicesImpl;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class CarteleraModelAndView : Carteleras
    {
        /* 
         * Implementación de los métodos de Services. 
         */

        sedeServiceImpl sedeService = new sedeServiceImpl();
        PeliculaServiceImpl peliculaService = new PeliculaServiceImpl();
        VersionServiceImpl versionService = new VersionServiceImpl();

        CarteleraServiceImpl carteleraService = new CarteleraServiceImpl();

        //Propiedades string. 
        public string idCarteleraModel { get; set; }
        [CustomValidation(typeof(CarteleraModelAndView), "ValueDistintoDe0")]
        public string idSedeCarteleraModel { get; set; }
        [CustomValidation(typeof(CarteleraModelAndView), "ValueDistintoDe0")]
        public string idPeliculaCarteleraModel { get; set; }
        [Required(ErrorMessage = "Ingrese la hora de inicio")]
        [CustomValidation(typeof(CarteleraModelAndView), "HoraInicioMayorA15")]
        public string horaInicioModel { get; set; }
        [Required(ErrorMessage = "Ingrese la fecha de inicio")]      
        [CustomValidation(typeof(CarteleraModelAndView), "ValidadorDeFechas")]
        public string fechaInicioModel { get; set; }
        [Required(ErrorMessage = "Ingrese la fecha de fin")]
        [CustomValidation(typeof(CarteleraModelAndView), "ValidadorDeFechas")]
        public string fechaFinModel { get; set; }
        [Required(ErrorMessage = "Ingrese el número de sala")]
        public string numeroSalaModel { get; set; }
        [CustomValidation(typeof(CarteleraModelAndView), "ValueDistintoDe0")]
        public string idVersionModel { get; set; }
        public string lunesModel { get; set; }
        public string martesModel { get; set; }
        public string miercolesModel { get; set; }
        public string juevesModel { get; set; }
        public string viernesModel { get; set; }
        public string sabadoModel { get; set; }
        public string domingoModel { get; set; }
        public string fechaCargaModel { get; set; }

        //Objetos. 
        public Sedes sedeObjetoModel { get; set; }
        public Peliculas peliculaObjetoModel { get; set; }

        //Listar carteleras. 
        public List<Carteleras> listadoDeCarteleras { get; set; }

        //Listar options de select. 
        public List<Sedes> listadoDeSedes { get; set; }
        public List<Peliculas> listadoDePeliculas { get; set; }

        public List<Versiones> listadoDeVersiones { get; set; }


        //Para las funciones. 
        public List<FuncionModelAndView> listadoDeFunciones { get; set; }


        public CarteleraModelAndView()
        {
            idCarteleraModel = IdCartelera.ToString();
            idSedeCarteleraModel = IdSede.ToString();
            idPeliculaCarteleraModel = IdPelicula.ToString();
            horaInicioModel = HoraInicio.ToString();
            fechaInicioModel = FechaInicio.ToString();
            fechaFinModel = FechaFin.ToString();
            numeroSalaModel = NumeroSala.ToString();
            idVersionModel = IdVersion.ToString();
            lunesModel = Lunes.ToString();
            martesModel = Martes.ToString();
            miercolesModel = Miercoles.ToString();
            juevesModel = Jueves.ToString();
            viernesModel = Viernes.ToString();
            sabadoModel = Sabado.ToString();
            domingoModel = Domingo.ToString();
            fechaCargaModel = FechaCarga.ToString();
            listadoDeCarteleras = new List<Carteleras>();

            llenarListados(); //Llena las listas Sedes, Peliculas y Versiones. 

            listadoDeFunciones = new List<FuncionModelAndView>();

        }

        public void llenarListados()
        {
            try
            {
                listadoDeSedes = sedeService.getListadoDeSedes();
                listadoDePeliculas = peliculaService.getListadoDePeliculas();
                listadoDeVersiones = versionService.getListadoDeVersiones();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }


        //Validaciones
        //(El problema con estas validaciones es que saltan en el formulario de agregar también, REVISAR ESO)
        public static ValidationResult ValueDistintoDe0(object value, ValidationContext c)
        {
            String valueString = value as String;
            if (valueString == "0")
            {
                return new ValidationResult("Elija una opcion");
            }
            return ValidationResult.Success;
        }

        public static ValidationResult HoraInicioMayorA15(object value, ValidationContext c)
        {
            int valueInt = Convert.ToInt32(value);
            if (valueInt < 15)
            {
                return new ValidationResult("La hora de inicio debe ser a partir de las 15:00hs.");
            }
            return ValidationResult.Success;
        }

        public static ValidationResult ValidadorDeFechas(object value, ValidationContext c)
        {
            string fechaAValidar = value as string;

            /* 
             * El método CompareTo() compara dos fechas. 
             * Si devuelve 0 significa que las fechas son iguales, 
             * si devuelve mayor a 0 significa que la fecha a comparar es mayor, 
             * si devuelve menor a 0 significa que la fecha a comparar es menor. 
            */

            //Si la fecha de inicio o fin son anteriores a la actual (es decir, ya pasaron), no permite agregar. 
            if (Convert.ToDateTime(fechaAValidar).CompareTo(DateTime.Now) < 0)
            {
                return new ValidationResult("La fecha ya expiró");
            }

            return ValidationResult.Success;
        }


        /*public void llenarListadoDeFunciones(CarteleraModelAndView cartelera)
        {
            int horaFuncion = cartelera.HoraInicio;
            int horaFinPelicula = horaFuncion + peliculaService.getPeliculaPorId(cartelera.IdPelicula).Duracion;
            string horaFuncionString = horaFuncion.ToString();
            for (var i = 1; i <= 7; i++)
            {
                if (i == 1)
                {
                    FuncionModelAndView nuevaFuncion = new FuncionModelAndView(i.ToString(), horaFuncionString);
                    horaFuncion = horaFinPelicula + Convert.ToInt32(0.5);
                    horaFinPelicula = horaFuncion + peliculaService.getPeliculaPorId(cartelera.IdPelicula).Duracion;

                    listadoDeFunciones.Add(nuevaFuncion);
                }
                else
                {
                    int horarioFuncion = horaFinPelicula + Convert.ToInt32(0.5);
                    FuncionModelAndView nuevaFuncion = new FuncionModelAndView(i.ToString(), horarioFuncion.ToString());
                    horaFuncion = horaFinPelicula + Convert.ToInt32(0.5);
                    horaFinPelicula = horaFuncion + peliculaService.getPeliculaPorId(cartelera.IdPelicula).Duracion;
                    listadoDeFunciones.Add(nuevaFuncion);
                }
            }
        */
        public void llenarListadoDeFunciones()
        {
            Carteleras cartelera = new Carteleras();
            int horaFuncion = cartelera.HoraInicio;
            int horaFinPelicula = horaFuncion + peliculaService.getPeliculaPorId(cartelera.IdPelicula).Duracion;
            string horaFuncionString = horaFuncion.ToString();
            for (var i = 1; i <= 7; i++)
            {
                if (i == 1)
                {
                    FuncionModelAndView primeraFuncion = new FuncionModelAndView(i.ToString(), horaFuncionString);
                    horaFuncion = horaFinPelicula + Convert.ToInt32(0.5);
                    horaFinPelicula = horaFuncion + peliculaService.getPeliculaPorId(cartelera.IdPelicula).Duracion;

                    listadoDeFunciones.Add(primeraFuncion);
                }
                else
                {
                    int horarioFuncion = horaFinPelicula + Convert.ToInt32(0.5);
                    FuncionModelAndView nuevaFuncion = new FuncionModelAndView(i.ToString(), horarioFuncion.ToString());
                    horaFuncion = horaFinPelicula + Convert.ToInt32(0.5);
                    horaFinPelicula = horaFuncion + peliculaService.getPeliculaPorId(cartelera.IdPelicula).Duracion;
                    listadoDeFunciones.Add(nuevaFuncion);
                }

            }
        }
    }
}