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
    public class readFile
    {
       public int  read(string text1, List<double> massYInetA, List<double> massYInetB, List<double> Times, int colvo)
        {
            Regex regex = new Regex(@"((\d{10,20})|(\d{1,20})\.(\d{1,4}))");//регулярное выражение 
            MatchCollection m = regex.Matches(text1);
            while (colvo < m.Count - 1)
            {
                
                Times.Add(Convert.ToDouble(m[colvo].Value));//Время в UNIX
                colvo++;//порядковый номер даты в списке
              
                    massYInetB.Add(Convert.ToDouble(m[colvo].Value)); //значение  продажа
                    colvo++;//порядковый номер 
               
               
                massYInetA.Add(Convert.ToDouble(m[colvo].Value));//число покупка
                colvo++;//порядковый номер  

            }
            return colvo;
        }// функция для прочтения файла и добавление значеений в массив

    }
}
