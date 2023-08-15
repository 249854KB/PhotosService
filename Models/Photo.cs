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
        public string Title { get; set; }
         [Required]
        public string Text { get; set; }
         [Required]
        public int UserId{ get; set; }
        public int DogsId{ get; set; }
        [Required]
        public int Date{ get; set; }
        public User User { get; set; }
    }
}