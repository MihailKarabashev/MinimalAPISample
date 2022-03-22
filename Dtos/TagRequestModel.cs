using System.ComponentModel.DataAnnotations;

namespace MinimalAPISample.Dtos
{
    public class TagRequestModel
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public int NewsId { get; set; }
    }
}
