namespace Client
{
    using System;
    using System.Drawing;
    using System.IO; // для класса 
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Начальное окно
    /// </summary>
    public partial class MainForm : Form
    {
        bool inet = false;
        double xS;
        double yS;
        private object sync = new object();
        private bool internetActionFinished = false;
        Task tConnect, tEurusd, tUsdjpy;
        #region Переменные закрытия окон
        public static bool HelpClosing = true; // Переменная отвечающая за закрытое окной HelpClosing
        public static bool SWindowClosing = true; // Переменная отвечающая за закрытое окной SWindowClosing
        public static bool WindowClosingEURUSD = true; // Переменная отвечающая за закрытое окной WindowClosing
        public static bool WindowClosingUSDJPY = true;
        public static bool SChartClosing = true; // Переменная отвечающая за закрытое окной SChartClosing 
        public List<string> valueP = new List<string>();
        #endregion

        /// <summary>
        /// Метод стартового состояния окна
        /// </summary>
        /// <param name="x">размер по оси x</param>
        /// <param name="y">размер по оси y</param>

        public MainForm(int x, int y)
        {
            string pathDirectory = Application.StartupPath; // Путь к директории
            string pathFile = pathDirectory + "\\" + "eurusd" + ".txt"; // Путь к файлу c котировками eurusd

            Methods Time = new Methods();
             // проверка интерент соединения ассинхронно
            tConnect = Task.Run(() =>
            {
                Internet inCon = new Internet();
                InetConnect.Inet = inCon.TryCon(inet, "eurusd", sync, internetActionFinished);
            });

            switch (Time.TradeStop(DateTime.Now))
            {
                case "Sat": MessageBox.Show("Forex day off"); break;
                case "Sun": MessageBox.Show("Forex day off"); break;
            }

            this.InitializeComponent();

            splitContainer1.Size = new Size(x, y);
            splitContainer1.Location = new Point(0, 0);
           
            // Массив кнопок интерфейса
            Button[] LButton = {buttonEurUsd, buttonUsdJpy}; 

            foreach (Button index in LButton)
            {
                splitContainer1.Panel2.Controls.Add(index);
            }

            // Массив меток интерфейса
            Label[] LLabel = { labelSelectPair };

            foreach (Label index in LLabel)
            {
                splitContainer1.Panel2.Controls.Add(index);
            }

            DirectoryInsspection.Set(pathDirectory); // проверка существования директории

            FileInspection.Set(pathFile); // проверка существования файла

            this.FormClosing += new FormClosingEventHandler(OnClosing);
            LButton[0].Location = new Point( x / 2 - 400, y / 2 - 100); // Первая кнопка EurUsd
            LLabel[0].Location = new Point(x / 2 - 100, y / 2 - 200); //  Метка
            LButton[1].Location = new Point(x / 2  + 200, y / 2 - 100); // Вторая кнопка UsdJpy
            xS = x / 1920.0; // настройка под все  экраны
            yS = y / 1080.0; // настройка под все  экраны
            this.Size = new Size(x, y); // задание размеров экрана
           
            #region Переменные командной комбинации к Меню текущей форме
            windowToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.W; // командная комбинация клавиш для откытия настроек окна
            chartToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C; // командная комбинация клавиш для откытия настроек графика
            eURUSDToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.U; // командная комбинация клавиш для откытия графика USDEUR
            helpToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.H; // командная комбинация клавиш для откытия помощи
            USDJPYToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Y; // командная комбинация клавиш для откытия графика EURYPJ
            #endregion

            if (HelpClosing == true)
            {
                helpToolStripMenuItem.CheckState = CheckState.Unchecked;
            }

            Internet IPair = new Internet();
            Cursor.Current = Cursors.WaitCursor; // Грузящий курсор
            tConnect.Wait(); 
            Cursor.Current = Cursors.Default; // Возвращение к нормальному состоянию
            LoadData(IPair); // загрузка данных 2х потоков с данными №1
        }

