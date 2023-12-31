using System.Reflection;
using System.Text;
using Domain;
using Infrastructure.DbContext;
using Kirel.Repositories.Infrastructure.Generics;
using Kirel.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApplication1.Service;

var builder = WebApplication.CreateBuilder(args);
//var configuration = new ConfigurationManager().AddJsonFile("appsettings.json").Build();

var authOptions = builder.Configuration.GetSection("AuthOptions").Get<AuthOptions>();
var connectionString = builder.Configuration.GetConnectionString("PostgreConnection");
// Add services to the container.
/*builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer("Server=localhost;Database=Users;Trusted_Connection=True;Encrypt=False;"));*/
builder.Services.AddDbContext<DataDbContext>(options =>
    options.UseNpgsql(connectionString));
/*builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());*/
builder.Services.AddSingleton(authOptions);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<MyAnimeListService>();
builder.Services.AddScoped<SeriesService>();
builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<UserSeriesService>();
builder.Services.AddScoped<CommentsService>();
builder.Services.AddScoped<UserFavouriteService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IKirelGenericEntityFrameworkRepository<int, User>, KirelGenericEntityFrameworkRepository<int, User, DataDbContext>>();
builder.Services.AddIdentity<User, IdentityRole<int>>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DataDbContext>();
builder.Services.AddScoped<IKirelGenericEntityFrameworkRepository<int, Movie>, KirelGenericEntityFrameworkRepository<int, Movie, DataDbContext>>();
builder.Services.AddScoped<IKirelGenericEntityFrameworkRepository<int, Anime>, KirelGenericEntityFrameworkRepository<int, Anime, DataDbContext>>();
builder.Services.AddScoped<IKirelGenericEntityFrameworkRepository<int, UserMovies>, KirelGenericEntityFrameworkRepository<int, UserMovies, DataDbContext>>();
builder.Services.AddScoped<IKirelGenericEntityFrameworkRepository<int, UserAnime>, KirelGenericEntityFrameworkRepository<int, UserAnime, DataDbContext>>();
builder.Services.AddScoped<IKirelGenericEntityFrameworkRepository<int, Series>, KirelGenericEntityFrameworkRepository<int, Series, DataDbContext>>();
builder.Services.AddScoped<IKirelGenericEntityFrameworkRepository<int, UserSeries>, KirelGenericEntityFrameworkRepository<int, UserSeries, DataDbContext>>();
builder.Services.AddScoped<IKirelGenericEntityFrameworkRepository<int, MovieComment>, KirelGenericEntityFrameworkRepository<int, MovieComment, DataDbContext>>();


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false; // Установите true для использования HTTPS в продакшене
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = authOptions.Issuer,

            ValidateAudience = true,
            ValidAudience = authOptions.Audience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.Key)),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero // Убедитесь, что время истечения токена строго соблюдается
        };
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()  
    {  
        Name = "Authorization",  
        Type = SecuritySchemeType.ApiKey,  
        Scheme = "Bearer",  
        BearerFormat = "JWT",  
        In = ParameterLocation.Header,  
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",  
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement  
    {  
        {  
            new OpenApiSecurityScheme  
            {  
                Reference = new OpenApiReference  
                {  
                    Type = ReferenceType.SecurityScheme,  
                    Id = "Bearer"  
                }  
            },  
            new string[] {}
        }  
    });
});




var app = builder.Build();


DataDbInitializer.Initialize(app.Services.GetRequiredService<IServiceProvider>().CreateScope().ServiceProvider);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your WebSaver.API V1");
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
