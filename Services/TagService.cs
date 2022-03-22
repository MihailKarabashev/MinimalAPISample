using MinimalAPISample.Data;
using MinimalAPISample.Dtos;
using MinimalAPISample.Models;

namespace MinimalAPISample.Services
{
    public class TagService : ITagService
    {
        private readonly ApplicationDbContext dbContext;

        public TagService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Tag> CreateAsync(TagRequestModel requestModel)
        {
            var news = dbContext.News.FirstOrDefault(x=> x.Id == requestModel.NewsId);

            if (news == null) throw new Exception("Some Exception here");

            var tag = new Tag()
            {
                Name = requestModel.Name,
                NewsId = requestModel.NewsId,
            };

            await dbContext.Tags.AddAsync(tag);
            await dbContext.SaveChangesAsync();

            return tag;
        }
    }
}
