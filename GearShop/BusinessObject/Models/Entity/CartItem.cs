using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models.Entity
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartItemId { get; set; } // New primary key

        [Required]
        public int CartId { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(10)]
        public string ProId { get; set; }

        [Required]
        [StringLength(50)]
        public string ProName { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "float")]
        public double Price { get; set; }

        // Navigation property for the many-to-one relationship
        public virtual Cart Cart { get; set; }
    }
}
