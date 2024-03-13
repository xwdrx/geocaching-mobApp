using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Geocaching.Data
{
    public class DataController
    {
        public readonly string consumer_Key = "JLUJs7AtyRzM2XgYtKWd";
        public readonly string consumer_Secret = "kn6A7NQnnZCbDkfW6S9B4axAesvQX3BPwyKAe8GJ";
        public readonly string latitiude = "50.047486";
        public readonly string longitude = "19.927897";
        public readonly string baseUrl = "https://opencaching.pl/okapi/services/";

        public DataController() {
        }

        private static readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri(Constants.baseUrl)

        };


        public async static Task<List<string>> GetInformationAboutAttributeID()
        {

            List<string> keys2 = new List<string>();

            var response = await client.GetAsync($"attrs/attribute_index?&consumer_key=JLUJs7AtyRzM2XgYtKWd");


            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                string json = content.ToString();
                var jsonObject = JObject.Parse(json);
                dynamic dynJson = JsonConvert.DeserializeObject(json);
                IList objectList = dynJson as IList;
                List<string> keys = new List<string>();
                foreach (var att in objectList)
                {
                    keys.Add(att.ToString());

                }

                foreach (var key in keys)
                {
                    keys2.Add(Regex.Replace(key.Substring(1, 3), @"[^0-9a-zA-Z\._]", ""));

                }

            }
            return keys2;



        }

        public async static Task<List<string>> GetInformationAboutGeocacheID()
        {
            var tasks = new List<string>();
            string url = $"caches/search/all?limit=500&consumer_key=JLUJs7AtyRzM2XgYtKWd";
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                string json = content.ToString();
                var jsonObject = JObject.Parse(json);
                dynamic dynJson = JsonConvert.DeserializeObject(json);
                IList objectList = dynJson as IList;
                IList<string> keys = new List<string>();
                foreach (var att in dynJson)
                {
                    keys.Add(att.ToString());

                }
                var codes = Regex.Replace(keys[0], @"""([^A-Z]*/?)", " ");
                tasks.AddRange(codes.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                   .Select(Convert.ToString));
            }
            return tasks;

        }

        public async static Task<List<string>> GetInformationAboutNearestGeocacheID()       //max 500
        {
            var tasks = new List<string>();
            var myLocation = await Geolocation.GetLocationAsync();


            var response = await client.GetAsync($"caches/search/nearest?center={myLocation.Latitude}|{myLocation.Longitude}&limit=100&consumer_key=JLUJs7AtyRzM2XgYtKWd");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                string json = content.ToString();
                var jsonObject = JObject.Parse(json);
                dynamic dynJson = JsonConvert.DeserializeObject(json);
                IList objectList = dynJson as IList;
                IList<string> keys = new List<string>();
                foreach (var att in dynJson)
                {
                    keys.Add(att.ToString());

                }
                var codes = Regex.Replace(keys[0], @"""([^A-Z]*/?)", " ");
                tasks.AddRange(codes.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                   .Select(Convert.ToString));
            }
            return tasks;

        }

        public async static Task<List<Geocache>> GetInformationAboutNearestGeocache()       //ten jest ok, ale nie robi tych co descritions nie jest poprawione

        {
            List<Geocache> geocaches = new List<Geocache>();
            var task = await GetInformationAboutNearestGeocacheID();

            List<string> ids = task;
            List<string> contents = new List<string>();

            foreach (var id in ids)
            {
                string url = $"caches/geocache?cache_code={id}&fields=short_descriptions|country2|region|images|code|name" +
                                $"|location|type|status|descriptions|protection_areas|difficulty|terrain|trip_time|trip_distance|rating|" +
                                $"recommendations|rating_votes|alt_wpts|last_found|last_modified|date_created|date_hidden|attr_acodes|attrnames" +
                                $"&consumer_key=JLUJs7AtyRzM2XgYtKWd";
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    string json = content.ToString();
                    contents.Add(json);
                }

            }
            var idx = 0;
            foreach (var i in contents)
            {
                try
                {
                    var dynJson = JsonConvert.DeserializeObject<Geocache>(i);

                    string descriptions = dynJson.descriptions.pl;

                    if (string.IsNullOrEmpty(dynJson.descriptions.pl))
                    {
                        descriptions = dynJson.descriptions.en;
                    }
                    string dj = Regex.Replace(descriptions, "<.*?>", String.Empty);
                    dynJson.descriptions.pl = dj;
                    if (string.IsNullOrEmpty(dynJson.descriptions.pl))
                    {
                        dynJson.descriptions.en = dj;
                    }
                    var location = dynJson.location.Split("|");
                    dynJson.latitiude = Convert.ToDouble(location[0]);
                    dynJson.longitude = Convert.ToDouble(location[1]);
                    geocaches.Add(dynJson);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message + "kod: " + ids[idx]); }
                idx++;

            }
            return geocaches;

        }

        public async static Task<List<Geocache>> GetInformationAboutGeocache()       //ten jest ok, ale nie robi tych co descritions nie jest poprawione

        {
            List<Geocache> geocaches = new List<Geocache>();
            var task = await GetInformationAboutGeocacheID();

            List<string> ids = task;
            List<string> contents = new List<string>();

            foreach (var id in ids)
            {
                string url = $"caches/geocache?cache_code={id}&fields=short_descriptions|country2|region|images|code|name" +
                                $"|location|type|status|descriptions|protection_areas|difficulty|terrain|trip_time|trip_distance|rating|" +
                                $"recommendations|rating_votes|alt_wpts|last_found|last_modified|date_created|date_hidden|attr_acodes|attrnames" +
                                $"&consumer_key=JLUJs7AtyRzM2XgYtKWd";
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    string json = content.ToString();
                    contents.Add(json);
                }

            }
            var idx = 0;
            foreach (var i in contents)
            {
                try
                {
                    var dynJson = JsonConvert.DeserializeObject<Geocache>(i);

                    string descriptions = dynJson.descriptions.pl;

                    if (string.IsNullOrEmpty(dynJson.descriptions.pl))
                    {
                        descriptions = dynJson.descriptions.en;
                    }
                    string dj = Regex.Replace(descriptions, "<.*?>", String.Empty);
                    dynJson.descriptions.pl = dj;
                    if (string.IsNullOrEmpty(dynJson.descriptions.pl))
                    {
                        dynJson.descriptions.en = dj;
                    }
                    var location = dynJson.location.Split("|");
                    dynJson.latitiude = Convert.ToDouble(location[0]);
                    dynJson.longitude = Convert.ToDouble(location[1]);
                    geocaches.Add(dynJson);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message + "kod: " + ids[idx]); }
                idx++;

            }
            return geocaches;

        }


        public async static Task<List<AttributeNotification>> GetInformationAboutGeocacheAttribute()
        {
            List<AttributeNotification> attributeNotifications = new List<AttributeNotification>();
            var task = await GetInformationAboutAttributeID();

            List<string> ids = task;

            foreach (var id in ids)
            {
                string url = $"attrs/attribute?acode={id}&fields=acode|name|names|descriptions|gc_equivs|local_icon_url&langpref=pl&consumer_key=JLUJs7AtyRzM2XgYtKWd";
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    string json = content.ToString();
                    var dynJson = JsonConvert.DeserializeObject<AttributeNotification>(json);
                    attributeNotifications.Add(dynJson);
                    Console.WriteLine(dynJson);

                }

            }
            return attributeNotifications;

        }
    }
}
