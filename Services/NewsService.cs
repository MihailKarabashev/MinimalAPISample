using Microsoft.EntityFrameworkCore;
using MinimalAPISample.Data;
using MinimalAPISample.Dtos;
using MinimalAPISample.Models;

namespace MinimalAPISample.Services
{
    public class NewsService : INewsService
    {
        private readonly ApplicationDbContext dbContext;

        public NewsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<News> CreateAsync(NewsRequestModel requestModel)
        {
            var news = new News()
            {
                Title = requestModel.Title,
                Description = requestModel.Description,
            };

            await dbContext.AddAsync(news);
            await dbContext.SaveChangesAsync();

            return news;
        }

        public async Task<News?> GetByIdAsync(int id) 
            => await this.dbContext.News
            .Where(x=> x.Id == id)
            .Include(x=> x.Tags)
            .FirstOrDefaultAsync();
    }
}
