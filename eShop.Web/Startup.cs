using eShop.DataStore.HardCoded;
using eShop.UseCases.PluginInterfaces.DataStore;
using eShop.UseCases.SearchProductScreen;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using eShop.Web.Data;
using eShop.UseCases.ViewProductScreen;
using eShop.UseCases.PluginInterfaces.UI;
using eShop.DataStore.LocalStorage;
using eShop.UseCases.ShoppingCartScreen;

namespace eShop.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISearchProductUseCase, SearchProductUseCase>();
            services.AddScoped<IViewProductUseCase, ViewProductUseCase>();
            services.AddScoped<IAddProductToCartUseCase, AddProductToCartUseCase>();
            services.AddScoped<IViewShoppingCartUseCase, ViewShoppingCartUseCase>();
            services.AddScoped<IShoppingCart, ShoppingCart>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
