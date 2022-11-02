using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;



namespace TechnicalTest.IntegrationTest
{
    public class IntegrationTest
    {

        protected readonly HttpClient TestClient;

        public IntegrationTest()
        {

            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(DataContext));
                        services.AddDbContext<DataContext>(options => { options.UseInMemoryDatabase("TestDb"); });
                    });
                });

            TestClient = appFactory.CreateClient();

        }
    }
}