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
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(OnClosing);
        }

        private void Help_Load(object sender, EventArgs e)
        {

        }
        private void OnClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.HelpClosing = true;
        }

    }
}
