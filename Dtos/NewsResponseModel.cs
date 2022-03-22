namespace MinimalAPISample.Dtos
{
    public class NewsResponseModel
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public IEnumerable<TagResponseModel> Tags { get; set; }
    }
}
