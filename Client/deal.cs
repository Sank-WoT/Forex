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
        ///  Constructor deal +
        /// </summary>
        /// <param name="typeDeal">Тип сделки true покупка false продажа</param>
        /// <param name="ArrayInetQuotes">Массив котировок после подключения</param>
        /// <param name="tic">текущее время от начала торгов</param>
        public Deal(bool typeDeal, List<double> ArrayInetQuotes, int tic)
        {
            _typeDeal = typeDeal;
            // Значение котировки
            try
            {
                _value = ArrayInetQuotes[tic - 1];
            }
            catch
            {
                MessageBox.Show("Ошибка массив вышел за границы. Передено значение тик 0");
            }
            // Время сделки
            _tic = tic;
            // вызвать метод уведомляющий о сделке
            Trade();
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
                player.SoundLocation = Application.StartupPath + "/Music/Test.wav"; // путь адреса музыки
                player.Play(); // Проигрывание звука
            }
            catch
            {
            }

            if (_typeDeal == false)
            {
                valueT = _value; // Запомнить значение продажи
                if (WString.Langue["RUS"] == true)
                {
                    text = "покупка";
                }
                if (WString.Langue["ENG"] == true)
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
                if (WString.Langue["RUS"] == true)
                {
                    text = "продажа";
                }
                if (WString.Langue["ENG"] == true)
                {
                    text = "sell";
                }
                else
                {
                    text = "sell";
                }
            }

            if (WString.Langue["RUS"] == true)
            {
                MessageBox.Show("Cовершена " + text + " по цене =" + valueT);
            }

            if (WString.Langue["ENG"] == true)
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

        /// <summary>
        /// Мето возвращение значения
        /// </summary>
        public double Value()
        {
            return _value;
        }
    }

}