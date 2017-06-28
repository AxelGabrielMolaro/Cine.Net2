using MVC.Entity;
using System.Collections.Generic;

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