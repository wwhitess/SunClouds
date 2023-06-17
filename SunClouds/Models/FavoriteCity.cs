using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SunClouds.Models
{
    internal class FavoriteCity
    {
        public FavoriteCity(string name, double lon, double lat)
        {
            Name = name;
            Lon = lon;
            Lat = lat;
        }

        public string Name { get; set; }
        public double Lon { get; set; }
        public double Lat { get; set; }
    }
}
