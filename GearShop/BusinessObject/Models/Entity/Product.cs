using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BusinessObject.Models.Entity
{
    public class Product
    {
        [Key]
        [Column(TypeName = "varchar")]
        [StringLength(10)]
        public string ProId { get; set; }

        [Required]
        public int CateId { get; set; }

        [Required]
        public int BrandId { get; set; }

        [Required]
        [StringLength(50)]
        public string ProName { get; set; }

        [Required]
        public int ProQuan { get; set; }

        [Required]
        [Column(TypeName = "float")]
        public double ProPrice { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string ProDes { get; set; }

        [Required]
        public int Discount { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; } = new List<ProductAttribute>();
    }
}
