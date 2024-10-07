using BusinessObject.DTOS;
using DataAccess.DAO;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductImageRepository : IProductImageRepository
    {
        public async Task<bool> AddImageOfSpecificProduct(List<ProductImageModel> imageLink)
        => await ProductImageDAO.AddImageOfSpecificProduct(imageLink);

        public async Task<List<ProductImageModel>> GetProductImagesByID(string ProId)
        => await ProductImageDAO.GetProductImagesByID(ProId);

        public async Task<bool> RemoveImageByID(List<ProductImageModel> imageLink)
        => await ProductImageDAO.RemoveImageByID(imageLink);
    }
}
