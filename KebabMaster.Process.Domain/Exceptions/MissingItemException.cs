namespace KebabMaster.Process.Domain.Exceptions;

public class MissingItemException : ApplicationValidationException
{
    private readonly int _missingId;
    
    public MissingItemException(int id) : base($"Requested item with id {id} is missing from db")
    {
        _missingId = id;
    }
    
    public override string GetValidationErrorMessage() => $"One of the items ({_missingId}) is missing!";
}