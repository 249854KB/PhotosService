namespace PhotosService.Dtos
{
    public class DogReadDto
    {
        public int Id { get; set;}
        public string Name { get; set;}
        public DateTime DateOfBirth  { get; set; }
        public string Race { get; set; }
        public int OwnersId { get; set; }

    }
}