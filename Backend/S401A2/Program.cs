using APICube.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;
using S401A2.Model.DataManager;
using S401A2.Model.EntityFramework;
using S401A2.Models.Repository;

namespace S401A2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration.GetConnectionString("LocalConnectionString");

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

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Cube V1");
                c.RoutePrefix = "swagger"; 
            });

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}