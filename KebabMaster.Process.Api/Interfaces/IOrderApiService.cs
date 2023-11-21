using KebabMaster.Process.Api.Models;
using KebabMaster.Process.Api.Models.Orders;
using KebabMaster.Process.Domain.Entities;
using KebabMaster.Process.Domain.Filters;

namespace KebabMaster.Process.Api.Interfaces;

public interface IOrderApiService
{
    public Task CreateOrder(OrderRequest order);
    public Task<ApplicationResponse<OrderResponse>> GetOrdersAsync(OrderFilter filter);
    public Task<OrderResponse> GetOrderById(int id);
    public Task DeleteOrder(int id);
    public Task UpdateOrder(OrderUpdateRequest order);
    Task<IEnumerable<MenuItem>> GetMenuItems();
}