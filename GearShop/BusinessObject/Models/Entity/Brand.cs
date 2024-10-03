using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Entity
{
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BrandId { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName ="varchar")]
        public string BrandName { get; set; }

        [Required]
        [StringLength(250)]
        [Column(TypeName = "varchar")]
        public string BrandLogo { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
