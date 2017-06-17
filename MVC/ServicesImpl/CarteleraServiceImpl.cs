using MVC.DaoImpl;
using MVC.Entity;
using MVC.Validadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.ServicesImpl
{
    public class CarteleraServiceImpl
    {
        CarteleraDaoImpl carteleraDao = new CarteleraDaoImpl();

        CarteleraValidation carteleraValidaciones = new CarteleraValidation();

        public List<Carteleras> getListadoDeCarteleras()
        {
            try
            {
                List<Carteleras> listadoDeCarteleras = carteleraDao.getListadoDeCarteleras();

                if (listadoDeCarteleras == null || listadoDeCarteleras.ToArray().Length == 0)
                {
                    throw new Exception("No hay ninguna cartelera cargada");
                }
                else
                {
                    return listadoDeCarteleras;
                }
            }
            catch (Exception e)
            {
                throw new Exception("No hay ninguna cartelera cargada");
            }
        }

        public void grabarUnaCartelera(Carteleras cartelera)
        {
            if (cartelera != null)
            {
                carteleraDao.grabarCarteleraEnLaBaseDeDatos(cartelera);
            }
            else
            {
                throw new Exception("Ingrese una cartelera antes de guardarla");
            }
        }

        public Carteleras obtenerCarteleraPorId(int id)
        {
            Carteleras cartelera = carteleraDao.consultarCarteleraPorIdEnLaBdd(id);
            if (id == 0)
            {
                throw new Exception("La cartelera no existe.");
            }
            if (cartelera == null)
            {
                throw new Exception("La cartelera no existe.");
            }
            else
            {
                return cartelera;
            }
        }

        public void crearCartelera(Carteleras carteleraAGrabar)
        {
            if (carteleraAGrabar == null)
            {
                throw new Exception("Debe ingresar una cartelera");
            }
            else
            {
                carteleraValidaciones.validarCarteleraIngresada(carteleraAGrabar);
            }
        }
        public void modificarCarteleraPorId(int id, string idSede, string idPelicula, string hora, string fechaInicio, string fechaFin, string sala, string idVersion, string lunes, string martes, string miercoles, string jueves, string viernes, string sabado, string domingo, string fechaCarga)
        {
            try
            {
                carteleraDao.modificarCarteleraDeLaBddPorId(id, idSede, idPelicula, hora, fechaInicio, fechaFin, sala, idVersion, lunes, martes, miercoles, jueves, viernes, sabado, domingo, fechaCarga);
            }
            catch
            {
                throw new Exception("Debe llenar todos los campos");
            }
        }
        public void eliminarCartelerametodo(Carteleras cartelera)
        {
            try
            {
                carteleraDao.eliminarCarteleraDeLaBdd(cartelera);
            }
            catch
            {
                throw new Exception("Error al eliminar");
            }
        }

        //Este método lo creé por si necesitamos traer la lista sin registros que sea más práctico borrar toda la lista.
        public void eliminarTodasLasCarteleras()
        {
            List<Carteleras> lista = carteleraDao.getListadoDeCarteleras();
            try
            {
                carteleraDao.eliminarTodasLasCarteleras(lista);
            }
            catch
            {
                throw new Exception("Error al eliminar");
            }
        }
    }
}