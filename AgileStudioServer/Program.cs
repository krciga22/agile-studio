namespace AgileStudioServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // todo update DBContextFactory to get configuration via dependency injection
            builder.Services.AddMyDB(builder.Configuration);
            builder.Services.AddMyRepositories();
            builder.Services.AddMyCoreServices();
            builder.Services.AddControllers();
            builder.Services.AddMyCoreFeatureServices();
            builder.Services.AddMyDtoHydrators();
            builder.Services.AddMyModelHydrators();
            builder.Services.AddMyEntityHydrators();
            builder.Services.AddMyAuth(builder.Configuration);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}