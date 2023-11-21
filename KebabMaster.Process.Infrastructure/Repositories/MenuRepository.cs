using KebabMaster.Process.Domain.Entities;
using KebabMaster.Process.Domain.Interfaces;
using KebabMaster.Process.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace KebabMaster.Process.Infrastructure.Repositories;

public class MenuRepository : IMenuRepository
{
    private readonly ApplicationDbContext _context;

    public MenuRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<MenuItem> GetMenuItemById(int id)
    {
        return _context.MenuItems.FirstOrDefaultAsync(item => item.Id == id);
    }

    public async Task<IEnumerable<MenuItem>> GetMenuItems()
    {
        return _context.MenuItems;
    }
}