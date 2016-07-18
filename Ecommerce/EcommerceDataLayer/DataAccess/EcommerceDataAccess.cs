﻿using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace EcommerceDataLayer
{
    public class EcommerceDataAccess : IDataAccess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EcommerceDataAccess"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public EcommerceDataAccess(string connectionString)
        {
            this.connString = connectionString;
        }

        /// <summary>
        /// The connection string
        /// </summary>
        private string connString;

        /// <summary>
        /// Gets the product category.
        /// </summary>
        /// <returns></returns>
        public List<ProductCategory> GetProductCategory()
        {
            List<ProductCategory> productCategories = null;
            using (SqlConnection conn = new SqlConnection(this.connString))
            using (SqlCommand cmd = new SqlCommand("dbo.SelectProductCategory", conn))
            using (DataTable productCategory = new DataTable())
            {
                productCategory.Locale = CultureInfo.InvariantCulture;
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(productCategory);
                }

                productCategories = productCategory.AsEnumerable().Select(row =>
                new ProductCategory
                {
                    CategoryId = row.Field<int>("CategoryId"),
                    Name = row.Field<string>("Name")
                }).ToList();
            }

            return productCategories;
        }

        /// <summary>
        /// Gets the product category by parent category identifier.
        /// </summary>
        /// <param name="parentCategoryId">The parent category identifier.</param>
        /// <returns></returns>
        public List<ProductCategory> GetProductCategory(int parentCategoryId)
        {
            List<ProductCategory> productCategories = null;
            using (SqlConnection conn = new SqlConnection(this.connString))
            using (SqlCommand cmd = new SqlCommand("dbo.SelectProductCategoryByParentCategoryId", conn))
            using (DataTable productCategory = new DataTable())
            {
                productCategory.Locale = CultureInfo.InvariantCulture;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@parentCategoryId", parentCategoryId);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(productCategory);
                }

                productCategories = productCategory.AsEnumerable().Select(row =>
                new ProductCategory
                {
                    CategoryId = row.Field<int>("CategoryId"),
                    Name = row.Field<string>("Name")
                }).ToList();
            }

            return productCategories;
        }
    }
}