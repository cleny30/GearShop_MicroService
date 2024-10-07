using BusinessObject.Core;
using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using ISUZU_NEXT.Server.Core.Extentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ProductImageDAO
    {

        public static async Task<bool> AddImageOfSpecificProduct(List<ProductImageModel> imageLink)
        {
            try
            {
                var dbContext = new ProductContext();
                foreach (var items in imageLink)
                {
                    ProductImage _image = new ProductImage();
                    _image.CopyProperties(items);
                    dbContext.ProductImages.Add(_image);
                    await dbContext.SaveChangesAsync();
                }
                return true;

            } catch (Exception ex)
            {

            }
            return false;
        }
    }
}
