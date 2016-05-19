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
    using System.Windows.Forms;

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
        } //  Тест на правильность работы конвертора времени.

        [TestMethod]
        public void TestMethod3()
        {
            Methods c = new Methods();
            double Value;
            List<double> BufferS = new List<double>();
            BufferS.Add(3);
            BufferS.Add(4);
            BufferS.Add(5);
            BufferS.Add(6);
            Value = c.TradeBuy(true, BufferS, 2);
            Assert.AreEqual(4, Value);
        } // Ткст  на правильность работы точек изменения тренда

        [TestMethod]
        public void TestMethod4()
        {
            bool inet = true;
            Windowd c = new Windowd();
            Assert.AreEqual(true, c.TryCon(inet));
        } // Тест на проверку интернета

        [TestMethod]
        public void TestMethod5()
        {
            string patchFile = "eurusd.txt";
            Windowd c = new Windowd();
             Assert.AreEqual(true, c.CreateFile(patchFile));
        } // Тест на наличие файла

        [TestMethod]
        public void TestMethod6()
        {
            DateTime Date = new DateTime();
            Methods c = new Methods();          
            string d = "Mon";
            string a = "Thu";
            Assert.AreEqual(d,c.TradeStop(Date.AddSeconds(1463674549)));
            Assert.IsFalse(a==c.TradeStop(Date.AddSeconds(1463674549)));
        } // Тест на правильную выборку дня недели
    }
}
