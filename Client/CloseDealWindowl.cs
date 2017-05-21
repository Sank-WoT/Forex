namespace Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
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
    /// <summary>
    /// Класс по закрытию сделки
    /// </summary>
    public partial class CloseDealWindow : Form
    {      
        Windowd main; 
        List<Deal> BUY = new List<Deal>();
        List<Deal> SELL = new List<Deal>();
        /// <summary>
        /// Лист покупок
        /// </summary>
        List<double> BufferB = new List<double>();
        /// <summary>
        /// Лист продаж
        /// </summary>
        List<double> BufferS = new List<double>();
        List<List<double>> data = new List<List<double>>();
        /// <summary>
        /// Объект закрытия сделки
        /// </summary>
        CloseDeal deal;

        /// <summary>
        /// Инициализация компонентов
        /// </summary>
        public CloseDealWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Закрытие сделки
        /// <param name="sender">Объект события</param>
        /// <param name="e">ТСобытие</param>
        /// </summary>
        public void CloseDeal_Load(object sender, EventArgs e)
        {   
            main = this.Owner as Windowd;     
            if (main != null)
            {
                // Получение данных из родительской формы BUY (класса)
                BUY = main.BUY;
                // Получение данных из родительской формы SELL (класса)
                SELL = main.SELL;
                // Получение данных по покупкам
                BufferB = main.BufferB;
                // Получение данных по продажам
                BufferS = main.BufferS;
            }
            ListForm("  покупка", "      sell", SELL);
            ListForm("  продано", "       buy", BUY);
        }

        /// <summary>
        /// Добавление сделок в лист формы
        /// <param name="Rus">русский язык</param>
        /// <param name="Eng">английский</param>
        /// <param name="sdel">Продажа</param>
        /// </summary>
        public void ListForm(string Rus, string Eng, List<Deal> sdel)
        {
            for (int i = 0; i < SELL.Count; i++)
            {
                if (WString.Langue["RUS"] == true)
                {
                    ListD.Items.Add(Convert.ToString(sdel[i].Value()) + Rus);
                }

                if (WString.Langue["ENG"] == true)
                {
                    ListD.Items.Add(Convert.ToString(sdel[i].Value()) + Eng);
                }
            }
        }

        /// <summary>
        /// Обработка листа со сделками
        /// <param name="sender">Объект события</param>
        /// <param name="e">ТСобытие</param>
        /// </summary>
        private void ListDeal(object sender, EventArgs e)
        {
            try
            {
                string chislo = ListD.Items[ListD.SelectedIndex].ToString();
                // Создать объект закрытия сделки
                deal = new CloseDeal(chislo, BufferB, BufferS);

                // отобразить выручку
                label1.Text = deal.getProfit().ToString();
            }

            catch
            {
                if(true == WString.Langue["ENG"])
                {
                    MessageBox.Show("select a transaction");
                }
                if (true == WString.Langue["RUS"])
                {
                    MessageBox.Show("Выберите сделку");
                }
            }
        }


        /// <summary>
        /// Выбор и закрытие ордера
        /// <param name="sender">Объект события</param>
        /// <param name="e">ТСобытие</param>
        /// </summary>
        private void CloseOrder_Click(object sender, EventArgs e)
        {
            try
            {
                List<double> iterData = new List<double>();
                if (WString.Langue["RUS"] == true)
                {
                    MessageBox.Show("Орден закрыт" + ListD.Items[ListD.SelectedIndex].ToString());
                }
                if (WString.Langue["ENG"] == true)
                {
                    MessageBox.Show("The order is closed" + ListD.Items[ListD.SelectedIndex].ToString());
                }
                string Value = ListD.Items[ListD.SelectedIndex].ToString();
                int dTime = Convert.ToInt32((DateTime.Now - new System.DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds - 5);
                DateTime Date = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(dTime);

                if (Value.Contains("  покупка") == true || Value.Contains("       buy") == true)
                {
                    Close( Value, Date, iterData, dTime);
                }

                if (Value.Contains("  продано") == true || Value.Contains("      sell") == true)
                {

                    Close( Value, Date, iterData, dTime);
                }

                if (main.BUY.Count >= ListD.SelectedIndex + 1)
                {
                    // Удаление данных Покупки из списка  по уже закрытой сделке 
                    main.BUY.RemoveAt(ListD.SelectedIndex);
                }
                else
                {
                    // Удаление данных Покупки из списка  по уже закрытой сделке 
                    main.SELL.RemoveAt(ListD.SelectedIndex - main.BUY.Count);
                }
                // Удалить выбранную запись  // осталось додумать  как отослать все это для отчета
                ListD.Items.RemoveAt(ListD.SelectedIndex);
            }
            catch
            {
                if (WString.Langue["RUS"] == true)
                {
                    MessageBox.Show("Не выбран ордер для закрытия");
                }
                if (WString.Langue["ENG"] == true)
                {
                  MessageBox.Show("Order");
                }

            }
        }

        /// <summary>
        /// Выбор и закрытие ордера
        /// <param name="profit">Прибыль</param>
        /// <param name="Value">Значение + операция над ней</param>
        /// <param name="Date">Время сделки</param>
        /// <param name="iterData">Контейнер</param>
        /// <param name="dTime">Время в формате UnixTime</param>
        /// </summary>
        public void Close(string Value, DateTime Date, List<double> iterData, int dTime)
        {
            if (WString.Langue["ENG"] == true)
            {
                MessageBox.Show("The order is closed   = " + BufferB[BufferB.Count - 1]);
            }
            if (WString.Langue["RUS"] == true)
            {
                MessageBox.Show("Орден закрыт   = " + BufferB[BufferB.Count - 1]);
            }
            
            if (WString.Langue["ENG"] == true)
            {
                // Сообщение о совершенной сделке  
                MessageBox.Show("Profit = " + deal.getProfit() + " Time order " + Date);
            }
            if (WString.Langue["RUS"] == true)
            {
                // Сообщение о совершенной сделке 
                MessageBox.Show("Прибыль = " + deal.getProfit() + " Время ордера " + Date);
            }

            iterData.Add(Convert.ToDouble(Value.Remove(Value.Length - 9, 9)));
            iterData.Add(BufferB[BufferB.Count - 1]);
            iterData.Add(deal.getProfit());
            iterData.Add(dTime);
            iterData.Add(0.0);
            // хранит в  себе цену покупку цену закрытия сделки, прибыль и время закрытия
            ReportTransit.data.Add(iterData); 
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
