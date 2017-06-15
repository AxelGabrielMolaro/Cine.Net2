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
            List<Reservas> listado = new List<Reservas>();
            DateTime fecha11 = new DateTime() ;
            DateTime fecha22 = new DateTime();
            if (fecha1 != null && fecha1 != "")
            {
                 fecha11 = DateTime.ParseExact(fecha1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            }

            if (fecha2 != null && fecha2 != "")
            {
                fecha22 = DateTime.ParseExact(fecha2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                
            }

            if (fecha1 == null && fecha22 == null)
            {
                listado = reservaDao.getListadoDeReservas();
                return listado;
            }
            if(fecha1 != null && fecha1 !="" && fecha2 == null)
            {
                var listado2 = repositorioManager.ctx.Reservas.OrderByDescending(o => o.FechaCarga > fecha11);
                listado = listado2.ToList();
                return listado;
            }
            if (fecha2 != null && fecha2 != "" && fecha1 == null)
            {
                var listado2 = repositorioManager.ctx.Reservas.OrderByDescending(o => o.FechaCarga < fecha22);
                listado = listado2.ToList();
                return listado;
            }
            if (fecha2 != null && fecha2 != "" && fecha1 != null && fecha1 == "")
            {
                var listado2 = repositorioManager.ctx.Reservas.OrderByDescending(o => o.FechaCarga > fecha11 && o.FechaCarga< fecha22);
                listado = listado2.ToList();
                return listado;
            }

            return listado;

        }
    }
}