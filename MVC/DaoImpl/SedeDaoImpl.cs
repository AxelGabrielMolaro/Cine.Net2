
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC.Entity;
using MVC.Manager;
using System.Data.Entity;

namespace MVC.DaoImpl
{
    public class SedeDaoImpl 
    {
        RepositorioManager repositorioManager = new RepositorioManager();


        //Busca una sede por id y la elimina
        public void eliminarSedeDeLaBddPorId(int id)
        {
            Sedes sedeABorrar = getSedePorId(id);
            repositorioManager.ctx.Sedes.Remove(sedeABorrar);
            repositorioManager.ctx.SaveChanges();
        }
        
        //Trae todas las sedes
        public List<Sedes> getListadoDeSedes()
        {
            var listadoDESedesBdd = from sede in repositorioManager.ctx.Sedes select sede;
            List<Sedes> listado = listadoDESedesBdd.ToList();
            return listado;
        }

        //Obtiene un sede por id
        public Sedes getSedePorId(int id)
        {
            Sedes sedeBuscada = repositorioManager.ctx.Sedes.OrderByDescending(o => o.IdSede == id).FirstOrDefault();
            return sedeBuscada;
        }

        //Graba una sede en la bdd, si la id es la misma la pisa
        public void grabarSedeEnLaBdd(Sedes sedeAGrabar)
        {
            repositorioManager.ctx.Sedes.Add(sedeAGrabar);
            repositorioManager.ctx.SaveChanges();
        }

        //si ningun campo esta vacio modifica un usuario, si no tira excepcion
        public void modificarSedeDeLaBddPorId(int id, string nombre, string direccion, string precioEntradaGeneral)
        {
            if (id == 0)
            {
                throw new Exception("Error en al id , al consultar sede en la base de datos");
            }
            else
            {
                Sedes sede = getSedePorId(id);
               
                if (nombre != null)
                {
                    sede.Nombre = nombre;
                }
                else
                {
                    throw new Exception("Error al modificar usuario en la base de datos, hay campos vacios");
                }

                if (direccion != null )
                {
                    sede.Direccion = direccion;
                }
                else
                {
                    throw new Exception("Error al modificar usuario en la base de datos, hay campos vacios");
                }

                if (precioEntradaGeneral != null )
                {
                    sede.PrecioGeneral = Convert.ToInt32(precioEntradaGeneral);
                }
                else
                {
                    throw new Exception("Error al modificar usuario en la base de datos, hay campos vacios");
                }

                repositorioManager.ctx.Sedes.Attach(sede);
                repositorioManager.ctx.Entry(sede).State = EntityState.Modified;
                repositorioManager.ctx.SaveChanges();
               
       
            }
        }
    }
}