using Api.Data;
using Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=Sing4Sats.db"));

builder.Services.AddHttpClient();
builder.Services.AddScoped<HashService>();
builder.Services.AddScoped<LnAddressService>();
builder.Services.AddScoped<LnbitsService>();
builder.Services.AddScoped<LnHookService>();
builder.Services.AddScoped<LnUrlService>();
builder.Services.AddScoped<YoutubeService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy.WithOrigins("http://localhost:5043", "https://karaoke.ngoline.com") // Your Blazor app URL
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("*");
    });
});

var app = builder.Build();

// Enable CORS before other middleware
app.UseCors("AllowBlazor");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
