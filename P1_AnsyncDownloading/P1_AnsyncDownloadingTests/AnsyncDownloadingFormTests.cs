using Microsoft.VisualStudio.TestTools.UnitTesting;
using P1_AnsyncDownloading;
using P1_AnsyncDownloading.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1_AnsyncDownloading.Tests
{
    [TestClass()]
    public class AnsyncDownloadingFormTests
    {   

        [TestMethod()]
        public void URLTest()
        {
            TargetFile file = new TargetFile("http://www.searhorse.vn/file.zip");
            Console.WriteLine(file.URL);
            Assert.AreEqual(true, file.ValidURL);

            file.URL = "https://drive.google.com/open?id=0B5fyPnNYo-UYTUdqV2toQlF3UUU";
            Console.WriteLine(file.URL);
            Assert.AreEqual(true, file.ValidURL);

            file.URL = "http://www.searhorse.vn";
            Console.WriteLine(file.URL);
            Assert.AreEqual(true, file.ValidURL);

            file.URL = "http://www.searhorse.vn/@";
            Console.WriteLine(file.URL);
            Assert.AreEqual(false, file.ValidURL);

            file.URL = "www.searhorse.vn";
            Console.WriteLine(file.URL);
            Assert.AreEqual(false, file.ValidURL);
        }
    }
}