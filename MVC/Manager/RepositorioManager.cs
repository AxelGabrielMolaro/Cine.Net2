using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC.Entity;
using System.Data.Entity.Core.Metadata.Edm;

namespace MVC.Manager
{

    /*
     * Esta clase lo que hace es simplificar el acceso a las clases de la base de datos generadas
     */
    public class RepositorioManager
    {
        public Calificaciones calificacioneEntity { get; set; }
        public Carteleras carteleraEntity  { get; set; }
        public Sedes sedeEntity { get; set; }
        public Reservas reservaEntity { get; set; }
        public Generos generoEntity { get; set; }
        // public Rrpo reporteEntity { get; set; }
        public Peliculas peliculaEntity { get; set; }
        public TiposDocumentos tipoDocumentoEntity { get; set; }
        public Usuarios usuarioEntity { get; set; } 
        public Versiones versionEntity { get; set; }
        public  TPBDDCINE ctx { get; set; }

        public RepositorioManager()
        {
            ctx = new TPBDDCINE();//es el nombre que le di a la base de datos
            carteleraEntity = new Carteleras();
            calificacioneEntity = new Calificaciones();
            sedeEntity = new Sedes();
            reservaEntity = new Reservas();
            generoEntity = new Generos();
            //reporteEntity = new REPORTES_DE_RESERVAS();
            peliculaEntity = new Peliculas();
            tipoDocumentoEntity = new TiposDocumentos();
            usuarioEntity = new Usuarios();
            versionEntity = new Versiones();
           
           
               
        }
    }
}