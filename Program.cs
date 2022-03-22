using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinimalAPISample;
using MinimalAPISample.Data;
using MinimalAPISample.Dtos;
using MinimalAPISample.Services;

var builder = WebApplication.CreateBuilder(args);

var Configuration = builder.Configuration;

builder.Services.AddAutoMapper(typeof(AppMappingProfile).Assembly);

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<INewsService, NewsService>();

var app = builder.Build();

var mapper = app.Services.GetService<IMapper>();

if (mapper == null)
{
    throw new InvalidOperationException(ExceptionMessages.MapperError);
}

var logger = app.Services.GetService<ILogger>();

if (logger == null)
{
    throw new InvalidOperationException(ExceptionMessages.LoggerError);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/news/{id:int}", async (int id, INewsService service) =>
{
    try
    {
        var news = await service.GetByIdAsync(id);

        if (news == null)
        {
            return Results.NotFound();
        }

        var mappedNews = mapper.Map<NewsResponseModel>(news);

        return Results.Ok(mappedNews);

    }
    catch (Exception ex)
    {

        logger.LogError(string.Format(ExceptionMessages.NewsLoggerNotFound, ex.Message));
    }

    return Results.BadRequest();

})
.WithName("news");

app.Run();
