
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC.Entity;
using MVC.Manager;
using System.Data.Entity;

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


        public void modificarPeliculaDeLaBdd(int id, string nombre, string descripcion, string idCalificacion, string duracion, string idGenero, string imagen)
        {
            if (id == 0)
            {
                throw new Exception("");
            }
            else
            {
                Peliculas peli = getPeliculaPorId(id);

                if (peli != null) //en lugar de validar campo por campo, valido no recibir el objeto peli vacío. 
                {
                    peli.Nombre = nombre;
                    peli.Descripcion = descripcion;
                    peli.IdCalificacion = Convert.ToInt32(idCalificacion);
                    peli.Duracion = Convert.ToInt32(duracion);
                    peli.IdGenero = Convert.ToInt32(idGenero);
                    peli.Imagen = imagen;
                    repositorioManager.ctx.Peliculas.Attach(peli);
                    repositorioManager.ctx.Entry(peli).State = EntityState.Modified;
                    repositorioManager.ctx.SaveChanges();
                }
                else
                {
                    throw new Exception("");
                }
            }

        }
    }
}