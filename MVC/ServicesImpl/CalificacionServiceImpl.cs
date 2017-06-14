using MVC.DaoImpl;
using MVC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.ServicesImpl
{
    public class CalificacionServiceImpl
    {
        CalificacionDaoImpl calificacionDao = new CalificacionDaoImpl();
        //Trae todas las calificaciones, si esta vacio tira exepcion , y si no trae el listado
        public List<Calificaciones> getListadoDeCalificaciones()
        {
            List<Calificaciones> listado = calificacionDao.getListadoDeCalificaciones();
            if (listado == null || listado.ToArray().Length == 0)
            {
                throw new Exception("No hay calificaciones cargadas");
            }
            else
            {
                return listado;
            }
        }

        //trae una calificacion por id , si el id es 0 tira error, o si la sede que traigo no existe
        public Calificaciones getcalificacionPorId(int id)
        {
            Calificaciones calificacion = calificacionDao.getGeneroPorId(id);
            if (id == 0)
            {
                throw new Exception("Error al buscar calificacion. Ese calificacion no existe");
            }
            if (calificacion == null)
            {
                throw new Exception("Error al buscar calificacion. No existe una calificacion con esa id");
            }
            else
            {
                return calificacion;
            }


        }
    }
}