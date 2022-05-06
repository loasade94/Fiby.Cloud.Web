using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Fiby.Cloud.Web.Util.Utility
{
    public class DataUtility
    {
        public static Int64 ObjectToInt64(object obj)
        {
            return ((obj == null) || (obj == DBNull.Value)) ? 0 : Convert.ToInt64(obj);
        }

        public static Int16 ObjectToInt16(object obj)
        {
            return ((obj == null) || (obj == DBNull.Value)) ? Int16.MinValue : Convert.ToInt16(obj);
        }

        //public static Int16 ObjectToInt16(IDataReader reader, string columnName)
        //{
        //    return ObjectToInt16(GetReaderValue(reader, columnName));
        //}

        public static Int32 ObjectToInt32(object obj)
        {
            return ((obj == null) || (obj == DBNull.Value) || string.IsNullOrEmpty(ObjectToString(obj))) ? 0 : Convert.ToInt32(obj);
        }

        //public static Int32? ObjectToInt32Null(object obj)
        //{
        //    Int32? valor = null;
        //    if ((obj == null) || (obj == DBNull.Value))
        //    { return valor; }
        //    else
        //    { valor = Convert.ToInt32(obj); }
        //    return valor;
        //}

        //public static Int32 ObjectToInt32(IDataReader reader, string columnName)
        //{
        //    return ObjectToInt32(GetReaderValue(reader, columnName));
        //}

        public static decimal ObjectToDecimal(object obj)
        {
            return ((obj == null) || (obj == DBNull.Value)) ? 0.00M : Convert.ToDecimal(obj);
        }

        //public static Decimal? ObjectToDecimalNull(object obj)
        //{
        //    Decimal? valor = null;
        //    if ((obj == null) || (obj == DBNull.Value))
        //    { return valor; }
        //    else
        //    { valor = Convert.ToDecimal(obj); }
        //    return valor;
        //}

        //public static decimal ObjectToDecimal(IDataReader reader, string columnName)
        //{
        //    return ObjectToDecimal(GetReaderValue(reader, columnName));
        //}

        //public static decimal StrToDecimal(string obj)
        //{
        //    return ((obj == null) || (obj == "")) ? 0.00M : Convert.ToDecimal(obj);
        //}

        //public static decimal StrToDecimal(IDataReader reader, string columnName)
        //{
        //    return StrToDecimal(ObjectToString(GetReaderValue(reader, columnName)));
        //}

        public static int ObjectToInt(object obj)
        {
            return ObjectToInt32(obj);
        }

        //public static int ObjectToInt(IDataReader reader, string columnName)
        //{
        //    return ObjectToInt(GetReaderValue(reader, columnName));
        //}

        public static double ObjectToDouble(object obj)
        {
            return ((obj == null) || (obj == DBNull.Value)) ? 0 : Convert.ToDouble(obj);
        }

        public static float ObjectToFloat(object obj)
        {
            return ((obj == null) || (obj == DBNull.Value)) ? 0 : float.Parse(obj.ToString());
        }

        //public static double ObjectToDouble(IDataReader reader, string columnName)
        //{
        //    return ObjectToDouble(GetReaderValue(reader, columnName));
        //}

        public static bool ObjectToBool(object obj)
        {
            obj = obj == null || obj.ToString() == "0" ? false : (obj.ToString() == "1" ? true : obj);
            return ((obj == null) || (obj == DBNull.Value)) ? false : Convert.ToBoolean(obj);
        }

        //public static bool ObjectToBool(IDataReader reader, string columnName)
        //{
        //    return ObjectToBool(GetReaderValue(reader, columnName));
        //}

        public static string ObjectToString(object obj)
        {
            return ((obj == null) || (obj == DBNull.Value)) ? "" : Convert.ToString(obj).Trim();
        }

        //public static string DateTimeNullToShortDateString(DateTime? obj)
        //{
        //    return ((obj == null)) ? "" : obj.Value.ToShortDateString();
        //}

        //public static string DateTimeNullToDateTimeString(DateTime? obj)
        //{
        //    return ((obj == null || obj.Value == null)) ? string.Empty : obj.Value.ToString("dd/MM/yyyy HH:mm");
        //}
        public static string DateTimeNullToDateTimeStringFormato(DateTime? obj)
        {
            return ((obj == null || obj.Value == null)) ? string.Empty : obj.Value.ToString("dd/MM/yyyy");
        }
        public static string DateTimeNullToDateTimeStringFormatoXml(DateTime? obj)
        {
            return ((obj == null || obj.Value == null)) ? string.Empty : obj.Value.ToString("yyyyMMdd");
        }
        //public static string DateTimeNullToShortTimeString(DateTime? obj)
        //{
        //    return ((obj == null)) ? "" : obj.Value.ToString("HH:mm");
        //}

        public static string SinTilde(string inputString)
        {
            Regex replace_a_Accents = new Regex("[á|à|ä|â]", RegexOptions.Compiled);
            Regex replace_a_AccentsA = new Regex("[Á]", RegexOptions.Compiled);
            Regex replace_e_Accents = new Regex("[é|è|ë|ê]", RegexOptions.Compiled);
            Regex replace_e_AccentsE = new Regex("[É]", RegexOptions.Compiled);
            Regex replace_i_Accents = new Regex("[í|ì|ï|î]", RegexOptions.Compiled);
            Regex replace_i_AccentsI = new Regex("[Í]", RegexOptions.Compiled);
            Regex replace_o_Accents = new Regex("[ó|ò|ö|ô]", RegexOptions.Compiled);
            Regex replace_o_AccentsO = new Regex("[Ó]", RegexOptions.Compiled);
            Regex replace_u_Accents = new Regex("[ú|ù|ü|û]", RegexOptions.Compiled);
            Regex replace_u_AccentsU = new Regex("[Ú]", RegexOptions.Compiled);

            inputString = replace_a_Accents.Replace(inputString, "a");
            inputString = replace_a_AccentsA.Replace(inputString, "A");
            inputString = replace_e_Accents.Replace(inputString, "e");
            inputString = replace_e_AccentsE.Replace(inputString, "E");
            inputString = replace_i_Accents.Replace(inputString, "i");
            inputString = replace_i_AccentsI.Replace(inputString, "I");
            inputString = replace_o_Accents.Replace(inputString, "o");
            inputString = replace_o_AccentsO.Replace(inputString, "O");
            inputString = replace_u_Accents.Replace(inputString, "u");
            inputString = replace_u_AccentsU.Replace(inputString, "U");

            return inputString;
            //xml.Replace('','');
            //string textoNormalizado = xml.Normalize(NormalizationForm.FormD);
            //Regex reg = new Regex("[^a-zA-Z0-9]");
            //return reg.Replace(textoNormalizado, "");
        }

        public static DateTime? ObjectToDateTime(object obj)
        {
            IFormatProvider culture = new CultureInfo("es-PE", true);
            return ((obj == null) || (obj == DBNull.Value)) ? DateTime.MinValue : Convert.ToDateTime(obj, culture);
        }

        public static DateTime? ObjectToDateTimeNull(object obj)
        {
            IFormatProvider culture = new CultureInfo("es-PE", true);
            DateTime? value = null;
            if ((obj == null) || (obj == DBNull.Value))
            { return value; }
            else
            { value = Convert.ToDateTime(obj, culture); }
            return value;
        }

        //public static DateTime ObjectToDateTime(IDataReader reader, string columnName)
        //{
        //    return ObjectToDateTime(GetReaderValue(reader, columnName));
        //}

        //public static DateTime StringToDateTime(string str)
        //{
        //    return ((str == "")) ? DateTime.MinValue : Convert.ToDateTime(str);
        //}

        //public static DateTime StringToDateTime(IDataReader reader, string columnName)
        //{
        //    return StringToDateTime(ObjectToString(GetReaderValue(reader, columnName)));
        //}

        public static byte ObjectToByte(object obj)
        {
            return ((obj == null) || (obj == DBNull.Value)) ? byte.MinValue : Convert.ToByte(obj);
        }

        //public static byte ObjectToByte(IDataReader reader, string columnName)
        //{
        //    return ObjectToByte(GetReaderValue(reader, columnName));
        //}

        //public static int StringToInt(string str)
        //{
        //    return ((str == null) || (str == "")) ? 0 : Convert.ToInt32(str);
        //}

        //public static int StringToInt(IDataReader reader, string columnName)
        //{
        //    return StringToInt(ObjectToString(GetReaderValue(reader, columnName)));
        //}

        //public static object IntToDBNull(int int1)
        //{
        //    return ((int1 == 0)) ? DBNull.Value : (object)int1;
        //}

        //public static object IntToDBNull(IDataReader reader, string columnName)
        //{
        //    return IntToDBNull(ObjectToInt(GetReaderValue(reader, columnName)));
        //}

        //public static object Int32ToDBNull(Int32 int1)
        //{
        //    return ((int1 == 0)) ? DBNull.Value : (object)int1;
        //}

        //public static object Int32ToDBNull(IDataReader reader, string columnName)
        //{
        //    return Int32ToDBNull(ObjectToInt32(GetReaderValue(reader, columnName)));
        //}

        //public static object Int64ToDBNull(Int64 int1)
        //{
        //    return ((int1 == 0)) ? DBNull.Value : (object)int1;
        //}

        //public static object Int64ToDBNull(IDataReader reader, string columnName)
        //{
        //    return Int64ToDBNull(ObjectToInt64(GetReaderValue(reader, columnName)));
        //}

        //public static object DateTimeToDBNull(DateTime date)
        //{
        //    return ((date == DateTime.MinValue)) ? DBNull.Value : (object)date;
        //}

        //public static object DateTimeToDBNull(IDataReader reader, string columnName)
        //{
        //    return DateTimeToDBNull(ObjectToDateTime(GetReaderValue(reader, columnName)));
        //}

        //public static string ObjectDecimalToStringFormatMiles(object obj)
        //{
        //    return ObjectToDecimal(obj).ToString("#,#0.00");
        //}

        //public static string ObjectDecimalToStringFormatMiles(IDataReader reader, string columnName)
        //{
        //    return ObjectDecimalToStringFormatMiles(GetReaderValue(reader, columnName));
        //}

        //public static string BoolToString(bool flag)
        //{
        //    return flag ? "1" : "0";
        //}

        //public static string BoolToString(IDataReader reader, string columnName)
        //{
        //    return BoolToString(ObjectToBool(GetReaderValue(reader, columnName)));
        //}

        //public static bool StringToBool(string flag)
        //{
        //    return flag.Equals("1");
        //}

        //public static bool StringToBool(IDataReader reader, string columnName)
        //{
        //    return StringToBool(ObjectToString(GetReaderValue(reader, columnName)));
        //}

        //public static string IntToString(int int1)
        //{
        //    return ((int1 == 0)) ? "" : Convert.ToString(int1);
        //}

        //public static string IntToString(IDataReader reader, string columnName)
        //{
        //    return IntToString(ObjectToInt(GetReaderValue(reader, columnName)));
        //}

        //public string GenerarXML(string[] array, bool cabecera)
        //{
        //    string retorna;
        //    MemoryStream memory_stream = new MemoryStream();
        //    XmlTextWriter xml_text_writer = new XmlTextWriter(memory_stream, System.Text.Encoding.UTF8);
        //    xml_text_writer.Formatting = System.Xml.Formatting.Indented;
        //    xml_text_writer.Indentation = 4;
        //    GeneraCabecera(xml_text_writer, cabecera, 'A');
        //    xml_text_writer.WriteStartElement("string-array");
        //    for (int i = 0; i < array.Length; i++)
        //    {
        //        if (array[i] == null) xml_text_writer.WriteElementString("null", "");
        //        else xml_text_writer.WriteElementString("string", array[i]);
        //    }

        //    xml_text_writer.WriteEndElement();
        //    GeneraCabecera(xml_text_writer, cabecera, 'C');
        //    xml_text_writer.Flush();
        //    // Declaramos un StreamReader para mostrar el resultado.
        //    StreamReader stream_reader = new StreamReader(memory_stream);
        //    memory_stream.Seek(0, SeekOrigin.Begin);
        //    retorna = stream_reader.ReadToEnd();
        //    xml_text_writer.Close();
        //    stream_reader.Close();
        //    stream_reader.Dispose();
        //    return retorna;
        //}

        //private void GeneraCabecera(XmlTextWriter xmlTextWriter, bool genera, char estado)
        //{
        //    if (genera)
        //    {
        //        switch (estado)
        //        {
        //            case 'A':
        //                xmlTextWriter.WriteStartDocument();
        //                break;
        //            case 'C':
        //                xmlTextWriter.WriteEndDocument();
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}

        //public static DateTime StringddmmyyyyToDate(string strDate)
        //{
        //    //dd/mm/yyyy
        //    int year = ObjectToInt(strDate.Split('/')[2]);
        //    int month = ObjectToInt(strDate.Split('/')[1]);
        //    int day = ObjectToInt(strDate.Split('/')[0]);
        //    return new DateTime(year, month, day);
        //}

        //public static string BytesToString(byte[] byt)
        //{
        //    return System.Text.Encoding.Default.GetString(byt);
        //}

        //public static string ObjectToXML(Object obj)
        //{
        //    XmlSerializer xs = new XmlSerializer(obj.GetType());
        //    string xml = string.Empty;
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        xs.Serialize(ms, obj);
        //        using (StreamReader sr = new StreamReader(ms))
        //        {
        //            ms.Seek(0, SeekOrigin.Begin);
        //            xml = sr.ReadToEnd();
        //        }
        //    }
        //    return xml;
        //}

        //public static string ObjectToXML2(Object obj)
        //{
        //    return obj.ToString(); ;
        //}

        //public static string StringToSlug(string texto)
        //{
        //    string str = texto;
        //    str = str.Normalize(System.Text.NormalizationForm.FormD);
        //    str = new Regex(@"[^a-zA-Z0-9 ]").Replace(str, "").Trim();
        //    str = new Regex(@"[\/_| -]+").Replace(str, "-");
        //    return str;
        //}

        //public static string RemoveDiacritics(string stIn)
        //{
        //    string stFormD = stIn.Normalize(NormalizationForm.FormD);
        //    StringBuilder sb = new StringBuilder();
        //    for (int ich = 0; ich <= stFormD.Length - 1; ich++)
        //    {
        //        UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
        //        if (uc != UnicodeCategory.NonSpacingMark || stFormD[ich].ToString() == "̃")
        //        {
        //            sb.Append(stFormD[ich]);
        //        }
        //    }

        //    return (sb.ToString().Normalize(NormalizationForm.FormC));
        //}

        //public static string GetHoraFromDate(DateTime? date)
        //{
        //    string hora = string.Empty;

        //    try
        //    {
        //        if (date != null)
        //        {
        //            string time = date.ToString().Substring(11, 13);
        //            string[] arrTime = time.Trim().Split(':');
        //            if (time.Trim().IndexOf('a') > -1) hora = time.Substring(0, 5);
        //            else
        //            {
        //                if (Convert.ToInt32(arrTime[0]) == 12) hora = string.Format("{0}:{1}", (Convert.ToInt32(arrTime[0])).ToString(), arrTime[1]);
        //                else hora = string.Format("{0}:{1}", (Convert.ToInt32(arrTime[0]) + 12).ToString(), arrTime[1]);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }

        //    return hora;
        //}

        ///// <summary>
        ///// Devuelve la fecha en formato: dd de MMMM del yyyy. Ejemplo: 16 de Enero del 2020
        ///// </summary>
        ///// <returns></returns>
        //public static string GetFechaFormatoLargo(DateTime fecha)
        //{
        //    string strDia = fecha.Day < 10 ? "0" + fecha.Day.ToString() : fecha.Day.ToString();
        //    string strMes = fecha.ToString("MMMM", CultureInfo.CurrentCulture);
        //    strMes = strMes[0].ToString().ToUpper() + strMes.Substring(1);

        //    string strFecha = strDia + " de " + strMes + " del " + fecha.Year.ToString();
        //    return strFecha;
        //}

        //public static T JsonDeserializeObject<T>(string str)
        //{
        //    return JsonConvert.DeserializeObject<T>(str);
        //}
        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();

            if (typeof(T) == typeof(object[]) || typeof(T) == typeof(List<string>))
            {

                int i = 0;
                foreach (object row in data)
                {

                    object[] trow = typeof(T) == typeof(List<string>) ? ((List<string>)row).ToArray() : (object[])row;


                    if (trow != null)
                    {
                        if (i == 0)
                        {
                            foreach (object col in trow)
                            {
                                string strcol = col == null ? string.Empty : col.ToString().Trim();
                                table.Columns.Add(strcol);
                            }
                        }
                        else
                        {
                            table.Rows.Add(trow);
                        }
                        i++;
                    }
                }

            }
            else
            {
                foreach (PropertyDescriptor prop in properties)
                {
                    Type t = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    if (t == DateTime.Now.GetType() || EsDecimal(t))
                    {
                        t = String.Empty.GetType();
                    }


                    table.Columns.Add(prop.Name, t);
                }
                foreach (T item in data)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                    {
                        Type t = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                        object value = prop.GetValue(item) ?? DBNull.Value;

                        if (t == DateTime.Now.GetType() && value != DBNull.Value && value != null)
                        {
                            DateTime temp = (DateTime)value;
                            value = temp.ToShortDateString();
                        }
                        else if (EsDecimal(t) && value != DBNull.Value && value != null)
                        {
                            string tmp = String.Format("{0:0.00}", value);
                            value = tmp;
                        }

                        row[prop.Name] = value;
                    }
                    table.Rows.Add(row);
                }
            }


            return table;

        }
        private static bool EsDecimal(Type t)
        {

            if (t == decimal.MaxValue.GetType() || t == double.Epsilon.GetType() || t == float.Epsilon.GetType())
            {
                return true;
            }

            return false;
        }

        public static int CalculateAgeCorrect(DateTime birthDate, DateTime now)
        {
            int age = now.Year - birthDate.Year;

            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                age--;

            return age;
        }

        public static string Generate(int length, string chars)
        {
            if (length <= 0)
                throw new ArgumentException("length debe ser mayor a cero", "length");
            if (length > 1000)
                throw new ArgumentException("length demasiado alto", "length");
            if (String.IsNullOrEmpty(chars))
                throw new ArgumentException("chars no puede estar vacio", "chars");

            StringBuilder result = new StringBuilder(length);
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[length];
                rng.GetNonZeroBytes(data);
                //rng.GetBytes(data);
                foreach (byte b in data)
                {
                    result.Append(chars[b % (chars.Length)]);
                }
            }
            return result.ToString();
        }


        public static string NumeroALetras(decimal numberAsString)
        {
            string dec;

            var entero = Convert.ToInt64(Math.Truncate(numberAsString));
            var decimales = Convert.ToInt32(Math.Round((numberAsString - entero) * 100, 2));
            if (decimales > 0)
            {
                //dec = " PESOS CON " + decimales.ToString() + "/100";
                dec = $" {decimales:0,0} /100 SOLES";
            }
            //Código agregado por mí
            else
            {
                //dec = " PESOS CON " + decimales.ToString() + "/100";
                dec = $" {decimales:0,0} /100 SOLES";
            }
            var res = NumeroALetras(Convert.ToDouble(entero)) + " CON" + dec;
            return res;
        }

        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        private static string NumeroALetras(double value)
        {
            string num2Text; value = Math.Truncate(value);
            if (value == 0) num2Text = "CERO";
            else if (value == 1) num2Text = "UNO";
            else if (value == 2) num2Text = "DOS";
            else if (value == 3) num2Text = "TRES";
            else if (value == 4) num2Text = "CUATRO";
            else if (value == 5) num2Text = "CINCO";
            else if (value == 6) num2Text = "SEIS";
            else if (value == 7) num2Text = "SIETE";
            else if (value == 8) num2Text = "OCHO";
            else if (value == 9) num2Text = "NUEVE";
            else if (value == 10) num2Text = "DIEZ";
            else if (value == 11) num2Text = "ONCE";
            else if (value == 12) num2Text = "DOCE";
            else if (value == 13) num2Text = "TRECE";
            else if (value == 14) num2Text = "CATORCE";
            else if (value == 15) num2Text = "QUINCE";
            else if (value < 20) num2Text = "DIECI" + NumeroALetras(value - 10);
            else if (value == 20) num2Text = "VEINTE";
            else if (value < 30) num2Text = "VEINTI" + NumeroALetras(value - 20);
            else if (value == 30) num2Text = "TREINTA";
            else if (value == 40) num2Text = "CUARENTA";
            else if (value == 50) num2Text = "CINCUENTA";
            else if (value == 60) num2Text = "SESENTA";
            else if (value == 70) num2Text = "SETENTA";
            else if (value == 80) num2Text = "OCHENTA";
            else if (value == 90) num2Text = "NOVENTA";
            else if (value < 100) num2Text = NumeroALetras(Math.Truncate(value / 10) * 10) + " Y " + NumeroALetras(value % 10);
            else if (value == 100) num2Text = "CIEN";
            else if (value < 200) num2Text = "CIENTO " + NumeroALetras(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) num2Text = NumeroALetras(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) num2Text = "QUINIENTOS";
            else if (value == 700) num2Text = "SETECIENTOS";
            else if (value == 900) num2Text = "NOVECIENTOS";
            else if (value < 1000) num2Text = NumeroALetras(Math.Truncate(value / 100) * 100) + " " + NumeroALetras(value % 100);
            else if (value == 1000) num2Text = "MIL";
            else if (value < 2000) num2Text = "MIL " + NumeroALetras(value % 1000);
            else if (value < 1000000)
            {
                num2Text = NumeroALetras(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0)
                {
                    num2Text = num2Text + " " + NumeroALetras(value % 1000);
                }
            }
            else if (value == 1000000)
            {
                num2Text = "UN MILLON";
            }
            else if (value < 2000000)
            {
                num2Text = "UN MILLON " + NumeroALetras(value % 1000000);
            }
            else if (value < 1000000000000)
            {
                num2Text = NumeroALetras(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0)
                {
                    num2Text = num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000) * 1000000);
                }
            }
            else if (value == 1000000000000) num2Text = "UN BILLON";
            else if (value < 2000000000000) num2Text = "UN BILLON " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            else
            {
                num2Text = NumeroALetras(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0)
                {
                    num2Text = num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
                }
            }
            return num2Text;
        }
    }
}
