using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YazilimYonetimAraci.BL;

namespace WhiteBoxTesting
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UserTest
    {
        public object TestLogin { get; private set; }

        [TestMethod]
        public void GirisTrue()
        {
            LoginTest login = new LoginTest();
            bool durum= login.Login("YZM_Emre", "111111");
            Assert.AreEqual(true, durum);
        }

        [TestMethod]
        public void GirisFalse()
        {
            LoginTest login = new LoginTest();
            bool durum = login.Login("YZM_Emre", "1111112");
            Assert.AreEqual(false, durum);
        }

        [TestMethod]
        public void EditTrue()
        {
            LoginTest login = new LoginTest();
            bool durum = login.EditProject("TWİTTER", DateTime.Today, DateTime.Today, 1, 500, true, 3);
            Assert.AreEqual(true, durum);
        }

        [TestMethod]
        public void EditFalse()
        {
            LoginTest login = new LoginTest();
            bool durum = login.EditProject("TWİTTER", DateTime.Today, DateTime.Today, 1, 500, true,323);
            Assert.AreEqual(false, durum);
        }
    }
}
