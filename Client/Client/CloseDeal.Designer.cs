namespace Client
{
    partial class CloseDeal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CloseDeal));
            this.ListD = new System.Windows.Forms.ListBox();
            this.CloseOrder = new System.Windows.Forms.Button();
<<<<<<< HEAD
            this.Profit = new System.Windows.Forms.Label();
            this.extendLabel1 = new Client.ExtendLabel();
=======
>>>>>>> parent of bcab9ee... Forex2.0 Переписать код под более строгую архитектуру ООП, создать БД.
            this.SuspendLayout();
            // 
            // ListD
            // 
            this.ListD.FormattingEnabled = true;
            this.ListD.Location = new System.Drawing.Point(12, 9);
            this.ListD.Name = "ListD";
            this.ListD.Size = new System.Drawing.Size(143, 134);
            this.ListD.TabIndex = 0;
            this.ListD.SelectedIndexChanged += new System.EventHandler(this.ListDeal);
            // 
            // CloseOrder
            // 
            this.CloseOrder.Location = new System.Drawing.Point(161, 109);
            this.CloseOrder.Name = "CloseOrder";
            this.CloseOrder.Size = new System.Drawing.Size(117, 34);
            this.CloseOrder.TabIndex = 1;
            this.CloseOrder.Text = "Close order";
            this.CloseOrder.UseVisualStyleBackColor = true;
            this.CloseOrder.Click += new System.EventHandler(this.CloseOrder_Click);
            // 
<<<<<<< HEAD
            // Profit
            // 
            this.Profit.AutoSize = true;
            this.Profit.Location = new System.Drawing.Point(162, 34);
            this.Profit.Name = "Profit";
            this.Profit.Size = new System.Drawing.Size(114, 13);
            this.Profit.TabIndex = 3;
            this.Profit.Text = "Ожидаемая прибыль";
            // 
            // extendLabel1
            // 
            this.extendLabel1.AutoSize = true;
            this.extendLabel1.Location = new System.Drawing.Point(162, 9);
            this.extendLabel1.Name = "extendLabel1";
            this.extendLabel1.Size = new System.Drawing.Size(53, 13);
            this.extendLabel1.TabIndex = 2;
            this.extendLabel1.Text = "Прибыль";
            // 
=======
>>>>>>> parent of bcab9ee... Forex2.0 Переписать код под более строгую архитектуру ООП, создать БД.
            // CloseDeal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(290, 155);
<<<<<<< HEAD
            this.Controls.Add(this.Profit);
            this.Controls.Add(this.ListD);
            this.Controls.Add(this.extendLabel1);
=======
>>>>>>> parent of bcab9ee... Forex2.0 Переписать код под более строгую архитектуру ООП, создать БД.
            this.Controls.Add(this.CloseOrder);
            this.Controls.Add(this.ListD);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CloseDeal";
            this.Text = "CloseDeal";
            this.Load += new System.EventHandler(this.CloseDeal_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox ListD;
        private System.Windows.Forms.Button CloseOrder;
<<<<<<< HEAD
        private ExtendLabel extendLabel1;
        private System.Windows.Forms.Label Profit;
=======
>>>>>>> parent of bcab9ee... Forex2.0 Переписать код под более строгую архитектуру ООП, создать БД.
    }
}