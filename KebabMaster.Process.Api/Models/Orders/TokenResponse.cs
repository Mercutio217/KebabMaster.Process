namespace KebabMaster.Process.Api.Models.Orders;

public class TokenResponse
{
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }    
}