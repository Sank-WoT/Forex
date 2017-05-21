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
    using System.Configuration;
    using Gettext;

    /// <summary>
    /// Стартовое окно
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Хранит все группу событий
        /// </summary>
        List<EventGroup> Groups;
        /// <summary>
        /// Хранит все  события
        /// </summary>
        List<Event> Events;
        List<Event> FutureEvent;
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
        Task tConnect;
        List<Task> tTask = new List<Task>();
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
            Gettext.LanguageCode = "ru";
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
                case "Sat": MessageBox.Show(Gettext._("Forex day off")); break;
                case "Sun": MessageBox.Show(Gettext._("Forex day off")); break;
            }

            this.InitializeComponent();
            // размеры контейнера
            startContainer.Size = new Size(x, y - WSettings.Size.Height);
            startContainer.Location = new Point(0, 0);
            // проверка существования директории
            DirectoryWork.Set(pathDirectory); 
            // проверка существования файла
            FileInspection.Set(pathFile); 
            // настройка под все  экраны
            xS = x / 1920.0; 
            // настройка под все  экраны
            yS = y / 1080.0;  
            // задание размеров экрана
            this.Size = new Size(x, y);
           
            #region Переменные командной комбинации к Меню текущей форме
            // командная комбинация клавиш для откытия настроек окна
            windowToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.W;
            // командная комбинация клавиш для откытия настроек графика
            chartToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C; 
            // командная комбинация клавиш для откытия графика USDEUR
            eURUSDToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.U; 
            // командная комбинация клавиш для откытия графика EURYPJ
            USDJPYToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Y; 
            #endregion

            // Грузящийся курсор
            Cursor.Current = Cursors.WaitCursor;
            tConnect.Wait();
            // Возвращение к нормальному состоянию
            Cursor.Current = Cursors.Default;
             
            // передача строки подключения
             Bd BasaDan = new Bd("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename='" + Application.StartupPath + "\\Forex.mdf'; Integrated Security = True; Connect Timeout = 30");
            // проверка интернет соединения 
            if ( true == InetConnect.Inet)
            {
             
                tTask = LoadBdQuote(BasaDan);
                List<int> timeL = new List<int>();
                List<double> sellL = new List<double>();
                List<double> buyL = new List<double>();
                // получение событий парралельно
                ParserEventFabric Parse = new ParserEventFabric();
                BasaDan.LoadInBdEvent("https://myfirstphpapp-skro.rhcloud.com/get_Event.php", BasaDan.SelectLastIdEvent(), Parse);

                ParserEventGroupFabric Parse1 = new ParserEventGroupFabric();
                BasaDan.LoadInBdEventGroup("https://myfirstphpapp-skro.rhcloud.com/get_EventGroup.php", BasaDan.SelectLastIdEventGroup(), Parse1);

                BasaDan.Select("eurusd", ref timeL, ref sellL, ref buyL);
            }
            // выбор событий из БД по id
            //Events = BasaDan.SelectEvent(5000);
            // выбор событий по Id_Group
            // Events = BasaDan.SelectSameEvent(20);
            // выбор грядущих событий
            // выбор из группы события по id
            // Groups = BasaDan.SelectEventGroup(500);
            // выбор группы по name
            // Groups = BasaDan.SelectEventGroup("'Выступление представителя ФРС США Джеффри Лэкера'");
            int  NowTime = Convert.ToInt32((DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds - 15) - 3600 * 1;
            Console.WriteLine(NowTime);
            FutureEvent = BasaDan.SelectEventTime(1495209600);
        }

        /// <summary>
        /// загрузка данных
        /// </summary>
        /// <param name="BasaDan"></param>
        /// <returns></returns>
        public List<Task> LoadBdQuote(Bd BasaDan)
        {
            // загрузка данных eurusd
            Task tEurusd = BasaDan.LoadDataQuote("eurusd");
            // загрузка данных usdjpy
            Task tUsdjpy = BasaDan.LoadDataQuote("usdjpy");
            // ассинхронная загрузка
            List<Task> tQuotes = new List<Task>();
            tQuotes.Add(tEurusd);
            tQuotes.Add(tUsdjpy);
            return tQuotes;
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
            // создание окна c проверкой на загрузку данных
            createWindow(tTask[0], WindowClosingUSDJPY, "eurusd");
           
        }

        /// <summary>
        /// Cоздание  окна
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        public void USDJPYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // создание окна c проверкой на загрузку данных
            createWindow(tTask[1], WindowClosingUSDJPY, "usdjpy");
        }

        /// <summary>
        /// Метод для создания окна котировок
        /// </summary>
        /// <param name="tConnect">Таск подключения</param>
        /// <param name="tLoadquotes">Таск погрузки данных</param>
        /// <param name="closeWindow"></param>
        ///<param name="value">наименование котировки</param>
        ///<param name="TQoute">таски</param>
        public bool createWindow(Task TQoute, bool closeWindow, string value)
        {
            int x = 0, y = 0;
            if (closeWindow == true)
            {
                // чтение из файла и присвоение размеров X,Y
                windowQuotes windowQuote = new windowQuotes();
                windowQuote.get(ref x, ref y, ref closeWindow);
                // Присвоение глобальной переменной для всего проекта для передачи значений между формами (размеры окна по X)
                WString.X = x;
                // Присвоение глобальной переменной для всего проекта для передачи значений между формами (размеры окна по Y)
                WString.Y = y;
                progressBar g = new progressBar();
                g.Show();
                Tasks(tConnect, TQoute, g);
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
        public bool Tasks(Task tConnect, Task tLoadquotes, progressBar g)
        {
            // ожидание
            Cursor.Current = Cursors.WaitCursor;
            // ожидать коннект
            tConnect.Wait();
            // ожидать загрузку 
            g.set(50);
            tLoadquotes.Wait();
            g.set(100);
            g.Close();
            return true;
        }

        /// <summary>
        ///  метод для отображения прогресс бара
        /// </summary>
        private void requestBd(progressBar progress, int procentComplete)
        {
            // процент выполнения
            progress.set(procentComplete);
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
                EditInterface("ru");
                #endregion
            }

            if (true == WString.Langue["ENG"])
            {
                #region перевод на английский язык меню текущей формы
                EditInterface("en");
                #endregion
            }
        }

        // Создание изменение интерфейса
        public void EditInterface(string lang)
        {
            Gettext.LanguageCode = lang;
            settingsToolStripMenuItem.Text = Gettext._("Settings");
            windowToolStripMenuItem.Text = Gettext._("Window");
            chartToolStripMenuItem.Text = Gettext._("Chart");
            AboutToolStripMenuItem.Text = Gettext._("About");
            создателиToolStripMenuItem.Text = Gettext._("Autors");
            helpToolStripMenuItem1.Text = Gettext._("Help");
            currencyPairsToolStripMenuItem.Text = Gettext._("Currency pairs");
            eURUSDToolStripMenuItem.Text = Gettext._("EUR/USD");
            USDJPYToolStripMenuItem.Text = Gettext._("USD/JPY");
            langToolStripMenuItem.Text = Gettext._("Russian");
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
            // сообщение о создателях
            MessageBox.Show(Gettext._("Creators \n\nSerobabov Alexandr - executive Director \nBondarev Alexandr -  executive Director\nTamarenko Andrey - specialist in web technologies\n\n ©Product Project Mordor")); 
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
                quotesList.Items.Add(windows[0].poslchisloBuy); 
        }
        /// <summary>
        /// Первая валюта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radImageItem1_Click(object sender, EventArgs e)
        {
            createWindow(tTask[0], WindowClosingUSDJPY, "eurusd");
        }
        /// <summary>
        /// Вторая валюта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radImageItem2_Click(object sender, EventArgs e)
        {
            createWindow(tTask[1], WindowClosingUSDJPY, "usdjpy");
        }

        private void startContainer_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radCarousel1_SelectedItemChanged(object sender, EventArgs e)
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

            mess = Gettext._("Are You Sure?");

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
                EditInterface("en");
            }

            // Выбор языка русский
            if (WString.Langue["ENG"])
            {
                WString.Langue["ENG"] = false;
                WString.Langue["RUS"] = true;
                EditInterface("ru");
            }
            ChangeTextLanguage();
        }
        // Событие запуска помощи
        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Help.ShowHelp(this, "Help.chm");
        }
    }
}
