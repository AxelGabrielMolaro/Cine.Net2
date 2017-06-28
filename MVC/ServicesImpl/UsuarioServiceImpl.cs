using System;
using MVC.Entity;
using MVC.DaoImpl;

namespace MVC.ServicesImpl
{
    public class UsuarioServiceImpl
    {
        UsuarioDaoImpl usuarioDao = new UsuarioDaoImpl();

        //Le pasas un nombre , lo va a buscar en la bdd ,y va a ver si coinciden las contraseñas
        //Si coinciden devuelve un usuario , si no coinciden tira una excepcion
        public Usuarios login(string nombre, string contraseña)
        {
            Usuarios usuarioBuscado = usuarioDao.buscarUnUsuarioPorNombre(nombre);
            try
            {
                if (usuarioBuscado.NombreUsuario == nombre && usuarioBuscado.Password == contraseña)
                {
                    return usuarioBuscado;
                }
                else
                {
                    throw new Exception("Error al ingresar, verifique su nombre y su contraseña");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}