using BusinessObject.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOS
{
    public class HomeModel : BaseViewModel
    {
        public List<ProductData> specialSale { get; set; }
        public List<ProductData> mouse { get; set; }
        public List<ProductData> keyboard { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Category> Categories { get; set; }

    }
}
