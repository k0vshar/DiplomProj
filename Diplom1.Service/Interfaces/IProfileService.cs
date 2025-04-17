using Diplom.Domain;
using Diplom.Domain.Response;
using Diplom.Domain.ViewModels.Profile;

namespace Diplom.Service.Interfaces
{
    public interface IProfileService
    {
        Task<BaseResponse<ProfileViewModel>> GetProfile(string userName);

        Task<BaseResponse<Profile>> Save(ProfileViewModel model);
    }
}
