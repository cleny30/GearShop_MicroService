using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models.Entity
{
    public class Customer
    {
        [Key]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(32)]
        [Column(TypeName = "varchar")]
        public string Password { get; set; }

        [Required]
        [StringLength(1000)]
        public string Fullname { get; set; }

        [Required]
        [StringLength(11)]
        [Column(TypeName = "varchar")]
        public string Phone { get; set; }

        [Required]
        [StringLength(255)]
        [Column(TypeName = "varchar")]
        public string Email { get; set; }

        public virtual ICollection<DeliveryAddress>? DeliveryAddresses { get; set; } = new List<DeliveryAddress>();

    }
}
