using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using DashboardAdmin.Admin_APIEndPoint;
using Repository.Core.Cloudiary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DashboardAdmin.Service
{
    public class ProductPageService
    {
        private readonly HttpClient _client;

        public ProductPageService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<ProductModel>> GetProductList()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Kiểm tra xem tên người dùng có tồn tại không
            var response = await _client.GetAsync(Admin_APIEndPoint_Product.GET_PRODUCT_LIST);
            var strData = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<ProductModel>>(strData, options);
        }

        public async Task<List<BrandModel>> GetBrandList()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Kiểm tra xem tên người dùng có tồn tại không
            var response = await _client.GetAsync(Admin_APIEndPoint_Product.GET_BRAND_LIST);
            var strData = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<BrandModel>>(strData, options);
        }

        public async Task<List<CategoryModel>> GetCategoryList()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Kiểm tra xem tên người dùng có tồn tại không
            var response = await _client.GetAsync(Admin_APIEndPoint_Product.GET_CATEGORY_LIST);
            var strData = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<CategoryModel>>(strData, options);
        }

        public async Task<string> GetNewProductID(int CatID)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Kiểm tra xem tên người dùng có tồn tại không
            var response = await _client.GetAsync(Admin_APIEndPoint_Product.GET_NEW_PRODUCT_ID + CatID);
            var strData = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<string>(strData, options);
        }

        public class InsertProductResult
        {
            public bool InsertProductSuccessful { get; set; }
            public bool InsertProductImageSuccessful { get; set; }
            public bool InsertProductAttributeSuccessful { get; set; }
        }

        public class UpdateProductResult
        {
            public bool UpdateProductSuccessful { get; set; }
            public bool UpdateProductImageSuccessful { get; set; }
            public bool UpdateProductAttributeSuccessful { get; set; }
        }

        public async Task<InsertProductResult> InsertNewProduct(ProductData product, ObservableCollection<string> SelectedFiles, List<string> attribute, List<string> description)
        {
            CloudinaryManagement cloud = new CloudinaryManagement();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = new InsertProductResult();
            if (product != null)
            {
                var productJson = JsonSerializer.Serialize(product, options);
                var content = new StringContent(productJson, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync(Admin_APIEndPoint_Product.ADD_PRODUCT, content);
                var strData = await response.Content.ReadAsStringAsync();

                result.InsertProductSuccessful = JsonSerializer.Deserialize<bool>(strData, options);
                if (result.InsertProductSuccessful)
                {
                    List<string> imageLink = new List<string>();

                    List<CategoryModel> _category = await GetCategoryList();

                    string CatName = _category.FirstOrDefault(c => c.CateId == product.CateId).CateName;

                    foreach (string items in SelectedFiles)
                    {
                        // Assuming Upload is an async method
                        string cloudinaryLink = await cloud.Upload(items, $"Products/{CatName}");
                        imageLink.Add(cloudinaryLink);
                    }

                    List<ProductImageModel> _img = new List<ProductImageModel>();
                    foreach (var image in imageLink)
                    {
                        _img.Add(new ProductImageModel
                        {
                            ProId = product.ProId,
                            ProImg = image
                        });
                    }

                    var productImageJson = JsonSerializer.Serialize(_img, options);
                    var contentImage = new StringContent(productImageJson, Encoding.UTF8, "application/json");

                    response = await _client.PostAsync(Admin_APIEndPoint_Product.ADD_PRODUCT_IMAGE, contentImage);
                    strData = await response.Content.ReadAsStringAsync();

                    result.InsertProductImageSuccessful = JsonSerializer.Deserialize<bool>(strData, options);

                    if (result.InsertProductImageSuccessful)
                    {
                        List<ProductAttributeModel> _productAttribute = new List<ProductAttributeModel>();

                        foreach (var (attr, desc) in attribute.Zip(description, (attr, desc) => (attr, desc)))
                        {
                            _productAttribute.Add(new ProductAttributeModel
                            {
                                ProId = product.ProId,
                                Description = desc,
                                Feature = attr
                            });
                        }

                        var productAttributeJson = JsonSerializer.Serialize(_productAttribute, options);
                        var contentAttribute = new StringContent(productAttributeJson, Encoding.UTF8, "application/json");

                        response = await _client.PostAsync(Admin_APIEndPoint_Product.ADD_PRODUCT_ATTRIBUTE, contentAttribute);
                        strData = await response.Content.ReadAsStringAsync();

                        result.InsertProductAttributeSuccessful = JsonSerializer.Deserialize<bool>(strData, options);
                    }
                }
            }
            return result;
        }

        public async Task<UpdateProductResult> UpdateProduct(ProductData product, ObservableCollection<string> SelectedFilesDelete, 
            ObservableCollection<string> SelectedFilesUpdate, List<string> attribute, List<string> description)
        {
            CloudinaryManagement cloud = new CloudinaryManagement();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = new UpdateProductResult();
            if(product != null)
            {
                var productJson = JsonSerializer.Serialize(product, options);
                var content = new StringContent(productJson, Encoding.UTF8, "application/json");
                var response = await _client.PutAsync(Admin_APIEndPoint_Product.UPDATE_PRODUCT, content);
                var strData = await response.Content.ReadAsStringAsync();

                result.UpdateProductSuccessful = JsonSerializer.Deserialize<bool>(strData, options);
                if (result.UpdateProductSuccessful)
                {
                    List<string> imageLink = new List<string>();
                    List<string> deleteList = new List<string>();

                    List<CategoryModel> _category = await GetCategoryList();

                    string CatName = _category.FirstOrDefault(c => c.CateId == product.CateId).CateName;

                    foreach(string items in SelectedFilesDelete)
                    {
                        bool delete = await cloud.Delete(items);
                        deleteList.Add(items);
                    }

                    List<ProductImageModel> _imgDelete = new List<ProductImageModel>();
                    foreach (var image in deleteList)
                    {
                        _imgDelete.Add(new ProductImageModel
                        {
                            ProId = product.ProId,
                            ProImg = image
                        });
                    }
                    var productImageDeleteJson = JsonSerializer.Serialize(_imgDelete, options);
                    var contentDeleteImage = new StringContent(productImageDeleteJson, Encoding.UTF8, "application/json");

                    response = await _client.PutAsync(Admin_APIEndPoint_Product.DELETE_IMAGE_BY_ID , contentDeleteImage);
                    strData = await response.Content.ReadAsStringAsync();

                    bool deleteImageSuccessful = JsonSerializer.Deserialize<bool>(strData, options);

                    foreach (string items in SelectedFilesUpdate)
                    {
                        // Assuming Upload is an async method
                        string cloudinaryLink = await cloud.Upload(items, $"Products/{CatName}");
                        imageLink.Add(cloudinaryLink);
                    }

                    List<ProductImageModel> _img = new List<ProductImageModel>();
                    foreach (var image in imageLink)
                    {
                        _img.Add(new ProductImageModel
                        {
                            ProId = product.ProId,
                            ProImg = image
                        });
                    }

                    var productImageJson = JsonSerializer.Serialize(_img, options);
                    var contentImage = new StringContent(productImageJson, Encoding.UTF8, "application/json");

                    response = await _client.PostAsync(Admin_APIEndPoint_Product.ADD_PRODUCT_IMAGE, contentImage);
                    strData = await response.Content.ReadAsStringAsync();

                    bool addImageSuccessful = JsonSerializer.Deserialize<bool>(strData, options);

                    result.UpdateProductImageSuccessful = deleteImageSuccessful && addImageSuccessful;

                    if (result.UpdateProductImageSuccessful)
                    {

                        response = await _client.DeleteAsync($"{Admin_APIEndPoint_Product.DELETE_ATTRIBUTE_BY_ID}?ProId={product.ProId}" );
                        strData = await response.Content.ReadAsStringAsync();

                        bool deleteAttributeSuccessful = JsonSerializer.Deserialize<bool>(strData, options);

                        List<ProductAttributeModel> _productAttribute = new List<ProductAttributeModel>();

                        foreach (var (attr, desc) in attribute.Zip(description, (attr, desc) => (attr, desc)))
                        {
                            _productAttribute.Add(new ProductAttributeModel
                            {
                                ProId = product.ProId,
                                Description = desc,
                                Feature = attr
                            });
                        }

                        var productAttributeJson = JsonSerializer.Serialize(_productAttribute, options);
                        var contentAttribute = new StringContent(productAttributeJson, Encoding.UTF8, "application/json");

                        response = await _client.PostAsync(Admin_APIEndPoint_Product.ADD_PRODUCT_ATTRIBUTE, contentAttribute);
                        strData = await response.Content.ReadAsStringAsync();

                        bool updateAttributeSuccessful = JsonSerializer.Deserialize<bool>(strData, options);

                        result.UpdateProductAttributeSuccessful = updateAttributeSuccessful && deleteAttributeSuccessful;
                    }
                }
            }
            return result;
        }
       
        public async Task<ProductModel> GetProductById(string ProId)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Kiểm tra xem tên người dùng có tồn tại không
            var response = await _client.GetAsync(Admin_APIEndPoint_Product.GET_PRODUCT_BY_ID + ProId);
            var strData = await response.Content.ReadAsStringAsync();
            ProductModel model = JsonSerializer.Deserialize<ProductModel>(strData, options);
            return JsonSerializer.Deserialize<ProductModel>(strData, options);
        }

        public async Task<List<ProductImageModel>> GetProductImageById(string ProId)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Kiểm tra xem tên người dùng có tồn tại không
            var response = await _client.GetAsync(Admin_APIEndPoint_Product.GET_PRODUCT_IMAGE_BY_ID + ProId);
            var strData = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<ProductImageModel>>(strData, options);
        }

        public async Task<List<ProductAttributeModel>> GetProductAttributeById(string ProId)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Kiểm tra xem tên người dùng có tồn tại không
            var response = await _client.GetAsync(Admin_APIEndPoint_Product.GET_PRODUCT_ATTRIBUTE_BY_ID + ProId);
            var strData = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<ProductAttributeModel>>(strData, options);
        }  

        public async Task<bool> ChangeProductStatus(string ProId, bool status)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Kiểm tra xem tên người dùng có tồn tại không
            var response = await _client.PutAsync($"{Admin_APIEndPoint_Product.CHANGE_PRODUCT_STATUS}?ProId={ProId}&status={status}", null);
            var strData = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<bool>(strData, options);
        }
    }
}
