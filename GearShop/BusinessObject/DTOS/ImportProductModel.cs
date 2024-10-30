using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOS
{
    public class ImportProductModel
    {
        public int ImportProductId { get; set; }

        public DateOnly DateImport { get; set; }

        public string PersonInCharge { get; set; } = null!;

        public double Payment { get; set; }
    }
}
