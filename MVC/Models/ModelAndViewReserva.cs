using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    //seguro muchas cosas cambiamos por Long o int por laid
    public class ModelAndViewReserva
    {
        [Required(ErrorMessage ="La versión es requerida para continuar")]
        public String version { get; set; }

        [Required(ErrorMessage = "La sede es requerida para continuar")]
        public String  sede { get; set; }

        [Required(ErrorMessage = "El dia es requerido para continuar")]
        public String dia { get; set; }

        [Required(ErrorMessage = "El horario es requerida para continuar")]
        public String horario { get; set; }

    }
}