using System;
using System.Collections.Generic;
using AutoMapper;
using PhotosService.Models;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using DogsService;

namespace PhotosService.SyncDataServices.Grpc
{
    public class DogDataClient : IDogDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public DogDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public IEnumerable<Dog> ReturnAllDogs()
        {
            Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcDog"]}");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcDog"]);
            var client = new GrpcDog.GrpcDogClient(channel);
            var request = new GetAllRequestDog();

            try
            {
                var reply = client.GetAllDogs(request);
                return _mapper.Map<IEnumerable<Dog>>(reply.Dog);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldnot call GRPC Server {ex.Message}");
                return null;
            }
        }
    }
}