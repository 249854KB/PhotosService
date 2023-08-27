using System;
using System.Collections.Generic;
using AutoMapper;
using PhotosService.Data;
using PhotosService.Dtos;
using PhotosService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace PhotosService.Controllers
{
    [Route("api/p/users/{userId}/dogs/{dogId}/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
         private readonly IWebHostEnvironment _environment;
        private readonly IPhotoRepo _repository;
        private readonly IMapper _mapper;

        public PhotosController(IWebHostEnvironment environment, IPhotoRepo repository, IMapper mapper)
        {
            _environment = environment;
            _repository = repository;
            _mapper = mapper;
            Console.WriteLine("Current dir is: " + Directory.GetCurrentDirectory());
        }

        [HttpGet]
        public async Task<IActionResult> GetPhotosOfDog(int userId, int dogId)
        {
             Console.WriteLine($"--> Hit GetPhotosOfDog: dog {dogId} of user {userId}");
            List<string> Imageurl = new List<string>();
            string hosturl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            try

            {
                string Filepath = GetFilepath(userId,dogId);

                if(System.IO.Directory.Exists(Filepath))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(Filepath);
                    FileInfo[] fileInfos=directoryInfo.GetFiles();
                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        string filename = fileInfo.Name;
                        string imagepath = Filepath + "//" + filename;
                        if (System.IO.File.Exists(imagepath))
                        {
                            Console.WriteLine($"--> Found image");
                           string _Imageurl = hosturl + "/Upload/users/" + userId + "/dogs/"+ dogId +"/" + filename;
                            Imageurl.Add(_Imageurl);
                        }
                        else
                        {
                            Console.WriteLine($"--> Image not found");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Error acured: {ex}");
                return NotFound();
            }
            return Ok(Imageurl);

        }


        // [HttpGet("{photoId}", Name = "GetPhotoOfDog")]
        // public ActionResult<PhotoReadDto> GetPhotoOfDog(int dogId, int photoId)
        // {
        //     Console.WriteLine($"--> Hit GetPhotoOfDog: {dogId} / {photoId}");

        //     if (!_repository.DogExists(dogId))
        //     {
        //         return NotFound();
        //     }

        //     var photo = _repository.GetPhoto(dogId, photoId);

        //     if(photo == null)
        //     {
        //         return NotFound();
        //     }

        //     return Ok(_mapper.Map<PhotoReadDto>(photo));
        // }

        [HttpPut]
        public async Task<IActionResult> UploadImageofDog(IFormFile formFile, int userId, int dogId)
        { 
            
            try
            {
                string Filepath = GetFilepath(userId,dogId);
                if (!System.IO.Directory.Exists(Filepath))
                {
                    System.IO.Directory.CreateDirectory(Filepath);
                }

                string imagepath = Filepath + "/" + formFile.FileName;
                if (System.IO.File.Exists(imagepath))
                {
                    System.IO.File.Delete(imagepath);
                }
                using (FileStream stream=System.IO.File.Create(imagepath))
                {
                    await formFile.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                 Console.WriteLine($"--> Error acured: {ex}");
                return NotFound();
            }

            return Ok();
            // return CreatedAtRoute(nameof(GetPhotoOfDog),
            //     new {dogId = dogId, photoId = id}, photoReadDto);
        }

        [NonAction]
        private string GetFilepath(int userId, int dogId)
        {
            return this._environment.WebRootPath + "/Upload/users/" + userId + "/dogs/" + dogId;
        }
    }

}