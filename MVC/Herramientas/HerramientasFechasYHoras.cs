using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Herramientas
{
    public  class HerramientasFechasYHoras
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
                minutostimeSpan = timeSpan.Minutes.ToString() + "0";
            }
            else
            {
                minutostimeSpan = timeSpan.Minutes.ToString();
            }

            string timeSpanStringFinal = (horatimeSpan + " : " + minutostimeSpan);

            return timeSpanStringFinal;
        } 
   }
}