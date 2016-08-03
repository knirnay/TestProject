using EcommerceDataLayer;
using EcommerceWebServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EcommerceWebApplication.Controllers
{
    public class ProductController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="ecommerceProxy">The ecommerce proxy.</param>
        public ProductController(EcommerceProxy ecommerceProxy, Product product)
        {
            this.proxy = ecommerceProxy;
            this.productField = product;
        }

        /// <summary>
        /// The proxy
        /// </summary>
        private EcommerceProxy proxy;

        /// <summary>
        /// The product
        /// </summary>
        private Product productField;

        /// <summary>
        /// Creates the new product.
        /// </summary>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpGet]
        public async Task<ActionResult> CreateNewProduct()
        {
            this.productField = new Product();
            this.productField.ProductCategories = await this.proxy.GetProductCategoryByParentCategoryId(null);
            return View(this.productField);
        }

        /// <summary>
        /// Creates the new product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpPost]
        public async Task<ActionResult> CreateNewProduct(Product product)
        {
            int productId = await this.proxy.CreateNewProductWithSpecification(product);
            return RedirectToAction("CreateNewProduct");
        }

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <param name="parentCategoryId">The parent category identifier.</param>
        /// <returns>Task&lt;JsonResult&gt;.</returns>
        [HttpGet]
        public async Task<JsonResult> GetCategory(int parentCategoryId)
        {
            IEnumerable<ProductCategory> productCategories = await this.proxy.GetProductCategoryByParentCategoryId(parentCategoryId);            
            //// JsonRequestBehavior by default is blocked on get for security reason so, setting it to AllowGet is necessary.
            return Json(new SelectList(productCategories, "CategoryId", "Name"), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SpecificationPartialView(int baseCategoryId)
        {
            IEnumerable<string> specifications = await this.proxy.GetSpecificationMetadataByBaseCategoryId(baseCategoryId);
            this.productField.Specs = new List<Specification>();
            foreach (string specification in specifications)
            {
                Specification spec = new Specification();
                spec.Name = specification;
                this.productField.Specs.Add(spec);
            }

            return PartialView(this.productField.Specs);
        }
    }
}