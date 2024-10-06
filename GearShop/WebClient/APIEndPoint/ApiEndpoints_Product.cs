namespace WebClient.APIEndPoint
{
    public static class ApiEndpoints_Product
    {
        public const string BASE_URL = "http://localhost:5000";

        public const string GET_ALL_PRODUCTS = $"{BASE_URL}/gateway/products/GetAllProducts";

        public const string GET_ALL_PRODUCT_IMAGES = $"{BASE_URL}/gateway/products/GetProductImages";

        public const string GET_ALL_PRODUCT_ATTRIBUTES = $"{BASE_URL}/gateway/products/GetProductAttributes";

        public const string GET_ALL_BRANDS = $"{BASE_URL}/gateway/brands";

        public const string GET_ALL_CATEGORIES = $"{BASE_URL}/gateway/categories";

        public const string GET_PRODUCT_BY_NAME = $"{BASE_URL}/gateway/products/GetProductByName";
    }
}
