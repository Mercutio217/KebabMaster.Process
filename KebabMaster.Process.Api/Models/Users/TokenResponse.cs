namespace KebabMaster.Process.Api.Models.Users;

public class TokenResponse
{
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }    
}