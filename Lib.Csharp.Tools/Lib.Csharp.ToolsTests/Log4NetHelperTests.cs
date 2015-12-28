using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.Csharp.Tools;
using NUnit.Framework;

namespace Lib.Csharp.ToolsTests
{
    [TestFixture()]
    public class Log4NetHelperTests
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
        public void DebugTest()
        {
            Log4NetHelper.Debug("test debug");
        }

        [Test()]
        public void DebugTest1()
        {
            try
            {
                var s1 = 0;
                var s2 = 0;
                var str = s1 / s2;
            }
            catch (Exception ex)
            {
                Log4NetHelper.Debug(ex);
            }
        }

        [Test()]
        public void ErrorTest()
        {
            Log4NetHelper.Debug("test error");
        }

        [Test()]
        public void FatalTest()
        {
            Log4NetHelper.Debug("test fatal");
        }

        [Test()]
        public void InfoTest()
        {
            Log4NetHelper.Debug("test info");
        }

        [Test()]
        public void WarnTest()
        {
            Log4NetHelper.Debug("test warn");
        }
    }
}
