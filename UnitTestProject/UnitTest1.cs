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
    public class UnitTest
    {
        [TestMethod]
        // Проверка пути первого запроса
        public void TestMethodTaskConnect()
        {
            MainForm Test = new MainForm(1000, 500);
            string pathFileEURUSD = Application.StartupPath + "\\" + "eurusd" + ".txt";
            string pathFileUSDJPY = Application.StartupPath + "\\" + "usdjpy" + ".txt";
            Internet IPair = new Internet();
            Assert.AreEqual(Test.TaskConnect(IPair, "eurusd"), pathFileEURUSD);
            Assert.AreEqual(Test.TaskConnect(IPair, "usdjpy"), pathFileUSDJPY);
        }

        [TestMethod]
        // Проверка пути первого запроса
        public void TestMethodConection()
        {
            Internet a = new Internet();
            StreamReader DataReader;
            DataReader = a.Conection(1, 0, "eurusd");
            string response = DataReader.ReadToEnd();
            Assert.AreEqual(response.Trim(), "1454702997,1.1135,1.1138");
            DataReader = a.Conection(1, 1454702997, "eurusd");
            response = DataReader.ReadToEnd();
            Assert.AreEqual(response.Trim(), "1454703008,1.1136,1.1139");
            DataReader = a.Conection(10, 1454702997, "eurusd");
            response = DataReader.ReadToEnd();
            Assert.AreEqual(response.Trim(), "1454703008,1.1136,1.1139;1454703893,1.1134,1.1137;1454703916,1.1134,1.1137;1454703932,1.1133,1.1136;1454703942,1.1132,1.1135;1454703966,1.1131,1.1134;1454703967,1.1131,1.1134;1454703971,1.1132,1.1135;1454703973,1.1131,1.1134;1454703981,1.1131,1.1134");
            DataReader = a.Conection(1, 1454703981, "eurusd");
            response = DataReader.ReadToEnd();
            Assert.AreEqual(response.Trim(), "1454703984,1.1132,1.1135");
            DataReader = a.Conection(1, 0, "usdjpy");
            response = DataReader.ReadToEnd();
            Assert.AreEqual(response.Trim(), "1463788765,110.11,110.14");
            // выдача ошибки
            DataReader = a.Conection(1, 0, "jkhjk");
            response = DataReader.ReadToEnd();
            Assert.AreEqual(response.Trim(), "4");
        }

        [TestMethod]
        // Проверка пути первого запроса
        public void Testdeal()
        {
            ClassSMA a = new ClassSMA();
            List<double> TestNum = new List<double>();
            List<double> Test2 = new List<double>();
            List<double> TestCheck = new List<double>();
            TestNum.Add(5);
            TestNum.Add(5);
            a.Add(2, TestNum);
            Test2 = a.GetSred();
            Assert.AreEqual(Test2[0].ToString(), "5");

            Test2.Clear();
            TestNum.Clear();
            TestNum.Add(5);
            TestNum.Add(6);
            a.Add(2, TestNum);
            Test2 = a.GetSred();
            Assert.AreEqual(Test2[0].ToString(), "5,5");


            Test2.Clear();
            TestNum.Clear();
            TestNum.Add(5);
            TestNum.Add(6);
            TestNum.Add(7);
            TestNum.Add(11);
            a.Add(2, TestNum);
            Test2 = a.GetSred();
            TestCheck.Add(5.5);
            TestCheck.Add(6.5);
            TestCheck.Add(9);
            for(int i = 0; i < Test2.Count; i++)
            Assert.AreEqual(Test2[i], TestCheck[i]);

            TestCheck.Clear();
            Test2.Clear();
            TestNum.Clear();
            TestNum.Add(5);
            TestNum.Add(6);
            TestNum.Add(7);
            TestNum.Add(11);
            a.Add(4, TestNum);
            Test2 = a.GetSred();
            TestCheck.Add(5.5);
            TestCheck.Add(6.5);
            TestCheck.Add(9);
            for (int i = 0; i < Test2.Count; i++)
            Assert.AreNotEqual(Test2[i], TestCheck[i]);

            TestCheck.Clear();
            Test2.Clear();
            TestNum.Clear();
            TestNum.Add(5);
            TestNum.Add(6);
            TestNum.Add(7);
            TestNum.Add(11);
            a.Add(4, TestNum);
            Test2 = a.GetSred();
            TestCheck.Add(7.25);
            for (int i = 0; i < Test2.Count; i++)
            Assert.AreEqual(Test2[i], TestCheck[i]);
        }
      
    }
}
