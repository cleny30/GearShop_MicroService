using BusinessObject.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOS
{
    public class BaseViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Brand> Brands { get; set; }
    }
}
