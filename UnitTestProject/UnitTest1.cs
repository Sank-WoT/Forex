namespace UnitTestProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.IO; // для класса 
    using System.Linq;
    using System.Media;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Windows.Forms.DataVisualization.Charting;
    using EnumDialogResult = System.Windows.Forms.DialogResult;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Client;
    using System.Windows.Forms;
    using System.Windows.Forms.DataVisualization.Charting;

    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        // Проверка пути первого запроса
        public void TestMethodTaskConnect()
        {
            Internet Test = new Internet();
            string pathFileEURUSD = Application.StartupPath + "\\" + "eurusd" + ".txt";
            string pathFileUSDJPY = Application.StartupPath + "\\" + "usdjpy" + ".txt";
            Internet IPair = new Internet();
            Assert.AreEqual(Test.TaskConnect("eurusd"), pathFileEURUSD);
            Assert.AreEqual(Test.TaskConnect("usdjpy"), pathFileUSDJPY);
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
            for (int i = 0; i < Test2.Count; i++)
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

        [TestMethod]
        public void TestConInet()
        {
            Internet intt = new Internet();
            bool internetActionFinished = true;
            intt.ReqestInet("eurusd", internetActionFinished);
            Assert.AreEqual(true, intt.ReqestInet("eurusd", internetActionFinished));
        }

        [TestMethod]
        public void TestConInets()
        {
            object sync = new object();
            bool internetActionFinished = false;
            Internet intt = new Internet();
            Assert.AreEqual(true, InetConnect.Inet = intt.TryCon("eurusd", sync, internetActionFinished));
        }

        [TestMethod]
        public void TestInterval()
        {
            List<List<double>> TestPoin = new List<List<double>>();
            List<double> TPoin = new List<double>();
            TPoin.Add(0);
            TPoin.Add(4);
            TestPoin.Add(TPoin);
            TPoin.Add(1);
            TPoin.Add(3);
            TestPoin.Add(TPoin);
            List<double> Buffer = new List<double>();
            Buffer.Add(4);
            Buffer.Add(3);
            Buffer.Add(5);
            IntervalResistance Test = new IntervalResistance(3, Buffer,  1232425);
            Console.WriteLine();
            Assert.AreEqual(TestPoin[0][1], Test.poin[0][1]);
            Assert.AreEqual(TestPoin[0][0], Test.poin[0][0]);
            Assert.AreEqual(TestPoin[1][1], Test.poin[1][1]);

            List<List<double>> TestPoin1 = new List<List<double>>();
            List<double> TPoin1 = new List<double>();
            TPoin1.Add(0);
            TPoin1.Add(6);
            TestPoin1.Add(TPoin);
            TPoin1.Add(-1);
            TPoin1.Add(7);
            TestPoin1.Add(TPoin);
            List<double> Buffer1 = new List<double>();
            Buffer1.Add(6);
            Buffer1.Add(7);
            Buffer1.Add(5);
            IntervalResistance Test1 = new IntervalResistance(3, Buffer, 1232425);
            Console.WriteLine();
            Assert.AreEqual(TestPoin1[0][1], Test.poin[0][1]);
            Assert.AreEqual(TestPoin1[0][0], Test.poin[0][0]);
            Assert.AreEqual(TestPoin1[1][1], Test.poin[1][1]);       
        }

        [TestMethod]
        public void TestLineCoord()
        {
            int x = 1920, y = 1080;
            double fX = 200,  fy = 200;
            Label lab_Cur = new Label();
            Label label_Y = new Label();
            Label label_X = new Label();
            Chart graphic = new Chart();
            LineCoord Test = new LineCoord(x, y, fX, fy, lab_Cur, label_Y, label_X, ref graphic);
            Assert.AreEqual(1, Test.Svoistvo_xS);
            Assert.AreEqual(1, Test.Svoistvo_yS);
            Assert.AreEqual(fX, Test.Svoistvo_fX);
            Assert.AreEqual(fy, Test.Svoistvo_fY);
        }

        [TestMethod]
        public void TestLineCoordObject()
        {
            int x = 1920, y = 1080;
            double fX = 200, fy = 200;
            Label lab_Cur = new Label();
            Label label_Y = new Label();
            Label label_X = new Label();
            Chart graphic = new Chart();
            CheckBox checkBoxLineCoord = new CheckBox();
            checkBoxLineCoord.Checked = true;
            LineCoord Test = new LineCoord(x, y, fX, fy, lab_Cur, label_Y, label_X, ref graphic);
            Test.Show(checkBoxLineCoord);
            Assert.AreEqual(true, Test.Svoistvo_label_X.Visible);
            Assert.AreEqual(true, Test.Svoistvo_label_Y.Visible);
            Assert.AreEqual(true, Test.Svoistvo_lab_Cur.Visible);
            checkBoxLineCoord.Checked = false;
            Test.Show(checkBoxLineCoord);
            Assert.AreEqual(false, Test.Svoistvo_label_X.Visible);
            Assert.AreEqual(false, Test.Svoistvo_label_Y.Visible);
            Assert.AreEqual(false, Test.Svoistvo_lab_Cur.Visible);
        }


        [TestMethod]
        public void TesConvertD()
        {
            Methods Test = new Methods();
            List<int> a = new List<int>();
            List<DateTime> b = new List<DateTime>();
            a.Add(1000);
            a.Add(5000);
            a.Add(7000);
            a.Add(17000);
            a.Add(1);
            b = Test.ConvertD(a);
            DateTime t = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(1000);
            Assert.AreEqual(t, b[0]);
            Assert.AreEqual( t = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(5000), b[1]);
            Assert.AreNotEqual(t = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(5000), b[2]);
            Assert.AreNotEqual(t = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(545435000), b[3]);
            Assert.AreEqual(t = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(1), b[4]);
        }


        [TestMethod]
        public void TestConvertU()
        {
            Methods Test = new Methods();
            List<DateTime> a = new List<DateTime>();
            List<int> b = new List<int>();
            a.Add((new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(5000));
            a.Add((new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(2000));
            a.Add((new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(1000));
            a.Add((new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(5454));
            a.Add((new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(1));
            b = Test.ConvertU(a);
            Assert.AreEqual(5000, b[0]);
            Assert.AreEqual(2000, b[1]);
            Assert.AreNotEqual(5000, b[2]);
            Assert.AreNotEqual(545435000, b[3]);
            Assert.AreEqual(1, b[4]);
        }

        [TestMethod]
        public void TestMinMax()
        {
            MinMax Test = new MinMax();
            List<double> TestV = new List<double>();
            List<DateTime> TestT = new List<DateTime>();
            TestV.Add(2);
            TestV.Add(3);
            TestV.Add(1);
            TestV.Add(8);
            TestT.Add((new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(101));
            TestT.Add((new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(102));
            TestT.Add((new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(104));
            TestT.Add((new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(106));
            int danoe = 0;
            Test.AddMinMax(TestV, TestT, danoe);
            Assert.AreEqual(1, Test.Svoistvo_poin);
        }

        [TestMethod]
        public void TestParser()
        {
            Parser Test = new Parser("1454702997,1.1135,1.1138;");
            Assert.AreEqual("1454702997,1.1135,1.1138;", Test.response_property);
            List<int> ListTBuf = new List<int>();
            List<double> BListBBuf = new List<double>();
            List<double> BListSBuf = new List<double>();
            Test.BDREqest(ref ListTBuf, ref BListSBuf, ref BListBBuf);
            Assert.AreEqual(1454702997, ListTBuf[0]);
            Assert.AreEqual(1.1138, BListBBuf[0]);
            Assert.AreEqual(1.1135, BListSBuf[0]);

            Parser Test1 = new Parser("1454703008,1.1136,1.1139;");
            Assert.AreEqual("1454703008,1.1136,1.1139;", Test1.response_property);
            List<int> ListTBuf1 = new List<int>();
            List<double> BListBBuf1 = new List<double>();
            List<double> BListSBuf1 = new List<double>();
            Test1.BDREqest(ref ListTBuf1, ref BListSBuf1, ref BListBBuf1);
            Assert.AreEqual(1454703008, ListTBuf1[0]);
            Assert.AreEqual(1.1139, BListBBuf1[0]);
            Assert.AreEqual(1.1136, BListSBuf1[0]);

            Parser Test2 = new Parser("1454712296,1.1145,1.1148;1454712307,1.1144,1.1147;1454712317,1.1145,1.1148;1454712322,1.1144,1.1147;1454712334,1.1145,1.1148;");
            Assert.AreEqual("1454712296,1.1145,1.1148;1454712307,1.1144,1.1147;1454712317,1.1145,1.1148;1454712322,1.1144,1.1147;1454712334,1.1145,1.1148;", Test2.response_property);
            List<int> ListTBuf2 = new List<int>();
            List<double> BListBBuf2 = new List<double>();
            List<double> BListSBuf2 = new List<double>();
            List<int> TestListTBuf2 = new List<int>();
            List<double> TestBListBBuf2 = new List<double>();
            List<double> TestBListSBuf2 = new List<double>();
            TestListTBuf2.Add(1454712296);
            TestListTBuf2.Add(1454712307);
            TestListTBuf2.Add(1454712317);
            TestListTBuf2.Add(1454712322);
            TestListTBuf2.Add(1454712334);

            TestBListBBuf2.Add(1.1148);
            TestBListBBuf2.Add(1.1147);
            TestBListBBuf2.Add(1.1148);
            TestBListBBuf2.Add(1.1147);
            TestBListBBuf2.Add(1.1148);

            TestBListBBuf2.Add(1.1145);
            TestBListBBuf2.Add(1.1144);
            TestBListBBuf2.Add(1.1145);
            TestBListBBuf2.Add(1.1144);
            TestBListBBuf2.Add(1.1145);
            Test2.BDREqest(ref ListTBuf1, ref BListSBuf2, ref BListBBuf2);
            for(int i = 0; i < ListTBuf2.Count; i++)
            {
                Assert.AreEqual(TestListTBuf2[i], ListTBuf2[i]);
                Assert.AreEqual(TestBListBBuf2[i], BListBBuf2[i]);
                Assert.AreEqual(TestBListSBuf2[i], BListSBuf2[i]);
            }
        }

        [TestMethod]
        public void TestGlue()
        {
            List<double> BListBBuf = new List<double>();
            List<double> BListBFile = new List<double>();
            List<double> Testi = new List<double>();
            List<double> Testin = new List<double>();
            BListBBuf.Add(1.1148);
            BListBFile.Add(1.1146);
            Splice Test = new Splice();
            Testi = Test.glue(BListBFile,BListBBuf, 1, 1);
            Testin.Add(1.1146);
            Testin.Add(1.1148);
            Assert.AreEqual(Testin[0], Testi[0]);
            Assert.AreEqual(Testin[1], Testi[1]);

            BListBBuf.Add(1.1142);
            BListBFile.Add(1.1141);
            Testi = Test.glue(BListBFile, BListBBuf, 2, 2);
            List<double> Testin1 = new List<double>();
            Testin1.Add(1.1146);
            Testin1.Add(1.1141);
            Testin1.Add(1.1148);
            Testin1.Add(1.1142);
            Assert.AreEqual(Testin1[0], Testi[0]);
            Assert.AreEqual(Testin1[2], Testi[2]);
            Assert.AreEqual(Testin1[3], Testi[3]);
            Assert.AreEqual(4, Testi.Count);

            Testi = Test.glue(BListBBuf,BListBFile);
            Assert.AreEqual(3, Testi.Count);
            Assert.AreEqual(Testin1[0], Testi[0]);
            Assert.AreEqual(Testin1[3], Testi[2]);
        }

        [TestMethod]
        public void TestWorkFile()
        {
            WorkFile Test = new WorkFile();
            Assert.AreEqual(true, Test.CreateFile(Application.StartupPath + "\\" + "eurusd" + ".txt"));
            Assert.IsNotNull(Test.ReadFile(Application.StartupPath + "\\" + "eurusd" + ".txt"));
        }
    }
}
