using BusinessObject.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOS
{
    public class ReceiptProductModel
    {

        public int ReceiptId { get; set; }


        public int ImportProductId { get; set; }


        public string ProId { get; set; }


        public string ProName { get; set; }


        public int Amount { get; set; }


        public double Price { get; set; }

    }
}
