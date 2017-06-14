using System;
using System.Collections.Generic;
using System.Linq;
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
        public string nombrePeliculaModel { get; set; }
        public string descripcionPeliculaModel { get; set; }
        public string idCalificacionPeliculaModel { get; set; }
        public string idgeneroPeliculaModel { get; set; }
        public string imagenPeliculaModel { get; set; }
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



        public PeliculaModelAndView()
        {
            this.idPeliculaModel = IdPelicula.ToString();
            this.nombrePeliculaModel = Nombre;
            this.descripcionPeliculaModel = Descripcion;
            this.idCalificacionPeliculaModel = IdCalificacion.ToString();
            this.idgeneroPeliculaModel = IdGenero.ToString();
            this.imagenPeliculaModel = Imagen;
            this.duracionPeliculaModel = Duracion.ToString();
            this.fechaDeCargaPeliculaModel = FechaCarga.ToString();
            this.listadoDePeliculas = new List<Peliculas>();

            llenarListados();
            //this.generoObjetoModel = generoService.getGeneroPorId(IdGenero);

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


    }
}

  