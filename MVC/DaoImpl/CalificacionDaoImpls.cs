using MVC.Entity;
using MVC.Manager;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVC.DaoImpl
{
    public class CalificacionDaoImpl
    {
        RepositorioManager repositorioManager = new RepositorioManager();

        //Trae todas las calificaciones de la base de datos. 
        public List<Calificaciones> getListadoDeCalificaciones()
        {
            var listadoDeGeneros = (from Calificacion in repositorioManager.ctx.Calificaciones select Calificacion);
            List<Calificaciones> listadoDeGenerosADevolver = listadoDeGeneros.ToList();

            if (listadoDeGeneros != null)
            {
                return listadoDeGenerosADevolver;
            }
            else
            {
                throw new Exception("Error al traer la lista de calificaciones de la base de datos");
            }

        }

        //Obtiene una calificación por id. 
        public Calificaciones getGeneroPorId(int id)
        {
            Calificaciones calificacionBuscada = repositorioManager.ctx.Calificaciones.OrderByDescending(o => o.IdCalificacion == id).FirstOrDefault();
            return calificacionBuscada;
        }
    }
}