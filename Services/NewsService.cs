using Microsoft.EntityFrameworkCore;
using MinimalAPISample.Data;
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

        public async Task<News?> GetByIdAsync(int id) => await this.dbContext.News
            .FirstOrDefaultAsync(x=> x.Id == id);
    }
}
