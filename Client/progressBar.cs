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
    public partial class progressBar : Form
    {
        public progressBar()
        {
            InitializeComponent();
            // 500 миллисекунд
            timer1.Interval = 500; 
            timer1.Enabled = true;
            timer1.Tick += timer1_Tick;  
        }

        public void set(int prog)
        {
            progressBar1.Value = prog;
        }

        // обработчик события Tick таймера
        void timer1_Tick(object sender, EventArgs e)
        {
        }

        private void progressBar_Load(object sender, EventArgs e)
        {
            label1.BringToFront();
        }
    }
}
