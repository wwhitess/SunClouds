using DerSerLib;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace SunClouds.Helpers
{
    internal class ApiHelper
    {
        private static string DefaultUrl = "https://api.openweathermap.org/data/2.5/weather?appid=9de009545719c498b993ae116d758d99&units=";
        private static string DefaultUrlHours = "http://api.openweathermap.org/data/2.5/forecast?appid=9de009545719c498b993ae116d758d99&lang=ru&cnt=8&units=";
        private static string TType = "metric";


        public static string Get( string city, string tempType)
        {
            try
            {

                if (tempType == "")
                {
                    tempType = TType;
                }
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(DefaultUrl + tempType + "&q=" + city).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                return "Ошибка получения информации от API: " + ex.Message;
            }
        }
        public static string GetHours(string city, string tempType)
        {
            try
            {

                if (tempType == "") { tempType = TType; }
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(DefaultUrlHours + tempType + "&q=" + city).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                return "Ошибка получения информации от API: " + ex.Message;
            }
        }


    }
}
