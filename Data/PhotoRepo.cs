using PhotosService.Models;

namespace PhotosService.Data
{
    public class PhotoRepo : IPhotoRepo
    {
        private readonly AppDbContext _context;

        public PhotoRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreatePhoto(int userId, Photo photo)
        {
            if(photo == null)
            {
                throw new ArgumentNullException(nameof(photo));
            }
            photo.UserId = userId;
            _context.Photos.Add(photo);

        }

        public void CreateUser(User user)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.Users.Add(user);
        }

        public bool ExternalUserExists(int externalUserId)
        {
             return _context.Users.Any(u => u.ExternalID == externalUserId);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public Photo GetPhoto(int userId, int photoId)
        {
            return _context.Photos
                .Where(f => f.UserId == userId && f.Id == photoId).FirstOrDefault();
        }

        public IEnumerable<Photo> GetPhotosForUser(int userId)
        {
            return _context.Photos.Where(f=> f.UserId == userId)
            .OrderBy(f=>f.User.Name);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(u => u.Id == userId);
        }
    }
}