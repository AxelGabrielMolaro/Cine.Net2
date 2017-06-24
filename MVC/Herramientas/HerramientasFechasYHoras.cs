using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Herramientas
{
    public class HerramientasFechasYHoras
    {
        public static string pasarUnTimeSpanAHHMMString(TimeSpan timeSpan)
        {
            string horatimeSpan = null;
            string minutostimeSpan = null;
            if (timeSpan.Hours <= 9)
            {
                horatimeSpan = timeSpan.Hours.ToString() + "0";
            }
            else
            {
                horatimeSpan = timeSpan.Hours.ToString();
            }

            if (timeSpan.Minutes <= 9)
            {
                minutostimeSpan = "0" + timeSpan.Minutes.ToString();
            }
            else
            {
                minutostimeSpan = timeSpan.Minutes.ToString();
            }

            string timeSpanStringFinal = (horatimeSpan + " : " + minutostimeSpan);

            return timeSpanStringFinal;
        }

        /// <summary>
        /// Traduce el dia de la semana en ingles al español
        /// </summary>
        /// <param name="diaDeSemna"></param>
        /// <returns></returns>
        public static string traducirDiaDeSemana(string diaDeSemna)
        {
            string dia = null;
            if (diaDeSemna == "Monday") dia = "Lunes";
            if (diaDeSemna == "Tuesday") dia = "Martes";
            if (diaDeSemna == "Wednesday") dia = "Miercoles";
            if (diaDeSemna == "Thursday") dia = "Jueves";
            if (diaDeSemna == "Friday") dia = "Viernes";
            if (diaDeSemna == "Saturday") dia = "Sabado";
            if (diaDeSemna == "Sunday") dia = "Domingo";
            return dia;
        }
   }
}