        /// <summary>
        /// Метод загрузки данных
        /// </summary>
        /// <param name="IPair">Объект интернет</param>
        public void LoadData(Internet IPair)
        {
            tEurusd = Task.Run(() =>
            {
                string pathFile1 = Application.StartupPath + "\\" + "eurusd" + ".txt"; // Путь к файлу c котировками eurusd
                IPair.FirstConnect("eurusd", pathFile1); // первое подключении
            }); // поток подключения eurusd

            string pathFile = Application.StartupPath + "\\" + "eurusd" + ".txt";
            string patch = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename='|DataDirectory|\\Forex.mdf'; Integrated Security = True; Connect Timeout = 30";// данные конфигурации
            BdReqest reqestBdEURUSD = new BdReqest(patch); // Создание объекта БД 
            string bdValue = "eurusd";
            string response = IPair.FirstConnectBD(bdValue, pathFile);
            List<int> BListTBuf = new List<int>();
            List<double> BListBBuf = new List<double>();
            List<double> BListSBuf = new List<double>();
            Parser BdParser = new Parser(response);
            // Присвоили данные к листам
            BdParser.BDREqest(ref BListTBuf, ref BListBBuf, ref BListSBuf);
            // Важный запрос добавления осталось это проверить
            reqestBdEURUSD.CommandInsert (bdValue, BListTBuf, BListBBuf, BListSBuf); 

            tUsdjpy = Task.Run(() =>
            {
                string pathFile2 = Application.StartupPath + "\\" + "usdjpy" + ".txt"; // Путь к файлу c котировками usdjpy
                IPair.FirstConnect("usdjpy", pathFile2); // первое подключении
            }); // поток подключения usdjpy
        }



        ///////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Cоздание размеров окна
        /// </summary>
        /// <param name="x">размер по кординате x</param>
        /// <param name="y">размер по кординате y</param>
        public void WindowQuotes(ref int x, ref int y, ref bool WindowClosing)
        {
            string readX, readY;
            // Задание размеров окна
            WindowClosing = false;
            string pathFile = Application.StartupPath + "\\SettingWindow.txt"; // путь
            //// проверка на существование файла
            if (!File.Exists(pathFile))
            {
                FileInfo writel = new FileInfo(pathFile); // получаем путь  для записи и создания
                StreamWriter l = writel.CreateText(); // создаем текст
                string text = "EURUSD   " + 1366 + "      " + 757;
                l.WriteLine(text);
                l.Close(); // закрыть запись
            } ////Создаем файл настроек окон первые две записи // необходимо придумать тег
            #region Чтение из файла  FileInfo write = new FileInfo(pathFile);
            FileInfo write = new FileInfo(pathFile); // получаем путь 
            StreamReader r1 = new StreamReader(pathFile);
            string text1 = r1.ReadToEnd(); // получение прочтенной записи
            r1.Close(); // закрыть чтение 
            Regex regex = new Regex(@"(EURUSD\s\s\s\d{1,20}\s\s\s\s\s\s\d{1,20})"); // регулярное выражение для поиска размеров окна
            Regex regex1 = new Regex(@"(\d{1,20})"); // регулярное выражение для поиска размеров окна
            MatchCollection m = regex.Matches(text1);
            string response = m[0].ToString();
            MatchCollection m1 = regex1.Matches(response);
            readX = m1[0].Value;
            readY = m1[1].Value;
            x = Convert.ToInt32(readX);
            y = Convert.ToInt32(readY);
            #endregion
            #region Присвоение прочтенного из файла к WSrting.X WSrting.Y
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Cоздание  окна
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        public void EURUSDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            tConnect.Wait();
            tEurusd.Wait();
          
            if (WindowClosingEURUSD == true)
            {
                int x = 0, y = 0;
                WindowQuotes(ref x, ref y,ref WindowClosingEURUSD);
                #region Присвоение прочтенного из файла к WSrting.X WSrting.Y
                WString.X = x; // Присвоение глобальной переменной для всего проекта для передачи значений между формами (размеры окна по X)
                WString.Y = y; // Присвоение глобальной переменной для всего проекта для передачи значений между формами (размеры окна по Y)
                #endregion
                #region  Window f1 = new Window(); Создание модального окна

                Windowd f1 = new Windowd("eurusd");
                // Set the Parent Form of the Child window.
                f1.MdiParent = this;
                // окно 
                splitContainer1.Visible = false;
                f1.Show();
                f1.Size = new Size(x, y); // Задаем значение размера формы Window 
                f1.Location = new Point(0, 0); // размещение окна EURUSD
                #endregion
            }
            #endregion
        } //// показать  график  EUR/USD

        public void USDJPYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            tConnect.Wait();
            tUsdjpy.Wait();
            int X = 0, Y = 0;

            if (WindowClosingUSDJPY == true)
            {
                WindowQuotes(ref X, ref Y,ref WindowClosingUSDJPY);
                // Присвоение глобальной переменной для всего проекта для передачи значений между формами (размеры окна по X)
                WString.X = X;
                // Присвоение глобальной переменной для всего проекта для передачи значений между формами (размеры окна по Y)
                WString.Y = Y;
                Windowd f2 = new Windowd("usdjpy");
                // Set the Parent Form of the Child window.
                f2.MdiParent = this;
                // окно 
                splitContainer1.Visible = false;
                f2.Show();
                f2.Size = new Size(X, Y); // Задаем значение размера формы Window 
                f2.Location = new Point(0, 0); // размещение окна EURUSD
            }
        } 
        
        // убрать данные переполнения

