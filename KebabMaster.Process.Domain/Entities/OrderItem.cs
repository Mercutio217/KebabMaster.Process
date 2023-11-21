using KebabMaster.Process.Domain.Entities.Base;
using KebabMaster.Process.Domain.Exceptions;

namespace KebabMaster.Process.Domain.Entities;

public class OrderItem : Entity
{
    public int MenuItemId { get; set; }
    public int Quantity { get; set; }
    
    private OrderItem() { }

    private OrderItem(int menuItemId, int quantity)
    {
        MenuItemId = menuItemId;
        Quantity = quantity;
    }

    public static OrderItem Create(int menuItemId, int quantity)
    {
        if (menuItemId == default)
            throw new MissingMandatoryPropertyException<MenuItem>(nameof(MenuItemId));

        if (quantity > 50 || quantity < 1)
            throw new InvalidQuantityOfProperty(nameof(Quantity), quantity);
        
        return new OrderItem(menuItemId, quantity);
    }
}