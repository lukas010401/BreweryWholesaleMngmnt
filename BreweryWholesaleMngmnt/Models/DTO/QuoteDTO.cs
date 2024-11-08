namespace BreweryWholesaleMngmnt.Models.DTO
{
    public class QuoteRequestDTO
    {
        public int ClientID { get; set; }
        public int WholesalerID { get; set; }
        public List<QuoteItemDTO>? Items { get; set; }
    }

    public class QuoteItemDTO
    {
        public int BeerID { get; set; }
        public int Quantity { get; set; }
    }

    public class QuoteResponseDTO
    {
        public decimal TotalPrice { get; set; }
        public List<QuoteItem>? Summary { get; set; }
    }
}
