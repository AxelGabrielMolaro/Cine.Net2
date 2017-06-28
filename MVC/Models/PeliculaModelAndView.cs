using System;
using System.Collections.Generic;
using System.Web;
using MVC.Entity;
using System.ComponentModel.DataAnnotations;
using MVC.ServicesImpl;


namespace MVC.Models
{
    public class PeliculaModelAndView : Peliculas
    {
        //implementacion de metodos 
        GeneroServiceImpl generoService = new GeneroServiceImpl();
        CalificacionServiceImpl calificaionService = new CalificacionServiceImpl();

        //Strings 
        public string idPeliculaModel { get; set; }
        [Required(ErrorMessage = "Ingrese el nombre")]
        public string nombrePeliculaModel { get; set; }

        [Required(ErrorMessage = "Ingrese la descripción")]
        public string descripcionPeliculaModel { get; set; }

        [CustomValidation(typeof(PeliculaModelAndView), "ValueDistintoDe0")]
        public string idCalificacionPeliculaModel { get; set; }

        [CustomValidation(typeof(PeliculaModelAndView), "ValueDistintoDe0")]
        public string idgeneroPeliculaModel { get; set; }

        [Required(ErrorMessage = "Ingrese una imagen")]
        public HttpPostedFileBase imagenPeliculaModel { get; set; }

        [Required(ErrorMessage = "Ingrese la duración")]
        [Range(15, 90, ErrorMessage = "Ingrese una duracion menor a 90 minutos y mayor a 15 minutos.")]
        public string duracionPeliculaModel { get; set; }

        public string fechaDeCargaPeliculaModel { get; set; }

        //objetos
        public Generos generoObjetoModel { get; set; }
        public Calificaciones calificaionGeneroModel { get; set; }

        //para listar las peliculas
        public List<Peliculas> listadoDePeliculas { get; set; }

        //listar combos
        public List<Generos> listadoDeGeneros { get; set; }
        public List<Calificaciones> listadoDeCalificaciones { get; set; }

        public string imagenNamePeliculaModel { get; set; }

        public PeliculaModelAndView()
        {
            this.listadoDePeliculas = new List<Peliculas>();
            llenarListados();
        }

        public PeliculaModelAndView(string id, string nombrep, string descripcionp, string calificacionp, string generoP, string imagenP, string duracionP)
        {
            this.nombrePeliculaModel = nombrep;
            this.descripcionPeliculaModel = descripcionp;
            this.duracionPeliculaModel = duracionP;
            this.idCalificacionPeliculaModel = calificacionp;
            this.idgeneroPeliculaModel = generoP;
            this.imagenNamePeliculaModel = imagenP;
            llenarListados();
        }

        //llena la lista genero y calificaciones
        public void llenarListados()
        {
            try
            {
                this.listadoDeGeneros = generoService.getListadoDeGeneros();
                this.listadoDeCalificaciones = calificaionService.getListadoDeCalificaciones();
            }

            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        //validaciones
        public static ValidationResult ValueDistintoDe0(object value, ValidationContext c)
        {
            String valueString = value as String;
            if (valueString == "0")
            {
                return new ValidationResult("Elija una opcion");
            }
            return ValidationResult.Success;
        }
    }
}