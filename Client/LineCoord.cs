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

    /// <summary>
    /// Класс отвечающий за координатные линии
    /// </summary>
    public class LineCoord
    {
        /// <summary>
        ///размеры экрана по икс
        /// </summary>
        private double xS;
        /// <summary>
        ///размеры экрана по y
        /// </summary>
        private double  yS;
        private double _fX;
        private double _fY;
        /// <summary>
        /// свойство размеры экрана по икс x
        /// </summary>
        public double Svoistvo_xS
        {
            get
            {
                return xS;
            }

            set
            {
                xS = value;
            }
        }
        /// <summary>
        ///свойство размеры экрана по икс y
        /// </summary>
        public double Svoistvo_yS
        {
            get
            {
                return yS;
            }

            set
            {
                yS = value;
            }
        }

        public double Svoistvo_fX
        {
            get
            {
                return _fX;
            }

            set
            {
                _fX = value;
            }
        }

        public double Svoistvo_fY
        {
            get
            {
                return _fY;
            }

            set
            {
                _fY = value;
            }
        }
        /// <summary>
        /// линия по x
        /// </summary>
        Label _label_X;
        /// <summary>
        /// свойство линии по x
        /// </summary>
        public Label Svoistvo_label_X
        {
            get
            {
                return _label_X;
            }

            set
            {
                _label_X = value;
            }
        }
        /// <summary>
        /// линия по y
        /// </summary>
        Label _label_Y;
        /// <summary>
        /// свойство линии по y
        /// </summary>
        public Label Svoistvo_label_Y
        {
            get
            {
                return _label_Y;
            }

            set
            {
                _label_Y = value;
            }
        }
        /// <summary>
        /// линия отображающая данные
        /// </summary>
        Label _lab_Cur;
        /// <summary>
        /// свойство линии отображающей данные
        /// </summary>
        public Label Svoistvo_lab_Cur
        {
            get
            {
                return _lab_Cur;
            }

            set
            {
                _lab_Cur = value;
            }
        }
        /// <summary>
        /// график
        /// </summary>
        Chart _graphic;
        /// <summary>
        /// свойство график
        /// </summary>
        public Chart Svoistvo_graphic
        {
            get
            {
                return _graphic;
            }

            set
            {
                _graphic = value;
            }
        }

        /// <summary>
        /// Конструктор для линий и лабел +
        /// </summary>
        /// <param name="x">размеры экрана по икс</param> 
        /// <param name="y">размеры экрана по игрик</param>
        /// <param name="fy"></param>
        /// <param name="fx"></param>
        /// <param name="lab_Cur">Курс в точке</param>
        /// <param name="label_Y">Линия по Y</param>
        /// <param name="label_X">Линия по X</param>
        /// <param name="graphic">Chart</param>
        public LineCoord(int x, int y, double fX, double fy,Label lab_Cur, Label label_Y, Label label_X, ref Chart graphic)
        {
            // настройка под все  экраны
            sizeLineCoord(x, y, fX, fy);
            ObLinecoord(lab_Cur, label_Y, label_X, ref graphic);
        }

        /// <summary>
        /// Присвоение лабел и чарт +
        /// </summary>
        /// <param name="lab_Cur">Курс в точке</param>
        /// <param name="label_Y">Линия по Y</param>
        /// <param name="label_X">Линия по X</param>
        /// <param name="graphic">Chart</param>
        public void ObLinecoord(Label lab_Cur, Label label_Y, Label label_X, ref Chart graphic) 
        {
            _label_X = label_X;
            _label_Y = label_Y;
            _lab_Cur = lab_Cur;
            _graphic = graphic;
        }

        /// <summary>
        /// Подстраивание размеров под экран +
        /// </summary>
        /// <param name="x">размеры экрана по икс</param> 
        /// <param name="y">размеры экрана по игрик</param>
        /// <param name="fy"></param>
        /// <param name="fx"></param>
        public void sizeLineCoord(int x, int y, double fX, double fy)
        {
            xS = x / 1920.0;
            yS = y / 1080.0;
            _fX = fX;
            _fY = fy;
        }

        /// <summary>
        /// Проверка активации чек бокса и отображение линий +
        /// </summary>
        /// <param name="checkBoxLineCoord">чек бокс отвечающий за активацию линий</param> 
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

        /// <summary>
        /// Событие отвечающие за наведение мыши с помощью линий
        /// </summary>
        /// <param name="e">чек бокс отвечающий за активацию линий</param> 
        /// <param name="sender">чек бокс отвечающий за активацию линий</param> 
        public void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            Set(e);
        }

        /// <summary>
        /// Приватная функция отвечающая за непосредственное наведение курсора мыши
        /// </summary>
        /// <param name="e">СОбытие мыши</param> 
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
