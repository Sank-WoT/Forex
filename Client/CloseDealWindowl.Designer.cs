namespace Client
{
    partial class CloseDealWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CloseDealWindow));
            this.ListD = new System.Windows.Forms.ListBox();
            this.CloseOrder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.extendLabel1 = new Client.ExtendLabel();
            this.SuspendLayout();
            // 
            // ListD
            // 
            this.ListD.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListD.FormattingEnabled = true;
            this.ListD.Location = new System.Drawing.Point(11, 11);
            this.ListD.Name = "ListD";
            this.ListD.Size = new System.Drawing.Size(143, 134);
            this.ListD.TabIndex = 0;
            this.ListD.SelectedIndexChanged += new System.EventHandler(this.ListDeal);
            // 
            // CloseOrder
            // 
            this.CloseOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseOrder.Location = new System.Drawing.Point(160, 111);
            this.CloseOrder.Name = "CloseOrder";
            this.CloseOrder.Size = new System.Drawing.Size(117, 34);
            this.CloseOrder.TabIndex = 1;
            this.CloseOrder.Text = "Close order";
            this.CloseOrder.UseVisualStyleBackColor = true;
            this.CloseOrder.Click += new System.EventHandler(this.CloseOrder_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(161, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Прибыль";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // extendLabel1
            // 
            this.extendLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.extendLabel1.AutoSize = true;
            this.extendLabel1.Location = new System.Drawing.Point(161, 11);
            this.extendLabel1.Name = "extendLabel1";
            this.extendLabel1.Size = new System.Drawing.Size(53, 13);
            this.extendLabel1.TabIndex = 2;
            this.extendLabel1.Text = "Прибыль";
            // 
            // CloseDealWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(290, 155);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListD);
            this.Controls.Add(this.extendLabel1);
            this.Controls.Add(this.CloseOrder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CloseDealWindow";
            this.Text = "CloseDeal";
            this.Load += new System.EventHandler(this.CloseDeal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox ListD;
        private System.Windows.Forms.Button CloseOrder;
        private ExtendLabel extendLabel1;
        private System.Windows.Forms.Label label1;
    }
}