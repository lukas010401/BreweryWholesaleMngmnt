namespace BreweryWholesaleMngmnt.Models
{
    public class Beer
    {
        public int BeerID { get; set; }
        public string? Name { get; set; }
        public decimal AlcoholContent { get; set; }
        public decimal Price { get; set; }

        
        public int BreweryID { get; set; }
        public Brewery? Brewery { get; set; }

        //public ICollection<WholesalerStock> WholesalerStocks { get; set; }
    }
}