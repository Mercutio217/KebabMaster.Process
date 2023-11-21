using KebabMaster.Process.Domain.Entities;
using KebabMaster.Process.Domain.Exceptions;

namespace KebabMaster.Process.Domain.Models;

public class OrderUpdateModel
{
    public int Id { get; private set; }
    public Address Address { get; private set; }
    public IEnumerable<OrderItem> OrderItems { get; private set; }

    public OrderUpdateModel(int id, Address address, IEnumerable<OrderItem> orderItems)
    {
        Id = id;
        Address = address;
        OrderItems = orderItems;
    }

    public static OrderUpdateModel Create(int id, Address address, IEnumerable<OrderItem> items)
    {

        return new(id, address, items);
    }
}