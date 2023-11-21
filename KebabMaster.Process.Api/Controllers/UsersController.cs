﻿using KebabMaster.Process.Api.Interfaces;
using KebabMaster.Process.Api.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KebabMaster.Process.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ApplicationBaseController
{
    private readonly IUserManagementService _service;

    public UsersController(IUserManagementService service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<TokenResponse>> Login(
        [FromBody] LoginModel model
        )
    {
        return await Execute(() =>  _service.Login(model));
    }
    
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterModel model
    )
    {
        return await Execute(() => _service.CreateUser(model), Ok());
    }
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [Route("users")]
    public async Task<ActionResult<IEnumerable<UserResponse>>> Get(
        [FromQuery] UserRequest model
    )
    {
        return await Execute(() => _service.GetByFilter(model));
    }
    
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("users/{email}")]
    public async Task<IActionResult> Delete(
        [FromRoute] string email
    )
    {
        return await Execute(() => _service.DeleteUser(email), Ok());
    }

}