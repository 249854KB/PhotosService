using System.ComponentModel.DataAnnotations;

namespace PhotosService.Dtos
{
    public class PhotoCreateDto
    {
        [Required]
        int Id { get; set; }
    }
}