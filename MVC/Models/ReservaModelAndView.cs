using MVC.Entity;
using MVC.ServicesImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class ReservaModelAndView : Reservas
    {
        ReservaServiceImpl reservaService = new ReservaServiceImpl();
        PeliculaServiceImpl peliculaService = new PeliculaServiceImpl();
        VersionServiceImpl versionService = new VersionServiceImpl();
        sedeServiceImpl sedeServiceImpl = new sedeServiceImpl();

        public string paso { get; set; }
        public string idReservaReservaModel{ get; set; }
        public string idSedeReservaModel { get; set; }
        public string idVersionReservaModel { get; set; }
        public string idPeliculaReservaModel { get; set; }
        public string fechaInicioReservaModel { get; set; }
        public string mailReservaModel { get; set; }
        public string idTipoDocumentoReservaModel { get; set; }
        public string numeroDocumentoReservaModel { get; set; }
        public string cantidadDeEntradasReservaModel { get; set; }
        public string fechaCargaReservaModel { get; set; }
        public string diaDeReservaReservaModel { get; set; }
        public string horaDeFuncionReservaModel { get; set; }
        //ver el tema de las funciones

        //reserva 2
        public Peliculas pelicula { get; set; }
        public Sedes sede { get; set; }
        public Versiones version { get; set; }

        //listados
        public List<Versiones> listadoDeVersionesReservaModel { get; set; }
        public List<TiposDocumentos> listadoDeTipoDocumentoModel { get; set; }
        public List<Sedes> listadoDeSedesReservaModel { get; set; }
        public List<string>  listadoDeDiasReservaModel { get; set; }
        public List<TiposDocumentos> listadoDeTipoDocumentoReservaModel { get; set; }

        //para pasos previos de listados de fechas
        public List<string> listadoDeDiasTemporal { get; set; }

        //para el listado de funciones
        public List<FuncionModelAndView> listadoDeFunciones { get; set; }

        //para reportes
        public List<Reservas> listadoDeReservasReporteModel { get; set; }
        public string fechaDesdeReporteModel { get; set; }
        public string fechaHastaReporteModel { get; set; }
        public ReservaModelAndView()
        {
            listadoDeDiasTemporal = new List<string>();
            listadoDeFunciones = new List<FuncionModelAndView>();
            listadoDeTipoDocumentoReservaModel = new  List<TiposDocumentos>();
        }

        /// <summary>
        /// Va a traer un listado de funciones y estas van a tener de diferencia el final de la pelicula,
        /// mas media hora 
        /// </summary>
        /// <param name="idPeliculaF"></param>
        /// <param name="idSedeF"></param>
        /// <param name="idVersionF"></param>
        public void llenarListadoDeFunciones(int idPeliculaF, int idSedeF,int idVersionF)
        {
            Carteleras cartelera = reservaService.getCarteleraReserva(idPeliculaF, idSedeF, idVersionF);
            int horaFuncion = cartelera.HoraInicio;
            TimeSpan horaDeFuncionEnHoras =TimeSpan.FromHours(horaFuncion);
            TimeSpan horaFinPeliculaEnMinutos = TimeSpan.FromMinutes(peliculaService.getPeliculaPorId(cartelera.IdPelicula).Duracion);
            TimeSpan horaFinPelicula = horaDeFuncionEnHoras + horaFinPeliculaEnMinutos;
            TimeSpan horaFuncionTime = horaFinPelicula;
            string horaFuncionString = horaFuncion.ToString();
            for (var i = 1; i<=7; i++)
            {
                if (i == 1)
                {
                    FuncionModelAndView nuevaFuncion = new FuncionModelAndView(i.ToString(), horaFuncionString);
                    horaFuncionTime = horaFuncionTime + TimeSpan.FromMinutes(30);
                    horaFinPelicula = horaFuncionTime + horaFinPeliculaEnMinutos;   
                    listadoDeFunciones.Add(nuevaFuncion);
                }
                else
                {
                    horaFuncionTime = horaFuncionTime + TimeSpan.FromMinutes(30);
                    FuncionModelAndView nuevaFuncion = new FuncionModelAndView(i.ToString(), horaFuncionTime.ToString());
                    horaFuncionTime = horaFuncionTime + TimeSpan.FromMinutes(30);
                    horaFinPelicula = horaFuncionTime + horaFinPeliculaEnMinutos;   
                    listadoDeFunciones.Add(nuevaFuncion);
                }

            }

            
        }

        public void setearSedeYPeliculaYVersionParaReserva2(int idPeli, int idSede, int idVers)
        {
            pelicula = peliculaService.getPeliculaPorId(idPeli);
            sede = sedeServiceImpl.getSedePorId(idSede);
            version = versionService.getVersionPorId(idVers);
        }
    }
}