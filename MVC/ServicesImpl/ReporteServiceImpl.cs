using MVC.DaoImpl;
using MVC.Entity;
using System;
using System.Collections.Generic;

namespace MVC.ServicesImpl
{
    public class ReporteServiceImpl
    {
        ReporteDaoImpl reporteDao = new ReporteDaoImpl();

        ReservaDaoImpl reservaDao = new ReservaDaoImpl();

        public List<Reservas> getListadoDeReservasConFilto(string fecha1, string fecha2)
        {
            if (fecha1 == null && fecha2 == null) //si los string recibidos son nulos, devuelve el listado de reservas completo. 
            {
                throw new Exception("No se ingresaron fechas.");
            }
            else //si no, las convierte a DateTime.
            {
                DateTime fecha11 = Convert.ToDateTime(fecha1);
                DateTime fecha22 = Convert.ToDateTime(fecha2);

                if (fecha1 == null) 
                {
                    fecha11 = DateTime.Now;
                }

                if (fecha2 == null)
                {
                    fecha22 = DateTime.Now;
                }
                if (fecha22.Month - fecha11.Month >= 1
                    && fecha22.Day > fecha11.Day) //valida que el período consultado no exceda los 30 días. 
                {
                    throw new Exception("El período de fechas ingresado es mayor a un mes");
                }
                else
                {
                    return reporteDao.getListadoDeReservasConFilto(fecha11, fecha22);
                }
            }
        }
    }
}