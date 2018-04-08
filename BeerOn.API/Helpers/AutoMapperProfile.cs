using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BeerOn.Data.DbModels;
using BeerOn.Data.ModelsDto;

namespace BeerOn.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();

            CreateMap<UserRoleDto, UserRole>();
            CreateMap<UserRole, UserRoleDto>();

            CreateMap<UserToken, UserTokenDto>();
            CreateMap<UserTokenDto, UserToken>();

            CreateMap<CreateUserDto, User>();
            CreateMap<User, CreateUserDto>();

            CreateMap<LoginUserDto, User>()
                .ForMember(gdt => gdt.Id, opt => opt.MapFrom(g => g.UserId));

            CreateMap<GetUserDataDto, User>();
            CreateMap<User, GetUserDataDto>();

        }
    }
}
