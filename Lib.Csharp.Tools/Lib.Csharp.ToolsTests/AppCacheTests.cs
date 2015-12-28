using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using Lib.Csharp.Tools;
using NUnit.Framework;

namespace Lib.Csharp.ToolsTests
{
    [TestFixture()]
    public class AppCacheTests
    {
        private string cacheKey = "Test-Key";
        private string cacheValue = "Test-Value";
        private string cacheKey2 = "Test-Key2";
        private string cacheValue2 = "Test-Value2";
        /// <summary>
        /// 全局setup,不能使用async
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            Thread.GetDomain().SetData(".appPath", "c:\\inetpub\\wwwroot\\webapp\\");
            Thread.GetDomain().SetData(".appVPath", "/");
            TextWriter tw = new StringWriter();
            String address = "home.myspace.cn";
            //HttpWorkerRequest wr = new MyWorkerRequest("default.aspx", "friendId=1300000000", tw, address);
            HttpWorkerRequest wr = new SimpleWorkerRequest("default.aspx", "friendId=1300000000", tw);
            HttpContext.Current = new HttpContext(wr);
        }
        /// <summary>
        /// 每次测试setup,不能使用async
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            
 
            AppCache.Remove(cacheKey);
        }

        [Test()]
        public void IsExistTest()
        {
            var isHave = AppCache.IsExist(cacheKey);
            Assert.IsFalse(isHave);

            AppCache.AddCache(cacheKey, cacheValue);
            isHave = AppCache.IsExist(cacheKey);
            Assert.IsTrue(isHave);


        }

        [Test()]
        public void AddTest()
        {
            AppCache.Add(cacheKey, cacheValue);
            var result = AppCache.Get(cacheKey);
            Assert.AreEqual(result.ToString(), cacheValue);

        }

        [Test()]
        public void AddTest1()
        {
            AppCache.AddCache(cacheKey, cacheValue);
            var result = AppCache.Get(cacheKey);
            Assert.AreEqual(result.ToString(), cacheValue);

            AppCache.AddCache(cacheKey2, cacheValue2);
            var result2 = AppCache.Get(cacheKey2);
            Assert.AreEqual(result2.ToString(), cacheValue2);
        }

        [Test()]
        public void RemoveTest()
        {
            AppCache.AddCache(cacheKey, cacheValue);
            var isHave = AppCache.IsExist(cacheKey);
            Assert.IsTrue(isHave);

            AppCache.Remove(cacheKey);
            isHave = AppCache.IsExist(cacheKey);
            Assert.IsFalse(isHave);
        }

       
    }
}
