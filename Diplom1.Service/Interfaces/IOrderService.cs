using Diplom.Domain.Response;
using Diplom.Domain.ViewModels.Order;

namespace Diplom.Service.Interfaces
{
    public interface IOrderService
    {
        Task<IBaseResponse<Order>> Create(CreateOrderViewModel model);

        Task<IBaseResponse<bool>> Delete(long id);
    }
}
