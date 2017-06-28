using MVC.Entity;
using System.Collections.Generic;

namespace MVC.Models
{
    public class HomeModelAndView
    {
        public List<Peliculas> listadoDePeliculasHome { get; set; }
        public string idPeliculaHome { get; set; }
        public HomeModelAndView() { }
    }
}