using System;
using System.Collections.Generic;
using AutoMapper;
using PhotosService.Data;
using PhotosService.Dtos;
using PhotosService.Models;
using Microsoft.AspNetCore.Mvc;

namespace PhotosService.Controllers
{
    [Route("api/photos/dogs/{dogId}/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoRepo _repository;
        private readonly IMapper _mapper;

        public PhotosController(IPhotoRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PhotoReadDto>> GetPhotosOfDog(int dogId)
        {
            Console.WriteLine($"--> Hit GetPhotosOfDog: {dogId}");

            if (!_repository.DogExists(dogId))
            {
                return NotFound();
            }

            var photos = _repository.GetPhotosOfDog(dogId);

            return Ok(_mapper.Map<IEnumerable<PhotoReadDto>>(photos));
        }

        [HttpGet("{photoId}", Name = "GetPhotoOfDog")]
        public ActionResult<PhotoReadDto> GetPhotoOfDog(int dogId, int photoId)
        {
            Console.WriteLine($"--> Hit GetPhotoOfDog: {dogId} / {photoId}");

            if (!_repository.DogExists(dogId))
            {
                return NotFound();
            }

            var photo = _repository.GetPhoto(dogId, photoId);

            if(photo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PhotoReadDto>(photo));
        }

        [HttpPost]
        public ActionResult<PhotoReadDto> AddPhotoOfDog(int dogId, PhotoCreateDto photoDto)
        {
             Console.WriteLine($"--> Hit CreatePhotoOfDog: {dogId}");

            if (!_repository.DogExists(dogId))
            {
                return NotFound();
            }

            var photo = _mapper.Map<Photo>(photoDto);

            _repository.CreatePhoto(dogId, photo);
            _repository.SaveChanges();

            var photoReadDto = _mapper.Map<PhotoReadDto>(photo);

            return CreatedAtRoute(nameof(GetPhotoOfDog),
                new {dogId = dogId, photoId = photoReadDto.Id}, photoReadDto);
        }

    }
}