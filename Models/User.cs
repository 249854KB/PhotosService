using System.ComponentModel.DataAnnotations;

namespace PhotosService.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int ExternalID{ get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int RankInSystem { get; set; }
        [Required]
        public int NumberOfDogs { get; set; }
       
        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
    }
}