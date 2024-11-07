using System.Text.Json.Serialization;

namespace BreweryWholesaleMngmnt.Models
{
    public class Brewery
    {
        public int BreweryID { get; set; }
        public string? Name { get; set; }
        [JsonIgnore]
        public ICollection<Beer>? Beers { get; set; }
    }
}
