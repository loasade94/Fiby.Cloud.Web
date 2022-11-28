using Fiby.Cloud.Web.Util.Resource;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace Fiby.Cloud.Web.Client.App_Start.Extensions
{
    public static class Lists
    {
        public static List<SelectListItem> GetListStatus()
        {
            var listaTmp = new List<SelectListItem>();

            listaTmp.Add(new SelectListItem
            {
                Value = "1",
                Text = "Activo"
            });

            listaTmp.Add(new SelectListItem
            {
                Value = "0",
                Text = "Inactivo"
            });

            return listaTmp;
        }

        public static List<SelectListItem> GeneraAnios(int anioInicial = 2007, bool todos = true)
        {
            var años = new List<SelectListItem>();
            for (var i = DateTime.Now.Year; i >= anioInicial; i--)
            {
                años.Add(new SelectListItem
                {
                    Value = i.ToString("00"),
                    Text = i.ToString()
                });
            }
            return años;
        }
        public static List<SelectListItem> GeneraMeses(bool todos = true, bool seleccione = false, string valor = "%")
        {
            var meses = new List<SelectListItem>();
            if (seleccione)
                meses.Add(new SelectListItem { Text = Combos.Seleccione, Value = string.Empty });
            else
            {
                if (todos)
                    meses.Add(new SelectListItem { Value = valor, Text = Combos.Todos });
            }

            for (var i = 1; i <= 12; i++)
            {
                meses.Add(new SelectListItem
                {
                    Value = i.ToString("00"),
                    Text = Convert.ToDateTime(String.Format("01/{0}/2015", i)).ToString(Formatos.NombreMes).ToUpper()
                });
            }
            return meses;
        }
    }
}
