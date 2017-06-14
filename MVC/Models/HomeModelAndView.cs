using MVC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class HomeModelAndView
    {
        public List<Peliculas> listadoDePeliculasHome { get; set; }
        public string idPeliculaHome { get; set; }
        public HomeModelAndView() { }
        
    }
}