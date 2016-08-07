namespace EcommerceDataLayer
{
    public class ProductCategory
    {
        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent category identifier.
        /// </summary>
        /// <value>
        /// The parent category identifier.
        /// </value>
        public int? ParentCategoryId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has child.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has child; otherwise, <c>false</c>.
        /// </value>
        public bool HasChild { get; set; }
    }
}
