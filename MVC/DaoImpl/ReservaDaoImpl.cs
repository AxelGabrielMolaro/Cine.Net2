using MVC.Entity;
using MVC.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.DaoImpl
{
    
    public class ReservaDaoImpl
    {
        RepositorioManager repositorioManager = new RepositorioManager();
        /// <summary>
        /// Agrega un reserva en la base de datos
        /// </summary>
        /// <param name="reserva"></param>
        public void agregarReserva(Reservas reserva)
        {
            repositorioManager.ctx.Reservas.Add(reserva);
            repositorioManager.ctx.SaveChanges();
            
        }

        /// <summary>
        /// Busca una reserva por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Reservas getReservaPorId(int id) {

            return null;
        }

        //--------PARA RESERVAS Mesco con otras clases-----//


        /// <summary>
        /// trae el listado de carteleras cuto ELIMINAR METODO
        /// </summary>
        /// <param name="idPelicula"></param>
        /// <returns></returns>
        [Obsolete]
        public List<Carteleras> getCartelerasCuyoIdDePeliculaSeaElMandado(int idPeliculaRecibido)
        {
            var listado = repositorioManager.ctx.Carteleras.OrderByDescending(o => o.IdPelicula == idPeliculaRecibido);
            List<Carteleras> listadoADevolder = new List<Carteleras>();
            if (listado != null)
            {
                listadoADevolder = listado.ToList();
            }
            return listadoADevolder;
        }


        public List<Carteleras> getListadosDeCartelerasParaReserva(int idPeliculaRecibido, int idVersionRecibido, int  idSedeRecibido) 
        {
          
            List<Carteleras> listadoADevolder = new List<Carteleras>();

            if (idSedeRecibido != 0)
            {
                var listado = from carteleras in repositorioManager.ctx.Carteleras where carteleras.IdPelicula == idPeliculaRecibido && carteleras.IdVersion == idVersionRecibido && carteleras.IdSede == idSedeRecibido select carteleras;
                if (listado != null)
                {
                    listadoADevolder = listado.ToList();
                }
            }

            else if (idVersionRecibido != 0)
            {
                var listado = from carteleras in repositorioManager.ctx.Carteleras where carteleras.IdPelicula == idPeliculaRecibido && carteleras.IdVersion == idVersionRecibido select carteleras;
                if (listado != null)
                {
                    listadoADevolder = listado.ToList();
                }
            }
            else if (idPeliculaRecibido != 0)
            {
                PeliculaDaoImpl peliculaDao = new PeliculaDaoImpl();
                var listado = from carteleras in repositorioManager.ctx.Carteleras where carteleras.IdPelicula == idPeliculaRecibido select carteleras;
                if (listado != null)
                {
                    listadoADevolder = listado.ToList();
                }
            }


            return listadoADevolder;
        }

        //reportes


        public List<Reservas> getListadoDeReservas()
        {
            var listadoDeReservas = (from reserva in repositorioManager.ctx.Reservas select reserva);
            List<Reservas> listadoDeReservasADevolver = listadoDeReservas.ToList();
            return listadoDeReservasADevolver;
        }


    }
}