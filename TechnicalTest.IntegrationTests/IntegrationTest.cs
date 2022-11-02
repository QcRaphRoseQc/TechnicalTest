using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TechnicalTest.API;

namespace TechnicalTest.IntegrationTests;

public class IntegrationTest
{
    protected readonly HttpClient TestClient;

    protected IntegrationTest()
    {
        var appFactory = new WebApplicationFactory<Startup>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.RemoveAll(typeof(DbContext));
                    services.AddDbContext<DbContext>(options => { options.UseInMemoryDatabase("TestDb"); });
                });
            });

        TestClient = appFactory.CreateClient();
    }


}