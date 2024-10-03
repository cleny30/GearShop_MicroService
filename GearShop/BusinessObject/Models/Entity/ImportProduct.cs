using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models.Entity
{
    public class ImportProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImportProductId { get; set; }

        [Required]
        public DateOnly DateImport { get; set; }

        [Required]
        [StringLength(50)]
        public string PersonInCharge { get; set; }

        [Required]
        [Column(TypeName = "float")]
        public double Payment { get; set; }

        public virtual ICollection<ReceiptProduct> ReceiptProducts { get; set; } = new List<ReceiptProduct>();
    }
}
