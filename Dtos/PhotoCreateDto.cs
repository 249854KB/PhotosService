using System.ComponentModel.DataAnnotations;

namespace PhotosService.Dtos
{
    public class PhotoCreateDto
    {
        [Required]
        public string HowTo { get; set; }

        [Required]
        public string CommandLine { get; set; }
    }
}