using System;
using System.Collections.Generic;
using Lib.Csharp.Tools;
using NUnit.Framework;
using System.Data;

namespace Lib.Csharp.ToolsTests
{
    [TestFixture()]
    public class JsonHelperTests
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
        public void ListToJsonTest()
        {
            var list = new List<UserData>();
            list.Add(new UserData() {Age=1,Passwork="111",Sex="man",UserId=1,UserName="001"});
            list.Add(new UserData() { Age = 1, Passwork = "222", Sex = "woman", UserId = 2, UserName = "002" });
            list.Add(new UserData() { Age = 1, Passwork = "333", Sex = "man", UserId = 3, UserName = "003" });

            var result = JsonHelper.ToJson(list);

            var list2 = JsonHelper.ToObject<List<UserData>>(result);

            Assert.AreEqual(list.Count, list2.Count);

        }
        [Test()]
        public void ToJsonTest()
        {
            var model= new UserData() { Age = 1, Passwork = "111", Sex = "man", UserId = 1, UserName = "001" };
            
            var result = JsonHelper.ToJson(model);
            var result2 = JsonHelper.ToObject<UserData>(result);

            Assert.AreEqual(model.Age== result2.Age,true);

        }
        [Test()]
        public void ToJsonByNameTest()
        {
            var model = new UserData() { Age = 1, Passwork = "111", Sex = "man", UserId = 1, UserName = "001" };

            var result = JsonHelper.ToJson(model,"userdata");
        }

        [Test()]
        public void DataTableToJsonTest()
        {
            var dt = new DataTable();
            dt.Columns.Add("Age", typeof (int));
            dt.Columns.Add("Passwork", typeof(string));
            dt.Columns.Add("Sex", typeof(string));
            dt.Columns.Add("UserId", typeof(int));
            dt.Columns.Add("UserName", typeof(string));

            dt.Rows.Add(new object[] {1,"111","man",1,"test111"});
            dt.Rows.Add(new object[] { 2, "222", "man", 2, "test222" });
            dt.AcceptChanges();

            var result = JsonHelper.ToJson(dt);
            var result2 = JsonHelper.ToObject<DataTable>(result);

            Assert.AreEqual(dt.Rows.Count == result2.Rows.Count, true);

        }
        [Test()]
        public void DataSetToJsonTest()
        {
            var dt = new DataTable();
            dt.Columns.Add("Age", typeof(int));
            dt.Columns.Add("Passwork", typeof(string));
            dt.Columns.Add("Sex", typeof(string));
            dt.Columns.Add("UserId", typeof(int));
            dt.Columns.Add("UserName", typeof(string));

            dt.Rows.Add(new object[] { 1, "111", "man", 1, "test111" });
            dt.Rows.Add(new object[] { 2, "222", "man", 2, "test222" });
            dt.AcceptChanges();

            var ds = new DataSet();
            ds.Tables.Add(dt);
            ds.AcceptChanges();

            var result = JsonHelper.ToJson(ds);
            var result2 = JsonHelper.ToObject<DataSet>(result);

            Assert.AreEqual(ds.Tables[0].Rows.Count == result2.Tables[0].Rows.Count, true);

        }
    }

}
