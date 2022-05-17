using ItuTweets.Data.Context;
using ItuTweets.Services;
using ItuTweets.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ItuTweets
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var connectionString = Configuration.GetConnectionString("Database");
            services.AddDbContext<ItuTweetsContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddEndpointsApiExplorer();

            services.AddScoped<IScrapperService, ScrapperService>();
            services.AddScoped<ITweetDataService, TweetDataService>();

            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
