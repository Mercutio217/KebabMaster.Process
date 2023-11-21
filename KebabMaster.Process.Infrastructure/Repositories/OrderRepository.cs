using KebabMaster.Process.Domain.Entities;
using KebabMaster.Process.Domain.Exceptions;
using KebabMaster.Process.Domain.Filters;
using KebabMaster.Process.Domain.Interfaces;
using KebabMaster.Process.Domain.Models;
using KebabMaster.Process.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace KebabMaster.Process.Infrastructure.Repositories;

public class OrderRepository  : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateOrder(Order order)
    {
        _context.Database.EnsureCreated();
        await _context.AddAsync(order);

        _context.SaveChanges();
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync(OrderFilter filter)
    {
        return Applyfilter(_context.Orders.Include(ord => ord.OrderItems)
            .Include(ord => ord.Address), filter);
    }

    private IQueryable<Order> Applyfilter(IQueryable<Order> collection, OrderFilter filter)
    {
        if (filter.Email is not null)
            collection = collection.Where(ord => ord.Email == filter.Email);
        if (filter.SteetName is not null)
            collection = collection.Where(ord => ord.Address.StreetName == filter.SteetName);

        return collection;
    }

    public async Task<Order> GetOrderById(int id)
    {
        return await _context.Orders.Include(ord => ord.OrderItems)
            .Include(ord => ord.Address).FirstAsync(order => order.Id == id);
    }

    public async Task DeleteOrder(int id)
    {
        var result = await _context.Orders.Include(ord => ord.OrderItems)
            .Include(ord => ord.Address).FirstAsync(order => order.Id == id);

        _context.Remove(result);

        _context.SaveChanges();
    }

    public async Task UpdateOrder(OrderUpdateModel order)
    {
        Order previousOrder = await GetOrderById(order.Id);
        if (previousOrder is null)
            throw new NotFoundException(order.Id.ToString());
        
        previousOrder.UpdateAddress(order.Address);
        previousOrder.UpdateOrderItems(order.OrderItems);

        _context.SaveChanges();
    }
}