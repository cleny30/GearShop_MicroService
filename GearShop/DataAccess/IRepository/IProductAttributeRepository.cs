using BusinessObject.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IProductAttributeRepository
    {
        public Task<bool> AddProductAttribute(List<ProductAttributeModel> productAttributes);

        public Task<List<ProductAttributeModel>> GetProductAttributesByID(string ProId);
    }
}
