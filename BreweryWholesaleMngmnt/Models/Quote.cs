using System.Text.Json.Serialization;

namespace BreweryWholesaleMngmnt.Models
{
    public class Quote
    {
        public int QuoteID { get; set; }
        public int WholesalerID { get; set; }
        public Wholesaler? Wholesaler { get; set; }
        public int ClientID { get; set; }
        public Client? Client { get; set; }
        public DateTime RequestedAt { get; set; }
        public decimal TotalPrice { get; set; }

        [JsonIgnore]
        public ICollection<QuoteItem> Items { get; set; } = new List<QuoteItem>();
    }
}