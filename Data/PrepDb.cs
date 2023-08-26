using PhotosService.Models;
using PhotosService.SyncDataServices.Grpc;
using System;

namespace PhotosService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            using(var servicesScope  = applicationBuilder.ApplicationServices.CreateScope())
            {
                var grpcClient = servicesScope.ServiceProvider.GetService<IDogDataClient>();
                var dogs = grpcClient.ReturnAllDogs();
                SeedData(servicesScope.ServiceProvider.GetService<IPhotoRepo>(),dogs);
            }
        }
        private static void SeedData(IPhotoRepo repo, IEnumerable<Dog> dogs)
        {
            Console.WriteLine("Seeding new dogs...");

            foreach (var dog in dogs)
            {
                if(!repo.ExternalDogExists(dog.ExternalID))
                {
                    repo.CreateDog(dog);
                }
                repo.SaveChanges();
            }
        }

        
    }
}