using MVC.DaoImpl;
using MVC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Validadores
{ 

    //ESTO HAY QUE REVISARLO BIEN!

    public class CarteleraValidation
    {
        //Implemento métodos de CarteleraDaoImpl 
        CarteleraDaoImpl carteleraDao = new CarteleraDaoImpl();
        public void validarCarteleraIngresada(Carteleras carteleraAGrabar)
        {
            /* 
             * El método CompareTo() compara dos fechas. 
             * Si devuelve 0 significa que las fechas son iguales, 
             * si devuelve mayor a 0 significa que la fecha a comparar es mayor, 
             * si devuelve menor a 0 significa que la fecha a comparar es menor. 
            */

            //Si la fecha de inicio o fin son anteriores a la actual (es decir, ya pasaron), no permite agregar. 
            if (carteleraAGrabar.FechaInicio.CompareTo(DateTime.Now) < 0
                || carteleraAGrabar.FechaFin.CompareTo(DateTime.Now) < 0)
            {
                throw new Exception("Las fechas de inicio o de fin ya expiraron");
            }

            //Si la fecha fin es anterior a la de inicio 
            if (carteleraAGrabar.FechaInicio.CompareTo(carteleraAGrabar.FechaFin) >= 0)
            {
                throw new Exception("La fecha de fin debe ser posterior a la fecha de inicio");
            }

            //Traigo la lista de carteleras de la base de datos para recorrerla y comparar: 
            List<Carteleras> listaParaValidar = carteleraDao.getListadoDeCarteleras();
            foreach (var c in listaParaValidar)
            {
                //Si la sede elegida es igual a una ya existente en una cartelera pueden pasar 2 cosas: 
                if (carteleraAGrabar.IdSede == c.IdSede)
                {
                    //Que la película y la versión sean iguales también, entonces no se puede agregar. 
                    if (carteleraAGrabar.IdPelicula == c.IdPelicula && carteleraAGrabar.IdVersion == c.IdVersion)
                    {
                        throw new Exception("Esa película con esa versión ya existe");
                    }

                    //O que el número de sala sea igual al cargado en otra sede 
                    if (carteleraAGrabar.NumeroSala == c.NumeroSala)
                    {
                        //y también coincidan las fechas de inicio y de fin 
                        //(en este if se compara si está dentro de ese período de fechas)
                        if (carteleraAGrabar.FechaInicio >= c.FechaInicio && carteleraAGrabar.FechaInicio <= c.FechaFin)
                        {
                            throw new Exception("Período de fechas ocupado");
                        }

                        /*
                         * REVISAR: Días y horarios en una misma fecha. 
                         * Por ejemplo: Se podría proyectar en la misma sede, misma sede, mismo período de fechas
                         * PERO días diferentes.
                         */
                    }
                }
            }

            //Si está todo OK, llama al método que graba en la bd. 
            carteleraDao.grabarCarteleraEnLaBaseDeDatos(carteleraAGrabar);
        }
    }
}