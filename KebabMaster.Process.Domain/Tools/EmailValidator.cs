using System.Net.Mail;
using KebabMaster.Process.Domain.Exceptions;

namespace KebabMaster.Process.Domain.Tools;

public static class EmailValidator
{
    public static void Validate(string email)
    {
        try
        {
            _ = new MailAddress(email);
        }
        catch
        {
            throw new InvalidEmailFormatException(email);
        }
    }
}