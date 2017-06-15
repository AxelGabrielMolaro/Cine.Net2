using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC.Entity;
using System.ComponentModel.DataAnnotations;
using MVC.DaoImpl;

namespace MVC.Models
{
    //Esta clase la uso mas para la parte visual, tipar las vistas, y manejar 
    //pasaje de parametros
    public class SedeModelAndView : Sedes 
    {

        public string idSedeModel { get; set; }
        //Le comente los required porque me explota algo, igual lo valide en Backend
        //[Required (ErrorMessage ="Por favor ingrese el nombre de la sede")]
        [MaxLength(30, ErrorMessage = "El nombre de la sede no puede ser mayor a 30 caracteres")]

        public string nombreSedeModel { get; set; }

        //[Required(ErrorMessage = "Por favor ingrese la dirección de la sede")]
        [MaxLength(30, ErrorMessage = "La dirección de la sede no puede ser mayor a 30 caracteres")]
        public string direccionSedeModel { get; set; }

        //[Required(ErrorMessage = "Por favor ingrese el precio de la sede")]
        [RegularExpression("^\\d+$", ErrorMessage = "Son solo validos numeros enteros en este campo")]
        [MaxLength(30, ErrorMessage = "El precio de la sede no puede ser mayor a 30 caracteres")]
        public string precioEntradaGeneralModel { get; set; }

        //Lo uso para listar sedes

        public List<Sedes> listadoDeSedesModel { get; set; }

        //aca creo un constructor donde seteo los valores de la clase de la bdd a las propiedades
        //de mi modelAndView
        public SedeModelAndView()
        {
            this.idSedeModel = IdSede.ToString();
            this.nombreSedeModel = Nombre;
            this.direccionSedeModel = Direccion;
            this.precioEntradaGeneralModel = PrecioGeneral.ToString();
        }

     
    }
}