using MVC.Entity;
using MVC.ServicesImpl;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [CustomValidation(typeof(CarteleraModelAndView), "ValueDistintoDe0")]
        public string horaInicioModel { get; set; }
        [Required(ErrorMessage = "Ingrese la fecha de inicio")]
        [CustomValidation(typeof(CarteleraModelAndView), "ValidadorDeFechas")]
        public string fechaInicioModel { get; set; }
        [Required(ErrorMessage = "Ingrese la fecha de fin")]
        [CustomValidation(typeof(CarteleraModelAndView), "ValidadorDeFechas")]
        public string fechaFinModel { get; set; }
        [Required(ErrorMessage = "Ingrese el número de sala")]
        [RegularExpression(@"^[+-]?\d+(\.\d+)?$", ErrorMessage = "El campo sala admite sólo números")]
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


        //VALIDACIONES: 
        public static ValidationResult ValueDistintoDe0(object value, ValidationContext c)
        {
            String valueString = value as String;
            if (valueString == null)
            {
                return new ValidationResult("Elija una opcion");
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
            if (fechaAValidar != null)
            {
                if (Convert.ToDateTime(fechaAValidar).CompareTo(DateTime.Now) < 0)
                {
                    return new ValidationResult("La fecha ya expiró");
                }
            }

            return ValidationResult.Success;
        }
    }
}