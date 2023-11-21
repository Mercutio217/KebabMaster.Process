using KebabMaster.Process.Domain.Entities;
using KebabMaster.Process.Domain.Filters;

namespace KebabMaster.Process.Domain.Interfaces;

public interface IUserRepository
{
    Task CreateUser(User user);
    Task<IEnumerable<User>> GetUserByFilter(UserFilter filter);
    Task<User> GetUserByEmail(string email);
    Task DeleteUser(string email);
    Task<Role?> GetRoleByName(string name);
}