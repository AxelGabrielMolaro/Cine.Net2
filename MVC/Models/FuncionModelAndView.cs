using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class FuncionModelAndView
    {
        public string numeroFuncionModel { get; set; }
        public string horaFuncionModel { get; set; }

        public FuncionModelAndView(string numeroFuncionModelp, string horaFuncionModelp)
        {
            this.numeroFuncionModel = numeroFuncionModelp;
            this.horaFuncionModel = horaFuncionModelp;
        }
    }
}