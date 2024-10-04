using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models.Entity
{
    public class ProductImage
    {
        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(10)]
        public string ProId { get; set; } = null!;

        [Required]
        [StringLength(255)]
        [Column(TypeName = "varchar")]
        public string ProImg { get; set; } = null!;

        public virtual Product Product { get; set; } = null!;
    }
}
