using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace SunClouds.Models
{
    internal class HoursList
    {
        public string cod { get; set; }
        public int message { get; set; }
        public int cnt { get; set; }
        public List<ListHoursModel> list { get; set; }
        public City city { get; set; }
    }
}
