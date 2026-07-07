using Goods.Services;
using Goods.Tools.Utils.Json;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using static Microsoft.AspNetCore.Http.StatusCodes;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureServices((_, serviceCollection) =>
    {
        serviceCollection
            .AddServices()
            .AddControllersWithViews()
            .AddJsonOptions(options => TextJsonSerializer.Configure(options.JsonSerializerOptions));

        serviceCollection.AddSingleton<IJsonSerializer>(new TextJsonSerializer());

        serviceCollection.Configure<GzipCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Optimal;
        });

        serviceCollection.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        });
    }
);
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});


WebApplication app = builder.Build();

app.UseResponseCompression();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
