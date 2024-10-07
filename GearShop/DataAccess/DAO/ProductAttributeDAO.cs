using BusinessObject.Core;
using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using ISUZU_NEXT.Server.Core.Extentions;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ProductAttributeDAO
    {
        public static async Task<bool> AddProductAttribute(List<ProductAttributeModel> productAttributes)
        {
            try
            {
                var dbContext = new ProductContext();
                foreach (var items in productAttributes)
                {
                    ProductAttribute _attr = new ProductAttribute();
                    _attr.CopyProperties(items);
                    dbContext.ProductAttributes.Add(_attr);
                    await dbContext.SaveChangesAsync();
                }
                return true;

            }
            catch (Exception ex)
            {

            }
            return false;
        }

    }
}
