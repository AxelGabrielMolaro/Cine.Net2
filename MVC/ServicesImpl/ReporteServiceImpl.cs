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
            DateTime fecha11 = Convert.ToDateTime(fecha1);
            DateTime fecha22 = Convert.ToDateTime(fecha2);

            if (fecha22.Month - fecha11.Month >= 1
                && fecha22.Day > fecha11.Day) //valida que el período consultado no exceda los 30 días. 
                                              //FALTA LANZARLA A LA VISTA
            {
                throw new Exception("El período de fechas ingresado es mayor a un mes");
            }
            else
            {
                return reporteDao.getListadoDeReservasConFilto(fecha1, fecha2);
            }
        }
    }
}