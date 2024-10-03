using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models.Entity
{
    public class DeliveryAddress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string Username { get; set; }

        [Required]
        [StringLength(1000)]
        public string Fullname { get; set; }

        [Required]
        [StringLength(11)]
        [Column(TypeName = "varchar")]
        public string Phone { get; set; }

        [Required]
        [StringLength(1000)]
        public string Address { get; set; }

        [Required]
        [StringLength(500)]
        public string? Specific { get; set; }

        [Required]
        public bool IsDefault { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
