using Data.Model;
using Nest;

namespace ElasticSearchTemplate.StartupConfiguration
{
    public static class ElasticSearchExtensions
    {
        public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            string uri = configuration["ELKConfiguration:Uri"];
            string defaultIndex = configuration["ELKConfiguration:Index"];

            var settings = new ConnectionSettings(new Uri(uri)).PrettyJson().DefaultIndex(defaultIndex);

            ElasticClient client = new ElasticClient(settings);

            services.AddSingleton<ElasticClient>(client);

            var createIndexResponse = client.Indices.Create(defaultIndex, index => index.Map<Product>(x => x.AutoMap()));
        }
    }
}
