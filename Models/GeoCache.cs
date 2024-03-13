


using Firebase.Auth;
using SQLite;
using System.Text.Json.Serialization;

namespace Geocaching
{

    public class Geocache
    {
        public int Id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public double latitiude { get; set; }
        public double longitude { get; set; }
        public string status { get; set; }
        public float? difficulty { get; set; }
        public float? terrain { get; set; }
        public float? trip_time { get; set; }
        public float? trip_distance { get; set; }
        public float? rating { get; set; }
        public float? recommendations { get; set; }
        public float? rating_votes { get; set; }
        public DateTime? last_found { get; set; }
        public DateTime? last_modified { get; set; }
        public DateTime? date_created { get; set; }
        public DateTime? date_hidden { get; set; }
        public string type { get; set; }
        public AttributeNotification? attr_acodes { get; set; }
        public Short_Descriptions? short_descriptions { get; set; }
        public Descriptions? descriptions { get; set; }
        public AttributeNotification? attrnames { get; set; }
        public string country2 { get; set; }
        public string region { get; set; }
        public Image? images { get; set; }
        public int? UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
       // public ICollection<Bookmark>? Bookmarks { get; set; }
    }

    public class Short_Descriptions
    {
        public string? en { get; set; }
        public string? pl { get; set; }
        public string? de { get; set; }
        public string? nl { get; set; }
        public string? ro { get; set; }
        public string? ros { get; set; }
        public string? da { get; set; }
    }

}
