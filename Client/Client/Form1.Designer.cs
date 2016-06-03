namespace Client
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
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
            this.labelSelectPair = new System.Windows.Forms.Label();
            this.buttonEurUsd = new System.Windows.Forms.Button();
            this.buttonUsdJpy = new System.Windows.Forms.Button();
            this.WSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.windowToolStripMenuItem,
            this.chartToolStripMenuItem,
            this.langToolStripMenuItem});
            this.settingsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.settingsToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItem_Click);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.windowToolStripMenuItem.Text = "Window";
            this.windowToolStripMenuItem.Click += new System.EventHandler(this.WindowToolStripMenuItem_Click);
            // 
            // chartToolStripMenuItem
            // 
            this.chartToolStripMenuItem.Name = "chartToolStripMenuItem";
            this.chartToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.chartToolStripMenuItem.Text = "Chart";
            this.chartToolStripMenuItem.Click += new System.EventHandler(this.СhartToolStripMenuItem_Click);
            // 
            // langToolStripMenuItem
            // 
            this.langToolStripMenuItem.Name = "langToolStripMenuItem";
            this.langToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.langToolStripMenuItem.Text = "Русский";
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
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.AboutToolStripMenuItem.Text = "About";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutProgrammToolStripMenuItem_Click);
            // 
            // создателиToolStripMenuItem
            // 
            this.создателиToolStripMenuItem.Name = "создателиToolStripMenuItem";
            this.создателиToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.создателиToolStripMenuItem.Text = " Creators";
            this.создателиToolStripMenuItem.Click += new System.EventHandler(this.CreatoresToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.HelpToolStripMenuItem_Click);
            // 
            // currencyPairsToolStripMenuItem
            // 
            this.currencyPairsToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.currencyPairsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eURUSDToolStripMenuItem,
            this.USDJPYToolStripMenuItem});
            this.currencyPairsToolStripMenuItem.Name = "currencyPairsToolStripMenuItem";
            this.currencyPairsToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.currencyPairsToolStripMenuItem.Text = "Currency pairs";
            this.currencyPairsToolStripMenuItem.Click += new System.EventHandler(this.CurrencyPairsToolStripMenuItem_Click);
            // 
            // eURUSDToolStripMenuItem
            // 
            this.eURUSDToolStripMenuItem.Name = "eURUSDToolStripMenuItem";
            this.eURUSDToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.eURUSDToolStripMenuItem.Text = "EUR/USD";
            this.eURUSDToolStripMenuItem.Click += new System.EventHandler(this.EURUSDToolStripMenuItem_Click);
            // 
            // USDJPYToolStripMenuItem
            // 
            this.USDJPYToolStripMenuItem.Name = "USDJPYToolStripMenuItem";
            this.USDJPYToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.USDJPYToolStripMenuItem.Text = "USD/JPY";
            this.USDJPYToolStripMenuItem.Click += new System.EventHandler(this.USDJPYToolStripMenuItem_Click);
            // 
            // WSettings
            // 
            this.WSettings.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.WSettings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.AboutToolStripMenuItem,
            this.currencyPairsToolStripMenuItem});
            this.WSettings.Location = new System.Drawing.Point(0, 0);
            this.WSettings.Name = "WSettings";
            this.WSettings.Size = new System.Drawing.Size(1300, 24);
            this.WSettings.TabIndex = 2;
            this.WSettings.Text = "menuStrip1";
            // 
            // labelSelectPair
            // 
            this.labelSelectPair.AutoSize = true;
            this.labelSelectPair.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelSelectPair.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelSelectPair.Location = new System.Drawing.Point(157, 39);
            this.labelSelectPair.Margin = new System.Windows.Forms.Padding(30, 30, 30, 0);
            this.labelSelectPair.Name = "labelSelectPair";
            this.labelSelectPair.Size = new System.Drawing.Size(254, 15);
            this.labelSelectPair.TabIndex = 3;
            this.labelSelectPair.Text = "Выберите валютную пару для начала торгов";
            this.labelSelectPair.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonEurUsd
            // 
            this.buttonEurUsd.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonEurUsd.Location = new System.Drawing.Point(160, 90);
            this.buttonEurUsd.Name = "buttonEurUsd";
            this.buttonEurUsd.Size = new System.Drawing.Size(98, 38);
            this.buttonEurUsd.TabIndex = 4;
            this.buttonEurUsd.Text = "EUR / USD";
            this.buttonEurUsd.UseVisualStyleBackColor = true;
            this.buttonEurUsd.Click += new System.EventHandler(this.EURUSDToolStripMenuItem_Click);
            // 
            // buttonUsdJpy
            // 
            this.buttonUsdJpy.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonUsdJpy.Location = new System.Drawing.Point(313, 90);
            this.buttonUsdJpy.Name = "buttonUsdJpy";
            this.buttonUsdJpy.Size = new System.Drawing.Size(98, 38);
            this.buttonUsdJpy.TabIndex = 5;
            this.buttonUsdJpy.Text = "USD / JPY";
            this.buttonUsdJpy.UseVisualStyleBackColor = true;
            this.buttonUsdJpy.Click += new System.EventHandler(this.USDJPYToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 560);
            this.Controls.Add(this.buttonUsdJpy);
            this.Controls.Add(this.buttonEurUsd);
            this.Controls.Add(this.labelSelectPair);
            this.Controls.Add(this.WSettings);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "PM";
            this.Load += new System.EventHandler(this.Form1_Load);
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
    }
}

