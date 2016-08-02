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

        /// <summary>
        /// Creates the new product with specification.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>System.Int32.</returns>
        int CreateNewProductWithSpecification(Product product);

        /// <summary>
        /// Gets the specification meta data by base category identifier.
        /// </summary>
        /// <param name="baseCategoryId">The base category identifier.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        IEnumerable<string> GetSpecificationMetadataByBaseCategoryId(int baseCategoryId);
    }
}
