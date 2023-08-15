using AutoMapper;
using PhotosService.Models;
using PhotosService.Dtos;
using UserService;

namespace PhotosService.Profiles
{
    public class PhotosProfile : Profile
    {
        public PhotosProfile()
        {
            //Source -> target
            CreateMap<User, UserReadDto>();
            CreateMap<PhotoCreateDto, Photo>();
            CreateMap<Photo, PhotoReadDto>();
            CreateMap<UserPublishedDto, User>()
                .ForMember(destination =>destination.ExternalID, opt => opt.MapFrom(source => source.Id));
            CreateMap<GrpcUserModel, User>()
            .ForMember(destination => destination.ExternalID, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Photos, opt =>opt.Ignore());


        }
    }
}