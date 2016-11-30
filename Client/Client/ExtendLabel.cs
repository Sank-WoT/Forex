using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class ExtendLabel: System.Windows.Forms.Label
    {
        public string rusLan;
        private System.Windows.Forms.Button button1;
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
