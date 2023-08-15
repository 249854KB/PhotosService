using PhotosService.Models;

namespace PhotosService.Data
{
    public interface IPhotoRepo
    {
        bool SaveChanges();
        IEnumerable<User> GetAllUsers();
        void CreateUser(User user);
        bool UserExists(int externalUserId);

        bool ExternalUserExists(int userId);

        IEnumerable<Photo> GetPhotosForUser(int userId);
        Photo GetPhoto(int userId, int photoId);
        void CreatePhoto(int userId, Photo photo);
    }
}