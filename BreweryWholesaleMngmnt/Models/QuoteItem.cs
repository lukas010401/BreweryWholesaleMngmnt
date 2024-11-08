namespace BreweryWholesaleMngmnt.Models
{
    public class QuoteItem
    {
        public int QuoteItemID { get; set; }
        public int QuoteID { get; set; }
        public Quote? Quote { get; set; }
        public int BeerID { get; set; }
        public Beer? Beer { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
