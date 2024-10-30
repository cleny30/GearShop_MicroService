
namespace DashboardAdmin.Admin_APIEndPoint
{
    public class Admin_APIEndPoint_Product
    {
        public const string BASE_URL = "http://localhost:5000";

        public const string GET_PRODUCT_LIST = BASE_URL + "/gateway/products/GetProductList";

        public const string GET_PRODUCT_LIST_WITHOUT_BRAND_AND_CATEGORY = BASE_URL + "/gateway/products/GetProductListWithoutBrandAndCategory";

        public const string GET_BRAND_LIST = BASE_URL + "/gateway/Brands";

        public const string GET_CATEGORY_LIST = BASE_URL + "/gateway/Categories";

        public const string GET_NEW_PRODUCT_ID = BASE_URL + "/gateway/products/GetNewProductID/";

        public const string ADD_PRODUCT = BASE_URL + "/gateway/products/InsertProduct";

        public const string ADD_PRODUCT_IMAGE = BASE_URL + "/gateway/products/InsertProductImage";

        public const string ADD_PRODUCT_ATTRIBUTE = BASE_URL + "/gateway/products/InsertProductAttribute";

        public const string GET_PRODUCT_IMAGE_BY_ID = BASE_URL + "/gateway/products/GetProductImageByID/";

        public const string GET_PRODUCT_ATTRIBUTE_BY_ID = BASE_URL + "/gateway/products/GetProductAttributeByID/";

        public const string GET_PRODUCT_BY_ID = BASE_URL + "/gateway/products/GetProductByID/";

        public const string UPDATE_PRODUCT = BASE_URL + "/gateway/products/UpdateProduct";

        public const string DELETE_IMAGE_BY_ID = BASE_URL + "/gateway/products/DeleteImageBaseOnProductID";

        public const string DELETE_ATTRIBUTE_BY_ID = BASE_URL + "/gateway/products/DeleteAttributeByID";

        public const string CHANGE_PRODUCT_STATUS = BASE_URL + "/gateway/products/ChangeProductStatus";

        public const string CHANGE_BRAND_STATUS = BASE_URL + "/gateway/Brands/ChangeBrandStatus";

        public const string INSERT_NEW_BRAND = BASE_URL + "/gateway/Brands/InsertNewBrand";

        public const string UPDATE_BRAND = BASE_URL + "/gateway/Brands/UpdateBrand";

        public const string IS_KEYWORD_EXISTED = BASE_URL + "/gateway/Categories/IsKeywordExisted";

        public const string CHANGE_CATEGORY_STATUS = BASE_URL + "/gateway/Categories/ChangeCategoryStatus";

        public const string INSERT_NEW_CATEGORY = BASE_URL + "/gateway/Categories/InsertNewCategory";

        public const string UPDATE_CATEGORY = BASE_URL + "/gateway/Categories/UpdateCategory";

        public const string ADD_QUANTITY_TO_PRODUCT = BASE_URL + "/gateway/products/AddQuantityToProduct";
    }
}
