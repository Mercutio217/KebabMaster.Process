using AutoMapper;
using KebabMaster.Process.Api.Models.Users;
using KebabMaster.Process.Domain.Entities;
using KebabMaster.Process.Domain.Filters;

namespace KebabMaster.Process.Api.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserRequest, UserFilter>();
        CreateMap<User, UserResponse>();
    }
}