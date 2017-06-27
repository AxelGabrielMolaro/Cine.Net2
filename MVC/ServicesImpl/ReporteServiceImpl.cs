using MVC.DaoImpl;
using MVC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.ServicesImpl
{
    public class ReporteServiceImpl
    {
        ReporteDaoImpl reporteDao = new ReporteDaoImpl();
        public List<Reservas> getListadoDeReservasConFilto(string fecha1, string fecha2)
        {
            return reporteDao.getListadoDeReservasConFilto(fecha1, fecha2);
        }
    }
}