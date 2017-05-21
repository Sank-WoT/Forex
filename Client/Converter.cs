using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Converter
    {
        /// <summary>
        /// Конвертация в стринг
        /// </summary>
        /// <param name="Time">Принмаерт время</param>
        /// <param name="Buy">Принимает значение покупки</param>
        /// <param name="Sell">Принимает значение продажи</param>
        /// <returns>Возвращает все данные в формате стринг</returns>
        public List<List<string>> toStringQuotes(List<int> Time, List<double> Buy, List<double> Sell)
        {
            List<List<string>> Q = new List<List<string>>();
            Q.Add(Time.Select(n => n.ToString()).ToList());
            Q.Add(Buy.Select(n => n.ToString()).ToList());
            Q.Add(Sell.Select(n => n.ToString()).ToList());
            return Q;
        }

        /// <summary>
        /// Конвертация в стринг
        /// </summary>
        /// <returns>Возвращает все данные в формате стринг</returns>
        public List<List<string>> toStringEvent(List<Event> per)
        {
            List<List<string>> Q = new List<List<string>>();
            Q.Add(per.Select(n => n.idEvent.ToString()).ToList());
            Q.Add(per.Select(n => n.idCountry.ToString()).ToList());
            Q.Add(per.Select(n =>  n.IdEvent_Group.ToString()).ToList());
            Q.Add(per.Select(n => n.Time.ToString()).ToList());
            Q.Add(per.Select(n => n.FactNumber.ToString()).ToList());
            Q.Add(per.Select(n => n.ExpectNumber.ToString()).ToList());
            Q.Add(per.Select(n => n.Pot.ToString()).ToList());
            return Q;
        }

        /// <summary>
        /// Конвертация в стринг
        /// </summary>
        /// <param name="per">Группа событий</param>
        /// <returns>Возвращает все данные в формате стринг</returns>
        public List<List<string>> toStringEventGroup(List<EventGroup> per)
        {
            List<List<string>> Q = new List<List<string>>();
            Q.Add(per.Select(n => n.idEventGroup.ToString()).ToList());
            Q.Add(per.Select(n => n.name.ToString()).ToList());
            Q.Add(per.Select(n => n.unitNumberst.ToString()).ToList());
            return Q;
        }
    }

}
