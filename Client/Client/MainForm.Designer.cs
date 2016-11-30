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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.buttonEurUsd = new System.Windows.Forms.Button();
            this.buttonUsdJpy = new System.Windows.Forms.Button();
            this.labelSelectPair = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.WSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            // 
            // splitContainer1.Panel2
            // 
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            this.splitContainer1.Panel2.Controls.Add(this.buttonEurUsd);
            this.splitContainer1.Panel2.Controls.Add(this.buttonUsdJpy);
            this.splitContainer1.Panel2.Controls.Add(this.labelSelectPair);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint_1);
            // 
            // buttonEurUsd
            // 
            resources.ApplyResources(this.buttonEurUsd, "buttonEurUsd");
            this.buttonEurUsd.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonEurUsd.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonEurUsd.Name = "buttonEurUsd";
            this.buttonEurUsd.UseVisualStyleBackColor = true;
            this.buttonEurUsd.Click += new System.EventHandler(this.EURUSDToolStripMenuItem_Click);
            // 
            // buttonUsdJpy
            // 
            resources.ApplyResources(this.buttonUsdJpy, "buttonUsdJpy");
            this.buttonUsdJpy.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonUsdJpy.Name = "buttonUsdJpy";
            this.buttonUsdJpy.UseVisualStyleBackColor = true;
            this.buttonUsdJpy.Click += new System.EventHandler(this.USDJPYToolStripMenuItem_Click);
            // 
            // labelSelectPair
            // 
            resources.ApplyResources(this.labelSelectPair, "labelSelectPair");
            this.labelSelectPair.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelSelectPair.Name = "labelSelectPair";
            // 
            // settingsToolStripMenuItem
            // 
            resources.ApplyResources(this.settingsToolStripMenuItem, "settingsToolStripMenuItem");
            this.settingsToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.windowToolStripMenuItem,
            this.chartToolStripMenuItem,
            this.langToolStripMenuItem});
            this.settingsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItem_Click);
            // 
            // windowToolStripMenuItem
            // 
            resources.ApplyResources(this.windowToolStripMenuItem, "windowToolStripMenuItem");
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Click += new System.EventHandler(this.WindowToolStripMenuItem_Click);
            // 
            // chartToolStripMenuItem
            // 
            resources.ApplyResources(this.chartToolStripMenuItem, "chartToolStripMenuItem");
            this.chartToolStripMenuItem.Name = "chartToolStripMenuItem";
            this.chartToolStripMenuItem.Click += new System.EventHandler(this.СhartToolStripMenuItem_Click);
            // 
            // langToolStripMenuItem
            // 
            resources.ApplyResources(this.langToolStripMenuItem, "langToolStripMenuItem");
            this.langToolStripMenuItem.Name = "langToolStripMenuItem";
            this.langToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // AboutToolStripMenuItem
            // 
            resources.ApplyResources(this.AboutToolStripMenuItem, "AboutToolStripMenuItem");
            this.AboutToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.AboutToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.AboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создателиToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutProgrammToolStripMenuItem_Click);
            // 
            // создателиToolStripMenuItem
            // 
            resources.ApplyResources(this.создателиToolStripMenuItem, "создателиToolStripMenuItem");
            this.создателиToolStripMenuItem.Name = "создателиToolStripMenuItem";
            this.создателиToolStripMenuItem.Click += new System.EventHandler(this.CreatoresToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.HelpToolStripMenuItem_Click);
            // 
            // currencyPairsToolStripMenuItem
            // 
            resources.ApplyResources(this.currencyPairsToolStripMenuItem, "currencyPairsToolStripMenuItem");
            this.currencyPairsToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.currencyPairsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eURUSDToolStripMenuItem,
            this.USDJPYToolStripMenuItem});
            this.currencyPairsToolStripMenuItem.Name = "currencyPairsToolStripMenuItem";
            this.currencyPairsToolStripMenuItem.Click += new System.EventHandler(this.CurrencyPairsToolStripMenuItem_Click);
            // 
            // eURUSDToolStripMenuItem
            // 
            resources.ApplyResources(this.eURUSDToolStripMenuItem, "eURUSDToolStripMenuItem");
            this.eURUSDToolStripMenuItem.Name = "eURUSDToolStripMenuItem";
            this.eURUSDToolStripMenuItem.Click += new System.EventHandler(this.EURUSDToolStripMenuItem_Click);
            // 
            // USDJPYToolStripMenuItem
            // 
            resources.ApplyResources(this.USDJPYToolStripMenuItem, "USDJPYToolStripMenuItem");
            this.USDJPYToolStripMenuItem.Name = "USDJPYToolStripMenuItem";
            this.USDJPYToolStripMenuItem.Click += new System.EventHandler(this.USDJPYToolStripMenuItem_Click);
            // 
            // WSettings
            // 
            resources.ApplyResources(this.WSettings, "WSettings");
            this.WSettings.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.WSettings.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.WSettings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.AboutToolStripMenuItem,
            this.currencyPairsToolStripMenuItem});
            this.WSettings.Name = "WSettings";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.Controls.Add(this.WSettings);
            this.Controls.Add(this.splitContainer1);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.WSettings;
            this.Name = "MainForm";
            this.TransparencyKey = System.Drawing.Color.White;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
        private System.Windows.Forms.Label labelSelectPair;
        private System.Windows.Forms.Button buttonEurUsd;
        private System.Windows.Forms.Button buttonUsdJpy;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}

