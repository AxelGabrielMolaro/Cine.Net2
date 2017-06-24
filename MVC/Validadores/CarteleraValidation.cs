using MVC.DaoImpl;
using MVC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Validadores
{
    public class CarteleraValidation
    {
        //Implemento métodos de CarteleraDaoImpl 
        CarteleraDaoImpl carteleraDao = new CarteleraDaoImpl();
        public void validarCarteleraIngresada(Carteleras carteleraAGrabar)
        {
            //Si la fecha de fin es anterior a la de inicio 
            if (Convert.ToDateTime(carteleraAGrabar.FechaInicio).CompareTo(Convert.ToDateTime(carteleraAGrabar.FechaFin)) >= 0)
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
                    //1. Que la película y la versión sean iguales también, entonces no permite agregar. 
                    if (carteleraAGrabar.IdPelicula == c.IdPelicula && carteleraAGrabar.IdVersion == c.IdVersion)
                    {
                        throw new Exception("Esa película con esa versión ya existe");
                    }

                    //2. Que el número de sala ingresado también sea igual al cargado en la sede existente.
                    if (carteleraAGrabar.NumeroSala == c.NumeroSala)
                    {
                        /*2. 
                         *      A. Si también coinciden las fechas de inicio y de fin, o las fechas ingresadas están dentro 
                         *      de ese período de fechas: 
                         */
                        if (carteleraAGrabar.FechaInicio >= c.FechaInicio && carteleraAGrabar.FechaInicio <= c.FechaFin)
                        {
                            //Se comparan los días para saber cuáles están disponibles: 
                            if (carteleraAGrabar.Lunes == true && c.Lunes == true)
                            {
                                throw new Exception("Está seleccionando días ocupados");
                            }
                            if (carteleraAGrabar.Martes == true && c.Martes == true)
                            {
                                throw new Exception("Está seleccionando días ocupados");
                            }
                            if (carteleraAGrabar.Miercoles == true && c.Miercoles == true)
                            {
                                throw new Exception("Está seleccionando días ocupados");
                            }
                            if (carteleraAGrabar.Jueves == true && c.Jueves == true)
                            {
                                throw new Exception("Está seleccionando días ocupados");
                            }
                            if (carteleraAGrabar.Viernes == true && c.Viernes == true)
                            {
                                throw new Exception("Está seleccionando días ocupados");
                            }
                            if (carteleraAGrabar.Sabado == true && c.Sabado == true)
                            {
                                throw new Exception("Está seleccionando días ocupados");
                            }
                            if (carteleraAGrabar.Domingo == true && c.Domingo == true)
                            {
                                throw new Exception("Está seleccionando días ocupados");
                            }
                        }
                    }
                }
            }

            //Si no cumple con ninguna de las condiciones, llama al método que graba en la base de datos. 
            carteleraDao.grabarCarteleraEnLaBaseDeDatos(carteleraAGrabar);
        }
    }
}