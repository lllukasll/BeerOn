using AutoMapper;
using BeerOn.Data.DbModels;
using BeerOn.Data.ModelsDto;
using BeerOn.Data.ModelsDto.Beer;
using BeerOn.Data.ModelsDto.Brewery;
using BeerOn.Data.ModelsDto.User;

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

            CreateMap<Brewery, GetBreweryDto>();
            CreateMap<GetBreweryDto, Brewery>();

            CreateMap<Brewery, SaveBreweryDto>();
            CreateMap<SaveBreweryDto, Brewery>()
                .ForMember(g => g.Id, opt => opt.Ignore());

            CreateMap<BeerType, SaveBeerTypeDto>();
            CreateMap<SaveBeerTypeDto, BeerType>()
                .ForMember(g => g.Id, opt => opt.Ignore());

            CreateMap<Beer, SaveBeerDto>();
            CreateMap<SaveBeerDto, Beer>();

            CreateMap<Beer, GetBeerDto>();
            CreateMap<GetBeerDto, Beer>();
            CreateMap<UploadAvatarDto, Beer>();
        }
    }
}
