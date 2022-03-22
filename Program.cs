using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinimalAPISample;
using MinimalAPISample.Data;
using MinimalAPISample.Dtos;
using MinimalAPISample.Services;
using MiniValidation;

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

var loggerFactory = app.Services.GetService<ILoggerFactory>();
var logger = loggerFactory?.CreateLogger<Program>();

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
            logger.LogError(ExceptionMessages.NewsNotFound);
            return Results.NotFound();
        }

        var mappedNews = mapper.Map<NewsResponseModel>(news);

        logger.LogInformation(string.Format(LoggerSucessfulMessages.GetNewsSuccesfully, id));

        return Results.Ok(mappedNews);

    }
    catch (Exception ex)
    {

        logger.LogError(string.Format(ExceptionMessages.NewsLoggerNotFound, ex.Message));
    }

    return Results.BadRequest();

})
.WithName("newsById")
.ProducesValidationProblem()
.Produces(StatusCodes.Status404NotFound)
.Produces<NewsRequestModel>(StatusCodes.Status200OK);

app.MapPost("/news", async (INewsService service, NewsRequestModel requestModel) =>
{
    if(!MiniValidator.TryValidate(requestModel, out var errors))
    {
        logger.LogError(ExceptionMessages.NewsValidationError);
        return Results.ValidationProblem(errors);
    }

    var news = await service.CreateAsync(requestModel); 

    var newsResponseModel = mapper.Map<NewsResponseModel>(news);

    logger.LogInformation(LoggerSucessfulMessages.CreateNewsSuccesfully);

    return Results.Created($"/news/{newsResponseModel.Id}", newsResponseModel);

})
    .WithName("createNews")
    .ProducesValidationProblem()
    .Produces<NewsRequestModel>(StatusCodes.Status201Created);

app.Run();
