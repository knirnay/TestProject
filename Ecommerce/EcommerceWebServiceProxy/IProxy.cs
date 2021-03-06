﻿using EcommerceDataLayer;
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
        /// <returns>Task&lt;IEnumerable&lt;ProductCategory&gt;&gt;.</returns>
        Task<IEnumerable<ProductCategory>> GetProductCategoryByParentCategoryId(int? parentCategoryId);

        /// <summary>
        /// Creates the new product with specification.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        Task<int> CreateNewProductWithSpecification(Product product);

        /// <summary>
        /// Gets the specification meta data by base category identifier.
        /// </summary>
        /// <param name="baseCategoryId">The base category identifier.</param>
        /// <returns>Task&lt;IEnumerable&lt;System.String&gt;&gt;.</returns>
        Task<IEnumerable<string>> GetSpecificationMetadataByBaseCategoryId(int baseCategoryId);

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;Product&gt;&gt;.</returns>
        Task<IEnumerable<Product>> GetProducts();

        /// <summary>
        /// Gets the specs by product identifier.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>Task&lt;IEnumerable&lt;Specification&gt;&gt;.</returns>
        Task<IEnumerable<Specification>> GetSpecsByProductId(int productId);

        /// <summary>
        /// Sets the product specification.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>Task.</returns>
        Task SetProductSpecification(Product product);

        /// <summary>
        /// Gets the product spec by product identifier.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>Task&lt;Product&gt;.</returns>
        Task<Product> GetProductSpecByProductId(int productId);
        /// <summary>
        /// Gets the product category by category identifier.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>Task&lt;IEnumerable&lt;ProductCategory&gt;&gt;.</returns>
        Task<IEnumerable<ProductCategory>> GetProductCategoryByCategoryId(int categoryId);
    }
}
