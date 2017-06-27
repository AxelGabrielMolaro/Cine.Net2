using MVC.Entity;
using MVC.Manager;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MVC.DaoImpl
{
    public class ReporteDaoImpl
    {
        RepositorioManager repositorioManager = new RepositorioManager();
        ReservaDaoImpl reservaDao = new ReservaDaoImpl();
        public List<Reservas> getListadoDeReservasConFilto(string fecha1, string fecha2)
        {
            if ((fecha1 == null || fecha1 == "") && (fecha2 == null || fecha2 == "")) //si los string recibidos son nulos, devuelve el listado de reservas completo. 
            {
                List<Reservas> listado = reservaDao.getListadoDeReservas();
                return listado;
            }
            else
            {
                DateTime fecha11 = Convert.ToDateTime(fecha1); //si no, convierte el string a DateTime
                DateTime fecha22 = Convert.ToDateTime(fecha2);

                if (fecha11 != null && fecha22 == null) //devuelve todas las reservas a partir de esa fecha 
                {
                    var listaDesde = (from reserva in repositorioManager.ctx.Reservas
                                      where reserva.FechaHoraInicio >= fecha11
                                      select reserva).ToList();
                    return listaDesde;
                }

                if (fecha11 == null && fecha22 != null) //devuelve todas las reservas hasta esa fecha inclusive
                {
                    var listaHasta = (from reserva in repositorioManager.ctx.Reservas
                                      where reserva.FechaHoraInicio <= fecha22
                                      select reserva).ToList();
                    return listaHasta;
                }
                else //devuelve las reservas que están dentro de la fecha de inicio y de fin especificadas 
                {
                    var listaDesdeHasta = (from reserva in repositorioManager.ctx.Reservas
                                           where reserva.FechaHoraInicio >= fecha11 && reserva.FechaHoraInicio <= fecha22
                                           select reserva).ToList();
                    return listaDesdeHasta;
                }
            }
        }
    }
}