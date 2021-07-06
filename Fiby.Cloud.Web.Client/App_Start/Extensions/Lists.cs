using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
