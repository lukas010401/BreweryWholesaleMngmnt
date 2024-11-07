using System.Text.Json.Serialization;

namespace BreweryWholesaleMngmnt.Models
{
    public class Wholesaler
    {
        public int WholesalerID { get; set; }
        public string? Name { get; set; }
        [JsonIgnore]
        public ICollection<WholesalerStock>? WholesalerStocks { get; set; }
    }

}
