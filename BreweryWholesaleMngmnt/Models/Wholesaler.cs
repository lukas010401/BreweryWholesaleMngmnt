namespace BreweryWholesaleMngmnt.Models
{
    public class Wholesaler
    {
        public int WholesalerID { get; set; }
        public string? Name { get; set; }
        public ICollection<WholesalerStock>? WholesalerStocks { get; set; }
    }

}
