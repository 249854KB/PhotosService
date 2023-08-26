using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace PhotosService.Models
{
    public class Photo
    {
        [Key]
         [Required]
        public int Id { get; set; }
     
        [Required]
        public int DogId{ get; set; }
       
        public Dog dog { get; set; }
    }
}