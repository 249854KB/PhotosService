using PhotosService.Models;

namespace PhotosService.Data
{
    public interface IPhotoRepo
    {
        bool SaveChanges();
        IEnumerable<Dog> GetAllDogs();
        void CreateDog(Dog dog);
        bool DogExists(int externalDogId);

        bool ExternalDogExists(int dogId);

        IEnumerable<Photo> GetPhotosOfDog(int userId, int dogId);
        Photo GetPhotoOfDog(int userId, int dogId, int photoId);
        void CreatePhoto(int dogId, Photo photo);
    }
}