using Demo.WebPubSub.Web.Models;

namespace Demo.WebPubSub.Web.ProdcutService
{
    public interface IProductService
    {
       List<Product> Products { get; set; }

       Task GetProductsAsync();

       Task<Product?> GetProductAsync(Guid id);
       Task AddProductAsync(Product product);

       Task UpdateProductAsync(Product product);

    }
}