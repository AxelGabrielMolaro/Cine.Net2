
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC.Entity;
using MVC.Manager;
namespace MVC.DaoImpl
{
    public class PeliculaDaoImpl 
    {
        RepositorioManager repositorioManager = new RepositorioManager();

        //Trae todas las peliculas de la base de datos
        public List<Peliculas> getListadoDePeliculas()
        {
            var listadoDePeliculas = (from pelicula in repositorioManager.ctx.Peliculas select pelicula);
            List<Peliculas> listadoDePeliculasADevolver = listadoDePeliculas.ToList();

            if (listadoDePeliculas != null)
            {
                return listadoDePeliculasADevolver;
            }
            else 
            {
                throw new Exception("Error al traer la lista de peliculas de la base de datos");
            }

        }

        //guarda una pelicula en al base de datos
        public void grabarPeliculaEnLaBaseDeDatos(Peliculas pelicula)
        {
            
            repositorioManager.ctx.Peliculas.Add(pelicula);
            repositorioManager.ctx.SaveChanges();
        }


        /// <summary>
        /// /Trae una pelicula por id¡
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Peliculas getPeliculaPorId(int id)
        {
            Peliculas peliculaBuscada = repositorioManager.ctx.Peliculas.OrderByDescending(o => o.IdPelicula == id).FirstOrDefault();
            return peliculaBuscada;
        }

        
    }
}