namespace WebClient.Models
{
    public class BrandModel
    {
        public int BrandId { get; set; }

        public string BrandName { get; set; } = null!;

        public string BrandLogo { get; set; } = null!;

        public int quantity { get; set; }

        public bool IsAvailable { get; set; }
    }
}
