using Birdi.TaskManagement.Application.DI;
using Birdi.TaskManagement.Application.Mappings;
using Birdi.TaskManagement.Core.Config;
using Birdi.TaskManagement.Data.DI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.ResolveDataDependency(builder.Configuration);
builder.Services.ResolveApplicationDependency(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(IMappingProfile));
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(AppSettings.DbName));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.Section));
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.IncludeErrorDetails = true;
    o.RequireHttpsMetadata = false;
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:KEY"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors(builder =>
        builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
