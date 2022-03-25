using eShop.CoreBusiness.Models;
using System.Threading.Tasks;

namespace eShop.UseCases
{
    public interface IViewShoppingCartUseCase
    {
        Task<Order> Execute();
    }
}