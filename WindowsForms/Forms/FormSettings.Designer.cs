namespace WindowsForms
{
    partial class FormSettings
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txbHoses = new System.Windows.Forms.TextBox();
            this.txbExtinguishers = new System.Windows.Forms.TextBox();
            this.txbFireCabinets = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.scrIconSize = new System.Windows.Forms.HScrollBar();
            this.label5 = new System.Windows.Forms.Label();
            this.lblIconSize = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txbHoses);
            this.groupBox1.Controls.Add(this.txbExtinguishers);
            this.groupBox1.Controls.Add(this.txbFireCabinets);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 167);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Правила именования";
            // 
            // txbHoses
            // 
            this.txbHoses.Location = new System.Drawing.Point(114, 73);
            this.txbHoses.Name = "txbHoses";
            this.txbHoses.Size = new System.Drawing.Size(174, 20);
            this.txbHoses.TabIndex = 1;
            // 
            // txbExtinguishers
            // 
            this.txbExtinguishers.Location = new System.Drawing.Point(114, 48);
            this.txbExtinguishers.Name = "txbExtinguishers";
            this.txbExtinguishers.Size = new System.Drawing.Size(174, 20);
            this.txbExtinguishers.TabIndex = 1;
            // 
            // txbFireCabinets
            // 
            this.txbFireCabinets.Location = new System.Drawing.Point(114, 24);
            this.txbFireCabinets.Name = "txbFireCabinets";
            this.txbFireCabinets.Size = new System.Drawing.Size(174, 20);
            this.txbFireCabinets.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(7, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(281, 55);
            this.label4.TabIndex = 0;
            this.label4.Text = "#L - номер локации\n#F - номер пожарного шкафа\n#E - номер огнетушителя\n#H - номер " +
    "рукава";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(7, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "Рукава";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Огнетушители";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Пожарные шкафы";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Location = new System.Drawing.Point(242, 218);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(75, 23);
            this.btnSaveSettings.TabIndex = 2;
            this.btnSaveSettings.Text = "Сохранить";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // scrIconSize
            // 
            this.scrIconSize.LargeChange = 1;
            this.scrIconSize.Location = new System.Drawing.Point(23, 222);
            this.scrIconSize.Maximum = 70;
            this.scrIconSize.Minimum = 20;
            this.scrIconSize.Name = "scrIconSize";
            this.scrIconSize.Size = new System.Drawing.Size(124, 19);
            this.scrIconSize.TabIndex = 3;
            this.scrIconSize.Value = 20;
            this.scrIconSize.ValueChanged += new System.EventHandler(this.scrIconSize_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 199);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Размер иконок:";
            // 
            // lblIconSize
            // 
            this.lblIconSize.AutoSize = true;
            this.lblIconSize.Location = new System.Drawing.Point(112, 199);
            this.lblIconSize.Name = "lblIconSize";
            this.lblIconSize.Size = new System.Drawing.Size(35, 13);
            this.lblIconSize.TabIndex = 5;
            this.lblIconSize.Text = "label6";
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(341, 265);
            this.Controls.Add(this.lblIconSize);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.scrIconSize);
            this.Controls.Add(this.btnSaveSettings);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormSettings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbHoses;
        private System.Windows.Forms.TextBox txbExtinguishers;
        private System.Windows.Forms.TextBox txbFireCabinets;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.HScrollBar scrIconSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblIconSize;
    }
}