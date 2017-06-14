
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC.Entity;
using MVC.DaoImpl;

namespace MVC.ServicesImpl
{
    public class UsuarioServiceImpl 
    {

        UsuarioDaoImpl usuarioDao = new UsuarioDaoImpl();
            
        //Le pasas un nombre , lo va a buscar en la bdd ,y va a ver si coinciden las contraseñas
        //Si counciden devuelve un usuario , si no counciden tira una excepcion
        public Usuarios login(string nombre, string contraseña)
        {
            Usuarios usuarioBuscado = usuarioDao.buscarUnUsuarioPorNombre(nombre);
            try
            {
                if (usuarioBuscado.NombreUsuario == nombre && usuarioBuscado.Password == contraseña)
                {
                    return usuarioBuscado;
                }
                else {
                    throw new Exception("Error al ingresar, verifique su nombre y su contraseña");
                }
               
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
    }
}