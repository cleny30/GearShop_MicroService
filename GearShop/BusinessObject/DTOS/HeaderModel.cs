using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOS
{
    public class HeaderModel
    {
        public List<BrandModel>? brand { get; set; }
        public List<CategoryModel>? category { get; set; }
        public int cardQuantity { get; set; } = 0;
    }
}
