using System;
using System.Text;
using Lib.Csharp.Tools;
using NUnit.Framework;

namespace Lib.Csharp.ToolsTests
{
    [TestFixture()]
    public class HttpWebRequestHelperTests
    {
        /// <summary>
        /// 全局setup,不能使用async
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {

        }
        /// <summary>
        /// 每次测试setup,不能使用async
        /// </summary>
        [SetUp]
        public void SetUp()
        {

        }

        [Test()]
        //[ExpectedException(typeof(Exception))]
        [TestCase("http://finance.sina.com.cn/stock/y/2016-01-05/doc-ifxneept3713456.shtml")]
        public void GetStrFromPcwebTest(string strUrl)
        {
            var result = HttpWebRequestHelper.GetStrFromPcweb(strUrl,Encoding.UTF8);
            Assert.AreEqual(result.Length > 1, true);

        }

        [Test()]
        [TestCase("http://finance.sina.com.cn/stock/y/2016-01-05/doc-ifxneept3713456.shtml")]
        public async void GetStrFromPcwebAsyncTest(string strUrl)
        {
            var result = await HttpWebRequestHelper.GetStrFromPcwebAsync(strUrl, Encoding.UTF8);
            Assert.AreEqual(result.Length > 1, true);
        }

        [Test()]
        [TestCase("http://bn.sina.cn/video/live?vt=4&pos=17&channel=finance&newsid=stock")]
        public async void GetStrFromMobilewebAsyncTest(string strUrl)
        {
            var result = await HttpWebRequestHelper.GetStrFromMobilewebAsync(strUrl, Encoding.UTF8);
            Assert.AreEqual(result.Length > 1, true);
        }

        [Test()]
        [TestCase("http://finance.sina.com.cn/stock/y/2016-01-05/doc-ifxneept3713456.shtml")]
        public void GetStrPcwebByCookieTest(string strUrl)
        {
            
        }

        [Test()]
        [TestCase("http://finance.sina.com.cn/stock/y/2016-01-05/doc-ifxneept3713456.shtml")]
        public void GetStrFromPcwebNoEncodingTest(string strUrl)
        {
            var result = HttpWebRequestHelper.GetStrFromPcwebNoEncoding(strUrl,null);
            Assert.AreEqual(result.Length > 1, true);
        }

        [Test()]
        public void PostLoginTest()
        {
            
        }
    }
}
