using AutoMapper;
using KebabMaster.Process.Api.Models.Orders;
using KebabMaster.Process.Domain.Entities;
using KebabMaster.Process.Domain.Filters;
using KebabMaster.Process.Domain.Models;

namespace KebabMaster.Process.Api.Mappings;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderRequest, Order>()
            .ConvertUsing(request =>
                Order.Create(request.Email,
                    Address.Create(request.Address.StreetName, request.Address.StreetNumber,
                        request.Address.FlatNumber),
                    request.OrderItems.Select(item => OrderItem.Create(item.MenuItemId, item.Quantity)).ToList()));

        CreateMap<Order, OrderResponse>()
            .ConvertUsing(order => new OrderResponse()
            {
                Email = order.Email,
                Id = order.Id,
                OrderItems = order.OrderItems.Select(item => new OrderItemDto()
                {
                    Quantity = item.Quantity,
                    MenuItemId = item.MenuItemId
                }).ToList()
            });
        
         CreateMap<OrderUpdateRequest, OrderUpdateModel>()
             .ConvertUsing(request =>
             
                OrderUpdateModel.Create(request.Id,
                    Address.Create(request.Address.StreetName, request.Address.StreetNumber,
                        request.Address.FlatNumber),
                    request.OrderItems.Select(item => OrderItem.Create(item.MenuItemId, item.Quantity)).ToList())
             );
         
         CreateMap<OrderFilterRequest, OrderFilter>();
    }
}
