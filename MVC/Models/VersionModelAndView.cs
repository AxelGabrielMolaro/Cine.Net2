using MVC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class VersionModelAndView : Versiones
    { 
        public string idVersionModel { get; set; } 
        public string nombreVersionModel { get; set; } 

        public List<Versiones> listadoDeVersiones { get; set; } 

        public VersionModelAndView()
        {
            idVersionModel = IdVersion.ToString();
            nombreVersionModel = Nombre;
        }
    }
}