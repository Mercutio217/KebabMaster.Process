﻿
namespace KebabMaster.Process.Api.Models.Orders;

public class OrderRequest
{
    public string? Email { get; set; }
    public AddressDto? Address { get; set; }
    public IEnumerable<OrderItemDto>? OrderItems { get; set; }
}