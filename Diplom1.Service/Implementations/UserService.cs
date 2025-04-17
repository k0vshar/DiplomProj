using Diplom.DAL;
using Diplom.Domain.Entities;
using Diplom.Domain.Response;
using Diplom.Domain.ViewModels.User;
using Diplom.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly AppDBContext _context;

        public UserService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IBaseResponse<User>> Create(UserViewModel model)
        {
            try
            {
                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password, // в реальном проекте нужно хэшировать!
                    Role = model.Role
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return new BaseResponse<User>
                {
                    Data = user,
                    Description = "Пользователь успешно создан",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>
                {
                    Description = $"[Create] Ошибка: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public BaseResponse<Dictionary<int, string>> GetRoles()
        {
            try
            {
                var roles = Enum.GetValues(typeof(Role))
                    .Cast<Role>()
                    .ToDictionary(k => (int)k, v => v.ToString());

                return new BaseResponse<Dictionary<int, string>>
                {
                    Data = roles,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>
                {
                    Description = $"[GetRoles] Ошибка: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers()
        {
            try
            {
                var users = await _context.Users.Select(u => new UserViewModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    Name = u.Name,
                    Role = u.Role
                }).ToListAsync();

                return new BaseResponse<IEnumerable<UserViewModel>>
                {
                    Data = users,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<UserViewModel>>
                {
                    Description = $"[GetUsers] Ошибка: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteUser(long id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                {
                    return new BaseResponse<bool>
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return new BaseResponse<bool>
                {
                    Data = true,
                    Description = "Пользователь успешно удалён",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Description = $"[DeleteUser] Ошибка: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}