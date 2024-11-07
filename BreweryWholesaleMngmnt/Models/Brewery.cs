namespace BreweryWholesaleMngmnt.Models
{
    public class Brewery
    {
        public int BreweryID { get; set; }
        public string? Name { get; set; }
        public ICollection<Beer>? Beers { get; set; }
    }
}
