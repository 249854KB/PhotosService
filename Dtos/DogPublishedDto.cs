namespace PhotosService
{
    public class DogPublishedDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public dateTime DateOfBirth  { get; set; }
        public string Race { get; set; }
        public int OwnersId { get; set; }
        public string Event { get; set; }
    }
}