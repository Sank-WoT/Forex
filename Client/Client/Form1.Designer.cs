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
            this.WSettings = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создателиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.currencyPairsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eURUSDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // WSettings
            // 
            this.WSettings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.оПрограммеToolStripMenuItem,
            this.currencyPairsToolStripMenuItem});
            this.WSettings.Location = new System.Drawing.Point(0, 0);
            this.WSettings.Name = "WSettings";
            this.WSettings.Size = new System.Drawing.Size(1300, 24);
            this.WSettings.TabIndex = 2;
            this.WSettings.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.windowToolStripMenuItem,
            this.chartToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(108, 20);
            this.settingsToolStripMenuItem.Text = "Standart Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.windowToolStripMenuItem.Text = "Window";
            this.windowToolStripMenuItem.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // chartToolStripMenuItem
            // 
            this.chartToolStripMenuItem.Name = "chartToolStripMenuItem";
            this.chartToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.chartToolStripMenuItem.Text = "Chart";
            this.chartToolStripMenuItem.Click += new System.EventHandler(this.chartToolStripMenuItem_Click);
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создателиToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(101, 20);
            this.оПрограммеToolStripMenuItem.Text = "About program";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // создателиToolStripMenuItem
            // 
            this.создателиToolStripMenuItem.Name = "создателиToolStripMenuItem";
            this.создателиToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.создателиToolStripMenuItem.Text = " Creators";
            this.создателиToolStripMenuItem.Click += new System.EventHandler(this.создателиToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // currencyPairsToolStripMenuItem
            // 
            this.currencyPairsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eURUSDToolStripMenuItem});
            this.currencyPairsToolStripMenuItem.Name = "currencyPairsToolStripMenuItem";
            this.currencyPairsToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.currencyPairsToolStripMenuItem.Text = "Currency pairs";
            // 
            // eURUSDToolStripMenuItem
            // 
            this.eURUSDToolStripMenuItem.Name = "eURUSDToolStripMenuItem";
            this.eURUSDToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.eURUSDToolStripMenuItem.Text = "EUR/USD";
            this.eURUSDToolStripMenuItem.Click += new System.EventHandler(this.eURUSDToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 560);
            this.Controls.Add(this.WSettings);
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

        private System.Windows.Forms.MenuStrip WSettings;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem создателиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem currencyPairsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eURUSDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    }
}

