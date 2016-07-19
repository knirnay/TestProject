using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EcommerceWebServiceProxy;
using EcommerceUnitTest.Properties;
using EcommerceDataLayer;
using System.Collections.Generic;
using System.Diagnostics;

namespace EcommerceUnitTest
{
    [TestClass]
    public class EcommerceWebProxyTest
    {
        public EcommerceWebProxyTest()
        {
            this.proxy = new EcommerceProxy(Settings.Default.EcommerceWebServiceRootUri);
        }

        private EcommerceProxy proxy;

        [TestMethod]
        public void GetAllProductCategory()
        {
            List<ProductCategory> productCategories = this.proxy.GetProductCategory().Result;
            foreach (ProductCategory productCategory in productCategories)
            {
                Trace.WriteLine(string.Format("CategoryId: {0}, Name: {1}", productCategory.CategoryId, productCategory.Name));
            }
        }

        [TestMethod]
        public void GetProductCategoryByCategoryId()
        {
            List<ProductCategory> productCategories = this.proxy.GetProductCategoryByParentCategoryId(null).Result;
            foreach (ProductCategory productCategory in productCategories)
            {
                Trace.WriteLine(string.Format("CategoryId: {0}, Name: {1}", productCategory.CategoryId, productCategory.Name));
            }
        }
    }
}
