using System.Collections.Generic;
using PhotosService.Models;

namespace PhotosService.SyncDataServices.Grpc
{
    public interface IDogDataClient
    {
        IEnumerable<Dog> ReturnAllDogs();
    }
}