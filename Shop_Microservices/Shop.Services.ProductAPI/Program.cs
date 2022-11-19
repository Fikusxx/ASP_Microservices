using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shop.Services.ProductAPI;



var builder = WebApplication.CreateBuilder();
var services = builder.Services;
var configuration = builder.Configuration;

#region EF Connection & Settings

string connection = configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));

services.AddScoped<IProductRepository, ProductRepository>();

#endregion

#region AutoMapper

IMapper mapper = MappingConfiguration.RegisterMaps().CreateMapper();
services.AddSingleton(mapper);
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#endregion



// Add services to the container.
services.AddControllers();

services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.Authority = "https://localhost:7213/";
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false
    };
});

services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "shop");
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo() { Title = "Shop.Services.ProductAPI", Version = "v1" });
    x.EnableAnnotations();
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = @"Enter 'Bearer' [space] and your token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    x.AddSecurityRequirement(new OpenApiSecurityRequirement() {
    {
        new OpenApiSecurityScheme()
        {
            Reference = new OpenApiReference()
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header
        },
        new List<string>()
        }
    });
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
