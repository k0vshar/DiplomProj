using Diplom.DAL;
using Diplom.Domain;
using Diplom.Domain.Entities;
using Diplom.Domain.Enum;
using Diplom.Domain.Response;
using Diplom.Domain.ViewModels.Profile;
using Diplom.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Diplom.Service.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly AppDBContext _context;

        public ProfileService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<ProfileViewModel>> GetProfile(string userName)
        {
            try
            {
                var profile = await _context.Profiles
                    .Include(p => p.User)
                    .FirstOrDefaultAsync(p => p.User.Name == userName);

                if (profile == null)
                {
                    return new BaseResponse<ProfileViewModel>
                    {
                        Description = "Профиль не найден",
                        StatusCode = StatusCode.ProfileNotFound
                    };
                }

                var viewModel = new ProfileViewModel
                {
                    Id = profile.Id,
                    Age = profile.Age,
                    Address = profile.Address,
                    UserName = profile.User.Name
                };

                return new BaseResponse<ProfileViewModel>
                {
                    Data = viewModel,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProfileViewModel>
                {
                    Description = $"[GetProfile] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Profile>> Save(ProfileViewModel model)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == model.UserName);
                if (user == null)
                {
                    return new BaseResponse<Profile>
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var profile = await _context.Profiles
                    .FirstOrDefaultAsync(p => p.UserId == user.Id);

                if (profile != null)
                {
                    profile.Age = model.Age;
                    profile.Address = model.Address;
                }
                else
                {
                    profile = new Profile
                    {
                        Age = model.Age,
                        Address = model.Address,
                        UserId = user.Id
                    };
                    await _context.Profiles.AddAsync(profile);
                }

                await _context.SaveChangesAsync();

                return new BaseResponse<Profile>
                {
                    Data = profile,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Profile>
                {
                    Description = $"[Save] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