        /// <summary>
        /// Создание окна SWindow
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        public void WindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
             if (SWindowClosing == true)
             {
              #region Создание окна SWindow
                    SWindow f2 = new SWindow();
                    SWindowClosing = false;
                    f2.Show();
                    #endregion
             }     
        } //// показать форму настройки окна

        /// <summary>
        /// Создание окна SChart
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        public void СhartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SChartClosing == true)
            {
             #region Создание окна SChart
                SChartcs f3 = new SChartcs();
                SChartClosing = false;
                f3.Show();
                #endregion
            }
        } //// показать форму настройки графика

       
       
        /// <summary>
        /// Создание окна SChart
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        public void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (HelpClosing == true)
            {
                #region Создание окна Help
                Help f4 = new Help();
                HelpClosing = false;
                f4.Show();
                #endregion
            }
        }

        /// <summary>
        /// Функция смены языка
        /// </summary>
        public void ChangeTextLanguage()
        {
            if (WString.RUS == true)
            {
                #region перевод на русский язык меню текущей формы
                EditInterface("Стандартные настройки", "Окно", "График", "О программе", "Создатели", "Справка", "Валютные пары", "Евро/Доллар", "Доллар/Йена", "English", "Выберите валютную пару для начала торгов");
                #endregion
            }

            if (WString.ENG == true)
            {
                #region перевод на английский язык меню текущей формы
                EditInterface("Settings", "Window", "Chart", "About", "Autors", "Help", "Currency pairs", "EUR / USD", "USD/JPY", "Русский", "Select a currency pair to start trading");
                #endregion
            }
        }

        public void EditInterface(string settingsToolStripMenu, string windowToolStripMenu, string chartToolStripMenu, string AboutToolStripMenu, string ToolStripMenu, string helpToolStripMenu, string currencyPairsToolStripMenu, string eURUSDToolStripMenu, string USDJPYToolStripMenu, string langToolStripMenu, string labelSelect)
        {
            settingsToolStripMenuItem.Text = settingsToolStripMenu;
            windowToolStripMenuItem.Text = windowToolStripMenu;
            chartToolStripMenuItem.Text = chartToolStripMenu;
            AboutToolStripMenuItem.Text = AboutToolStripMenu;
            создателиToolStripMenuItem.Text =ToolStripMenu;
            helpToolStripMenuItem.Text = helpToolStripMenu;
            currencyPairsToolStripMenuItem.Text = currencyPairsToolStripMenu;
            eURUSDToolStripMenuItem.Text = eURUSDToolStripMenu;
            USDJPYToolStripMenuItem.Text = USDJPYToolStripMenu;
            langToolStripMenuItem.Text = langToolStripMenu;
            labelSelectPair.Text = labelSelect;
        } // Создание изменение интерфейса

        private void Form1_Load(object sender, EventArgs e)
        {
            ChangeTextLanguage();
            // Присвоение MDI контейнеру цвета background Form
            MdiClient ctlMDI;
            // Loop through all of the form's controls looking
            // for the control of type MdiClient.
            foreach (Control ctl in this.Controls)
            {
                try
                {
                    // Attempt to cast the control to type MdiClient.
                    ctlMDI = (MdiClient)ctl;

                    // Set the BackColor of the MdiClient control.
                    ctlMDI.BackColor = this.BackColor;
                }
                catch (InvalidCastException exc)
                {
                    // Catch and ignore the error if casting failed.
                }
            }
        }

        private void AboutProgrammToolStripMenuItem_Click(object sender, EventArgs e)
        {     
        }

        /// <summary>
        /// The output method window about the creators
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void CreatoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("                             Creators \n\nSerobabov Alexandr - executive Director \nBondarev Alexandr -  executive Director\nTamarenko Andrey - specialist in web technologies\n\n ©Product Project Mordor"); // сообщение о создании директории
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CurrencyPairsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        ///  Метод закрытия приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClosing(object sender, FormClosingEventArgs e)
        {
            string mess = "";

            if (WString.RUS == true)
            {
                mess = "Вы уверены?";
            }

            if (WString.ENG == true)
            {
                mess = "Are You Sure?";
            }

            if (MessageBox.Show(mess, "", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
                Activ.Form1 = false;
            }          
            else
            {
                e.Cancel = false; // Продолжить закрытие
                this.Hide(); // Скрыть форму до закрытия
            }
        }

        private void ChangeLanguage(object sender, EventArgs e)
        {
            if (WString.RUS)
            {
                WString.RUS = false;
                WString.ENG = true; // Выбор языка английский
            }
            if (WString.ENG)
            {
                WString.ENG = false;
                WString.RUS = true; // Выбор языка русский
            }
            ChangeTextLanguage();
        } /////////////////////////// Необходимо изменить
               
    }
}
