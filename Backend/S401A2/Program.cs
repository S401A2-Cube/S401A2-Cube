using APICube.Models.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using S401A2.Model;
using S401A2.Model.DataManager;
using S401A2.Model.EntityFramework;
using S401A2.Models.Repository;
using System.Text;

namespace S401A2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowVueApp",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod();
                    });
            });

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                // 1. D�finir le bouton "Authorize" (le cadenas)
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Copie-colle ton token JWT ici (sans �crire 'Bearer' devant)."
                });

                // 2. Appliquer le cadenas � toutes les routes de l'API
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<CubeDBContext>(options =>
                options.UseNpgsql(connectionString));


            builder.Services.AddScoped<IDataRepository<Article>, ArticleManager>();
            builder.Services.AddScoped<IDataRepository<Categorie>, CategorieManager>();
            builder.Services.AddScoped<IDataRepository<MotCle>, MotCleManager>();
            builder.Services.AddScoped<IDataRepository<Velo>, VeloManager>();
            builder.Services.AddScoped<IDataRepository<Modele>, ModeleManager>();
            builder.Services.AddScoped<IDataRepository<Taille>, TailleManager>();
            builder.Services.AddScoped<IDataRepository<Cadre>, CadreManager>();
            builder.Services.AddScoped<IDataRepository<Geometrie>, GeometrieManager>();
            builder.Services.AddScoped<IDataRepository<Commande>, CommandeManager>();
            builder.Services.AddScoped<IDataRepository<Client>, ClientManager>();
            builder.Services.AddScoped<IDataRepository<Couleur>, CouleurManager>();
            builder.Services.AddScoped<IDataRepository<Millesime>, MillesimeManager>();



            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.RequireHttpsMetadata = false;
                 options.SaveToken = true;
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = builder.Configuration["Jwt:Issuer"],
                     ValidAudience = builder.Configuration["Jwt:Audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
                     ClockSkew = TimeSpan.Zero
                 };
             });


            builder.Services.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                config.AddPolicy(Policies.User, Policies.UserPolicy());
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Cube V1");
                c.RoutePrefix = "swagger"; 
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseCors("AllowVueApp");
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
