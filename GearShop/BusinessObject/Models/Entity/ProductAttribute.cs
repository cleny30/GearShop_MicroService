using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Entity
{
    public class ProductAttribute
    {
        [Key]
        [Column(TypeName = "varchar")]
        [StringLength(10)]
        public string ProId { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string Feature { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Description { get; set; }

        public virtual Product Product { get; set; }
    }
}
