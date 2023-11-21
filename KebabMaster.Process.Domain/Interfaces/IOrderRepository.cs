using KebabMaster.Process.Domain.Entities;
using KebabMaster.Process.Domain.Filters;
using KebabMaster.Process.Domain.Models;

namespace KebabMaster.Process.Domain.Interfaces;

public interface IOrderRepository
{
    public Task CreateOrder(Order order);
    public Task<IEnumerable<Order>> GetOrdersAsync(OrderFilter filter);
    public Task<Order> GetOrderById(int id);
    public Task DeleteOrder(int id);
    public Task UpdateOrder(OrderUpdateModel order);
}