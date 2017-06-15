using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static System.Collections.Specialized.BitVector32;

namespace MVC.SessionManger
{
    //va a manejar las variasbles de session
    public class SessionManager
    {
        public string urlAntetior { get; set; }
        public string  usuario { get; set; }

        public SessionManager() { }


        public void SetearVariablesSesion(string url,  string usuario)
        {
            this.urlAntetior = url;
            this.usuario = usuario;
        }

        public int tienePermisos(string nombreUsuario) 
        {
            if (nombreUsuario == null)
            {
                return 0;
            }
            else {
                return 1;
            }


        }
    }
}