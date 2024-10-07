using BusinessObject.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IProductImageRepository
    {
        public Task<bool> AddImageOfSpecificProduct(List<ProductImageModel> imageLink);
    }
}
