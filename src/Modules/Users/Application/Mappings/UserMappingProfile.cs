using AutoMapper;
using Users.Application.DTOs;
using Users.Domain.Entities;
using Users.Infra.Entities;

namespace Users.Application.Mappings
{
    public class UserMappingProfile: Profile
    {
        public UserMappingProfile() {
            // Domain <=> DTO
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserProfile, UserProfileDto>().ReverseMap();
            // Domain <=> Infra Entity
            CreateMap<User, UserEntity>().ReverseMap();
            CreateMap<UserProfile, UserProfileEntity>().ReverseMap();
        }
    }
}
