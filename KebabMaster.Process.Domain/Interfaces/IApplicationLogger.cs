using KebabMaster.Process.Domain.Exceptions;

namespace KebabMaster.Process.Domain.Interfaces;

public interface IApplicationLogger
{
    void LogGetStart(object request);
    void LogGetEnd(object request);
    void LogPostStart(object request);
    void LogPostEnd(object request);
    void LogDeleteStart(object request);
    void LogDeleteEnd(object request);
    void LogPutEnd(object request);
    void LogPutStart(object request);
    void LogException(Exception exception);
    void LogValidationException(ApplicationValidationException applicationValidationException);
    void LogRegistrationStart(object request);
    void LogRegistrationEnd(object request);
    void LogLoginStart(object request);
    void LogLoginEnd(object request);
    
}