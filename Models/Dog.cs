using System.ComponentModel.DataAnnotations;

namespace PhotosService.Models
{
    public class Dog
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int ExternalID{ get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Race { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public int OwnersId { get; set; }
       
        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
    }
}