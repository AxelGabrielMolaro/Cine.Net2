using MVC.DaoImpl;
using MVC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.ServicesImpl
{
    public class GeneroServiceImpl
    {
        GeneroDaoImpl generoDao = new GeneroDaoImpl();
        //Trae todas los generos, si esta vacio tira exepcion , y si no trae el listado
        public List<Generos> getListadoDeGeneros()
        {
            List<Generos> listado = generoDao.getListadoDeGeneros();
            if (listado == null || listado.ToArray().Length == 0)
            {
                throw new Exception("No hay generos cargadas");
            }
            else
            {
                return listado;
            }
        }

        //trae una sede por id , si el id es 0 tira error, o si la sede que traigo no existe
        public Generos getGeneroPorId(int id)
        {
            Generos genero = generoDao.getGeneroPorId(id);
            if (id == 0)
            {
                throw new Exception("Error al buscar genero. Ese sede no existe");
            }
            if (genero == null)
            {
                throw new Exception("Error al buscar genero. No existe un genero con esa id");
            }
            else
            {
                return genero;
            }


        }
    }
}