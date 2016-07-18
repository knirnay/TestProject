using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EcommerceDataLayer;

namespace EcommerceWebServiceProxy
{
    public class EcommerceProxy
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
        /// Gets the product category.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<List<ProductCategory>> GetProductCategory()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.rootUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage response = await client.GetAsync("api/Ecommerce/GetProductCategory"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsAsync<List<ProductCategory>>();
                    }
                    else
                    {
                        throw new HttpRequestException(response.Content.ReadAsStringAsync().Result);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the product category.
        /// </summary>
        /// <param name="parentCategoryId">The parent category identifier.</param>
        /// <returns></returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<List<ProductCategory>> GetProductCategory(int parentCategoryId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.rootUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage response = await client.GetAsync(string.Concat("api/Ecommerce/GetProductCategory/", parentCategoryId)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsAsync<List<ProductCategory>>();
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
