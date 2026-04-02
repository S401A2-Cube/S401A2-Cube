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

            var allowedOrigins = builder.Configuration
                .GetSection("Cors:AllowedOrigins")
                .Get<string[]>()
                ?? ["http://localhost:5173", "https://localhost:5173"];

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowVueApp",
                    policy =>
                    {
                        policy.WithOrigins(allowedOrigins)
                              .AllowAnyHeader()
                              .AllowAnyMethod();
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
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Copie-colle ton token JWT ici (sans écrire 'Bearer' devant)."
                });

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
            builder.Services.AddScoped<IVeloRepository, VeloManager>();
            builder.Services.AddScoped<IDataRepository<Modele>, ModeleManager>();
            builder.Services.AddScoped<IDataRepository<Taille>, TailleManager>();
            builder.Services.AddScoped<IDataRepository<Cadre>, CadreManager>();
            builder.Services.AddScoped<IDataRepository<Geometrie>, GeometrieManager>();
            builder.Services.AddScoped<IDataRepository<Commande>, CommandeManager>();
            builder.Services.AddScoped<IDataRepository<Client>, ClientManager>();
            builder.Services.AddScoped<IDataRepository<Couleur>, CouleurManager>();
            builder.Services.AddScoped<IDataRepository<Millesime>, MillesimeManager>();
            builder.Services.AddScoped<IDataRepository<Image>, ImageManager>();
            builder.Services.AddScoped<IDataRepository<Accessoire>, AccessoireManager>();

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

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Cube V1"));

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("AllowVueApp");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}