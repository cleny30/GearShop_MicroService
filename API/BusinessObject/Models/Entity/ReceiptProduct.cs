using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models.Entity
{
    public class ReceiptProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReceiptId { get; set; }

        [Required]
        public int ImportProductId { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(10)]
        public string ProId { get; set; }

        [Required]
        [StringLength(50)]
        public string ProName { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        [Column(TypeName = "float")]
        public double Price { get; set; }

        // Navigation property for the many-to-one relationship
        [ForeignKey("ImportProductId")]
        public virtual ImportProduct ImportProduct { get; set; }
    }
}
