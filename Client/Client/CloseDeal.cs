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
            List<double> Buffer = new List<double>();
            List<double> BufferS = new List<double>();
            List<List<double>> data = new List<List<double>>();

        public CloseDeal()
        {
            InitializeComponent();           
        }

            public void CloseDeal_Load(object sender, EventArgs e)
        {   
            Windowd main = this.Owner as Windowd;
            List<double> BUY = new List<double>();
            List<double> SELL = new List<double>();
           

            if (main != null)
            {
                BUY = main.BUY; // Получение данных из родительской формы BUY (класса)
                SELL = main.SELL; // Получение данных из родительской формы SELL (класса)
                Buffer = main.Buffer;
                BufferS = main.BufferS;
            }

            for (int i = 0; i < BUY.Count; i++ )
            {
               ListD.Items.Add(Convert.ToString(BUY[i]) + "  покупка" );
            }

            for (int i = 0; i < SELL.Count; i++)
            {
               ListD.Items.Add(Convert.ToString(SELL[i]) + "  продано");
            }
       
        }

        private void ListDeal(object sender, EventArgs e)
        { 
        }

        private void CloseOrder_Click(object sender, EventArgs e)
        {
            List <double> iterData = new List <double>();
            MessageBox.Show("Closed the order  " + ListD.Items[ListD.SelectedIndex].ToString()); 
            string Value = ListD.Items[ListD.SelectedIndex].ToString();
            double chislo, profit;
            int dTime = Convert.ToInt32((DateTime.Now - new System.DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds - 5);
            DateTime Date = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(dTime); 

            if (Value.Contains("  покупка") == true)
            {               
                MessageBox.Show("Закрыта сделка по цене  = " + BufferS[BufferS.Count - 1]);
                chislo = Convert.ToDouble(Value.Remove(Value.Length - 9, 9)); // Выбранное число при закрытии сделки
                profit = Math.Round((BufferS[BufferS.Count - 1] - chislo), 4); // Прибыль за сделку
                MessageBox.Show("Profit = " + profit + " Время сделки " + Date); // Сообщение о совершенной сделке  
                iterData.Add(chislo);
                iterData.Add(BufferS[BufferS.Count - 1]);
                iterData.Add(profit);
                iterData.Add(dTime);
                iterData.Add(0.0);
                ReportTransit.data.Add(iterData); // хранит в  себе цену покупку цену закрытия сделки, прибыль и время закрытия
            }

            if (Value.Contains("  продано") == true)
            {
                MessageBox.Show("Закрыта сделка по цене  = " + Buffer[BufferS.Count - 1]);
                chislo = Convert.ToDouble(Value.Remove(Value.Length - 9, 9)); // Выбранное число при закрытии сделки
                profit = Math.Round((chislo - Buffer[BufferS.Count - 1]), 4); // Прибыль за сделку
                MessageBox.Show("Profit = " + profit + " Время сделки " + Date); // Сообщение о совершенной сделке    
                iterData.Add(Buffer[BufferS.Count - 1]);
                iterData.Add(chislo);
                iterData.Add(profit);
                iterData.Add(dTime);
                iterData.Add(1.0);
                ReportTransit.data.Add(iterData); // хранит в  себе цену покупку цену закрытия сделки, прибыль и время закрытия
            }
            ListD.Items.RemoveAt(ListD.SelectedIndex); // Удалить выбранную запись  // осталось додумать  как отослать все это для отчета        
        }
    }
}
