using Data.Model;
using Nest;

namespace Business
{
    public class ProductBusiness
    {
        private readonly IElasticClient _elasticClient;
        public ProductBusiness(ElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
        /// <summary>
        /// Searching in description prop only.
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<List<Product>> Get(string keyword)
        {
            var result = await _elasticClient.SearchAsync<Product>(
                x => x.Query(
                    y => y.Fuzzy(z =>
                        z.Name(keyword).Field(p => p.Description).Fuzziness(Fuzziness.Auto).Value(keyword)
                    )
                )
            );
            return result.Documents.ToList();
        }

        public async Task<List<Product>> Get()
        {
            var result = await _elasticClient.SearchAsync<Product>();
            return result.Documents.ToList();
        }

        public async void Post(Product product)
        {
            await _elasticClient.IndexDocumentAsync(product);
        }

        public async void Delete(int id)
        {
            await _elasticClient.DeleteAsync<Product>(id);
        }
    }
}
