using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using System.Net.Http.Headers;
using System.Text.Json;
using WebClient.APIEndPoint;
using BusinessObject.DTOS;

namespace WebClient
{
    public class SignalRServer:Hub
    {
        private static ConcurrentDictionary<string, string> _userConnections = new ConcurrentDictionary<string, string>();
        private readonly IHttpContextAccessor _contx;
        private readonly HttpClient client;


        public SignalRServer(IHttpContextAccessor contx)
        {
            _contx = contx;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public override Task OnConnectedAsync()
        {
            // You can get the username from the query string or from the context
  
            var username = Context.GetHttpContext().Request.Query["username"];

            if (!string.IsNullOrEmpty(username))
            {
                _userConnections[username] = Context.ConnectionId;
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var username = _userConnections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
            if (!string.IsNullOrEmpty(username))
            {
                _userConnections.TryRemove(username, out _);
            }
            return base.OnDisconnectedAsync(exception);
        }

        public async Task LoadOrder(string username)
        {
            if (_userConnections.TryGetValue(username, out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync("LoadOrder");
            }
        }

        public async Task LoadCartData()
        {
            var userSession = _contx.HttpContext.Session.GetString("username");
            if (_userConnections.TryGetValue(userSession, out var connectionId))
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }; 

                var _cartResponse = await client.GetAsync($"{ApiEndPoints_Cart.GET_CART_BY_USERNAME}?username={userSession}");
                string strCart = await _cartResponse.Content.ReadAsStringAsync();
                List<CartModel> cartList = new List<CartModel>();

                if(!string.IsNullOrEmpty(strCart))
                {
                    cartList = JsonSerializer.Deserialize<List<CartModel>>(strCart, options);
                }
                int count = cartList.Count();
                _contx.HttpContext.Session.SetString("cartQuantity", Newtonsoft.Json.JsonConvert.SerializeObject(count));
                await Clients.Client(connectionId).SendAsync("ReceiveLoadCardData", count);
            }
        }
    }
}
