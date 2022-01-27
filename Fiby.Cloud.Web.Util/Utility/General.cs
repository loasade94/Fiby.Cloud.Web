using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

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

        public const string FechaInicial = "01/01/0001";
        public const string FechaInicial2 = "01/01/1900";

        public static string NroDecimales(this decimal? valor, int nroDecimales = 2)
        {
            return Math.Round(Convert.ToDecimal(valor), nroDecimales, MidpointRounding.AwayFromZero).ToString("N2");
        }

        public static string NroDecimales(this float? valor, int nroDecimales = 2)
        {
            return Math.Round(Convert.ToDecimal(valor), nroDecimales, MidpointRounding.AwayFromZero).ToString("N" + nroDecimales);
        }
        public static string NroDecimales(this decimal valor, int nroDecimales = 2)
        {
            return Math.Round(Convert.ToDecimal(valor), nroDecimales, MidpointRounding.AwayFromZero).ToString("N" + nroDecimales);
        }

        public static string ConvierteACadena(this DateTime? valor)
        {
            //if (valor == null) return string.Empty;
            if (valor.ToString().Contains(FechaInicial) || valor.ToString().Contains(FechaInicial2))
                return string.Empty;
            return valor.Value.ToString("dd/MM/yyyy");
        }

        public static string ConvierteACadena(this DateTime valor)
        {
            //if (valor == null) return string.Empty;
            if (valor.ToString().Contains(FechaInicial) || valor.ToString().Contains(FechaInicial2))
                return string.Empty;
            return valor.ToString("dd/MM/yyyy");
        }

        public static string ConvierteACadena(this int? valor)
        {
            var resultado = "0";

            if (valor != null)
                resultado = valor.ToString();

            return resultado;
        }

        public static string ConvierteACadena(this decimal? valor)
        {
            var resultado = "0";

            if (valor != null)
                resultado = valor.ToString();

            return resultado;
        }

        public static string ConvierteACadena(this byte? valor)
        {
            var resultado = "0";

            if (valor != null)
                resultado = valor.ToString();

            return resultado;
        }

        public static string ConvierteACadena(this float? valor)
        {
            var resultado = "0";

            if (valor != null)
                resultado = valor.ToString();

            return resultado;
        }


        public static string GetNameMonth(string mes)
        {
            int numberMonth = 0;
            int.TryParse(mes, out numberMonth);
            string nameMes = string.Empty;
            switch (numberMonth)
            {
                case 1:
                    nameMes = "Enero";
                    break;
                case 2:
                    nameMes = "Febrero";
                    break;
                case 3:
                    nameMes = "Marzo";
                    break;
                case 4:
                    nameMes = "Abril";
                    break;
                case 5:
                    nameMes = "Mayo";
                    break;
                case 6:
                    nameMes = "Junio";
                    break;
                case 7:
                    nameMes = "Julio";
                    break;
                case 8:
                    nameMes = "Agosto";
                    break;
                case 9:
                    nameMes = "Septiembre";
                    break;
                case 10:
                    nameMes = "Octubre";
                    break;
                case 11:
                    nameMes = "Noviembre";
                    break;
                case 12:
                    nameMes = "Diciembre";
                    break;
            }

            return nameMes;

        }

        public static string GetXMLFromString(List<string> listXml, string key)
        {
            string xml = "";
            try
            {
                XmlDocument xmlCoverage = new XmlDocument();
                XmlNode CoverageNode = xmlCoverage.CreateElement("ROOT");
                xmlCoverage.AppendChild(CoverageNode);

                foreach (string item in listXml)
                {
                    XmlNode _CoverageNode = xmlCoverage.CreateElement("doc");
                    CoverageNode.AppendChild(_CoverageNode);

                    string[] arrItem = item.Trim().Split('|');
                    string[] arrKey = key.Trim().Split('|');
                    for (int t = 0; t < arrKey.Length; t++)
                    {
                        XmlAttribute value = xmlCoverage.CreateAttribute(arrKey[t]);
                        value.Value = arrItem[t].Trim();
                        _CoverageNode.Attributes.Append(value);
                    }
                }

                StringWriter sw = new StringWriter();
                XmlTextWriter xw = new XmlTextWriter(sw);
                xmlCoverage.WriteTo(xw);
                xml = sw.ToString();
            }
            catch
            {
                xml = "";
            }
            finally
            {
                if (string.IsNullOrEmpty(xml))
                {
                    xml = "<ROOT></ROOT>";
                }
            }

            return xml;
        }

        public static string GetMonthByNumber(string value)
        {
            string result = string.Empty;
            switch (value)
            {
                case "01":
                    result = "Enero";
                    break;
                case "02":
                    result = "Febrero";
                    break;
                case "03":
                    result = "Marzo";
                    break;
                case "04":
                    result = "Abril";
                    break;
                case "05":
                    result = "Mayo";
                    break;
                case "06":
                    result = "Junio";
                    break;
                case "07":
                    result = "Julio";
                    break;
                case "08":
                    result = "Agosto";
                    break;
                case "09":
                    result = "Setiembre";
                    break;
                case "10":
                    result = "Octubre";
                    break;
                case "11":
                    result = "Noviembre";
                    break;
                case "12":
                    result = "Diciembre";
                    break;
                default:
                    result = "Enero";
                    break;

            }
            return result;
        }

    }
}
