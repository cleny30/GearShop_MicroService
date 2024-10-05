namespace WebClient.Models
{
    public class CategoryModel
    {
        public int CateId { get; set; }

        public string CateName { get; set; } = null!;

        public string Keyword { get; set; } = null!;

        public int quantity { get; set; }

        public bool IsAvailable { get; set; }
    }
}
