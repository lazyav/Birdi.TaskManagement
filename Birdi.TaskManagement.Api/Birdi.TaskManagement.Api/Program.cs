using Birdi.TaskManagement.Application.DI;
using Birdi.TaskManagement.Application.Mappings;
using Birdi.TaskManagement.Core;
using Birdi.TaskManagement.Data.DI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.ResolveDataDependency(builder.Configuration);
builder.Services.ResolveApplicationDependency(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(IMappingProfile));
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(AppSettings.DbName));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
