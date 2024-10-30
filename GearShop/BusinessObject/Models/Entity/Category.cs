using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models.Entity
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CateId { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string CateName { get; set; } = null!;

        [Required]
        [StringLength(2)]
        [Column(TypeName = "varchar")]
        public string Keyword { get; set; } = null!;

        [Required]
        public bool IsAvailable { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
