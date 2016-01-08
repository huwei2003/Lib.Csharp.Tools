using System;
using Lib.Csharp.Tools;
using NUnit.Framework;

namespace Lib.Csharp.ToolsTests
{
    [TestFixture()]
    public class SmtpMailHelperTests
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
        [ExpectedException(typeof(Exception))]
        public async void SendMailTest_Noinit_Exception()
        {
            var mail = new MailData()
            {
                Attachments="",
                Content="邮件测试啦",
                MailTextType = MediaTextType.Text,
                Subject="test mail",
                To = "******@qq.com"
            };
            var result = await SmtpMailHelper.SendMail(mail);
        }

        [Test()]
        public async void SendMailTest_Success()
        {
            var mail = new MailData()
            {
                Attachments = "",
                Content = "邮件测试啦",
                MailTextType = MediaTextType.Text,
                Subject = "test mail",
                To = "****@qq.com"
            };
            var config = new SmtpMailConfig()
            {
                FromEail="******@163.com",
                FromName="nickname",
                Password="password",
                ReplyEmail = "*****@163.com",
                SmsCanUse="true",
                SmtpHost="smtp.163.com",
                SmtpPort=25,
            };
            SmtpMailHelper.Init(config);

            var result = await SmtpMailHelper.SendMail(mail);

            Assert.AreEqual(result, true);
        }
    }
}
