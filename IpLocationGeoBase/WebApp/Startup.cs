using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleInjector;
using WebApp.Services;
using WebApp.Services.Implementation;

public class Startup
{
    private readonly Container _container = new SimpleInjector.Container();

    public Startup(IConfiguration configuration)
    {
        _container.Options.ResolveUnregisteredConcreteTypes = false;

        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();

        services.AddLogging();
        services.AddLocalization(options => options.ResourcesPath = "Resources");

        services.AddSimpleInjector(_container, options =>
        {
            options.AddAspNetCore()
                .AddControllerActivation()
                .AddViewComponentActivation()
                .AddPageModelActivation()
                .AddTagHelperActivation();

            options.AddLogging();
            options.AddLocalization();
        });

        services.AddMvc(options =>
        {
            options.RespectBrowserAcceptHeader = true; // false by default
        });
        
        InitializeContainer();
    }

    private void InitializeContainer()
    {
        _container.Register<IGeoBaseService, GeoBaseService>(Lifestyle.Singleton);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSimpleInjector(_container);

        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=GeoBase}/{action=Index}/{id?}");
        });

        _container.Verify();
    }
}