using System.Net.WebSockets;
using System.Text;
using Demo.WebPubSub.Web.Models;
using Newtonsoft.Json;
using Azure.Messaging.WebPubSub;
using Azure.Messaging.WebPubSub.Clients;

namespace Demo.WebPubSub.Web.ProdcutService
{
    public class WebSocketService : IWebSocketService
    {
        private readonly ClientWebSocket _webSocket = new();
        private readonly Uri _uri;
        
       public List<Product> Products { get ; set; } = new List<Product>();

        public WebSocketService(Uri uri)
        {
            _uri = uri;
        }



            public async Task ConnectAsync(CancellationToken cancellationToken)
        {
            if(_webSocket.State != WebSocketState.Open || _webSocket.State != WebSocketState.Connecting)
            {
                await _webSocket.ConnectAsync(_uri, cancellationToken);
            }
            
            StartReceivingMessages(); 
        }

        public async Task DisconnectAsync()
        {
            if (_webSocket.State == WebSocketState.Open || _webSocket.State == WebSocketState.Connecting)
            {
                await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            }
        }

        private async void StartReceivingMessages()
        {
            var buffer = new byte[1024];

            while (_webSocket.State == WebSocketState.Open)
            {
                var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var json = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Products = JsonConvert.DeserializeObject<List<Product>>(json) ?? new List<Product>();
                }
            }
        }
    }
}

