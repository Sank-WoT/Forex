using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO; //для класса 
using System.Text.RegularExpressions;
using System.Net;
using System.Globalization;
using System.Threading;
using System.Net.Sockets;
using EnumDialogResult = System.Windows.Forms.DialogResult;
using System.Media;
namespace Client
{
    /// <summary>
    /// Class save deal
    /// </summary>
    public class Deal
    {
        private bool _typeDeal;
        private double _value;
        private int _tic;
        /// <summary>
        ///  Constructor deal
        /// </summary>
        /// <param name="value">Значчение сделки</param>   
        //// Отображение галочек  в меню
        public Deal(bool buy, List<double> bufferS, int tic)
        {
            _typeDeal = buy;
            _value = bufferS[tic - 1];
            _tic = tic;
            Trade();
        }

        public double profit(double present_value)
        {
            double prof;
            if(_typeDeal == true)
            {

            }
            prof = present_value - _value;
            return prof;
        }

        /// <summary>
        /// Метод запоминание совершенных покупок и продаж
        /// </summary>
        /// <param name="buy"></param>
        /// <param name="bufferS">Массив покупки</param>
        /// <param name="tic">текущее время отначала торгов</param>
        public void Trade()
        {
            double valueT;
            string text;
            SoundPlayer player = new SoundPlayer();
            try
            {
                player.SoundLocation = Application.StartupPath + "/Music/test.wav"; // путь адреса музыки
                player.Play(); // Проигрывание звука
            }
            catch
            {
            }

            if (_typeDeal == false)
            {
                valueT = _value; // Запомнить значение продажи
                if (WString.RUS == true)
                {
                    text = "покупка";
                }
                if (WString.ENG == true)
                {
                    text = "buy";
                }
                else
                {
                    text = "buy";
                }
            }
            else
            {
                valueT = _value; // Запомнить значение продажи
                if (WString.RUS == true)
                {
                    text = "продажа";
                }
                if (WString.ENG == true)
                {
                    text = "sell";
                }
                else
                {
                    text = "sell";
                }
            }

            if (WString.RUS == true)
            {
                MessageBox.Show("Cовершена " + text + " по цене =" + valueT);
            }

            if (WString.ENG == true)
            {
                if (text == "продажа" || text == "sell")
                {
                    MessageBox.Show("committed " + "selling" + " for the price =" + valueT);
                }
                if (text == "покупка" || text == "buy")
                {
                    MessageBox.Show("done " + "buing" + " for the price =" + valueT);
                }
            }
        }

        public double Value()
        {
            return _value;
        }
    }

}