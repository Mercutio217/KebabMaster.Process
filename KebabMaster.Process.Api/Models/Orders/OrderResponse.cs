namespace KebabMaster.Process.Api.Models.Orders;

public class OrderResponse
{
    public int Id { get; set; }
    public string Email { get; set; }
    public IEnumerable<OrderItemDto> OrderItems { get; set; }
}