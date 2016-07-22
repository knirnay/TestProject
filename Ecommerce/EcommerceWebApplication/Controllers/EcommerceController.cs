using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcommerceDataLayer;
using EcommerceWebServiceProxy;
using System.Threading.Tasks;

namespace EcommerceWebApplication.Controllers
{
    public class EcommerceController : Controller
    {
        public EcommerceController(EcommerceProxy ecommerceProxy)
        {
            this.proxy = ecommerceProxy;
        }

        private EcommerceProxy proxy;
        
        // GET: Ecommerce
        public async Task<ActionResult> ProductCategory(int? parentCategoryId)
        {
            IEnumerable<ProductCategory> productCategory = await this.proxy.GetProductCategoryByParentCategoryId(parentCategoryId);
            return View(productCategory);
        }
    }
}