using System;
using Lib.Csharp.Tools;
using NUnit.Framework;

namespace Lib.Csharp.ToolsTests
{
    [TestFixture()]
    public class ImageHelperTests
    {
        const string OriginPic = "ResourcesForTest/login.jpg";
        const string WaterPic = "ResourcesForTest/cow.png";
        const string SavePic = "ResourcesForTest/deal.jpg";
        const string WaterText = "同意";

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
        public void MakeThumbnailTest()
        {
            ImageHelper.MakeThumbnail(OriginPic, SavePic, 200, 200, "W");

            var result = ImageHelper.GetPicInfo(SavePic);
            Assert.AreEqual(result.Width, 200);
        }

        [Test()]
        public void AddWaterTest()
        {
            ImageHelper.AddWater(OriginPic, SavePic, WaterText, false);
        }

        [Test()]
        public void AddWaterPicTest()
        {
            ImageHelper.AddWaterPic(OriginPic, SavePic, WaterPic, false);
        }

        [Test()]
        public void IsAllowedExtensionTest()
        {
            var result = ImageHelper.IsAllowedExtension(OriginPic);
            Assert.AreEqual(result, true);

            var result2 = ImageHelper.IsAllowedExtension(OriginPic.Replace("jpg","xxx"));
            Assert.AreEqual(result2, false);
        }

        [Test()]
        public void IsAllowedLengthTest()
        {
            
        }
    }
}
