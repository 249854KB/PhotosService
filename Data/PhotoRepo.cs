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
        public void CreatePhoto(int dogId, Photo photo)
        {
            if(photo == null)
            {
                throw new ArgumentNullException(nameof(photo));
            }
            photo.DogId = dogId;
            _context.Photos.Add(photo);

        }

        public void CreateDog(Dog dog)
        {
            if(dog == null)
            {
                throw new ArgumentNullException(nameof(dog));
            }
            _context.Dogs.Add(dog);
        }

        public bool ExternalDogExists(int externalDogId)
        {
             return _context.Dogs.Any(u => u.ExternalID == externalDogId);
        }

        public IEnumerable<Dog> GetAllDogs()
        {
            return _context.Dogs.ToList();
        }

        public Photo GetPhotoOfDog(int UserId, int dogId, int photoId)
        {
            return _context.Photos
                .Where(f => f.DogId == dogId && f.Id == photoId).FirstOrDefault();
        }

        public IEnumerable<Photo> GetPhotosOfDog(int userId, int dogId)
        {
            return _context.Photos.Where(f=> f.DogId == dogId)
            .OrderBy(f=>f.dog.Name);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool DogExists(int dogId)
        {
            return _context.Dogs.Any(u => u.Id == dogId);
        }
    }
}