namespace WindowsForms
{
    partial class FormReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReport));
            this.listView = new System.Windows.Forms.ListView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.FullMenu = new System.Windows.Forms.ToolStripButton();
            this.FireCabinetsMenu = new System.Windows.Forms.ToolStripButton();
            this.ExtinguishersMenu = new System.Windows.Forms.ToolStripButton();
            this.HosesMenu = new System.Windows.Forms.ToolStripButton();
            this.HydrantsMenu = new System.Windows.Forms.ToolStripButton();
            this.RechargeExtinguishersMenu = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(0, 27);
            this.listView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(624, 423);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FullMenu,
            this.FireCabinetsMenu,
            this.ExtinguishersMenu,
            this.HosesMenu,
            this.HydrantsMenu,
            this.RechargeExtinguishersMenu});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(624, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // FullMenu
            // 
            this.FullMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FullMenu.Image = ((System.Drawing.Image)(resources.GetObject("FullMenu.Image")));
            this.FullMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FullMenu.Name = "FullMenu";
            this.FullMenu.Size = new System.Drawing.Size(23, 22);
            this.FullMenu.Text = "Полный отчет";
            // 
            // FireCabinetsMenu
            // 
            this.FireCabinetsMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FireCabinetsMenu.Image = ((System.Drawing.Image)(resources.GetObject("FireCabinetsMenu.Image")));
            this.FireCabinetsMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FireCabinetsMenu.Name = "FireCabinetsMenu";
            this.FireCabinetsMenu.Size = new System.Drawing.Size(23, 22);
            this.FireCabinetsMenu.Text = "Пожарные шкафы";
            // 
            // ExtinguishersMenu
            // 
            this.ExtinguishersMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ExtinguishersMenu.Image = ((System.Drawing.Image)(resources.GetObject("ExtinguishersMenu.Image")));
            this.ExtinguishersMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ExtinguishersMenu.Name = "ExtinguishersMenu";
            this.ExtinguishersMenu.Size = new System.Drawing.Size(23, 22);
            this.ExtinguishersMenu.Text = "Огнетушители";
            // 
            // HosesMenu
            // 
            this.HosesMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.HosesMenu.Image = ((System.Drawing.Image)(resources.GetObject("HosesMenu.Image")));
            this.HosesMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.HosesMenu.Name = "HosesMenu";
            this.HosesMenu.Size = new System.Drawing.Size(23, 22);
            this.HosesMenu.Text = "Рукава";
            // 
            // HydrantsMenu
            // 
            this.HydrantsMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.HydrantsMenu.Image = ((System.Drawing.Image)(resources.GetObject("HydrantsMenu.Image")));
            this.HydrantsMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.HydrantsMenu.Name = "HydrantsMenu";
            this.HydrantsMenu.Size = new System.Drawing.Size(23, 22);
            this.HydrantsMenu.Text = "Гидранты";
            // 
            // RechargeExtinguishersMenu
            // 
            this.RechargeExtinguishersMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RechargeExtinguishersMenu.Image = ((System.Drawing.Image)(resources.GetObject("RechargeExtinguishersMenu.Image")));
            this.RechargeExtinguishersMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RechargeExtinguishersMenu.Name = "RechargeExtinguishersMenu";
            this.RechargeExtinguishersMenu.Size = new System.Drawing.Size(23, 22);
            this.RechargeExtinguishersMenu.Text = "Перезарядка огнетушителей";
            // 
            // FormReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 450);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.listView);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Report";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton FullMenu;
        private System.Windows.Forms.ToolStripButton FireCabinetsMenu;
        private System.Windows.Forms.ToolStripButton ExtinguishersMenu;
        private System.Windows.Forms.ToolStripButton HosesMenu;
        private System.Windows.Forms.ToolStripButton HydrantsMenu;
        private System.Windows.Forms.ToolStripButton RechargeExtinguishersMenu;
    }
}