namespace Client
{
    using System;
    using System.Drawing;
    using System.IO; // для класса 
    using System.Text.RegularExpressions;
    using System.Windows.Forms;


    /// <summary>
    /// Начальное окно
    /// </summary>
    public partial class Form1 : Form
    {
        #region Переменные закрытия окон
        public static bool HelpClosing = true; // Переменная отвечающая за закрытое окной HelpClosing
        public static bool SWindowClosing = true; // Переменная отвечающая за закрытое окной SWindowClosing
        public static bool WindowClosing = true; // Переменная отвечающая за закрытое окной WindowClosing
        public static bool SChartClosing = true; // Переменная отвечающая за закрытое окной SChartClosing
        #endregion
        
        /// <summary>
        /// Функция стартового состояния окна
        /// </summary>
        public Form1()
        {
            Methods Time = new Methods();
            
            switch (Time.TradeStop(DateTime.Now))
            {
                case "Sat": MessageBox.Show("Forex day off"); break;
                case "Sun": MessageBox.Show("Forex day off"); break;
            }
            this.InitializeComponent();   
            string pathDirectory = Application.StartupPath; // Путь к директории

            #region Проверка существования директрории по pathDirectory
            //// Проверка  на существование директории
            if (!Directory.Exists(pathDirectory)) 
            {
                Directory.CreateDirectory(pathDirectory); // создание директории 
                MessageBox.Show("Директория создана путь : " + pathDirectory); // сообщение о создании директории
            }

            string pathFile = pathDirectory + "\\" + "eurusd" + ".txt"; // Путь к файлу c котировками eurusd
            #endregion 

            #region Проверка существования файла по pathDirectory
            //// Проверка на существование файла
            if (!File.Exists(pathFile)) 
            {
                MessageBox.Show("Вас приветствует программа Project Mordor, спасибо за то что вы с нами, желаем успешных торгов и хорошей прибыли"); // сообщение о создании файла
                FileInfo writel = new FileInfo(pathFile); // получаем путь 
                StreamWriter l = writel.CreateText(); // создаем текст
                l.Close(); // закрыть запись
            } // Развертывание сервера в заранее известном каталоге 
            #endregion

            this.FormClosing += new FormClosingEventHandler(OnClosing);

            int y = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height; // высота экрана
            int x = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width; // ширина экрана  

            double xS = x / 1920.0; // настройка под все  экраны
            double yS = y / 1080.0; // настройка под все  экраны
            this.Size = new Size(x, y); // задание размеров экрана

            #region Переменные командной комбинации к Меню текущей форме
            windowToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.W; // командная комбинация клавиш для откытия настроек окна
            chartToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C; // командная комбинация клавиш для откытия настроек графика
            eURUSDToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.E; // командная комбинация клавиш для откытия настроек графика
            helpToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.H; // командная комбинация клавиш для откытия помощи
            #endregion

            if (HelpClosing == true)
            {
                helpToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
        }

        /// <summary>
        /// Cоздание модального окна
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        public void EEURUSDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string readX, readY;
            int x = 0, y = 0;

            if (WindowClosing == true)
            {
                WindowClosing = false;
            string pathFile = Application.StartupPath + "\\SettingWindow.txt"; // Очень важно указать путь
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
            #endregion
            #region Присвоение прочтенного из файла к WSrting.X WSrting.Y
            x = Convert.ToInt32(readX);
            y = Convert.ToInt32(readY);
            WSrting.X = x; // Присвоение глобальной переменной для всего проекта для передачи значений между формами (размеры окна по X)
            WSrting.Y = y; // Присвоение глобальной переменной для всего проекта для передачи значений между формами (размеры окна по Y)
                #endregion
                #region  Window f1 = new Window(); Создание модального окна
               WSrting.VALUE = "eurusd";
                Windowd f1 = new Windowd();
            f1.Show(); // модольное окно 
            f1.Size = new Size(x, y); // Задаем значение размера формы Window 
            f1.Location = new Point(0, 0); // размещение окна EURUSD
            #endregion
            }
        } //// показать  график  EUR/USD
        public void EURJPYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string readX, readY;
            int X = 0, Y = 0;

            if (WindowClosing == true)
            {
                WindowClosing = false;
                string pathFile = Application.StartupPath + "\\SettingWindow.txt"; // Очень важно указать путь
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
                #endregion
                #region Присвоение прочтенного из файла к WSrting.X WSrting.Y
                X = Convert.ToInt32(readX);
                Y = Convert.ToInt32(readY);
                WSrting.X = X; // Присвоение глобальной переменной для всего проекта для передачи значений между формами (размеры окна по X)
                WSrting.Y = Y; // Присвоение глобальной переменной для всего проекта для передачи значений между формами (размеры окна по Y)
                #endregion
                WSrting.VALUE = "usdjpy";
                Windowd f2 = new Windowd();
                f2.Show(); // модольное окно 
                f2.Size = new Size(X, Y); // Задаем значение размера формы Window 
                f2.Location = new Point(0, 0); // размещение окна USDJPY
            }
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
        public void MenuForm1()
        {
            if (WSrting.RUS == true)
            {
             #region перевод на русский язык меню текущей формы
              settingsToolStripMenuItem.Text = "Стандартные настройки";
              windowToolStripMenuItem.Text = "Окно";
              chartToolStripMenuItem.Text = "График";
              оПрограммеToolStripMenuItem.Text = "О программе";
              создателиToolStripMenuItem.Text = "Создатели";
              helpToolStripMenuItem.Text = "Помощь";
              currencyPairsToolStripMenuItem.Text = "Валютные пары";
              eURUSDToolStripMenuItem.Text = "Евро/Доллар";
              eURJPYToolStripMenuItem.Text = "Доллар/Йена";
              langueToolStripMenuItem.Text = "Язык";
              eURToolStripMenuItem.Text = "Английский";
              rusToolStripMenuItem.Text = "Русский";

              #endregion
            }

            if (WSrting.ENG == true)
            {
             #region перевод на английский язык меню текущей формы
              settingsToolStripMenuItem.Text = "Standart settings"; 
              windowToolStripMenuItem.Text = "Window";
              chartToolStripMenuItem.Text = "Chart"; 
              оПрограммеToolStripMenuItem.Text = "About program";
              создателиToolStripMenuItem.Text = "Creators";
              helpToolStripMenuItem.Text = "Help";
              currencyPairsToolStripMenuItem.Text = "Currency pairs";
              eURUSDToolStripMenuItem.Text = "EUR/USD";
              eURJPYToolStripMenuItem.Text = "USD/JPY";
              langueToolStripMenuItem.Text = "Langue";
              eURToolStripMenuItem.Text = "Eng";
              rusToolStripMenuItem.Text = "Rus";
              #endregion
            }           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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

        /// <summary>
        ///  Метод закрытия прмложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClosing(object sender, FormClosingEventArgs e)
        {
            string mess = "Do you have to go?"; // стартовое значение

            if (WSrting.RUS == true)
            {
                mess = "Вы уже уходите?";
            }

            if (WSrting.ENG == true)
            {
                mess = "Do you have to go?";
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

        private void LangueToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Обработчик выбора в меню пункта
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void EEURUSDToolStripMenuItem(object sender, EventArgs e)
        {
            WSrting.RUS = false; 
            WSrting.ENG = true; // Выбор языка английский
            MenuForm1();
        }

        /// <summary>
        /// Обработчик выбора в меню пункта
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void RusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WSrting.ENG = false;
            WSrting.RUS = true; // Выбор языка русский
            MenuForm1();
        }

       
    }
}
