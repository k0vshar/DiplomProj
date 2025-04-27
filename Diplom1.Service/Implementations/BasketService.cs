using Diplom.DAL;
using Diplom.Domain.Entities;
using Diplom.Domain.Enum;
using Diplom.Domain.Response;
using Diplom.Domain.ViewModels.Order;
using Diplom.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Service.Implementations
{
    public class BasketService : IBasketService
    {
        private readonly AppDBContext _context;

        public BasketService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IBaseResponse<IEnumerable<OrderViewModel>>> GetItems(string userName)
        {
            try
            {
                var orders = await _context.Orders
                    .Include(o => o.Good)
                    .Include(o => o.Basket)
                        .ThenInclude(b => b.User)
                    .Where(o => o.Basket.User.Name == userName)
                    .Select(o => new OrderViewModel
                    {
                        Id = o.Id,
                        GoodName = o.Good.Name,
                        Quantity = o.Quantity,
                        Total = o.Quantity * o.Good.Price
                    }).ToListAsync();

                return new BaseResponse<IEnumerable<OrderViewModel>>()
                {
                    Data = orders,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<OrderViewModel>>()
                {
                    Description = $"[GetItems] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<OrderViewModel>> GetItem(string userName, long id)
        {
            try
            {
                var order = await _context.Orders
                .Include(o => o.Good)
                .Include(o => o.Basket)
                    .ThenInclude(b => b.User)
                .FirstOrDefaultAsync(o => o.Basket.User.Name == userName && o.Id == id);


                if (order == null)
                {
                    return new BaseResponse<OrderViewModel>()
                    {
                        Description = "Order not found",
                        StatusCode = StatusCode.OrderNotFound
                    };
                }

                var viewModel = new OrderViewModel
                {
                    Id = order.Id,
                    GoodName = order.Good.Name,
                    Quantity = order.Quantity,
                    Total = order.Quantity * order.Good.Price
                };

                return new BaseResponse<OrderViewModel>()
                {
                    Data = viewModel,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderViewModel>()
                {
                    Description = $"[GetItem] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
