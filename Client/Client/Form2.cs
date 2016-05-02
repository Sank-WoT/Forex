using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form2 : Windowd
    {
        Windowd w = new Windowd();
        double poslTime;
        public Form2()
        {
           
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            poslTime = 0;
            poslTime = w.Conect("eurjpy", poslTime, 100000);
            Console.WriteLine("poslTime111");
            Console.WriteLine(poslTime);
        }
    }
}
