namespace WebClient.APIEndPoint
{
    public class ApiEndPoints_Cart
    {
        public const string BASE_URL = "http://localhost:5000";

        public const string GET_CART_BY_USERNAME = $"{BASE_URL}/gateway/carts";

        public const string ADD_OR_UPDATE = $"{BASE_URL}/gateway/carts/AddOrUpdateCart";

        public const string UPDATE_CART = $"{BASE_URL}/gateway/carts/UpdateCart";

        public const string DELETE_CART = $"{BASE_URL}/gateway/carts/Delete/{{0}}/{{1}}";

        public const string GET_CHECKED_PRODUCTS = $"{BASE_URL}/gateway/carts/GetCheckedProduct/{{0}}/{{1}}";
    }
}
