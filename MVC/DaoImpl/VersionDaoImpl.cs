using MVC.Entity;
using MVC.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.DaoImpl
{
    public class VersionDaoImpl
    {
        RepositorioManager repositorioManager = new RepositorioManager();

        //Consulta la lista de versiones de la base de datos.
        public List<Versiones> getListadoDeVersiones()
        {
            var listadoDeVersiones = (from version in repositorioManager.ctx.Versiones select version);
            List<Versiones> listadoDeVersionesADevolver = listadoDeVersiones.ToList();

            if (listadoDeVersiones != null)
            {
                return listadoDeVersionesADevolver;
            }
            else
            {
                throw new Exception("Error al traer la lista de carteleras de la base de datos");
            }
        }

        public Versiones getVersionPorId(int id)
        {
            Versiones versionBuscada = repositorioManager.ctx.Versiones.OrderByDescending(o => o.IdVersion == id).FirstOrDefault();
            return versionBuscada;
        }
    }
}