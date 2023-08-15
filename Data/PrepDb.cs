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
                var grpcClient = servicesScope.ServiceProvider.GetService<IUserDataClient>();
                var users = grpcClient.ReturnAllUsers();
                SeedData(servicesScope.ServiceProvider.GetService<IPhotoRepo>(),users);
            }
        }
        private static void SeedData(IPhotoRepo repo, IEnumerable<User> users)
        {
            Console.WriteLine("Seeding new users...");

            foreach (var user in users)
            {
                if(!repo.ExternalUserExists(user.ExternalID))
                {
                    repo.CreateUser(user);
                }
                repo.SaveChanges();
            }
        }

        
    }
}