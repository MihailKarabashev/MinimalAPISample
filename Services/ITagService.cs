using MinimalAPISample.Dtos;
using MinimalAPISample.Models;

namespace MinimalAPISample.Services
{
    public interface ITagService
    {
        Task<Tag> CreateAsync(TagRequestModel requestModel);
    }
}
