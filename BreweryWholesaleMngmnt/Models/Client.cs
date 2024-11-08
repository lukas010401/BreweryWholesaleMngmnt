using System.Text.Json.Serialization;

namespace BreweryWholesaleMngmnt.Models
{
    public class Client
    {
        public int ClientID { get; set; }
        public string? Name { get; set; }
        [JsonIgnore]
        public ICollection<Quote>? Quotes { get; set; }
    }
}
