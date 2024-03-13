using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SQLite;

namespace Geocaching
{
    public class Descriptions
    {
        public string en { get; set; }
        public string pl { get; set; }
        public string de { get; set; }
        public string nl { get; set; }
        public string ro { get; set; }
    }

    public class GcEquiv
    {
        public int id { get; set; }
        public int? inc { get; set; }
        public string name { get; set; }
    }

    public class Names
    {
        public string en { get; set; }
        public string pl { get; set; }
        public string de { get; set; }
        public string nl { get; set; }
        public string ro { get; set; }
        public string ros { get; set; }
        public string da { get; set; }
    }

    public class AttributeNotification
    {
        public string acode { get; set; }
        public string name { get; set; }
        public Names names { get; set; }
        public Descriptions descriptions { get; set; }
        public List<GcEquiv> gc_equivs { get; set; }
        public string local_icon_url { get; set; }
    }


}
