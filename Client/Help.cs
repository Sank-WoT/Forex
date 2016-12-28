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
    /// <summary>
    /// Класс отвечающий за  окно Help
    /// </summary>
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
        /// <summary>
        /// событие срабатываемое при нажатии закрытия формы
        /// </summary>
        private void OnClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.HelpClosing = true;
        }

    }
}
