using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.UI;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace eShop.DataStore.LocalStorage
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly IJSRuntime jSRuntime;
        private const string cstrShoppingCart = "eShop.ShoppingCart";

        public ShoppingCart(IJSRuntime jSRuntime)
        {
            this.jSRuntime = jSRuntime;
        }
        public async Task<Order> AddProductAsync(Product product)
        {
            Order order = await this.GetOrderFromLocalStorageAsync();
            order.AddProduct(product.ProductId, 1, product.Price);
            await SetOrderInLocalStorageAsync(order);
            return order;
        }

        public Task<Order> DeleteProductAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task EmptyAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> PlaceOrderAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> UpdateQuantityAsync(int productId, int quanity)
        {
            throw new NotImplementedException();
        }

        private async Task<Order> GetOrderFromLocalStorageAsync()
        {
            Order order;

            var strOrder = await jSRuntime.InvokeAsync<string>("localStorage.getItem", cstrShoppingCart);
            if (!string.IsNullOrWhiteSpace(strOrder))
            {
                order = JsonConvert.DeserializeObject<Order>(strOrder);
            }
            else
            {
                order = new Order();
                await SetOrderInLocalStorageAsync(order);
            }

            return order;
        }

        private async Task SetOrderInLocalStorageAsync(Order order)
        {
            await jSRuntime.InvokeVoidAsync(
                "localStorage.setItem",
                cstrShoppingCart,
                JsonConvert.SerializeObject(order)
            );
        }
    }
}
