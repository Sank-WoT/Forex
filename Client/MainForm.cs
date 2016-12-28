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
    /// Стартовое окно
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Реализация объекта работы с файлами
        /// </summary>
        WorkFile workFile = new WorkFile();
        /// <summary>
        /// высота экрана 
        /// </summary>
        double xS;
        /// <summary>
        /// ширина экрана 
        /// </summary>
        double yS;
        /// <summary>
        /// Объект синх 
        /// </summary>
        private object sync = new object();
        private bool internetActionFinished = false;
        /// <summary>
        /// Таски для работы с валютами 
        /// </summary>
        Task tConnect, tEurusd, tUsdjpy;
        #region Переменные закрытия окон
        /// <summary>
        /// поле отвечающая за закрытое окной Help
        /// </summary>
        public static bool HelpClosing = true;
        /// <summary>
        /// поле отвечающая за закрытое окной SWindow
        /// </summary>
        public static bool SWindowClosing = true;
        /// <summary>
        /// поле отвечающая за закрытое окной Window
        /// </summary>
        public static bool WindowClosingEURUSD = true;
        /// <summary>
        /// поле отвечающая за закрытое окной USDJPY
        /// </summary>
        public static bool WindowClosingUSDJPY = true;
        /// <summary>
        /// поле отвечающая за закрытое окной SChar 
        /// </summary>
        public static bool SChartClosing = true;
        public List<string> valueP = new List<string>();
        /// <summary>
        /// реализация окна валюты
        /// </summary>
        public List<Windowd> windows = new List<Windowd>();
        #endregion

        /// <summary>
        /// Конструктор стартового состояния окна
        /// </summary>
        /// <param name="x">размер по оси x</param>
        /// <param name="y">размер по оси y</param>

        public MainForm(int x, int y)
        {
            string pathDirectory = Application.StartupPath; // Путь к директории
            string pathFile = pathDirectory + "\\" + "eurusd" + ".txt"; // Путь к файлу c котировками eurusd

            Methods Time = new Methods();
             // проверка интернет соединения ассинхронно
            tConnect = Task.Run(() =>
            {
                Internet inCon = new Internet();
                InetConnect.Inet = inCon.TryCon("eurusd", sync, internetActionFinished);
            });

            switch (Time.TradeStop(DateTime.Now))
            {
                case "Sat": MessageBox.Show("Forex day off"); break;
                case "Sun": MessageBox.Show("Forex day off"); break;
            }

            this.InitializeComponent();
            // размеры контейнера
            startContainer.Size = new Size(x, y - WSettings.Size.Height);
            startContainer.Location = new Point(0, 0);
           
            DirectoryWork.Set(pathDirectory); // проверка существования директории

            FileInspection.Set(pathFile); // проверка существования файла

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

            // Грузящийся курсор
            Cursor.Current = Cursors.WaitCursor;
            tConnect.Wait();
            // Возвращение к нормальному состоянию
            Cursor.Current = Cursors.Default;
            // загрузка данных 2х потоков с данными №1
            Bd BasaDan = new Bd("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename='" + Application.StartupPath + "\\Forex.mdf'; Integrated Security = True; Connect Timeout = 30");
            tEurusd = BasaDan.LoadData("eurusd");
            tUsdjpy = BasaDan.LoadData("usdjpy");
        }

        /// <summary>
        /// Конструктор стартового состояния окна
        /// </summary>
        /// <param name="pathFile">Путь в файл</param>
        public void writeFile(string pathFile)
        {
            string text = "EURUSD   " + 1366 + "      " + 757;
            // проверка и создания файла
            workFile.CreateFile(pathFile, text);
        }

        /// <summary>
        /// Cоздание  окна
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        public void EURUSDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // создание окна
            createWindow(tEurusd, WindowClosingUSDJPY, "eurusd");
        }

        /// <summary>
        /// Cоздание  окна
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        public void USDJPYToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            // создание окна
            createWindow(tUsdjpy, WindowClosingUSDJPY, "usdjpy");
        }

        /// <summary>
        /// Метод для создания окна котировок
        /// </summary>
        /// <param name="tConnect">Таск подключения</param>
        /// <param name="tLoadquotes">Таск погрузки данных</param>
        /// <param name="closeWindow"></param>
        ///<param name="value">наименование котировки</param>
        public bool createWindow(Task TQoute, bool closeWindow, string value)
        {
            int x = 0, y = 0;
            Tasks(tConnect, TQoute);
            if (closeWindow == true)
            {
                // чтение из файла и присвоение размеров X,Y
                windowQuotes windowQuote = new windowQuotes();
                windowQuote.get(ref x, ref y, ref closeWindow);
                // Присвоение глобальной переменной для всего проекта для передачи значений между формами (размеры окна по X)
                WString.X = x;
                // Присвоение глобальной переменной для всего проекта для передачи значений между формами (размеры окна по Y)
                WString.Y = y;
                Windowd Quote = new Windowd(value);
                WindowSizeLocation(Quote, WString.X, WString.Y);
                return Quote == null ? false : true;
            }
            return false;
        }

        /// <summary>
        /// Ожидание загрузки данных
        /// </summary>
        /// <param name="tConnect">Таск подключения</param>
        /// <param name="tLoadquotes">Таск погрузки данных</param>
        public bool Tasks(Task tConnect, Task tLoadquotes)
        {
            // ожидание
            Cursor.Current = Cursors.WaitCursor;
            // ожидать коннект
            tConnect.Wait();
            // ожидать загрузку
            tLoadquotes.Wait();
            return true;
        }

        /// <summary>
        /// Метод для установки окну параметра MdiParent, скрытия контейнера, показ окна, и сохранение в хранилище окон
        /// </summary>
        /// <param name="quotesWindow">окно котировок</param>
        /// <param name="sizeX">размеры по X</param>
        /// <param name="sizeY">размеры по Y</param>
        public void WindowSizeLocation(Windowd quotesWindow, int sizeX, int sizeY)
        {
            // startContainer.Panel2.Add(EURUSD);
            // Set the Parent Form of the Child window.
            quotesWindow.MdiParent = this;
            // скрыть контейнер
            startContainer.Visible = false;
            quotesWindow.Show();
            // Задаем значение размера формы Window 
            quotesWindow.Size = new Size(sizeX, sizeY);
            // координаты размещение окна EURUSD
            quotesWindow.Location = new Point(0, 0);
            // добаляем окно в хранилище
            windows.Add(quotesWindow);
        }

        /// <summary>
        /// Создание окна SWindow
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        public void WindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
             if (SWindowClosing == true)
             {
                    SWindow Sw = new SWindow();
                    SWindowClosing = false;
                    Sw.Show();
             }     
        }


        /// <summary>
        /// Создание окна HelpClosing
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        public void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (HelpClosing == true)
            {
                Help f4 = new Help();
                HelpClosing = false;
                f4.Show();
            }
        }

        /// <summary>
        /// Метод смены языка
        /// </summary>
        public void ChangeTextLanguage()
        {
            if (true == WString.Langue["RUS"])
            {
                #region перевод на русский язык меню текущей формы
                EditInterface("Стандартные настройки", "Окно", "График", "О программе", "Создатели", "Справка", "Валютные пары", "Евро/Доллар", "Доллар/Йена", "English", "Выберите валютную пару для начала торгов");
                #endregion
            }

            if (true == WString.Langue["ENG"])
            {
                #region перевод на английский язык меню текущей формы
                EditInterface("Settings", "Window", "Chart", "About", "Autors", "Help", "Currency pairs", "EUR / USD", "USD/JPY", "Русский", "Select a currency pair to start trading");
                #endregion
            }
        }

        // Создание изменение интерфейса
        public void EditInterface(string settingsToolStripMenu, string windowToolStripMenu, string chartToolStripMenu, string AboutToolStripMenu, string ToolStripMenu, string helpToolStripMenu, string currencyPairsToolStripMenu, string eURUSDToolStripMenu, string USDJPYToolStripMenu, string langToolStripMenu, string labelSelect)
        {
            settingsToolStripMenuItem.Text = settingsToolStripMenu;
            windowToolStripMenuItem.Text = windowToolStripMenu;
            chartToolStripMenuItem.Text = chartToolStripMenu;
            AboutToolStripMenuItem.Text = AboutToolStripMenu;
            создателиToolStripMenuItem.Text = ToolStripMenu;
            helpToolStripMenuItem.Text = helpToolStripMenu;
            currencyPairsToolStripMenuItem.Text = currencyPairsToolStripMenu;
            eURUSDToolStripMenuItem.Text = eURUSDToolStripMenu;
            USDJPYToolStripMenuItem.Text = USDJPYToolStripMenu;
            langToolStripMenuItem.Text = langToolStripMenu;
        } 

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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
                quotesList.Items.Add(windows[0].poslchisloBuy); 
        }

        private void radImageItem1_Click(object sender, EventArgs e)
        {
            createWindow(tEurusd, WindowClosingUSDJPY, "eurusd");
        }
        private void radImageIteь2_Click(object sender, EventArgs e)
        {
            createWindow(tUsdjpy, WindowClosingUSDJPY, "usdjpy");
        }

        /// <summary>
        ///  Метод закрытия приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClosing(object sender, FormClosingEventArgs e)
        {
            string mess = "";

            if (WString.Langue["RUS"] == true)
            {
                mess = "Вы уверены?";
            }

            if (WString.Langue["ENG"] == true)
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
                // Продолжить закрытие
                e.Cancel = false;
                // Скрыть форму до закрытия
                this.Hide();
            }
        }

        private void ChangeLanguage(object sender, EventArgs e)
        {
            // Выбор языка английский
            if (WString.Langue["RUS"])
            {
                WString.Langue["RUS"] = false;
                WString.Langue["ENG"] = true;
            }

            // Выбор языка русский
            if (WString.Langue["ENG"])
            {
                WString.Langue["ENG"] = false;
                WString.Langue["RUS"] = true;
            }
            ChangeTextLanguage();
        }
               
    }
}
