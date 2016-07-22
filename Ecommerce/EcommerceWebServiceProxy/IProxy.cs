using EcommerceDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebServiceProxy
{
    public interface IProxy
    {
        /// <summary>
        /// Gets the product category by parent category identifier.
        /// </summary>
        /// <param name="parentCategoryId">The parent category identifier.</param>
        /// <returns></returns>
        Task<IEnumerable<ProductCategory>> GetProductCategoryByParentCategoryId(int? parentCategoryId);
    }
}
