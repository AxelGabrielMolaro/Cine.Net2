using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Paso2ModelAndView : ReservaModelAndView
    {
        [Required(ErrorMessage = "Seleccione la versión")]
        public string versionPaso1 { get; set; }
    }
}