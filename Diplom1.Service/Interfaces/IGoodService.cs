using Diplom.Domain.Entities;
using Diplom.Domain.Response;
using Diplom.Domain.ViewModels.Good;

namespace Diplom.Service.Interfaces
{
    public interface IGoodService
    {
        BaseResponse<Dictionary<int, string>> GetTags();

        IBaseResponse<List<Good>> GetGoods();

        Task<IBaseResponse<GoodViewModel>> GetGood(long id);

        Task<BaseResponse<Dictionary<long, string>>> GetGood(string term);

        Task<IBaseResponse<Good>> Create(GoodViewModel good, byte[] imageData);

        Task<IBaseResponse<bool>> DeleteGood(long id);

        Task<IBaseResponse<Good>> Edit(long id, GoodViewModel model);
    }
}
