using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Client
{
    public class ParserEventFabric : ParserFabric
        /// <summary>
    {
        /// Лист Событий
        /// </summary>
        private  List<Event> Ev;
        /// <summary>
        /// Метод парсинга данных
        /// </summary>
        /// <param name="inputString">Строка с данными</param>
        public override void Parse(StreamReader inputString)
        {
            CultureInfo temp_culture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Ev = new List<Event>();
            int colvo;
            string response = "";
            response = inputString.ReadToEnd();
            Regex regex = new Regex(@"([^&|^;]+)");
            MatchCollection stringEvent = regex.Matches(response);
            colvo = 0;
            while (colvo < stringEvent.Count - 1)
            {
                Event A = new Event();
                //порядковый номер 
                A.Time = int.Parse(stringEvent[colvo].ToString().Trim());
                //Время 
                colvo++;
                if (stringEvent[colvo].ToString().Trim() != "")
                {
                    A.FactNumber = double.Parse(stringEvent[colvo].ToString().Trim());
                }
                else
                {
                    A.FactNumber = 0;
                }
                colvo++;
                if (stringEvent[colvo].ToString().Trim() != "")
                {
                    A.ExpectNumber = double.Parse(stringEvent[colvo].ToString().Trim());
                }
                else
                {
                    A.ExpectNumber = 0;
                }
                colvo++;
                if (stringEvent[colvo].ToString().Trim() != "")
                {
                    A.Pot = "'"+stringEvent[colvo].ToString().Trim()+"'";
                }
                else
                {
                    A.Pot = "'no'";
                }
                colvo++;
                if (stringEvent[colvo].ToString().Trim() != "")
                {
                    A.IdEvent_Group = int.Parse(stringEvent[colvo].ToString().Trim());
                }
                colvo++;
                if (stringEvent[colvo].ToString().Trim() != "")
                {
                    A.idEvent = int.Parse(stringEvent[colvo].ToString().Trim());
                }
                colvo++;
                // айди страны
                if (stringEvent[colvo].ToString().Trim() != "")
                {
                    A.idCountry = int.Parse(stringEvent[colvo].ToString().Trim());
                }
                colvo++;
                Ev.Add(A);
            }
        }


        public List<Event> getEvent()
        {
            return this.Ev;
        }
    }
}