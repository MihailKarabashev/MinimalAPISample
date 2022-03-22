using System.ComponentModel.DataAnnotations;

namespace MinimalAPISample.Dtos
{
    public class NewsRequestModel
    {
        [MaxLength(40)]
        public string? Title { get; set; }

        [MaxLength(120)]
        public string? Description { get; set; }
    }
}
