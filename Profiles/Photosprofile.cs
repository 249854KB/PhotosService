using AutoMapper;
using PhotosService.Models;
using PhotosService.Dtos;
using DogsService;

namespace PhotosService.Profiles
{
    public class PhotosProfile : Profile
    {
        public PhotosProfile()
        {
            //Source -> target
            CreateMap<Dog, DogReadDto>();
            CreateMap<PhotoCreateDto, Photo>();
            CreateMap<Photo, PhotoReadDto>();
            CreateMap<DogPublishedDto, Dog>()
                .ForMember(destination =>destination.ExternalID, opt => opt.MapFrom(source => source.Id));
            CreateMap<GrpcDogModel, Dog>()
            .ForMember(destination => destination.ExternalID, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Photos, opt =>opt.Ignore())
            .ForMember(dest => dest.Race, opt =>opt.MapFrom(src => src.Race))
            .ForMember(dest => dest.OwnersId, opt =>opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.DateOfBirth, opt =>opt.MapFrom(src => src.DateOfBirth));


        }
    }
}