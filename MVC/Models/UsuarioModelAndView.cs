using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC.Entity;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class UsuarioModelAndView  : Usuarios
    {

        public string idUsuarioModel { get; set; }


        [RegularExpression("^[a-zA-Z0-9\040]+$", ErrorMessage = "Caráctetes no validos")]
        [StringLength(12, ErrorMessage = "El nombre no puede contener más de 12 caracteres")]
        [Required(ErrorMessage = "Ingrese su nombre")]
        public string nombreUsuarioModel { get; set; }

       // [MinLength(8, ErrorMessage = "La contraseña debe tener por lo menos 8 carácteres")]
        [StringLength(20, ErrorMessage = "La contraseña no puede contener más de 20 caracteres")]
        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        public string  contraseñaUsuarioModel { get; set; }

        public UsuarioModelAndView()
        {
            this.idUsuarioModel = IdUsuario.ToString() ;
            this.nombreUsuarioModel = NombreUsuario;
            this.contraseñaUsuarioModel = Password;
        }
    }
}