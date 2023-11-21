namespace KebabMaster.Process.Api.Models.Orders;

public class OrderUpdateRequest
{
    public int Id { get; set; }
    public AddressDto Address { get; set; }
    public IEnumerable<OrderItemDto> OrderItems { get; set; }
}