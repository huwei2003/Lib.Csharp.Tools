using System;
using Lib.Csharp.Tools.NetHttp;
using NUnit.Framework;

namespace Lib.Csharp.ToolsTests.NetHttp
{
    [TestFixture()]
    public class NetHttpTests
    {
        static INetHttp Client;
        /// <summary>
        /// 全局setup,不能使用async
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            Client = NetHttpFactory.GetInstanse();
        }
        /// <summary>
        /// 每次测试setup,不能使用async
        /// </summary>
        [SetUp]
        public void SetUp()
        {

        }

        [Test()]
        public async void GetStringAsyncTest()
        {
            var result = await Client.GetStringAsync("http://www.baidu.com");
            Assert.AreEqual(result.Length > 1, true);

        }

        [Test()]
        public void GetStreamAsyncTest()
        {
            
        }

        [Test()]
        public void GetAsyncTest()
        {
            
        }

        [Test()]
        public void PostAsyncTest()
        {
            
        }

        [Test()]
        public void DeleteAsyncTest()
        {
            
        }

        [Test()]
        public void PutAsyncTest()
        {
            
        }

        [Test()]
        public void PatchAsyncTest()
        {
            
        }
    }
}
