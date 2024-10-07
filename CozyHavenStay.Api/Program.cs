
using System.Reflection;
using System.Text;
using CozyHavenStay.Api.DataContext;
using CozyHavenStay.Api.Middleware;
using CozyHavenStay.Api.RepositoryAbsractions;
using CozyHavenStay.Api.RepositoryAbstractions;
using CozyHavenStay.Api.RepositoryImplementation;
using CozyHavenStay.Api.Service;
using CozyHavenStay.Api.ServiceAbstractions;
using CozyHavenStay.Api.ServiceImplementation;
using CozyHavenStay.Data.Models;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CozyHavenStay.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews()
  .AddNewtonsoftJson(options =>
  options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalhost",
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        //builder.Services.AddControllersWithViews(options =>
        //{
        //    options.Filters.Add<LoggingActionFilter>();
        //});

        // Add services to the container.
        builder.Services.AddTransient<IUserService, UserService>();
        builder.Services.AddTransient<IHotelService, HotelService>();
        builder.Services.AddTransient<IAmenityService, AmenityService>();
        builder.Services.AddTransient<IHotelAmenityService, HotelAmenityService>();
        builder.Services.AddTransient<IRoomAmenityService, RoomAmenityService>();
        builder.Services.AddScoped<IReviewService, ReviewService>();
        builder.Services.AddScoped<IRoomService, RoomService>();
        builder.Services.AddTransient<ICityService, CityService>();
        builder.Services.AddScoped<IBookingService, BookingService>();
        builder.Services.AddScoped<IPaymentService, PaymentService>();
        builder.Services.AddScoped<IRepository<string, User>, UserRepository>();
        builder.Services.AddTransient<IRepository<int, City>, CityRepository>();
        builder.Services.AddTransient<IRepository<int, Hotel>, HotelRepository>();
        builder.Services.AddScoped<IRepository<int, Amenity>, AmenityRepository>();
        builder.Services.AddScoped<IRepository<int, Room>, RoomRepository>();
        builder.Services.AddScoped<IRepository<int, Review>, ReviewRepository>();
        builder.Services.AddScoped<IRepository<int, Booking>, BookingRepository>();
        builder.Services.AddScoped<IRepository<int, Payment>, PaymentRepository>();
        builder.Services.AddTransient<IHotelRepository, HotelRepository>();


        // Configure Log4Net
        var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
        XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

        

        builder.Services.AddDbContext<AppDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),ServiceLifetime.Transient);
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");
        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
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
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
        });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy => policy.RequireRole("admin"));
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<GlobalExceptionMiddleware>();
        app.UseHttpsRedirection();
        app.UseCors("AllowLocalhost");
        app.UseAuthentication();
        app.UseAuthorization();
        

        app.MapControllers();

        app.Run();
    }
}

