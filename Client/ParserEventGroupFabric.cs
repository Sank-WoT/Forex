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
    public class ParserEventGroupFabric : ParserFabric
    {
        private List<EventGroup> EvG;
        public override void Parse(StreamReader inputString)
        {
            CultureInfo temp_culture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            // создание объекта
            int colvo;
            string response = "";
            response = inputString.ReadToEnd();
            Regex regex = new Regex(@"([^`]+)");
            MatchCollection eventMatch = regex.Matches(response);
            // количество просмотренных записей
            colvo = 0;
            // создаем лист групп
            EvG = new List<EventGroup>();
            while(colvo < eventMatch.Count - 1)
            {
                // создаем группу событий
                EventGroup Events = new EventGroup();
                Events.idEventGroup = int.Parse(eventMatch[colvo].ToString().Trim());
                colvo++;
                // наименование группы 
                if (eventMatch[colvo].ToString().Trim() != "")
                {
                    Events.name = "'"+eventMatch[colvo].ToString().Trim()+"'";
                }
                else
                {
                    Events.name = "'no'";
                }
                // единица измерения
                colvo++;
                if (eventMatch[colvo].ToString().Trim() != "")
                {
                    Events.unitNumberst = "'"+eventMatch[colvo].ToString().Trim()+"'";
                }
                else
                {
                    Events.unitNumberst = "'no'";
                }
                colvo++;
                EvG.Add(Events);
            }
        }

        public List<EventGroup> getEventGroup()
        {
            return this.EvG;
        }
    }
}
