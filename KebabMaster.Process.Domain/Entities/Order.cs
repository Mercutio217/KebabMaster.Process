using KebabMaster.Process.Domain.Entities.Base;
using KebabMaster.Process.Domain.Exceptions;
using KebabMaster.Process.Domain.Tools;

namespace KebabMaster.Process.Domain.Entities;

public class Order : Entity
{
    public string Email { get; private set; }
    public Address Address { get; private set; }
    public IEnumerable<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();
    private Order() { }

    private Order(string email, Address address, IEnumerable<OrderItem> orderItems)
    {
        Email = email;
        Address = address;
        OrderItems = orderItems;
    }

    public static Order Create(string email, Address address, IEnumerable<OrderItem> orderItems)
    {
        EmailValidator.Validate(email);

        return new Order(email, address, orderItems);
    }

    public void UpdateAddress(Address address)
    {
        Address = address ?? throw new MissingMandatoryPropertyException<Order>(nameof(Address));
    }

    public void UpdateOrderItems(IEnumerable<OrderItem> orderItems)
    {
        OrderItems = orderItems ?? throw new MissingMandatoryPropertyException<Order>(nameof(Address));
    }
}