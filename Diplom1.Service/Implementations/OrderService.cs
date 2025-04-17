using Diplom.DAL;
using Diplom.Domain.Entities;
using Diplom.Domain.Enum;
using Diplom.Domain.Response;
using Diplom.Domain.ViewModels.Order;
using Diplom.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Diplom.Service.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly AppDBContext _context;

        public OrderService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IBaseResponse<Order>> Create(CreateOrderViewModel model)
        {
            var response = new BaseResponse<Order>();
            try
            {
                var user = await _context.Users
                    .Include(u => u.Basket)
                    .FirstOrDefaultAsync(u => u.Name == model.Login);

                if (user == null)
                {
                    response.Description = "Пользователь не найден";
                    response.StatusCode = StatusCode.UserNotFound;
                    return response;
                }

                var basket = user.Basket;
                if (basket == null)
                {
                    response.Description = "Корзина пользователя не найдена";
                    response.StatusCode = StatusCode.BasketNotFound;
                    return response;
                }

                var good = await _context.Goods.FirstOrDefaultAsync(g => g.Id == model.GoodId);
                if (good == null)
                {
                    response.Description = "Товар не найден";
                    response.StatusCode = StatusCode.GoodNotFound;
                    return response;
                }

                var order = new Order
                {
                    Good = good,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    Address = model.Address,
                    Basket = basket
                };

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                response.Data = order;
                response.StatusCode = StatusCode.OK;
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Order>
                {
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> Delete(long id)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
                if (order == null)
                {
                    response.Description = "Заказ не найден";
                    response.StatusCode = StatusCode.OrderNotFound;
                    response.Data = false;
                    return response;
                }

                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();

                response.Data = true;
                response.StatusCode = StatusCode.OK;
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Description = $"[Delete] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    Data = false
                };
            }
        }
    }
}