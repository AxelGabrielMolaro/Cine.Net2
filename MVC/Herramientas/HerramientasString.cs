using System;
using System.Text.RegularExpressions;

namespace MVC.Herramientas
{
    public static class HerramientasString
    {
        /// <summary>
        /// Si es nulo vacio devuelve true sino false
        /// 
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static bool esNuloVacio(string texto)
        {
            if (texto == "" || texto == null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool esNumero(string texto)
        {
            try
            {
                int textoNum = Convert.ToInt32(texto);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool documentoEsValido(string tipo, string numero)
        {
            if (tipo == "1")
            {
                if (numero.Length == 8)
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public static bool mailEsValido(string mail)
        {
            bool esValido = Regex.IsMatch(mail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            return esValido;
        }
    }
}