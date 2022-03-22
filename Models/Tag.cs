namespace MinimalAPISample.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int NewsId { get; set; }

        public News News { get; set; }
    }
}
