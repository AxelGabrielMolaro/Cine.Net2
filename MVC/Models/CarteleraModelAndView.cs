using MVC.Entity;
using MVC.ServicesImpl;
using System;
using System.Collections.Generic;
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

        //Propiedades string. 
        public string idCarteleraModel { get; set; }
        public string idSedeCarteleraModel { get; set; }
        public string idPeliculaCarteleraModel { get; set; }
        public string horaInicioModel { get; set; }
        public string fechaInicioModel { get; set; }
        public string fechaFinModel { get; set; }
        public string numeroSalaModel { get; set; }
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
    }
}