using KebabMaster.Process.Domain.Entities;

namespace KebabMaster.Process.Domain.Interfaces;

public interface IMenuRepository
{
    public Task<MenuItem> GetMenuItemById(int id);
    Task<IEnumerable<MenuItem>> GetMenuItems();
}