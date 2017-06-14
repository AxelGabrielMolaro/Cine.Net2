using MVC.DaoImpl;
using MVC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.ServicesImpl
{
    public class VersionServiceImpl
    {
        VersionDaoImpl versionDao = new VersionDaoImpl();

        public List<Versiones> getListadoDeVersiones()
        {
            try
            {
                List<Versiones> listadoDeVersiones = versionDao.getListadoDeVersiones();

                if (listadoDeVersiones == null || listadoDeVersiones.ToArray().Length == 0)
                {
                    throw new Exception("");
                }
                else
                {
                    return listadoDeVersiones;
                }
            }
            catch (Exception e)
            {
                throw new Exception("");
            }
        }
        public Versiones getVersionPorId(int id)
        {
            Versiones version = versionDao.getVersionPorId(id);
            if (id == 0)
            {
                throw new Exception("Error al buscar version. Esa version no existe");
            }
            if (version == null)
            {
                throw new Exception("Error al buscar version. No existe una version con esa id");
            }
            else
            {
                return version;
            }


        }
    }
}