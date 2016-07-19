using System.Collections.Generic;

namespace EcommerceDataLayer
{
    public interface IDataAccess
    {
        /// <summary>
        /// Gets the product category.
        /// </summary>
        /// <returns></returns>
        List<ProductCategory> GetProductCategory();

        /// <summary>
        /// Gets the product category by parent category identifier.
        /// </summary>
        /// <param name="parentCategoryId">The parent category identifier.</param>
        /// <returns></returns>
        List<ProductCategory> GetProductCategory(int? parentCategoryId);
    }
}
