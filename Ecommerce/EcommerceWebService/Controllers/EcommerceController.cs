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
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public IEnumerable<ProductCategory> GetProductCategory()
        {
            try
            {
                return data.GetProductCategory();
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex));
            }
        }

        /// <summary>
        /// Gets the product category.
        /// </summary>
        /// <param name="parentCategoryId">The parent category identifier.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public IEnumerable<ProductCategory> GetProductCategoryByParentCategoryId(int? parentCategoryId)
        {
            try
            {
                return data.GetProductCategory(parentCategoryId);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex));
            }
        }
    }
}
