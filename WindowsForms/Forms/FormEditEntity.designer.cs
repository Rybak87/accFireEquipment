namespace WindowsForms
{
    partial class FormEditEntity
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.numCountCopy = new System.Windows.Forms.NumericUpDown();
            this.lblCountCopy = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numCountCopy)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(74, 317);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(56, 19);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(204, 317);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(56, 19);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // numCountCopy
            // 
            this.numCountCopy.Location = new System.Drawing.Point(200, 25);
            this.numCountCopy.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCountCopy.Name = "numCountCopy";
            this.numCountCopy.Size = new System.Drawing.Size(150, 20);
            this.numCountCopy.TabIndex = 1;
            this.numCountCopy.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblCountCopy
            // 
            this.lblCountCopy.Location = new System.Drawing.Point(25, 25);
            this.lblCountCopy.Name = "lblCountCopy";
            this.lblCountCopy.Size = new System.Drawing.Size(175, 25);
            this.lblCountCopy.TabIndex = 2;
            this.lblCountCopy.Text = "Количество";
            // 
            // FormEditEntity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.lblCountCopy);
            this.Controls.Add(this.numCountCopy);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormEditEntity";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактор оснащения";
            ((System.ComponentModel.ISupportInitialize)(this.numCountCopy)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown numCountCopy;
        private System.Windows.Forms.Label lblCountCopy;
    }
}