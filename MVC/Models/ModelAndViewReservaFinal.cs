using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    //seguro muchas cosas cambiamos por Long o int por laid
    public class ModelAndViewReservaFinal
    {
        [RegularExpression("^[a-zA-Z0-9\040]+$", ErrorMessage = "Caráctetes no validos")]
        [StringLength(12, ErrorMessage = "El nombre no puede contener más de 12 caracteres")]
        [Required(ErrorMessage = "El nombre es requierido para continual")]
        public string nombre { get; set; }


        [Required(ErrorMessage = "El tipo de documento es requerido para continuar")]
        public string tipoDocumento{ get; set; }


        [MinLength(8,ErrorMessage ="El numero de documento debe tener al menos 8 carácteres")]
        [StringLength(9, ErrorMessage = "El nombre no puede contener más de 12 caracteres")]
        [Required(ErrorMessage = "La sede es requerida para continuar")]
        public string  numeroDocumento { get; set; }

        
        [Required(ErrorMessage = "La cantidad de entradas es requerida para continuar")]
        public string cantidadDeEntradas { get; set; }


       

    }
}