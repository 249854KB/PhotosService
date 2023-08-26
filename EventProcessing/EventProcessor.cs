using System;
using System.Text.Json;
using AutoMapper;
using PhotosService.Data;
using PhotosService.Dtos;
using PhotosService.Models;
using Microsoft.Extensions.DependencyInjection;

namespace PhotosService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, AutoMapper.IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.DogPublished:
                    addDog(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notifcationMessage)
        {
            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notifcationMessage);

            switch(eventType.Event)
            {
                case "Dog_Published":
                    Console.WriteLine("--> Dog Published Event Detected");
                    return EventType.DogPublished;
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

        private void addDog(string dogPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IPhotoRepo>();
                
                var dogPublishedDto = JsonSerializer.Deserialize<DogPublishedDto>(dogPublishedMessage);

                try
                {
                    var plat = _mapper.Map<Dog>(dogPublishedDto);
                    if(!repo.ExternalDogExists(plat.ExternalID))
                    {
                        repo.CreateDog(plat);
                        repo.SaveChanges();
                        Console.WriteLine("--> Dog added!");
                    }
                    else
                    {
                        Console.WriteLine("--> Dog already exisits...");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not add Dog to DB {ex.Message}");
                }
            }
        }
    }

    enum EventType
    {
        DogPublished,
        Undetermined
    }
}