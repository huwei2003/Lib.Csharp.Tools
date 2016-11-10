using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.Csharp.Tools;
using Lib.Csharp.Tools.Extend;
using NUnit.Framework;


namespace Lib.Csharp.ToolsTests
{
    [TestFixture()]
    public class DateTimeHelperTests
    {
        private DateTime _originDt = DateTime.Now;
        /// <summary>
        /// 全局setup,不能使用async
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _originDt = DateTime.Now;
        }
        /// <summary>
        /// 每次测试setup,不能使用async
        /// </summary>
        [SetUp]
        public void SetUp()
        {

        }

        [Test()]
        public void GetTimeStampTest()
        {
            var dt = DateTime.Now;
            var d1 = DateTimeExt.GetTimeStamp(dt,true);
            var d2 = DateTimeExt.GetTimeStamp(dt,false);
            Console.WriteLine("d1="+d1);
            Console.WriteLine("d2=" + d2);
            Assert.IsTrue(d2.ToString().Length==d1.ToString().Length+3);

        }

        [Test()]
        public void GetFormatDateTest()
        {
            var dt1 = DateTimeExt.GetFormatDate(DateTime.Now);
            Console.WriteLine(dt1);
            Assert.AreEqual(dt1.Length, 19);
        }

        [Test()]
        public void GetFormatLongDateTest()
        {
            var dt1 = DateTimeExt.GetFormatLongDate(DateTime.Now);
            Console.WriteLine(dt1);
            Assert.AreEqual(dt1.Length, 26);
        }

        [Test()]
        public void GetGmtTimeTest()
        {
            var dt1 = DateTimeExt.GetGmtTime(DateTime.Now);
            Console.WriteLine(dt1);
            Assert.AreEqual(dt1.Length, 29);
        }

        [Test()]
        public void ToDateTimeTest()
        {
            var dt1 = DateTimeExt.ToDateTime("2015-12-1 12:22");
            Console.WriteLine(dt1);
            Assert.AreEqual(dt1.ToString().Length, 18);
        }

        [Test()]
        public void ToDateTimeTest1()
        {
            var t1 = (DateTime.UtcNow.Ticks - 621355968000000000) / 10000000;
            var dt1 = DateTimeExt.ToDateTime(t1);
            Console.WriteLine(dt1);
        }

        [Test()]
        public void GetUtcDateTimeTest()
        {
            var dt1 = DateTimeExt.GetUtcDateTime(DateTime.Now);
            Console.WriteLine(dt1);
        }

        [Test()]
        public void GetChineseDateTest()
        {
            var dt1 = DateTimeExt.GetChineseDate("2015-12-1 12:22", 1);
            Console.WriteLine(dt1);

            var dt2 = DateTimeExt.GetChineseDate("2015-12-1 12:22", 2);
            Console.WriteLine(dt2);
        }

        [Test()]
        public void DateSubtractTest()
        {
            var dt1 = DateTimeExt.DateSubtract("2015-12-1 12:22", "2015-12-11 10:22");
            Console.WriteLine(dt1);
        }

        [Test()]
        public void OSetServerTimeTest()
        {
            var dt1 = DateTime.Now.AddDays(-30);
            DateTimeExt.SetServerTime(dt1);
            var dt2 = DateTime.Now;
            var dt3 = DateTimeExt.DateSubtract(dt1.ToString(), dt2.ToString());
            Console.WriteLine(dt1);
            Console.WriteLine(dt2);
            Console.WriteLine(dt3);
            Assert.AreEqual(dt3<=1, true);
        }

        [Test()]
        public async Task ReSetServerTimeTest()
        {
            var ip = "202.120.2.101";
            var port = "123";
            await DateTimeExt.ReSetServerTime(ip,port);


            var dt2 = DateTime.Now;

            var dt3 = DateTimeExt.DateSubtract(_originDt.ToString(), dt2.ToString());

            Console.WriteLine(_originDt);
            Console.WriteLine(dt2);
            Console.WriteLine(dt3);
            Assert.AreEqual(dt3<=1, true);

        }
    }
}
