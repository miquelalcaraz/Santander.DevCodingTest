
using System.Threading.RateLimiting;

using Microsoft.AspNetCore.RateLimiting;


using Santander.DevCodingTest.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();


builder.Services.AddHttpClient<IHackerNewsApiClient, HackerNewsApiClient>(client =>
{
    client.BaseAddress = new Uri("https://hacker-news.firebaseio.com/");
});
builder.Services.AddSingleton<IHackerNewsService, HackerNewsService>();


builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddFixedWindowLimiter("hackernews-policy", limiter =>
    {
        limiter.PermitLimit = 3;
        limiter.Window = TimeSpan.FromSeconds(60);
        limiter.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        limiter.QueueLimit = 0;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseAuthorization();


app.MapControllers();

app.Run();



public partial class Program { }

