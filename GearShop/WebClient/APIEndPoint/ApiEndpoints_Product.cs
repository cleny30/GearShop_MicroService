namespace WebClient.APIEndPoint
{
    public class ApiEndpoints_Product
    {
        public const string BASE_URL = "http://localhost:5000";

        public const string GET_HOME_PRODUCTS = $"{BASE_URL}/gateway/Home/GetHomeList";
        public const string GET_HOME_BRANDS = $"{BASE_URL}/gateway/Brands/GetHomeBrands";
        public const string GET_HOME_CATEGORIES = $"{BASE_URL}/gateway/Categories/GetHomeCategories";
    }
}
