namespace MinimalAPISample.Models
{
    public class News
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public ICollection<Tag> Tags { get; set; } =  new HashSet<Tag>();
    }
}
