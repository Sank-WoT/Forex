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
    public partial class CloseDeal : Form
    {      
            Windowd main; 
<<<<<<< HEAD
            List<Deal> BUY = new List<Deal>();
            List<Deal> SELL = new List<Deal>();
            List<double> BufferB = new List<double>();
=======
            List<double> BUY = new List<double>();
            List<double> SELL = new List<double>();
            List<double> Buffer = new List<double>();
>>>>>>> parent of bcab9ee... Forex2.0 Переписать код под более строгую архитектуру ООП, создать БД.
            List<double> BufferS = new List<double>();
            List<List<double>> data = new List<List<double>>();

        public CloseDeal()
        {
            InitializeComponent();           
        }

<<<<<<< HEAD

        public void CloseDeal_Load(object sender, EventArgs e)
=======
            public void CloseDeal_Load(object sender, EventArgs e)
>>>>>>> parent of bcab9ee... Forex2.0 Переписать код под более строгую архитектуру ООП, создать БД.
        {   
            main = this.Owner as Windowd;     
            if (main != null)
            {
                // Получение данных из родительской формы BUY (класса)
                BUY = main.BUY;
                // Получение данных из родительской формы SELL (класса)
                SELL = main.SELL;
                BufferB = main.Buffer;
                BufferS = main.BufferS;
            }

            for (int i = 0; i < main.BUY.Count; i++)
            {
                if(WString.RUS == true)
                {
                  ListD.Items.Add(Convert.ToString(main.BUY[i]) + "  покупка");
                }
                if (WString.ENG == true)
                {
                    ListD.Items.Add(Convert.ToString(main.BUY[i]) + "       buy");
                }
            }

            for (int i = 0; i < main.SELL.Count; i++)
            {
                if(WString.RUS == true)
                {
                 ListD.Items.Add(Convert.ToString(main.SELL[i]) + "  продано");
                }
               
                if (WString.ENG == true)
                {
                  ListD.Items.Add(Convert.ToString(main.SELL[i]) + "      sell");
                }
            }
       
        }

        private void ListDeal(object sender, EventArgs e)
        {
            DealMove("  покупка", "       buy",BufferS);
            DealMove("  продано", "      sell", BufferB);
        }

        public void DealMove(string Eng, string Rus,  List<double> Buffer2)
        {
            string a = ListD.Items[ListD.SelectedIndex].ToString();
            if (a.Contains(Rus) == true || a.Contains(Eng) == true)
            {
                // разбиваем на цену и операцию
                string[] words = a.Split(new char[] { ' ' });
                // переод цены в доубле
                double priceOpen = Convert.ToDouble(words[0]);
                // вывод профит
                // При продаже
                if (a.Contains("      sell"))
                {
                    Profit.Text = (priceOpen - Buffer2[Buffer2.Count - 1]).ToString();
                }
                else
                {
                    Profit.Text = (Buffer2[Buffer2.Count - 1] - priceOpen).ToString();
                }
            }
        }

        public void closeOrder(string Value, double chislo, double profit, DateTime Date, int dTime, List<double> iterData, List<double> BufferB)
        {
           
                if (WString.ENG == true)
                {
                    MessageBox.Show("The order is closed   = " + BufferB[BufferB.Count - 1]);
                }
                if (WString.RUS == true)
                {
                    MessageBox.Show("Орден закрыт   = " + BufferB[BufferB.Count - 1]);
                }
                chislo = Convert.ToDouble(Value.Remove(Value.Length - 9, 9)); // Выбранное число при закрытии сделки
                profit = Math.Round((chislo - BufferB[BufferB.Count - 1]), 4); // Прибыль за сделку
                if (WString.ENG == true)
                {
                    MessageBox.Show("Profit = " + profit + " Time order " + Date); // Сообщение о совершенной сделке  
                }
                if (WString.RUS == true)
                {
                    MessageBox.Show("Прибыль = " + profit + " Время ордена " + Date); // Сообщение о совершенной сделке 
                }
                // добавим значение цены открытой сделки      
                iterData.Add(chislo);
                // добавим значение цены закрытой сделки  
                iterData.Add(BufferS[BufferS.Count - 1]);
                // добавим значение прибыли
                iterData.Add(profit);
                // добавим значение времени
                iterData.Add(dTime);
                // добавим значение операцию);
                // хранит в  себе цену покупку цену закрытия сделки, прибыль и время закрытия
                ReportTransit.data.Add(iterData);
            }

        private void CloseOrder_Click(object sender, EventArgs e)
        {
            string Value = ListD.Items[ListD.SelectedIndex].ToString();
            double chislo = 0, profit = 0;
            int dTime = Convert.ToInt32((DateTime.Now - new System.DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds - 5);
            DateTime Date = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(dTime);
            try
            {
            // Хранит в себе список заратых сделок
            List <double> iterData = new List <double>();
            if  (WString.RUS == true)
            {
                MessageBox.Show("Ордер закрыт" + ListD.Items[ListD.SelectedIndex].ToString());
            }
            if (WString.ENG == true)
            {
                MessageBox.Show("The order is closed" + ListD.Items[ListD.SelectedIndex].ToString());
            }
                // закрытие ордена
                if (Value.Contains("  покупка") == true || Value.Contains("       buy") == true)
                {
                    closeOrder(Value, chislo, profit, Date, dTime, iterData, BufferB);
                }

            if (Value.Contains("  продано") == true || Value.Contains("      sell"))
            {
                    closeOrder(Value, chislo, profit, Date, dTime, iterData, BufferS);
                }

            if (main.BUY.Count >= ListD.SelectedIndex +1)
            {
                main.BUY.RemoveAt(ListD.SelectedIndex); // Удаление данных Покупки из списка  по уже закрытой сделке 
            }
            else
            {
                main.SELL.RemoveAt(ListD.SelectedIndex - main.BUY.Count); // Удаление данных Покупки из списка  по уже закрытой сделке 
            }
            ListD.Items.RemoveAt(ListD.SelectedIndex); // Удалить выбранную запись  // осталось додумать  как отослать все это для отчета
            }
            catch
            {
                if(WString.RUS == true)
                {
                  MessageBox.Show("Не выбран ордер для закрытия");
                }
                if (WString.ENG == true)
                {
                  MessageBox.Show("Order");
                }

            }
        }
    }
}
