using System.Collections.Generic;
using PhotosService.Models;

namespace PhotosService.SyncDataServices.Grpc
{
    public interface IUserDataClient
    {
        IEnumerable<User> ReturnAllUsers();
    }
}