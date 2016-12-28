using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    /// <summary>
    /// Класс расширяюший базовые возможности чекбокс
    /// </summary>
    class ExtendCheckbox : System.Windows.Forms.CheckBox
    {
        /// <summary>
        /// Наименование на русском
        /// </summary>
        public string rusLan;
        /// <summary>
        /// Наименование на английском
        /// </summary>
        public string engLan;
        public void Translate(string name1)
        {
            switch (name1)
            {
                case "rus": this.Text = rusLan; break;
                case "eng": this.Text = engLan; break;
            }
        }
    }
}
