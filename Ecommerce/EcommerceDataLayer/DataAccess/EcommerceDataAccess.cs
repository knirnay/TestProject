using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace EcommerceDataLayer
{
    /// <summary>
    /// Class EcommerceDataAccess.
    /// </summary>
    /// <seealso cref="EcommerceDataLayer.IDataAccess" />
    public class EcommerceDataAccess : IDataAccess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EcommerceDataAccess" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
        /// <exception cref="ArgumentException">Value cannot be whitespace</exception>
        public EcommerceDataAccess(string connectionString)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString), "Value cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Value cannot be whitespace", nameof(connectionString));
            }

            this.connString = connectionString;
        }

        /// <summary>
        /// The connection string
        /// </summary>
        private string connString;

        /// <summary>
        /// Gets the product category by parent category identifier.
        /// </summary>
        /// <param name="parentCategoryId">The parent category identifier.</param>
        /// <returns></returns>
        public IEnumerable<ProductCategory> GetProductCategoryByParentCategoryId(int? parentCategoryId)
        {
            IEnumerable<ProductCategory> productCategories = null;
            using (SqlConnection conn = new SqlConnection(this.connString))
            using (SqlCommand cmd = new SqlCommand("dbo.SelectProductCategoryByParentCategoryId", conn))
            using (DataTable productCategory = new DataTable())
            {
                productCategory.Locale = CultureInfo.InvariantCulture;
                cmd.CommandType = CommandType.StoredProcedure;
                if (parentCategoryId.HasValue)
                {
                    cmd.Parameters.AddWithValue("@parentCategoryId", parentCategoryId);
                }

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(productCategory);
                }

                productCategories = productCategory.AsEnumerable().Select(row =>
                new ProductCategory
                {
                    CategoryId = row.Field<int>("CategoryId"),
                    Name = row.Field<string>("Name"),
                    ParentCategoryId = row.Field<int?>("ParentCategoryId"),
                    HasChild = Convert.ToBoolean(row.Field<int>("HasChild"))
                }).ToList();
            }

            return productCategories;
        }

        /// <summary>
        /// Gets the product category by category identifier.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        public IEnumerable<ProductCategory> GetProductCategoryByCategoryId(int categoryId)
        {
            IEnumerable<ProductCategory> productCategories = null;
            using (SqlConnection conn = new SqlConnection(this.connString))
            using (SqlCommand cmd = new SqlCommand("SelectProductCategoryByCategoryId", conn))
            using (DataTable productCategory = new DataTable())
            {
                productCategory.Locale = CultureInfo.InvariantCulture;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(productCategory);
                }

                productCategories = productCategory.AsEnumerable().Select(row =>
                new ProductCategory
                {
                    CategoryId = row.Field<int>("CategoryId"),
                    Name = row.Field<string>("Name"),
                    ParentCategoryId = row.Field<int?>("ParentCategoryId"),
                    HasChild = Convert.ToBoolean(row.Field<int>("HasChild"))
                });
            }

            return productCategories;
        }


        /// <summary>
        /// Creates the new product with specification.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentNullException">
        /// Value cannot be null.
        /// or
        /// Value cannot be null.
        /// or
        /// Value cannot be null.
        /// or
        /// Value cannot be null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Value cannot be whitespace
        /// or
        /// Value cannot be whitespace
        /// </exception>
        public int CreateNewProductWithSpecification(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Value cannot be null.");
            }

            if (product.Name == null)
            {
                throw new ArgumentNullException(nameof(product.Name), "Value cannot be null.");
            }

            if (product.Description == null)
            {
                throw new ArgumentNullException(nameof(product.Description), "Value cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(product.Name))
            {
                throw new ArgumentException("Value cannot be whitespace", nameof(product.Name));
            }

            if (string.IsNullOrWhiteSpace(product.Description))
            {
                throw new ArgumentException("Value cannot be whitespace", nameof(product.Description));
            }

            using (SqlConnection conn = new SqlConnection(this.connString))
            using (SqlCommand cmd = new SqlCommand("dbo.InsertNewProductWithSpec", conn))
            using (DataTable dt = EntityToDataTable.GetDataTable<Specification>(product.Specs))
            {
                dt.Locale = CultureInfo.InvariantCulture;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@categoryId", product.CategoryId);
                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@description", product.Description);
                SqlParameter specification = cmd.Parameters.AddWithValue("@specification", dt);
                specification.TypeName = "dbo.Specification";
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// Gets the specification meta data by base category identifier.
        /// </summary>
        /// <param name="baseCategoryId">The base category identifier.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        public IEnumerable<string> GetSpecificationMetadataByBaseCategoryId(int baseCategoryId)
        {
            IEnumerable<string> specifications = null;
            using (SqlConnection conn = new SqlConnection(this.connString))
            using (SqlCommand cmd = new SqlCommand("dbo.SelectSpecByBaseCategoryId", conn))
            using (DataTable specs = new DataTable())
            {
                specs.Locale = CultureInfo.InvariantCulture;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@baseCategoryId", baseCategoryId);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(specs);
                }

                specifications = specs.AsEnumerable().Select(s => s.Field<string>("Name")).ToList();
            }

            return specifications;
        }
    }
}
