namespace Client
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            Telerik.WinControls.UI.CarouselEllipsePath carouselEllipsePath1 = new Telerik.WinControls.UI.CarouselEllipsePath();
            this.startContainer = new System.Windows.Forms.SplitContainer();
            this.quotesList = new System.Windows.Forms.ListBox();
            this.radCarousel1 = new Telerik.WinControls.UI.RadCarousel();
            this.radImageItem1 = new Telerik.WinControls.UI.RadImageItem();
            this.radImageItem2 = new Telerik.WinControls.UI.RadImageItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.langToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создателиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.currencyPairsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eURUSDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.USDJPYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WSettings = new System.Windows.Forms.MenuStrip();
            this.radThemeManager1 = new Telerik.WinControls.RadThemeManager();
            this.object_5264c970_8296_4db5_a8ca_d09417e2a110 = new Telerik.WinControls.RootRadElement();
            ((System.ComponentModel.ISupportInitialize)(this.startContainer)).BeginInit();
            this.startContainer.Panel1.SuspendLayout();
            this.startContainer.Panel2.SuspendLayout();
            this.startContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radCarousel1)).BeginInit();
            this.WSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // startContainer
            // 
            this.startContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.startContainer, "startContainer");
            this.startContainer.Name = "startContainer";
            // 
            // startContainer.Panel1
            // 
            this.startContainer.Panel1.Controls.Add(this.quotesList);
            // 
            // startContainer.Panel2
            // 
            this.startContainer.Panel2.Controls.Add(this.radCarousel1);
            this.startContainer.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint_1);
            // 
            // quotesList
            // 
            this.quotesList.FormattingEnabled = true;
            resources.ApplyResources(this.quotesList, "quotesList");
            this.quotesList.Name = "quotesList";
            this.quotesList.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // radCarousel1
            // 
            carouselEllipsePath1.Center = new Telerik.WinControls.UI.Point3D(52.462121212121211D, 51.620947630922693D, 0D);
            carouselEllipsePath1.FinalAngle = -100D;
            carouselEllipsePath1.InitialAngle = -90D;
            carouselEllipsePath1.U = new Telerik.WinControls.UI.Point3D(-20D, -17D, -50D);
            carouselEllipsePath1.V = new Telerik.WinControls.UI.Point3D(32.007575757575758D, -26.932668329177059D, -60D);
            carouselEllipsePath1.ZScale = 500D;
            this.radCarousel1.CarouselPath = carouselEllipsePath1;
            resources.ApplyResources(this.radCarousel1, "radCarousel1");
            this.radCarousel1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radImageItem1,
            this.radImageItem2});
            this.radCarousel1.Name = "radCarousel1";
            this.radCarousel1.ThemeName = "ControlDefault";
            this.radCarousel1.VisibleItemCount = 4;
            this.radCarousel1.SelectedItemChanged += new System.EventHandler(this.radCarousel1_SelectedItemChanged);
            // 
            // radImageItem1
            // 
            this.radImageItem1.Image = ((System.Drawing.Image)(resources.GetObject("radImageItem1.Image")));
            this.radImageItem1.Name = "radImageItem1";
            resources.ApplyResources(this.radImageItem1, "radImageItem1");
            this.radImageItem1.Click += new System.EventHandler(this.radImageItem1_Click);
            // 
            // radImageItem2
            // 
            this.radImageItem2.Image = ((System.Drawing.Image)(resources.GetObject("radImageItem2.Image")));
            this.radImageItem2.Name = "radImageItem2";
            resources.ApplyResources(this.radImageItem2, "radImageItem2");
            this.radImageItem2.Click += new System.EventHandler(this.radImageItem1_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.windowToolStripMenuItem,
            this.chartToolStripMenuItem,
            this.langToolStripMenuItem});
            this.settingsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            resources.ApplyResources(this.settingsToolStripMenuItem, "settingsToolStripMenuItem");
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItem_Click);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            resources.ApplyResources(this.windowToolStripMenuItem, "windowToolStripMenuItem");
            this.windowToolStripMenuItem.Click += new System.EventHandler(this.WindowToolStripMenuItem_Click);
            // 
            // chartToolStripMenuItem
            // 
            this.chartToolStripMenuItem.Name = "chartToolStripMenuItem";
            resources.ApplyResources(this.chartToolStripMenuItem, "chartToolStripMenuItem");
            // 
            // langToolStripMenuItem
            // 
            this.langToolStripMenuItem.Name = "langToolStripMenuItem";
            resources.ApplyResources(this.langToolStripMenuItem, "langToolStripMenuItem");
            this.langToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.AboutToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.AboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создателиToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            resources.ApplyResources(this.AboutToolStripMenuItem, "AboutToolStripMenuItem");
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutProgrammToolStripMenuItem_Click);
            // 
            // создателиToolStripMenuItem
            // 
            this.создателиToolStripMenuItem.Name = "создателиToolStripMenuItem";
            resources.ApplyResources(this.создателиToolStripMenuItem, "создателиToolStripMenuItem");
            this.создателиToolStripMenuItem.Click += new System.EventHandler(this.CreatoresToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.HelpToolStripMenuItem_Click);
            // 
            // currencyPairsToolStripMenuItem
            // 
            this.currencyPairsToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.currencyPairsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eURUSDToolStripMenuItem,
            this.USDJPYToolStripMenuItem});
            this.currencyPairsToolStripMenuItem.Name = "currencyPairsToolStripMenuItem";
            resources.ApplyResources(this.currencyPairsToolStripMenuItem, "currencyPairsToolStripMenuItem");
            this.currencyPairsToolStripMenuItem.Click += new System.EventHandler(this.CurrencyPairsToolStripMenuItem_Click);
            // 
            // eURUSDToolStripMenuItem
            // 
            this.eURUSDToolStripMenuItem.Name = "eURUSDToolStripMenuItem";
            resources.ApplyResources(this.eURUSDToolStripMenuItem, "eURUSDToolStripMenuItem");
            this.eURUSDToolStripMenuItem.Click += new System.EventHandler(this.EURUSDToolStripMenuItem_Click);
            // 
            // USDJPYToolStripMenuItem
            // 
            this.USDJPYToolStripMenuItem.Name = "USDJPYToolStripMenuItem";
            resources.ApplyResources(this.USDJPYToolStripMenuItem, "USDJPYToolStripMenuItem");
            this.USDJPYToolStripMenuItem.Click += new System.EventHandler(this.USDJPYToolStripMenuItem_Click);
            // 
            // WSettings
            // 
            this.WSettings.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.WSettings.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.WSettings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.AboutToolStripMenuItem,
            this.currencyPairsToolStripMenuItem});
            resources.ApplyResources(this.WSettings, "WSettings");
            this.WSettings.Name = "WSettings";
            // 
            // object_5264c970_8296_4db5_a8ca_d09417e2a110
            // 
            this.object_5264c970_8296_4db5_a8ca_d09417e2a110.Name = "object_5264c970_8296_4db5_a8ca_d09417e2a110";
            this.object_5264c970_8296_4db5_a8ca_d09417e2a110.StretchHorizontally = true;
            this.object_5264c970_8296_4db5_a8ca_d09417e2a110.StretchVertically = true;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.Controls.Add(this.WSettings);
            this.Controls.Add(this.startContainer);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.WSettings;
            this.Name = "MainForm";
            this.TransparencyKey = System.Drawing.Color.White;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.startContainer.Panel1.ResumeLayout(false);
            this.startContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.startContainer)).EndInit();
            this.startContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radCarousel1)).EndInit();
            this.WSettings.ResumeLayout(false);
            this.WSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem langToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem создателиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem currencyPairsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eURUSDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem USDJPYToolStripMenuItem;
        private System.Windows.Forms.MenuStrip WSettings;
        private System.Windows.Forms.SplitContainer startContainer;
        private System.Windows.Forms.ListBox quotesList;
        private Telerik.WinControls.RadThemeManager radThemeManager1;
        private Telerik.WinControls.UI.RadCarousel radCarousel1;
        private Telerik.WinControls.RootRadElement object_5264c970_8296_4db5_a8ca_d09417e2a110;
        private Telerik.WinControls.UI.RadImageItem radImageItem1;
        private Telerik.WinControls.UI.RadImageItem radImageItem2;
    }
}

