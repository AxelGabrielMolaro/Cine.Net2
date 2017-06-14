using MVC.DaoImpl;
using MVC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.ServicesImpl
{
    /// <summary>
    /// Agregar una reserva y valida los datos, tira excepciones
    /// </summary>
    public class ReservaServiceImpl
    {
        ReservaDaoImpl reservaDao = new ReservaDaoImpl();
        VersionServiceImpl versionServive = new VersionServiceImpl();
        sedeServiceImpl  sedeServive = new sedeServiceImpl();
        CarteleraDaoImpl carteleraDao = new CarteleraDaoImpl();
        public void agregarReserva(Reservas reserva)
        {
            //validar
            reservaDao.agregarReserva(reserva);
        }

        //para el formulario de reservaç


        //borrar esto
        /// <summary>
        /// Este metodo en la lista de carteleras por id de pelicula trae
        /// la cantida de posible idiomas que va a  haber para seguir filtrando
        /// </summary>
        /// <param name="idDePelicula"></param>
        /// <returns></returns>
        
        public List<Versiones> getListadosDeVersionesParaReserva(int idDePelicula)
        {
            List<Versiones> listado = new List<Versiones>();
            List<Carteleras> listadoDeCarteleras = reservaDao.getListadosDeCartelerasParaReserva(idDePelicula,0,0);

            foreach (var cartelera in listadoDeCarteleras)
            {
                Versiones versionCartelera = versionServive.getVersionPorId(cartelera.IdVersion);
                foreach(var version in versionServive.getListadoDeVersiones())
                {
                    if (!listado.Contains(version))
                    {
                        listado.Add(version);
                    }
                }
                
            }
            return listado; 

        }

        public List<Sedes> getListadoDeSedesParaReserva(int idPelicula, int idVersion)
        {
            List<Sedes> listado = new List<Sedes>();
            List<Carteleras> listadoDeCarteleras = reservaDao.getListadosDeCartelerasParaReserva(idPelicula, idVersion, 0);

            foreach (var cartelera in listadoDeCarteleras)
            {
                Sedes sedeCartelera = sedeServive.getSedePorId(cartelera.IdSede);
                foreach (var sede in sedeServive.getListadoDeSedes())
                {
                    if (!listado.Contains(sedeCartelera))
                    {
                        listado.Add(sedeCartelera);
                    }
                }

            }
            return listado;
        }

        /// <summary>
        /// Este metood es el ultimo paso del formulario que trae una lisata
        /// de string de dias disponibles segun la cartelera
        /// </summary>
        /// <param name="idPelicula"></param>
        /// <param name="idVersion"></param>
        /// <param name="idSede"></param>
        /// <returns></returns>
       
        public List<string> getListadoDeDiasParaReserva(int idPelicula, int idVersion, int idSede)
        {
            List<string> listado = new List<string>();
            List<Carteleras> listadoDeCarteleras = reservaDao.getListadosDeCartelerasParaReserva(idPelicula, idVersion, idSede);
            string lunes = "Lunes";
            string martes = "Martes";
            string miercoles = "Miercoles";
            string jueves = "Jueves";
            string viernes = "Viernes";
            string sabado = "Sabado";
            string domingo = "Domingo";

            foreach (var cartelera in listadoDeCarteleras)
            {
                bool lunesBool = cartelera.Lunes;
                bool martesBool = cartelera.Martes;
                bool miercolesBool = cartelera.Miercoles;
                bool juevesBool = cartelera.Jueves;
                bool viernesBool = cartelera.Viernes;
                bool sabadoBool = cartelera.Sabado;
                bool domingoBool = cartelera.Domingo;

                if (lunesBool == true)
                {
                    if (!listado.Contains(lunes))
                    {
                        listado.Add(lunes);
                    }
                }
                if (martesBool == true)
                {
                    if (!listado.Contains(martes))
                    {
                        listado.Add(martes);
                    }
                }
                if (miercolesBool == true)
                {
                    if (!listado.Contains(miercoles))
                    {
                        listado.Add(miercoles);
                    }
                }

                if (juevesBool == true)
                {
                    if (!listado.Contains(jueves))
                    {
                        listado.Add(jueves);
                    }
                }

                if (viernesBool == true)
                {
                    if (!listado.Contains(viernes))
                    {
                        listado.Add(viernes);
                    }
                }

                if (sabadoBool == true)
                {
                    if (!listado.Contains(sabado))
                    {
                        listado.Add(sabado);
                    }
                }

                if (domingoBool == true)
                {
                    if (!listado.Contains(domingo))
                    {
                        listado.Add(domingo);
                    }
                }

               
            }
            return listado;
        }
        /// <summary>
        /// Devuelve una cartelera unica con esos datos
        /// </summary>
        /// <param name="idPeliculap"></param>
        /// <param name="idSedep"></param>
        /// <param name="idVersionp"></param>
        /// <returns></returns>
        public Carteleras getCarteleraReserva(int idPeliculap, int idSedep, int idVersionp)
        {
            List<Carteleras> listado = carteleraDao.getListadoDeCarteleras();
            foreach (var cartelera in listado)
            {
                if (cartelera.IdPelicula == idPeliculap && cartelera.IdSede == idSedep && cartelera.IdVersion == idVersionp)
                {
                    return cartelera;
                }
            }
            return null;
        }

        public List<Reservas> getListadoDeReservas()
        {
            return reservaDao.getListadoDeReservas();
         }

    }




}