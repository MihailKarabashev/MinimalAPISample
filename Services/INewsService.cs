using MinimalAPISample.Dtos;
using MinimalAPISample.Models;

namespace MinimalAPISample.Services
{
    public interface INewsService
    {
        Task<News?> GetByIdAsync(int id);

        Task<News> CreateAsync(NewsRequestModel requestModel);
    }
}
