namespace UnitTestProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.IO; // для класса 
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Client;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<DateTime> DINET = new List<DateTime>();
            List<double> Num = new List<double>();
            Num.Add(0);
            Methods a = new Methods();
            DINET = a.Convert(Num, DINET);
            string zna;
            zna = DINET[0].ToString("F");
            Console.WriteLine(zna);
            string zna1 = "1 января 1970 г. 0:00:00";
            Assert.AreEqual(zna1, zna);
        }
    }
}
