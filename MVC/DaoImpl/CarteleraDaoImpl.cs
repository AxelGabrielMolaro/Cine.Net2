using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC.Entity;
using MVC.Manager;
using System.Data.Entity;

namespace MVC.DaoImpl
{
    public class CarteleraDaoImpl
    {
        RepositorioManager repositorioManager = new RepositorioManager();

        //Consulta la lista de carteleras de la base de datos.
        public List<Carteleras> getListadoDeCarteleras()
        {
            var listadoDeCarteleras = (from cartelera in repositorioManager.ctx.Carteleras select cartelera);
            List<Carteleras> listadoDeCartelerasADevolver = listadoDeCarteleras.ToList();

            if (listadoDeCarteleras != null)
            {
                return listadoDeCartelerasADevolver;
            }
            else
            {
                throw new Exception("Error al traer la lista de carteleras de la base de datos");
            }
        }

        //Guarda en la base de datos.
        public void grabarCarteleraEnLaBaseDeDatos(Carteleras cartelera)
        {
            repositorioManager.ctx.Carteleras.Add(cartelera);
            repositorioManager.ctx.SaveChanges();
        }

        public Carteleras consultarCarteleraPorIdEnLaBdd(int id)
        {
            Carteleras carteleraBuscada = repositorioManager.ctx.Carteleras.OrderByDescending(o => o.IdCartelera == id).FirstOrDefault();
            return carteleraBuscada;
        }

        public void modificarCarteleraDeLaBddPorId(int id, string idSede, string idPelicula, string hora, string fechaInicio, string fechaFin, string sala, string idVersion, string lunes, string martes, string miercoles, string jueves, string viernes, string sabado, string domingo, string fechaCarga)
        {
            if (id == 0)
            {
                throw new Exception("Error en al id , al consultar cartelera en la base de datos");
            }
            else
            {
                Carteleras cartelera = consultarCarteleraPorIdEnLaBdd(id);

                if (idSede != null)
                {
                    cartelera.IdSede = Convert.ToInt32(idSede);
                }
                if (idPelicula != null)
                {
                    cartelera.IdPelicula = Convert.ToInt32(idPelicula);
                }
                if (hora != null)
                {
                    cartelera.HoraInicio = Convert.ToInt32(hora);
                }
                if (fechaInicio != null)
                {
                    cartelera.FechaInicio = Convert.ToDateTime(fechaInicio);
                }
                if (fechaFin != null)
                {
                    cartelera.FechaFin = Convert.ToDateTime(fechaFin);
                }
                if (sala != null)
                {
                    cartelera.NumeroSala = Convert.ToInt32(sala);
                }
                if (idVersion != null)
                {
                    cartelera.IdVersion = Convert.ToInt32(idVersion);
                }
                if (lunes != null)
                {
                    cartelera.Lunes = Convert.ToBoolean(lunes);
                }
                if (martes != null)
                {
                    cartelera.Martes = Convert.ToBoolean(martes);
                }
                if (miercoles != null)
                {
                    cartelera.Miercoles = Convert.ToBoolean(miercoles);
                }
                if (jueves != null)
                {
                    cartelera.Jueves = Convert.ToBoolean(jueves);
                }
                if (viernes != null)
                {
                    cartelera.Viernes = Convert.ToBoolean(viernes);
                }
                if (sabado != null)
                {
                    cartelera.Sabado = Convert.ToBoolean(sabado);
                }
                if (domingo != null)
                {
                    cartelera.Domingo = Convert.ToBoolean(domingo);
                }
                if (fechaCarga != null)
                {
                    cartelera.FechaCarga = Convert.ToDateTime(fechaCarga);
                }
                else
                {
                    throw new Exception("");
                }

                repositorioManager.ctx.Carteleras.Attach(cartelera);
                repositorioManager.ctx.Entry(cartelera).State = EntityState.Modified;
                repositorioManager.ctx.SaveChanges();
            }
        }
        public void eliminarCarteleraDeLaBdd(Carteleras cartelera)
        {
            if (cartelera == null)
            {
                throw new Exception("");
            }
            else
            {
                repositorioManager.ctx.Carteleras.Remove(cartelera);
                repositorioManager.ctx.SaveChanges();
            }
        }

        
    }
}