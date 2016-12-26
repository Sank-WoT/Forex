namespace Client
{
    partial class Windowd
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Windowd));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lab_Cur = new System.Windows.Forms.Label();
            this.label_X = new System.Windows.Forms.Label();
            this.label_Y = new System.Windows.Forms.Label();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.timeLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.secondToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minutesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minutesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.hourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.weekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toCloseTheDealToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBoxLevelSupandResis = new Client.ExtendCheckbox();
            this.checkBoxSMA = new Client.ExtendCheckbox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkBoxLineCoord = new Client.ExtendCheckbox();
            this.checkBoxBinding = new Client.ExtendCheckbox();
            this.buttonBuy = new Client.ExtendButton();
            this.buttonSell = new Client.ExtendButton();
            this.price = new Client.ExtendButton();
            this.buttonPriceBuy = new Client.ExtendButton();
            this.buttonPriceSell = new Client.ExtendButton();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lab_Cur
            // 
            resources.ApplyResources(this.lab_Cur, "lab_Cur");
            this.lab_Cur.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lab_Cur.Name = "lab_Cur";
            this.lab_Cur.Click += new System.EventHandler(this.lab_Cur_Click);
            // 
            // label_X
            // 
            this.label_X.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            resources.ApplyResources(this.label_X, "label_X");
            this.label_X.Name = "label_X";
            this.label_X.Click += new System.EventHandler(this.label_X_Click);
            // 
            // label_Y
            // 
            this.label_Y.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            resources.ApplyResources(this.label_Y, "label_Y");
            this.label_Y.Name = "label_Y";
            this.label_Y.Click += new System.EventHandler(this.label_Y_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.timeLevelToolStripMenuItem,
            this.reportToolStripMenuItem,
            this.settingToolStripMenuItem,
            this.toCloseTheDealToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // timeLevelToolStripMenuItem
            // 
            this.timeLevelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.secondToolStripMenuItem,
            this.minutesToolStripMenuItem,
            this.minutesToolStripMenuItem1,
            this.hourToolStripMenuItem,
            this.dayToolStripMenuItem,
            this.weekToolStripMenuItem,
            this.monthToolStripMenuItem});
            this.timeLevelToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.timeLevelToolStripMenuItem.Name = "timeLevelToolStripMenuItem";
            resources.ApplyResources(this.timeLevelToolStripMenuItem, "timeLevelToolStripMenuItem");
            this.timeLevelToolStripMenuItem.Click += new System.EventHandler(this.timeLevelToolStripMenuItem_Click);
            // 
            // secondToolStripMenuItem
            // 
            this.secondToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.secondToolStripMenuItem.Name = "secondToolStripMenuItem";
            resources.ApplyResources(this.secondToolStripMenuItem, "secondToolStripMenuItem");
            this.secondToolStripMenuItem.Click += new System.EventHandler(this.SecondToolStripMenuItem_Click);
            // 
            // minutesToolStripMenuItem
            // 
            this.minutesToolStripMenuItem.Name = "minutesToolStripMenuItem";
            resources.ApplyResources(this.minutesToolStripMenuItem, "minutesToolStripMenuItem");
            this.minutesToolStripMenuItem.Click += new System.EventHandler(this.MinutesToolStripMenuItem_Click);
            // 
            // minutesToolStripMenuItem1
            // 
            this.minutesToolStripMenuItem1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.minutesToolStripMenuItem1.Name = "minutesToolStripMenuItem1";
            resources.ApplyResources(this.minutesToolStripMenuItem1, "minutesToolStripMenuItem1");
            this.minutesToolStripMenuItem1.Click += new System.EventHandler(this.MinutesToolStripMenuItem1_Click);
            // 
            // hourToolStripMenuItem
            // 
            this.hourToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.hourToolStripMenuItem.Name = "hourToolStripMenuItem";
            resources.ApplyResources(this.hourToolStripMenuItem, "hourToolStripMenuItem");
            this.hourToolStripMenuItem.Click += new System.EventHandler(this.HourToolStripMenuItem_Click);
            // 
            // dayToolStripMenuItem
            // 
            this.dayToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dayToolStripMenuItem.Name = "dayToolStripMenuItem";
            resources.ApplyResources(this.dayToolStripMenuItem, "dayToolStripMenuItem");
            this.dayToolStripMenuItem.Click += new System.EventHandler(this.DayToolStripMenuItem_Click);
            // 
            // weekToolStripMenuItem
            // 
            this.weekToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.weekToolStripMenuItem.Name = "weekToolStripMenuItem";
            resources.ApplyResources(this.weekToolStripMenuItem, "weekToolStripMenuItem");
            this.weekToolStripMenuItem.Click += new System.EventHandler(this.WeekToolStripMenuItem_Click);
            // 
            // monthToolStripMenuItem
            // 
            this.monthToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.monthToolStripMenuItem.Name = "monthToolStripMenuItem";
            resources.ApplyResources(this.monthToolStripMenuItem, "monthToolStripMenuItem");
            this.monthToolStripMenuItem.Click += new System.EventHandler(this.MonthToolStripMenuItem_Click);
            // 
            // reportToolStripMenuItem
            // 
            this.reportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createReportToolStripMenuItem});
            this.reportToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            resources.ApplyResources(this.reportToolStripMenuItem, "reportToolStripMenuItem");
            this.reportToolStripMenuItem.Click += new System.EventHandler(this.reportToolStripMenuItem_Click);
            // 
            // createReportToolStripMenuItem
            // 
            this.createReportToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.createReportToolStripMenuItem.Name = "createReportToolStripMenuItem";
            resources.ApplyResources(this.createReportToolStripMenuItem, "createReportToolStripMenuItem");
            this.createReportToolStripMenuItem.Click += new System.EventHandler(this.CreateReportToolStripMenuItem_Click);
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            resources.ApplyResources(this.settingToolStripMenuItem, "settingToolStripMenuItem");
            this.settingToolStripMenuItem.Click += new System.EventHandler(this.SettingToolStripMenuItem_Click);
            // 
            // toCloseTheDealToolStripMenuItem
            // 
            this.toCloseTheDealToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toCloseTheDealToolStripMenuItem.Name = "toCloseTheDealToolStripMenuItem";
            resources.ApplyResources(this.toCloseTheDealToolStripMenuItem, "toCloseTheDealToolStripMenuItem");
            this.toCloseTheDealToolStripMenuItem.Click += new System.EventHandler(this.ToCloseTheDealToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Pm.ico");
            // 
            // checkBox4
            // 
            resources.ApplyResources(this.checkBox4, "checkBox4");
            this.checkBox4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            this.checkBox4.Click += new System.EventHandler(this.checkBox4_Check);
            // 
            // numericUpDown1
            // 
            resources.ApplyResources(this.numericUpDown1, "numericUpDown1");
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.checkBoxLevelSupandResis);
            this.tabPage1.Controls.Add(this.numericUpDown1);
            this.tabPage1.Controls.Add(this.checkBoxSMA);
            this.tabPage1.Controls.Add(this.checkBox4);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // checkBoxLevelSupandResis
            // 
            resources.ApplyResources(this.checkBoxLevelSupandResis, "checkBoxLevelSupandResis");
            this.checkBoxLevelSupandResis.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxLevelSupandResis.Name = "checkBoxLevelSupandResis";
            this.checkBoxLevelSupandResis.UseVisualStyleBackColor = true;
            this.checkBoxLevelSupandResis.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBoxSMA
            // 
            resources.ApplyResources(this.checkBoxSMA, "checkBoxSMA");
            this.checkBoxSMA.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxSMA.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxSMA.Name = "checkBoxSMA";
            this.checkBoxSMA.UseVisualStyleBackColor = false;
            this.checkBoxSMA.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            this.checkBoxSMA.Click += new System.EventHandler(this.checkBoxSMA_Checked);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.checkBoxLineCoord);
            this.tabPage2.Controls.Add(this.checkBoxBinding);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBoxLineCoord
            // 
            resources.ApplyResources(this.checkBoxLineCoord, "checkBoxLineCoord");
            this.checkBoxLineCoord.ForeColor = System.Drawing.SystemColors.InfoText;
            this.checkBoxLineCoord.Name = "checkBoxLineCoord";
            this.checkBoxLineCoord.UseVisualStyleBackColor = true;
            this.checkBoxLineCoord.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBoxBinding
            // 
            resources.ApplyResources(this.checkBoxBinding, "checkBoxBinding");
            this.checkBoxBinding.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxBinding.Name = "checkBoxBinding";
            this.checkBoxBinding.UseVisualStyleBackColor = true;
            this.checkBoxBinding.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
            // 
            // buttonBuy
            // 
            this.buttonBuy.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonBuy, "buttonBuy");
            this.buttonBuy.Name = "buttonBuy";
            this.buttonBuy.UseVisualStyleBackColor = true;
            this.buttonBuy.Click += new System.EventHandler(this.Buy_Click);
            // 
            // buttonSell
            // 
            this.buttonSell.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonSell, "buttonSell");
            this.buttonSell.Name = "buttonSell";
            this.buttonSell.UseVisualStyleBackColor = true;
            this.buttonSell.Click += new System.EventHandler(this.Sell_Click);
            // 
            // price
            // 
            this.price.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.price, "price");
            this.price.Name = "price";
            this.price.UseVisualStyleBackColor = true;
            this.price.Click += new System.EventHandler(this.button8_Click);
            // 
            // buttonPriceBuy
            // 
            this.buttonPriceBuy.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonPriceBuy, "buttonPriceBuy");
            this.buttonPriceBuy.Name = "buttonPriceBuy";
            this.buttonPriceBuy.UseVisualStyleBackColor = true;
            this.buttonPriceBuy.Click += new System.EventHandler(this.button7_Click);
            // 
            // buttonPriceSell
            // 
            this.buttonPriceSell.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.buttonPriceSell, "buttonPriceSell");
            this.buttonPriceSell.Name = "buttonPriceSell";
            this.buttonPriceSell.UseVisualStyleBackColor = true;
            this.buttonPriceSell.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Windowd
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.lab_Cur);
            this.Controls.Add(this.label_Y);
            this.Controls.Add(this.label_X);
            this.Controls.Add(this.buttonBuy);
            this.Controls.Add(this.buttonSell);
            this.Controls.Add(this.price);
            this.Controls.Add(this.buttonPriceBuy);
            this.Controls.Add(this.buttonPriceSell);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Windowd";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Window_FormClosing);
            this.Load += new System.EventHandler(this.Window_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart graphic;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lab_Cur;
        private System.Windows.Forms.Label label_X;
        private System.Windows.Forms.Label label_Y;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.ToolTip toolTip3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem timeLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem secondToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minutesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minutesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem hourToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem weekToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toCloseTheDealToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.CheckBox checkBox4;
        private ExtendButton buttonPriceBuy;
        private ExtendButton buttonPriceSell;
        private ExtendCheckbox checkBoxSMA;
        private ExtendCheckbox checkBoxBinding;
        private ExtendCheckbox checkBoxLineCoord;
        private ExtendButton price;
        private ExtendCheckbox checkBoxLevelSupandResis;
        private ExtendButton buttonSell;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private ExtendButton buttonBuy;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}
