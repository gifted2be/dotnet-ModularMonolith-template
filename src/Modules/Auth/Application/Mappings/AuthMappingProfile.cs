using Auth.Application.DTOs.RequestModel;
using AutoMapper;
using ModularMonolith.Template.SharedKernel.Auth;
using Users.Application.DTOs;

namespace Auth.Application.Mappings
{
    public class AuthMappingProfile: Profile
    {
        public AuthMappingProfile() {
            // Login <=> UserDto
            CreateMap<LoginDto, UserDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ReverseMap();

            CreateMap<RegisterDto, UserDto>()
                .ForMember(dest => dest.Profile, opt => opt.MapFrom(src => new UserProfileDto()))
                .ReverseMap();

            CreateMap<UserDto, JwtUserInfo>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "Client"))
                .ReverseMap();
        }
    }
}
