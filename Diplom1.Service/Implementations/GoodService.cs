using Diplom.DAL.Interfaces;
using Diplom.Domain.Entities;
using Diplom.Domain.Enum;
using Diplom.Domain.Response;
using Diplom.Domain.ViewModels.Good;
using Diplom.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Diplom.Service.Implementations
{
    public class GoodService : IGoodService
    {
        private readonly IGoodRepository _goodRepository;

        public GoodService(IGoodRepository goodRepository)
        {
            _goodRepository = goodRepository;
        }

        public async Task<IBaseResponse<GoodViewModel>> GetGood(long id)
        {
            var response = new BaseResponse<GoodViewModel>();

            try
            {
                var good = await _goodRepository.GetByIdAsync((int)id);
                if (good == null)
                {
                    response.Description = "Товар не найден";
                    response.StatusCode = StatusCode.GoodNotFound;
                    return response;
                }

                var viewModel = new GoodViewModel
                {
                    Id = good.Id,
                    Name = good.Name,
                    Description = good.Description,
                    Price = (decimal)good.Price,
                    Rate = good.Rate,
                    Tag = good.Tag,
                    Quantity = good.Quantity
                };

                response.Data = viewModel;
                response.StatusCode = StatusCode.OK;
                return response;
            }
            catch (Exception ex)
            {
                response.Description = $"[GetGood] : {ex.Message}";
                response.StatusCode = StatusCode.InternalServerError;
                return response;
            }
        }

        public Task<BaseResponse<Dictionary<int, string>>> GetTags()
        {
            try
            {
                var tags = ((Tags[])Enum.GetValues(typeof(Tags)))
                    .ToDictionary(k => (int)k, v => v.ToString());

                return Task.FromResult(new BaseResponse<Dictionary<int, string>>
                {
                    Data = tags,
                    StatusCode = StatusCode.OK
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new BaseResponse<Dictionary<int, string>>
                {
                    Description = $"[GetTags]: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                });
            }
        }

        public async Task<IBaseResponse<Good>> Create(GoodViewModel model, byte[] imageData)
        {
            var response = new BaseResponse<Good>();

            try
            {
                var good = new Good
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = (float)model.Price,
                    Rate = model.Rate,
                    Quantity = model.Quantity,
                    Tag = model.Tag,
                    Image = imageData
                };

                await _goodRepository.AddAsync(good);

                response.Data = good;
                response.StatusCode = StatusCode.OK;
                return response;
            }
            catch (Exception ex)
            {
                response.Description = $"[Create]: {ex.Message}";
                response.StatusCode = StatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<IBaseResponse<Good>> Edit(long id, GoodViewModel model)
        {
            var response = new BaseResponse<Good>();

            try
            {
                var good = await _goodRepository.GetByIdAsync((int)id);
                if (good == null)
                {
                    response.Description = "Товар не найден";
                    response.StatusCode = StatusCode.GoodNotFound;
                    return response;
                }

                good.Name = model.Name;
                good.Description = model.Description;
                good.Price = (float)model.Price;
                good.Rate = model.Rate;
                good.Tag = model.Tag;
                good.Quantity = model.Quantity;

                await _goodRepository.UpdateAsync(good);

                response.Data = good;
                response.StatusCode = StatusCode.OK;
                return response;
            }
            catch (Exception ex)
            {
                response.Description = $"[Edit]: {ex.Message}";
                response.StatusCode = StatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<IBaseResponse<bool>> DeleteGood(long id)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var good = await _goodRepository.GetByIdAsync((int)id);
                if (good == null)
                {
                    response.Description = "Товар не найден";
                    response.StatusCode = StatusCode.GoodNotFound;
                    return response;
                }

                await _goodRepository.DeleteAsync(good.Id);

                response.Data = true;
                response.StatusCode = StatusCode.OK;
                return response;
            }
            catch (Exception ex)
            {
                response.Description = $"[DeleteGood]: {ex.Message}";
                response.StatusCode = StatusCode.InternalServerError;
                return response;
            }
        }

        public IBaseResponse<List<Good>> GetGoods()
        {
            var response = new BaseResponse<List<Good>>();
            try
            {
                var goods = _goodRepository.GetAllAsync().Result.ToList();
                response.Data = goods;
                response.StatusCode = StatusCode.OK;
                return response;
            }
            catch (Exception ex)
            {
                response.Description = $"[GetGoods]: {ex.Message}";
                response.StatusCode = StatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<BaseResponse<Dictionary<long, string>>> GetGood(string term)
        {
            var response = new BaseResponse<Dictionary<long, string>>();

            try
            {
                var goods = await _goodRepository.GetAllAsync();

                var result = goods
                    .Where(x => x.Name.ToLower().Contains(term.ToLower()))
                    .ToDictionary(k => (long)k.Id, v => v.Name);

                response.Data = result;
                response.StatusCode = StatusCode.OK;
                return response;
            }
            catch (Exception ex)
            {
                response.Description = $"[GetGood(term)]: {ex.Message}";
                response.StatusCode = StatusCode.InternalServerError;
                return response;
            }
        }
    }
}
