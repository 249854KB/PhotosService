using AutoMapper;
using PhotosService.Data;
using PhotosService.Dtos;
using PhotosService.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace PhotosService.Controllers
{
    [Route("api/p/[controller]")]
    [ApiController]
    public class DogsController: ControllerBase
    {
        private readonly IPhotoRepo _repository;
        private readonly IMapper _mapper;

        public DogsController(IPhotoRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<DogReadDto>> GetDogs()
        {
            Console.WriteLine("-->> Getting Dog From Photo service");
            var dogItems = _repository.GetAllDogs();
            return Ok(_mapper.Map<IEnumerable<DogReadDto>>(dogItems));
        }
        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inboud POST # Command Service");
            return Ok("Inmbound test ok for photos controller");
        }
        //Https and grcp is synchronius
    }
}