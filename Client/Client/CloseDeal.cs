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
            List<Deal> BUY = new List<Deal>();
            List<Deal> SELL = new List<Deal>();
            List<double> Buffer = new List<double>();
            List<double> BufferS = new List<double>();
            List<List<double>> data = new List<List<double>>();

        public CloseDeal()
        {
            InitializeComponent();
        }

     /*   public Add()
        {
            for (int i = 0; i < main.BUY.Count; i++)
            {
                if (WString.RUS == true)
                {
                    ListD.Items.Add(Convert.ToString(main.BUY[i].Value()) + "  покупка");
                }
                if (WString.ENG == true)
                {
                    ListD.Items.Add(Convert.ToString(main.BUY[i].Value()) + "       buy");
                }
            }
        }
        */

        public void CloseDeal_Load(object sender, EventArgs e)
        {   
            main = this.Owner as Windowd;     
            if (main != null)
            {
                BUY = main.BUY; // Получение данных из родительской формы BUY (класса)
                SELL = main.SELL; // Получение данных из родительской формы SELL (класса)
                Buffer = main.Buffer;
                BufferS = main.BufferS;
            }

            for (int i = 0; i < BUY.Count; i++)
            {
                if(WString.RUS == true)
                {
                  ListD.Items.Add(Convert.ToString(BUY[i].Value()) + "  покупка");
                }
                if (WString.ENG == true)
                {
                    ListD.Items.Add(Convert.ToString(BUY[i].Value()) + "       buy");
                }
            }

            for (int i = 0; i < SELL.Count; i++)
            {
                if(WString.RUS == true)
                {
                 ListD.Items.Add(Convert.ToString(SELL[i].Value()) + "  продано");
                }
               
                if (WString.ENG == true)
                {
                  ListD.Items.Add(Convert.ToString(SELL[i].Value()) + "      sell");
                }
            }
       
        }

        private void ListDeal(object sender, EventArgs e)
        { 
        }

        private void CloseOrder_Click(object sender, EventArgs e)
        {
            try
            {
            List <double> iterData = new List <double>();
            if  (WString.RUS == true)
            {
                MessageBox.Show("Орден закрыт" + ListD.Items[ListD.SelectedIndex].ToString());
            }
            if (WString.ENG == true)
            {
                MessageBox.Show("The order is closed" + ListD.Items[ListD.SelectedIndex].ToString());
            }
            string Value = ListD.Items[ListD.SelectedIndex].ToString();
            double chislo, profit;
            int dTime = Convert.ToInt32((DateTime.Now - new System.DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds - 5);
            DateTime Date = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(dTime); 

            if (Value.Contains("  покупка") == true || Value.Contains("       buy") == true)
            {
                if (WString.ENG == true)
                {
                 MessageBox.Show("The order is closed   = " + Buffer[Buffer.Count - 1]);
                }
                if (WString.RUS == true)
                {
                    MessageBox.Show("Орден закрыт   = " + Buffer[Buffer.Count - 1]);
                }
                chislo = Convert.ToDouble(Value.Remove(Value.Length - 9, 9)); // Выбранное число при закрытии сделки
                Console.WriteLine(Value.Remove(Value.Length - 9, 8) + "   Проверка");
                profit = Math.Round((chislo - Buffer[Buffer.Count - 1]), 4); // Прибыль за сделку
                if (WString.ENG == true)
                {
                   MessageBox.Show("Profit = " + profit + " Time order " + Date); // Сообщение о совершенной сделке  
                }
                if (WString.RUS == true)
                {
                    MessageBox.Show("Прибыль = " + profit + " Время ордена " + Date); // Сообщение о совершенной сделке 
                }                
                iterData.Add(chislo);
                iterData.Add(BufferS[BufferS.Count - 1]);
                iterData.Add(profit);
                iterData.Add(dTime);
                iterData.Add(0.0);
                ReportTransit.data.Add(iterData); // хранит в  себе цену покупку цену закрытия сделки, прибыль и время закрытия
            }

            if (Value.Contains("  продано") == true || Value.Contains("      sell"))
            {
                if (WString.ENG == true)
                {
                    MessageBox.Show("The order is closed   = " + BufferS[BufferS.Count - 1]);
                }
                if (WString.RUS == true)
                {
                    MessageBox.Show("Орден закрыт   = " + BufferS[BufferS.Count - 1]);
                }
                chislo = Convert.ToDouble(Value.Remove(Value.Length - 9, 9)); // Выбранное число при закрытии сделки
                Console.WriteLine(chislo + " Продажа");
                profit = Math.Round((chislo - BufferS[BufferS.Count - 1]), 4); // Прибыль за сделку
                if(WString.ENG == true)
                {
                    MessageBox.Show("Profit = " + profit + " Time order " + Date); // Сообщение о совершенной сделке  
                }
                if (WString.RUS == true)
                {
                    MessageBox.Show("Орден закрыт   = " + BufferS[BufferS.Count - 1]);
                }
                iterData.Add(chislo);
                iterData.Add(Buffer[BufferS.Count - 1]);
                iterData.Add(profit);
                iterData.Add(dTime);
                iterData.Add(1.0);
                ReportTransit.data.Add(iterData); // хранит в  себе цену покупку цену закрытия сделки, прибыль и время закрытия
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
                  MessageBox.Show("Не выбран орден для закрытия");
                }
                if (WString.ENG == true)
                {
                  MessageBox.Show("Order");
                }

            }
        }
    }
}
