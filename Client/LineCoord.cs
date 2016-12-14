namespace Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Globalization;
    using System.IO; // для класса 
    using System.Linq;
    using System.Media;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Windows.Forms.DataVisualization.Charting;
    using EnumDialogResult = System.Windows.Forms.DialogResult;
    class LineCoord
    {
        private  double xS;
        private double yS;
        private double _fX;
        private double _fY;
        Label _label_X;
        Label _label_Y;
        Label _lab_Cur;
        Chart _graphic;
        public LineCoord(int x, int y, double fX, double fy,Label lab_Cur, Label label_Y, Label label_X, ref Chart graphic)
        {
            // настройка под все  экраны
            xS = x / 1920.0;
            yS = y / 1080.0;
            _fX = fX;
            _fY = fy;
            _label_X = label_X;
            _label_Y = label_Y;
            _lab_Cur = lab_Cur;
            _graphic = graphic;
        }

        public void Show(CheckBox checkBoxLineCoord)
        {
            if (true == checkBoxLineCoord.Checked)
            {
                _label_X.Visible = true;
                _label_Y.Visible = true;
                _lab_Cur.Visible = true;
                // линии должны зависить от чарта
                _label_X.Size = new Size(Convert.ToInt32(860 * xS * (WString.X / _fX)), 1);
                _label_Y.Size = new Size(1, Convert.ToInt32(550 * xS * (WString.Y / _fY)));
                // цвет линии по X
                _label_X.BackColor = Color.FromArgb(0, 0, 0);
                // цвет линии по Y
                _label_Y.BackColor = Color.FromArgb(0, 0, 0);
                _graphic.MouseMove += new MouseEventHandler(this.chart1_MouseMove);
            }
            if (false == checkBoxLineCoord.Checked)
            {
                // убрать отображение линии по X
                _label_X.Visible = false;
                // убрать отображение линии по Y
                _label_Y.Visible = false;
                // убрать отображение значений
                _lab_Cur.Visible = false;
            }
        }

        public void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            Set(e);
        }

        private void  Set(MouseEventArgs e)
        {
            if (e.Y > Convert.ToInt32(27 * yS * (WString.Y / _fY)) && e.Y < Convert.ToInt32(629 * yS * (WString.Y / _fY)) && e.X > Convert.ToInt32(110 * xS * (WString.X / _fX)))
            {
                // перемещение линии по y
                _label_X.Location = new Point(Convert.ToInt32(110 * xS * (WString.X / _fX)), e.Y);
                // перемещение линии по X
                _label_Y.Location = new Point(e.X, Convert.ToInt32(30 * yS * (WString.Y / _fY)));
                // привязка значения 
                _lab_Cur.Location = new Point(Convert.ToInt32(700 * xS * (WString.X / _fX)), e.Y);
                double YCur = _graphic.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);
                double XCur = _graphic.ChartAreas[0].AxisX.PixelPositionToValue(e.X) / 0.000011574074074074;
                // время в формате UNIX
                DateTime Date1 = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(XCur);
                // перевод времени
                _lab_Cur.Text = string.Concat(string.Concat(Math.Round(YCur, 5).ToString(), " , "), Date1.ToString("h:mm:ss.fff tt"));
            }
        }
    }
}
