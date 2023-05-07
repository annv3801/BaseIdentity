using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext"));
});
builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddWebApi(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwagger(option => { });
    app.UseSwaggerUI(option =>
        {
            option.EnableFilter();
        }
    );
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
    RequestPath = "/Resources"
});
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors();
app.Run();