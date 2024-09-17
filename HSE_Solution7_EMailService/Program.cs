using HSE_Solution7_EMailService.Controllers;


namespace HSE_Solution7_EMailService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddSingleton<JsonFileService>();

        // Add Swagger services
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        

        // Enable Swagger middleware
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            //c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
        });

        app.UseHttpsRedirection(); 
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        // Explicit route for root path to redirect to Home controller
        app.MapControllerRoute(
            name: "root",
            pattern: "",
            defaults: new { controller = "Home", action = "Index" });
        
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
