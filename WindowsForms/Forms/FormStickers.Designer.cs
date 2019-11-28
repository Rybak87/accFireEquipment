namespace WindowsForms
{
    partial class FormStickers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStickers));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.FireCabinetsMenu = new System.Windows.Forms.ToolStripButton();
            this.ExtinguishersMenu = new System.Windows.Forms.ToolStripButton();
            this.chkWithoutStickers = new System.Windows.Forms.CheckBox();
            this.listView = new System.Windows.Forms.ListView();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FireCabinetsMenu,
            this.ExtinguishersMenu});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(457, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // FireCabinetsMenu
            // 
            this.FireCabinetsMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FireCabinetsMenu.Image = ((System.Drawing.Image)(resources.GetObject("FireCabinetsMenu.Image")));
            this.FireCabinetsMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FireCabinetsMenu.Name = "FireCabinetsMenu";
            this.FireCabinetsMenu.Size = new System.Drawing.Size(23, 22);
            this.FireCabinetsMenu.Text = "toolStripButton1";
            // 
            // ExtinguishersMenu
            // 
            this.ExtinguishersMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ExtinguishersMenu.Image = ((System.Drawing.Image)(resources.GetObject("ExtinguishersMenu.Image")));
            this.ExtinguishersMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ExtinguishersMenu.Name = "ExtinguishersMenu";
            this.ExtinguishersMenu.Size = new System.Drawing.Size(23, 22);
            this.ExtinguishersMenu.Text = "toolStripButton2";
            // 
            // chkWithoutStickers
            // 
            this.chkWithoutStickers.AutoSize = true;
            this.chkWithoutStickers.Checked = true;
            this.chkWithoutStickers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWithoutStickers.Location = new System.Drawing.Point(13, 29);
            this.chkWithoutStickers.Name = "chkWithoutStickers";
            this.chkWithoutStickers.Size = new System.Drawing.Size(129, 17);
            this.chkWithoutStickers.TabIndex = 1;
            this.chkWithoutStickers.Text = "Только без наклеек";
            this.chkWithoutStickers.UseVisualStyleBackColor = true;
            // 
            // listView
            // 
            this.listView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(0, 52);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(457, 398);
            this.listView.TabIndex = 2;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // FormStickers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 450);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.chkWithoutStickers);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormStickers";
            this.Text = "FormStickers";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton FireCabinetsMenu;
        private System.Windows.Forms.ToolStripButton ExtinguishersMenu;
        private System.Windows.Forms.CheckBox chkWithoutStickers;
        private System.Windows.Forms.ListView listView;
    }
}