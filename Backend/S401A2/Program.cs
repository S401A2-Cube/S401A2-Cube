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

            // Add services to the container.

            //builder.Services.AddControllers();
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<CubeDBContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("LocalConnectionString")));

            builder.Services.AddScoped<IDataRepository<Article>, ArticleManager>();
            builder.Services.AddScoped<IDataRepository<Categorie>, CategorieManager>();
            builder.Services.AddScoped<IDataRepository<MotCle>, MotCleManager>();
            builder.Services.AddScoped<IDataRepository<Velo>, VeloManager>();
            builder.Services.AddScoped<IDataRepository<Modele>, ModeleManager>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
