﻿using KebabMaster.Process.Domain.Entities;
using KebabMaster.Process.Domain.Exceptions;
using KebabMaster.Process.Domain.Filters;
using KebabMaster.Process.Domain.Interfaces;
using KebabMaster.Process.Domain.Models;

namespace KebabMaster.Process.Domain.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IMenuRepository _menuRepository;
    
    public OrderService(IOrderRepository repository, IMenuRepository menuRepository)
    {
        _repository = repository;
        _menuRepository = menuRepository;
    }

    public async Task CreateOrder(Order order)
    {
        await ValidateOrderItems(order.OrderItems);
        
        await _repository.CreateOrder(order);
    }

    private async Task ValidateOrderItems(IEnumerable<OrderItem> orderOrderItems)
    {
        MenuItem menuItem;
        
        foreach (var item in orderOrderItems)
        {
            menuItem = await _menuRepository.GetMenuItemById(item.MenuItemId);
            if (menuItem is null)
                throw new MissingItemException(item.MenuItemId);
        }
    }

    public Task<IEnumerable<Order>> GetOrdersAsync(OrderFilter filter)
    {
        return _repository.GetOrdersAsync(filter);
    }

    public Task<Order> GetOrderByIdAsync(int id)
    {
        return _repository.GetOrderById(id);
    }

    public Task DeleteOrder(int id)
    {
        return _repository.DeleteOrder(id);
    }

    public Task UpdateOrder(OrderUpdateModel order)
    {
        return _repository.UpdateOrder(order);
    }
}