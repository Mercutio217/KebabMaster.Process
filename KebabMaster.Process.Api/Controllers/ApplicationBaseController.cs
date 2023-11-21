﻿using KebabMaster.Process.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace KebabMaster.Process.Api.Controllers;

public abstract class ApplicationBaseController  : ControllerBase
{
    protected async Task<IActionResult> Execute(Func<Task> function, ActionResult actionResult)
    {
        try
        {
            await function();
            
            return actionResult;
        }
        catch (NotFoundException)
        {
            return StatusCode(StatusCodes.Status404NotFound);
        }
        catch (ApplicationValidationException validationException)
        {
            return StatusCode(StatusCodes.Status400BadRequest, validationException.GetValidationErrorMessage());
        }
        catch (Exception exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    protected async Task<ActionResult<T>> Execute<T>(Func<Task<T>> function)
    {
        try
        {
            return await function();
        }
        catch (NotFoundException)
        {
            return StatusCode(StatusCodes.Status404NotFound);
        }
        catch (ApplicationValidationException validationException)
        {
            return StatusCode(StatusCodes.Status400BadRequest, validationException.GetValidationErrorMessage());
        }
        catch (Exception exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}