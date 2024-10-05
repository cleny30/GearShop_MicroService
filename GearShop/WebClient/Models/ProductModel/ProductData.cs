namespace WebClient.Models
{
    public class ProductData
    {
        public string ProId { get; set; } = null!;

        public int CateId { get; set; }

        public int BrandId { get; set; }

        public string ProName { get; set; } = null!;

        public int ProQuan { get; set; }

        public double ProPrice { get; set; }

        public string ProDes { get; set; } = null!;

        public int Discount { get; set; }

        public bool IsAvailable { get; set; }

        public List<string> ProImg { get; set; } = new List<string>();
        public Dictionary<string, string> ProAttribute { get; set; } = new Dictionary<string, string>();
    }
}