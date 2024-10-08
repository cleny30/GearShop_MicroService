using BusinessObject.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IHomeRepository
    {
        //List<ProductData> GetSpecialSaleProducts();
        List<ProductData> GetMouseProducts();
        List<ProductData> GetKeyboardProducts();
        List<ProductData> HomeGetData();
    }
}
