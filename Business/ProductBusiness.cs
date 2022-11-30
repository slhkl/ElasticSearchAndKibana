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

        public async Task<List<Product>> Get(string keyword)
        {
            var result = await _elasticClient.SearchAsync<Product>(
                x => x.Query(
                    y => y.QueryString(
                        z => z.Query('*' + keyword + '*')
                        )).Size(5000));
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
