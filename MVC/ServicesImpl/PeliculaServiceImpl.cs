﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC.Entity;
using MVC.DaoImpl;
using MVC.Manager;
using System.IO;

namespace MVC.ServicesImpl
{
    public class PeliculaServiceImpl
    {
        PeliculaDaoImpl peliculaDao = new PeliculaDaoImpl();

        //Si hay un error en la consulta de la bdd devuelve una lista vacia y manda el msj de error
        //Si no hay peliculas cargadas tambien se lanza una excepción
        //Si no hay errores, manda el listado de películas
        public List<Peliculas> getListadoDePeliculas()
        {
            try
            {
                List<Peliculas> listadoDePeliculas = peliculaDao.getListadoDePeliculas();

                if (listadoDePeliculas == null || listadoDePeliculas.ToArray().Length == 0)
                {
                    throw new Exception("No hay ninguna película cargada");

                }
                else
                {
                    return listadoDePeliculas;
                }

            }
            catch (Exception e)
            {
                //Mostrar excepcion 
                throw new Exception("No hay ninguna pelicula cargada ");
            }

        }

        //si es null lanza excepcion
        //Si es correcta se graba la pelicula en la bdd
        public void grabarUnaPelicula(Peliculas pelicula)
        {
            if (pelicula != null)
            {
                peliculaDao.grabarPeliculaEnLaBaseDeDatos(pelicula);
            }
            else
            {
                throw new Exception("Ingrese una película antes de guardarla");
            }
        }

        public Peliculas getPeliculaPorId(int id)
        {
            Peliculas peliculaBuscada = peliculaDao.getPeliculaPorId(Convert.ToInt32(id));
            return peliculaBuscada;
        }


        public string guardarUnaImagenEnUnCarpetaSeServidor(string urlCarpeta, HttpPostedFileBase imagenPelicula)
        {
            string extencion = Path.GetExtension(imagenPelicula.FileName);
            if (extencion != ".png" && extencion != ".jpg")
            {
                throw new FormatException("Por favor ingrese una imagen de extención png o jpg .");
            }
            string savePath = urlCarpeta;
            string fileName = imagenPelicula.FileName;
            string pathToCheck = savePath + fileName;
            string tempfileName = "";
            if (System.IO.File.Exists(pathToCheck))
            {
                int counter = 2;
                while (System.IO.File.Exists(pathToCheck))
                {
                    // if a file with this name already exists,
                    // prefix the filename with a number.
                    tempfileName = counter.ToString() + fileName;
                    pathToCheck = savePath + tempfileName;
                    counter++;
                }

                fileName = tempfileName;
            }
            else
            {
                Console.WriteLine("Se guardo la imagen correctamente");
            }

            // Append the name of the file to upload to the path.
            savePath += fileName;

            // Call the SaveAs method to save the uploaded
            // file to the specified directory.
            imagenPelicula.SaveAs(savePath);

            return fileName;
        }

        /// <summary>
        /// Trar un listado de peliculas actuales y con un mes de anticipacion
        /// </summary>
        public List<Peliculas> getListadoDePeliculasHome()
        {
            List<Peliculas> listadoDePeliculasAMostrar = new List<Peliculas>();
            List<Carteleras> listadoDeCarteleras = getListadoDeCartelerasHome();
            foreach (Carteleras cartelera in listadoDeCarteleras)
            {
                if (!listadoDePeliculasAMostrar.Contains(peliculaDao.getPeliculaPorId(cartelera.IdPelicula)))
                {
                    listadoDePeliculasAMostrar.Add(peliculaDao.getPeliculaPorId(cartelera.IdPelicula));
                }

            }
            return listadoDePeliculasAMostrar;
        }


        //pasar a cartelera
        /// <summary>
        /// Trar un listado de carteleras actuales y con un mes de anticipacion
        /// </summary>
        public List<Carteleras> getListadoDeCartelerasHome()
        {

            CarteleraDaoImpl carteleraDao = new CarteleraDaoImpl();
            List<Carteleras> listadoDeCarteleras = carteleraDao.getListadoDeCarteleras();
            List<Carteleras> listadoDeCartelerasAMostrar = new List<Carteleras>();
            foreach (Carteleras cartelera in listadoDeCarteleras)
            {
                /* 
                 * si la diferencia entre el mes de la fecha de inicio de la cartelera y el mes actual
                 es 1 o menor, y la diferencia entre el mes de la fecha de fin y el mes de actual también es 1 
                 o menor, y además el día de la fecha de fin de la cartelera es menor o igual al día actual 
                 agrega la cartelera a ser mostrada. 
                 */
                if (cartelera.FechaInicio.Month - DateTime.Now.Month <= 1
                        && cartelera.FechaFin.Month - DateTime.Now.Month <= 1
                        && cartelera.FechaFin.Day <= DateTime.Now.Day)
                {
                    listadoDeCartelerasAMostrar.Add(cartelera);
                }
            }

            return listadoDeCartelerasAMostrar;

        }

        public Carteleras getCarteleraPorId(int id)
        {
            RepositorioManager repositorioManager = new RepositorioManager();
            Carteleras carteleraBuscada = repositorioManager.ctx.Carteleras.OrderByDescending(o => o.IdCartelera == id).FirstOrDefault();
            return carteleraBuscada;
        }

        public void modificarPelicula(int id, string nombre, string descripcion, string idCalificacion, string duracion, string idGenero, string imagen)
        {
            try
            {
                peliculaDao.modificarPeliculaDeLaBdd(id, nombre, descripcion, idCalificacion, duracion, idGenero, imagen);
            }
            catch (Exception e)
            {
                throw new Exception("Error al modificar");
            }
        }
    }
}