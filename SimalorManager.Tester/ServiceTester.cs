using Microsoft.VisualStudio.TestTools.UnitTesting;
using OPT.Product.SimalorManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimalorManager.Tester
{
    [TestClass]
    public class ServiceTester
    {

        /// <summary> 测试加载Eclipse数据 </summary>
        [TestMethod]
        public void ThreadLoadResize()
        {
            string filePath = @"D:\WorkArea\LaoBB\3106-Eclipse格式\3106_E100.DATA";

            var data = FileFactoryService.Instance.ThreadLoadResize(filePath);


            //// Todo ：单元测试用断言检验运行情况 
            //Assert.IsFalse(true);
        }
    }
}
