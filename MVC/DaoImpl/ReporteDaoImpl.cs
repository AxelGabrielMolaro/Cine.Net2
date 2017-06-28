using MVC.Entity;
using MVC.Manager;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVC.DaoImpl
{
    public class ReporteDaoImpl
    {
        RepositorioManager repositorioManager = new RepositorioManager();
        ReservaDaoImpl reservaDao = new ReservaDaoImpl();
        public List<Reservas> getListadoDeReservasConFilto(DateTime fecha1, DateTime fecha2)
        {
            if (fecha1 != null && fecha2 == null) //devuelve todas las reservas a partir de esa fecha 
            {
                var listaDesde = (from reserva in repositorioManager.ctx.Reservas
                                  where reserva.FechaHoraInicio >= fecha1
                                  select reserva).ToList();
                return listaDesde;
            }

            if (fecha1 == null && fecha2 != null) //devuelve todas las reservas hasta esa fecha inclusive
            {
                var listaHasta = (from reserva in repositorioManager.ctx.Reservas
                                  where reserva.FechaHoraInicio <= fecha2
                                  select reserva).ToList();
                return listaHasta;
            }
            else //devuelve las reservas que están dentro de la fecha de inicio y de fin especificadas 
            {
                var listaDesdeHasta = (from reserva in repositorioManager.ctx.Reservas
                                       where reserva.FechaHoraInicio >= fecha1 && reserva.FechaHoraInicio <= fecha2
                                       select reserva).ToList();
                return listaDesdeHasta;
            }
        }
    }
}