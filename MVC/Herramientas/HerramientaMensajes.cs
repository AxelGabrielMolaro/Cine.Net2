using System.Collections.Generic;

namespace MVC.Herramientas
{
    public class HerramientaMensajes
    {
        public List<string> listaDeMensajes { get; set; }

        public HerramientaMensajes()
        {
            listaDeMensajes = new List<string>();
        }
        public void addMensaje(string mensaje)
        {
            listaDeMensajes.Add(mensaje);
        }
    }
}