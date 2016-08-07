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

        /// <summary>
        /// Edits the specified product.
        /// If we pass in Product as a parameter we have a problem with unintended updates. 
        /// i.e. Even though we have made the Name field display which can't be edited. 
        /// We can do so using Fiddler which is demonstrated here: 
        /// https://www.youtube.com/watch?v=T__S4GmQsYs&list=PL6n9fhu94yhVm6S8I2xd6nYz2ZORd7X2v&index=19 
        /// & 
        /// https://www.youtube.com/watch?v=kfBvS-VOZFw&list=PL6n9fhu94yhVm6S8I2xd6nYz2ZORd7X2v&index=20 
        /// To avoid doing so we need to use UpdateModel or TryUpateModel function to bind the model and use BlackList (Exclude List) Or WhiteList (Include List). 
        /// The example here uses Exclude List. It is just a different overload of the UpdateModel/TryUpateModel function.
        /// </summary>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpPost]
        [ActionName("Edit")]
        public async Task<ActionResult> EditPost(int id)
        {
            Product product = await proxy.GetProductSpecByProductId(id);
            /*
             * Below line uses so called BlackList (exclude properties) for update model. It simply means that Name property will be excluded from the model binding.
             * If we would have used WhiteList (include properties), it would only include the mentioned property for model binding. 
             * Note: Please notice first line of this method. In which we are fetching the product back from DB and there is reason for that.
             * We have decorated our property Name in the Product class with Required attribute and in TryUpdateModel method we are black listing the Name that means
             * we are telling our code not to include Name property in binding the model, hence when the user post the request it won't find the Name property and 
             * throws error that it required.
             */
            TryUpdateModel(product, null, null, new string[] { "Name" });
            if (product.Name == null)
            {
                Response.Write("Product name is null.<br/>");
            }

            if (ModelState.IsValid)
            {
                await this.proxy.SetProductSpecification(product);
                return RedirectToAction("Details", new { id = product.ProductId });
            }

            /*
             * If there is any error while validating model state the above if statement gets false for ModelState.IsValid and the view won't get updated. 
             * We still want to retain the same view for the users so, that they can correct the mistake and post the form back. 
             * While doing so we will have ProductCategories property null since it is used to render the drop-down box and remember, 
             * we never really bind the whole list of product categories instead we only bind the selected value for the categoryId from the drop-down box, 
             * hence we are in a need to fetch back the ProductCategories just like we have done in the Get method for Edit. 
             */
            if (product.ProductCategories == null)
            {
                IEnumerable<ProductCategory> productCategories = await this.proxy.GetProductCategoryByCategoryId(product.CategoryId);
                int? parentCategoryId = productCategories.FirstOrDefault().ParentCategoryId;
                product.ProductCategories = await this.proxy.GetProductCategoryByParentCategoryId(parentCategoryId);
            }

            return View(product);
        }
    }
}