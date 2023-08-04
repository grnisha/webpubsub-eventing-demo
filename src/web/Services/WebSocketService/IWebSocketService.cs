using Demo.WebPubSub.Web.Models;

namespace Demo.WebPubSub.Web.ProdcutService
{
    public interface IWebSocketService
    {
         List<Product> Products { get; set; }
        Task ConnectAsync(CancellationToken cancellationToken);
        Task DisconnectAsync();
    }

}