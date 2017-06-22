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
        DocumentoServiceImpl documentoServise = new DocumentoServiceImpl();

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

        //para los errores del ultimo paso
        //public List<string> erroresListado { get; set; }
    
        public ReservaModelAndView()
        {
            listadoDeDiasTemporal = new List<string>();
            listadoDeFunciones = new List<FuncionModelAndView>();
            listadoDeTipoDocumentoReservaModel = new  List<TiposDocumentos>();
            listadoDeVersionesReservaModel = new List<Versiones>();
            listadoDeSedesReservaModel = new List<Sedes>();
            listadoDeDiasReservaModel = new List<string>();
           // erroresListado = new List<string>();
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
            TimeSpan horaDeInicioCarteleraTimeSpan = TimeSpan.FromHours(cartelera.HoraInicio);
            TimeSpan horaDeFuncionEnHoras =TimeSpan.FromHours(horaFuncion);
            TimeSpan horaFinPeliculaEnMinutos = TimeSpan.FromMinutes(peliculaService.getPeliculaPorId(cartelera.IdPelicula).Duracion);
            TimeSpan horaFinPelicula = horaDeFuncionEnHoras + horaFinPeliculaEnMinutos;
            TimeSpan horaFuncionTime = horaFinPelicula;
            string horaFuncionString = Herramientas.HerramientasFechasYHoras.pasarUnTimeSpanAHHMMString(horaDeInicioCarteleraTimeSpan);
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
                    if (horaFuncionTime.Days.ToString() == "1")
                    {
                        TimeSpan unDia = TimeSpan.FromDays(1);
                        horaFuncionTime = horaFuncionTime - unDia;
                    }
                    //formateo de la funcion en hh:mm
                    string horaFuncionStringFinal = Herramientas.HerramientasFechasYHoras.pasarUnTimeSpanAHHMMString(horaFuncionTime);
                    FuncionModelAndView nuevaFuncion = new FuncionModelAndView(i.ToString(), horaFuncionStringFinal);
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

        //la creo para limpiar los pasos la validacion de else if
        public void SetearValoresReservaModelAndViewFinal(string idPelicula, string idSedem, string idVersion, string dia, string hora, string paso, string numerodoc, string mail)
        {
            this.pelicula = peliculaService.getPeliculaPorId(Convert.ToInt32(idPelicula));
            this.idPeliculaReservaModel = idPelicula;
            this.idSedeReservaModel = idSedem;
            this.idVersionReservaModel = idVersion;
            this.diaDeReservaReservaModel = dia;
            this.listadoDeTipoDocumentoReservaModel = documentoServise.getListadoTipoDocumento();
            this.paso = paso;
            setearSedeYPeliculaYVersionParaReserva2(Convert.ToInt32(idPelicula), Convert.ToInt32(idSedem), Convert.ToInt32(idVersion));
            this.mailReservaModel = mail;
            this.NumeroDocumento = numerodoc;
        }
    }
}