using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OpenAI.Extensions;
using Vid2.Blog.Api.Data;
using Vid2.Blog.Api.Services;
using YoutubeExplode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder
    .Services
    .AddDbContext<DataContext>(
        options =>
            options.UseNpgsql(builder.Configuration
                .GetConnectionString("DefaultConnection"))
                .UseSnakeCaseNamingConvention()
            
    );

builder.Services.AddOpenAIService();

builder.Services.AddTransient<YoutubeClient>();
builder.Services.AddTransient<IYtdlService, YtdlService>();
builder.Services.AddTransient<ITranscriptionService, TranscriptionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
