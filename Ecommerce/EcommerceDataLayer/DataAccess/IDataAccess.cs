using System.Collections.Generic;

namespace EcommerceDataLayer
{
    public interface IDataAccess
    {
        /// <summary>
        /// Gets the product category by parent category identifier.
        /// </summary>
        /// <param name="parentCategoryId">The parent category identifier.</param>
        /// <returns></returns>
        IEnumerable<ProductCategory> GetProductCategoryByParentCategoryId(int? parentCategoryId);

        /// <summary>
        /// Gets the product category by category identifier.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        IEnumerable<ProductCategory> GetProductCategoryByCategoryId(int categoryId);
    }
}
