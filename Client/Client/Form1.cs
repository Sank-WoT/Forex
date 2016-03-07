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

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            int y = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height; //высота экрана
            int x = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width; //ширина экрана  
            double xS =(x / 1920.0);//настройка под все  экраны
            double yS =(y / 1080.0);//настройка под все  экраны
            this.Size = new Size(x, y);// задание размеров экрана
            Console.WriteLine(xS);
            button1.Location = new Point(0,0);
            button1.Size = new Size((int)(xS * 180), (int)(yS * 40));//настроил размеры  кнопки 
            Settings.Location = new Point((int)(xS * 180), 0);
            Settings.Size = new Size((int)(xS * 180), (int)(yS * 40));//настроил размеры  кнопки 
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            Window f = new Window();
            f.Show();//модольное окно 
        }
        private void Settings_Click(object sender, EventArgs e)
        { 
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
