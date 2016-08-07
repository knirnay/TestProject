using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EcommerceDataLayer;

namespace EcommerceWebService.Controllers
{
    [RoutePrefix("Ecommerce")]
    public class EcommerceController : ApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EcommerceController"/> class.
        /// </summary>
        /// <param name="dataAccess">The data access.</param>
        public EcommerceController(IDataAccess dataAccess)
        {
            this.data = dataAccess;
        }

        /// <summary>
        /// The data
        /// </summary>
        private IDataAccess data;

        /// <summary>
        /// Gets the product category.
        /// </summary>
        /// <param name="parentCategoryId">The parent category identifier.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpGet]
        public IEnumerable<ProductCategory> GetProductCategoryByParentCategoryId(int? parentCategoryId)
        {
            try
            {
                return this.data.GetProductCategoryByParentCategoryId(parentCategoryId);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex));
            }
        }

        /// <summary>
        /// Creates the new product with specification.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpPost]
        public int CreateNewProductWithSpecification(Product product)
        {
            try
            {
                return this.data.CreateNewProductWithSpecification(product);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex));
            }
        }

        /// <summary>
        /// Gets the specification meta data by base category identifier.
        /// </summary>
        /// <param name="baseCategoryId">The base category identifier.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpGet]
        public IEnumerable<string> GetSpecificationMetadataByBaseCategoryId(int baseCategoryId)
        {
            try
            {
                return this.data.GetSpecificationMetadataByBaseCategoryId(baseCategoryId);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex));
            }
        }

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <returns>IEnumerable&lt;Product&gt;.</returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            try
            {
                return this.data.GetProducts();
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex));
            }
        }

        /// <summary>
        /// Gets the spec by product identifier.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>IEnumerable&lt;Specification&gt;.</returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpGet]
        public IEnumerable<Specification> GetSpecByProductId(int productId)
        {
            try
            {
                return this.data.GetSpecByProductId(productId);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex));
            }
        }

        /// <summary>
        /// Sets the product specification.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <exception cref="HttpResponseException"></exception>
        [HttpPut]
        public void SetProductSpecification(Product product)
        {
            try
            {
                this.data.SetProductSpecification(product);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex));
            }
        }

        /// <summary>
        /// Gets the product spec by product identifier.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>Product.</returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpGet]
        public Product GetProductSpecByProductId(int productId)
        {
            try
            {
                return this.data.GetProductSpecByProductId(productId);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex));
            }
        }

        /// <summary>
        /// Gets the product category by category identifier.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>IEnumerable&lt;ProductCategory&gt;.</returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpGet]
        public IEnumerable<ProductCategory> GetProductCategoryByCategoryId(int categoryId)
        {
            try
            {
                return this.data.GetProductCategoryByCategoryId(categoryId);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex));
            }
        }
    }
}
