using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MVC.Entity;
using MVC.Manager;

namespace MVC.DaoImpl
{
    public class UsuarioDaoImpl 
    {

        RepositorioManager repositorioManager = new RepositorioManager();
   

        //Le pasas un id y te devuelve un usuario, el primero que encuentre con ese id
        public Usuarios buscarUnUsuarioPorId(int id)
        {
           var usuarioBuscado = repositorioManager.ctx.Usuarios.LastOrDefault(o => o.IdUsuario == id);
            if (usuarioBuscado != null)
            {
                return usuarioBuscado;
            }
            else {
                   
                   throw new Exception ("Error , el usuario que quieres buscar por id no existe");
            }
        }

        //Le pasas un nombre y te devuelve el primer usuario con ese nombre
        public Usuarios buscarUnUsuarioPorNombre(string nombreUsuario)
        {
           
            var usuarioBuscado = repositorioManager.ctx.Usuarios.OrderByDescending(o => o.NombreUsuario == nombreUsuario).FirstOrDefault();

           // var usuarioBuscado = (from u in repositorioManager.ctx.USUARIO select u).FirstOrDefault();

            if(usuarioBuscado != null)
            {
                return usuarioBuscado;
            }
            else {
                throw new Exception("Error, el usuario que quieres buscar por nombre no existe");
            }

        }

    }
}