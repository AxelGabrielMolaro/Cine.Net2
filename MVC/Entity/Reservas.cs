//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reservas
    {
        public int IdReserva { get; set; }
        public int IdSede { get; set; }
        public int IdVersion { get; set; }
        public int IdPelicula { get; set; }
        public System.DateTime FechaHoraInicio { get; set; }
        public string Email { get; set; }
        public int IdTipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public int CantidadEntradas { get; set; }
        public System.DateTime FechaCarga { get; set; }
    
        public virtual Peliculas Peliculas { get; set; }
        public virtual Sedes Sedes { get; set; }
        public virtual TiposDocumentos TiposDocumentos { get; set; }
        public virtual Versiones Versiones { get; set; }
    }
}
