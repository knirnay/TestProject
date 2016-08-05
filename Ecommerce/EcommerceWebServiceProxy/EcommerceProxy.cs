using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EcommerceDataLayer;
using System.Globalization;

namespace EcommerceWebServiceProxy
{
    public class EcommerceProxy : IProxy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EcommerceProxy"/> class.
        /// </summary>
        /// <param name="rootUri">The root URI.</param>
        public EcommerceProxy(string rootUri)
        {
            this.rootUrl = rootUri;
        }

        /// <summary>
        /// The root URL
        /// </summary>
        private string rootUrl;

        /// <summary>
        /// Gets the product category by parent category identifier.
        /// </summary>
        /// <param name="parentCategoryId">The parent category identifier.</param>
        /// <returns>Task&lt;IEnumerable&lt;ProductCategory&gt;&gt;.</returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<IEnumerable<ProductCategory>> GetProductCategoryByParentCategoryId(int? parentCategoryId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.rootUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage response = await client.GetAsync(string.Format(CultureInfo.InvariantCulture, "api/Ecommerce/GetProductCategoryByParentCategoryId?parentCategoryId={0}", parentCategoryId)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsAsync<IEnumerable<ProductCategory>>();
                    }
                    else
                    {
                        throw new HttpRequestException(response.Content.ReadAsStringAsync().Result);
                    }
                }
            }
        }

        /// <summary>
        /// Creates the new product with specification.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<int> CreateNewProductWithSpecification(Product product)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.rootUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage response = await client.PostAsJsonAsync("api/Ecommerce/CreateNewProductWithSpecification", product))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsAsync<int>();
                    }
                    else
                    {
                        throw new HttpRequestException(response.Content.ReadAsStringAsync().Result);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the specification meta data by base category identifier.
        /// </summary>
        /// <param name="baseCategoryId">The base category identifier.</param>
        /// <returns>Task&lt;IEnumerable&lt;System.String&gt;&gt;.</returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<IEnumerable<string>> GetSpecificationMetadataByBaseCategoryId(int baseCategoryId)
        { 
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.rootUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage response = await client.GetAsync(string.Format(CultureInfo.InvariantCulture, "api/Ecommerce/GetSpecificationMetaDataByBaseCategoryId?baseCategoryId={0}", baseCategoryId)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsAsync<IEnumerable<string>>();
                    }
                    else
                    {
                        throw new HttpRequestException(response.Content.ReadAsStringAsync().Result);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;Product&gt;&gt;.</returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<IEnumerable<Product>> GetProducts()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.rootUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage response = await client.GetAsync("api/Ecommerce/GetProducts"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsAsync<IEnumerable<Product>>();
                    }
                    else
                    {
                        throw new HttpRequestException(response.Content.ReadAsStringAsync().Result);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the specs by product identifier.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>Task&lt;IEnumerable&lt;Specification&gt;&gt;.</returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<IEnumerable<Specification>> GetSpecsByProductId(int productId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.rootUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage response = await client.GetAsync(string.Format(CultureInfo.InvariantCulture, "api/Ecommerce/GetSpecByProductId?productId={0}", productId)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsAsync<IEnumerable<Specification>>();
                    }
                    else
                    {
                        throw new HttpRequestException(response.Content.ReadAsStringAsync().Result);
                    }
                }
            }
        }
    }
}
