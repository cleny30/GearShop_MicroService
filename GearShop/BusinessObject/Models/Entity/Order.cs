using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models.Entity
{
    public class Order
    {
        [Key]
        [Column(TypeName = "varchar")]
        [StringLength(10)]
        public string OrderId { get; set; }

        public int? ManagerId { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [Column(TypeName = "float")]
        public double TotalPrice { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        [Column(TypeName = "text")]
        public string? OrderDes { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        [StringLength(255)]
        public string Fullname { get; set; }

        [Required]
        [StringLength(11)]
        [Column(TypeName = "varchar")]
        public string Phone { get; set; }

        [Required]
        [StringLength(1000)]
        public string Address { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
