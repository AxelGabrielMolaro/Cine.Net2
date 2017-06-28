using System;

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

        public static string pasarUnMesAStringddmm(string diap, string mesp)
        {
            string dia = diap;
            string mes = mesp;
            if (Convert.ToInt32(dia) <= 9)
            {
                dia = "0" + dia;
            }

            if (Convert.ToInt32(mes) <= 9)
            {
                mes = "0" + mes;
            }

            string diaMes = (dia + mes);

            return diaMes;
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