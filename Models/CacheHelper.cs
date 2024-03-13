


using SQLite;

namespace Geocaching
{

    public class CacheHelper
    {
        public string aCodeCache { get; set; }
        public static void GetNearestCacheId(double latitude, double longitude,int radius) 
        {
            string url = string.Format(Constants.baseUrl,"caches/search/nearest?center=", latitude,"|",longitude, "&radius=",radius, "&consumer_key=",Constants.consumer_Key);
        }
        public static void GetAllCacheId(double latitude, double longitude, int radius)
        {
            string url = string.Format(Constants.baseUrl, "caches/search/nearest?center=", latitude, "|", longitude, "&radius=", radius, "&consumer_key=", Constants.consumer_Key);
        }
    }
}
