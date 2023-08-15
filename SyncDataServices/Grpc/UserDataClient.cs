using System;
using System.Collections.Generic;
using AutoMapper;
using PhotosService.Models;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using UserService;

namespace PhotosService.SyncDataServices.Grpc
{
    public class UserDataClient : IUserDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public IEnumerable<User> ReturnAllUsers()
        {
            Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcUser"]}");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcUser"]);
            var client = new GrpcUser.GrpcUserClient(channel);
            var request = new GetAllRequest();

            try
            {
                var reply = client.GetAllUsers(request);
                return _mapper.Map<IEnumerable<User>>(reply.User);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldnot call GRPC Server {ex.Message}");
                return null;
            }
        }
    }
}