using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.Util.Utility
{
    public static class General
    {

        public static DateTime? ConvertFormatDateTime(string date)
        {
            DateTime? startDate = null;
            if (!string.IsNullOrWhiteSpace(date))
            {
                startDate = DateTime.ParseExact(date, "dd/MM/yyyy", null);
            }
            else
            {
                startDate = DateTime.Now;
            }

            return startDate;

        }

    }
}
