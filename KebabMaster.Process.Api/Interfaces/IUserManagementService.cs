using KebabMaster.Process.Api.Models.Users;

namespace KebabMaster.Process.Api.Interfaces;

public interface IUserManagementService
{
    Task CreateUser(RegisterModel model);
    Task<TokenResponse> Login(LoginModel model);
    Task<IEnumerable<UserResponse>> GetByFilter(UserRequest request);
    Task DeleteUser(string email);

}