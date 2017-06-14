using MVC.Entity;
using MVC.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.DaoImpl
{
    public class GeneroDaoImpl
    {

        RepositorioManager repositorioManager = new RepositorioManager();

        //Trae todas los generos de la base de datos
        public List<Generos> getListadoDeGeneros()
        {
            var listadoDeGeneros = (from genero in repositorioManager.ctx.Generos select genero);
            List<Generos> listadoDeGenerosADevolver = listadoDeGeneros.ToList();

            if (listadoDeGeneros != null)
            {
                return listadoDeGenerosADevolver;
            }
            else
            {
                throw new Exception("Error al traer la lista de peliculas de la base de datos");
            }

        }

        //Obtiene un genero por id
        public Generos getGeneroPorId(int id)
        {
            Generos generoBuscado = repositorioManager.ctx.Generos.OrderByDescending(o => o.IdGenero == id).FirstOrDefault();
            return generoBuscado;
        }

    }
}