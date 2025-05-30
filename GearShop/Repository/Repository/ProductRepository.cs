﻿using BusinessObject.Models.Entity;
using BusinessObject.DTOS;
using DataAccess.DAO;
using Repository.IRepository;

namespace Repository.Repository
{
    public class ProductRepository : IProductRepository
    {
        public List<ProductAttribute> GetProductAttributes() => ProductDAO.GetProductAttributes();

        public List<ProductImage> GetProductImages() => ProductDAO.GetProductImages();

        public List<Product> GetProducts() => ProductDAO.GetProducts();
        public async Task<List<ProductModel>> GetProductListAdmin()
        => await ProductDAO.GetProductListAdmin();

        public async Task<List<ProductData>> SearchProductsByName(string productName)
        => await ProductDAO.SearchProductByName(productName);

        public async Task<string> GetNewProductID(int CatID)
        => await ProductDAO.GetNewProductID(CatID);

        public async Task<bool> InsertProduct(ProductData product)
        => await ProductDAO.InsertProduct(product);

        public async Task<ProductModel> GetProductByID(string ProId)
        => await ProductDAO.GetProductByID(ProId);

        public async Task<bool> UpdateProduct(ProductData product)
        => await ProductDAO.UpdateProduct(product);

        public async Task<bool> ChangeProductStatus(string ProId, bool Status)
        => await ProductDAO.ChangeProductStatus(ProId, Status);

        public async Task<bool> AddQuantityToProduct(List<ReceiptProductModel> products)
        => await ProductDAO.AddQuantityToProduct(products);

        public async Task<bool> UpdateQuantityToProduct(List<ProductData> products)
        {
            foreach (var item in products)
            {
                await ProductDAO.UpdateQuantityProduct(item);
            }
            return true;
        }
    }
}
