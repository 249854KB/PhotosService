using AutoMapper;
using PhotosService.Data;
using PhotosService.Dtos;
using PhotosService.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace PhotosService.Controllers
{
    [Route("api/f/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {
        private readonly IPhotoRepo _repository;
        private readonly IMapper _mapper;

        public UsersController(IPhotoRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<UserReadDto>> GetUsers()
        {
            Console.WriteLine("-->> Getting User From Photo service");
            var userItems = _repository.GetAllUsers();
            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(userItems));
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