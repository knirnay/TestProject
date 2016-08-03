using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EcommerceWebServiceProxy;
using EcommerceUnitTest.Properties;
using EcommerceDataLayer;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace EcommerceUnitTest
{
    [TestClass]
    public class EcommerceWebProxyTest
    {
        public EcommerceWebProxyTest()
        {
            this.proxy = new EcommerceProxy(Settings.Default.EcommerceWebServiceRootUri);
        }

        private IProxy proxy;

        [TestMethod]
        public void GetProductCategoryByCategoryId()
        {
            IEnumerable<ProductCategory> productCategories = this.proxy.GetProductCategoryByParentCategoryId(null).Result;
            foreach (ProductCategory productCategory in productCategories)
            {
                Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "CategoryId: {0}, Name: {1}, ParentCategoryId:{2}, HasChild: {3}", productCategory.CategoryId, productCategory.Name, productCategory.ParentCategoryId, productCategory.HasChild));
            }
        }

        [TestMethod]
        public void GetSpecificationMetadataByBaseCategoryId()
        {
            IEnumerable<string> specifications = this.proxy.GetSpecificationMetadataByBaseCategoryId(1).Result;
            foreach(string specification in specifications)
            {
                Trace.WriteLine(specification);
            }
        }

        [TestMethod]
        public void CreateNewProduct()
        {
            Product product = new Product();
            product.Name = "Test-Computer";
            product.CategoryId = 10;
            product.Description = "Test product has no description.";
            product.Specs = new List<Specification>();
            product.Specs.Add(new Specification { Name = "Battery Type", SpecValue = "Lithium Polymer (LiPo)" });
            product.Specs.Add(new Specification { Name = "Brand Name", SpecValue = "Apple" });
            int productId = this.proxy.CreateNewProductWithSpecification(product).Result;
            Trace.WriteLine(productId);
        }
    }
}
