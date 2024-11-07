namespace BreweryWholesaleMngmnt.Models
{
    public class WholesalerStock
    {
        public int WholesalerStockID { get; set; }

        public int BeerID { get; set; }
        public Beer? Beer { get; set; }

        public int WholesalerID { get; set; }
        public Wholesaler? Wholesaler { get; set; }

        public int Quantity { get; set; }
    }

}