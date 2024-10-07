using Azure.Core;
using BusinessObject.Core;
using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using ISUZU_NEXT.Server.Core.Extentions;
using Microsoft.CodeAnalysis.Elfie.Model;
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

        public static async Task<List<ProductAttributeModel>> GetProductAttributesByID(string ProId)
        {
            List<ProductAttribute> _img = new List<ProductAttribute>(); 
            List<ProductAttributeModel> model = new List<ProductAttributeModel>();
            try
            {
                var dbContext = new ProductContext();
                _img = dbContext.ProductAttributes.ToList();
                foreach(var items in _img)
                {
                    ProductAttributeModel _item = new ProductAttributeModel();
                    _item.CopyProperties(items);
                    model.Add(_item);
                }
                return model;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}
