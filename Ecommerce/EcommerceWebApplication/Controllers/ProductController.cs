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
        private IProxy proxy;

        /// <summary>
        /// The product
        /// </summary>
        private Product productField;

        /// <summary>
        /// Creates the new product.
        /// </summary>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            this.productField = new Product();
            this.productField.ProductCategories = await this.proxy.GetProductCategoryByParentCategoryId(null);
            return View(this.productField);
        }

        /// <summary>
        /// Creates the new product.
        /// Note: We can change the method signature by removing input parameter product and using UpdateModel or TryUpdateModel function.
        /// It updates the specified model instance using values from the controller's current value provider. It also inspects all the 
        /// HttpRequest inputs such as posted Form data, QueryString, Cookies and Server variables and populate the object.
        /// Product product = new Product();
        /// UpdateModel(product);
        /// Above two lines explains how can we use UpdateModel function and remove the input parameter of the Create method.
        /// The TryUpdateModel method is like the UpdateModel method except that the TryUpdateModel method does not throw an 
        /// InvalidOperationException exception if the updated model state is not valid.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpPost]
        public async Task<ActionResult> Create(Product product)
        {
            //// Check if there are any model binding validation errors. So, if there are some errors in validating model state we
            //// want to give a user an opportunity to correct those errors by keeping them in the same view.
            if (ModelState.IsValid)
            {
                int productId = await this.proxy.CreateNewProductWithSpecification(product);
                return RedirectToAction("Details", new { id = productId });
            }

            return View();
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

        /// <summary>
        /// Specifications the partial view.
        /// </summary>
        /// <param name="baseCategoryId">The base category identifier.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
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

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        public async Task<ActionResult> Get()
        {
            IEnumerable<Product> products = await this.proxy.GetProducts();
            return View(products);
        }

        /// <summary>
        /// Details of the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        public async Task<ActionResult> Details(int id)
        {
            Product product = await this.proxy.GetProductSpecByProductId(id);
            return View(product);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            this.productField = await this.proxy.GetProductSpecByProductId(id);
            IEnumerable<ProductCategory> productCategories = await this.proxy.GetProductCategoryByCategoryId(this.productField.CategoryId);
            int? parentCategoryId = productCategories.FirstOrDefault().ParentCategoryId;
            this.productField.ProductCategories = await this.proxy.GetProductCategoryByParentCategoryId(parentCategoryId);
            
            return View(this.productField);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                await this.proxy.SetProductSpecification(product);
                return RedirectToAction("Details", new { id = product.ProductId });
            }

            return RedirectToAction("Edit", new { id = product.ProductId });
        }
    }
}