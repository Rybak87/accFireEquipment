namespace WindowsForms
{
    partial class FormEditTypes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditTypes));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.FireCabinetsMenu = new System.Windows.Forms.ToolStripButton();
            this.ExtinguishersMenu = new System.Windows.Forms.ToolStripButton();
            this.HosesMenu = new System.Windows.Forms.ToolStripButton();
            this.listView = new System.Windows.Forms.ListView();
            this.Label = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Manufacturer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FireCabinetsMenu,
            this.ExtinguishersMenu,
            this.HosesMenu});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(424, 27);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // FireCabinetsMenu
            // 
            this.FireCabinetsMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FireCabinetsMenu.Image = ((System.Drawing.Image)(resources.GetObject("FireCabinetsMenu.Image")));
            this.FireCabinetsMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FireCabinetsMenu.Name = "FireCabinetsMenu";
            this.FireCabinetsMenu.Size = new System.Drawing.Size(24, 24);
            this.FireCabinetsMenu.Text = "Типы пожарных шкафов";
            this.FireCabinetsMenu.Click += new System.EventHandler(this.FireCabinetsMenu_Click);
            // 
            // ExtinguishersMenu
            // 
            this.ExtinguishersMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ExtinguishersMenu.Image = ((System.Drawing.Image)(resources.GetObject("ExtinguishersMenu.Image")));
            this.ExtinguishersMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ExtinguishersMenu.Name = "ExtinguishersMenu";
            this.ExtinguishersMenu.Size = new System.Drawing.Size(24, 24);
            this.ExtinguishersMenu.Text = "Типы огнетушителей";
            this.ExtinguishersMenu.Click += new System.EventHandler(this.ExtinguishersMenu_Click);
            // 
            // HosesMenu
            // 
            this.HosesMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.HosesMenu.Image = ((System.Drawing.Image)(resources.GetObject("HosesMenu.Image")));
            this.HosesMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.HosesMenu.Name = "HosesMenu";
            this.HosesMenu.Size = new System.Drawing.Size(24, 24);
            this.HosesMenu.Text = "Типы рукавов";
            this.HosesMenu.Click += new System.EventHandler(this.HosesMenu_Click);
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Label,
            this.Manufacturer});
            this.listView.FullRowSelect = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(9, 24);
            this.listView.Margin = new System.Windows.Forms.Padding(2);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(291, 332);
            this.listView.TabIndex = 1;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
            // 
            // Label
            // 
            this.Label.Text = "Имя";
            this.Label.Width = 100;
            // 
            // Manufacturer
            // 
            this.Manufacturer.Text = "Производитель";
            this.Manufacturer.Width = 150;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(322, 116);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(92, 28);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "Удалить";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.BtnRemove_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(322, 69);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(92, 28);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "Изменить";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(322, 24);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(92, 28);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(322, 161);
            this.btnImport.Margin = new System.Windows.Forms.Padding(2);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(92, 28);
            this.btnImport.TabIndex = 3;
            this.btnImport.Text = "Импорт...";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(322, 209);
            this.btnExport.Margin = new System.Windows.Forms.Padding(2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(92, 28);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Экспорт...";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // FormEditTypes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 366);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.toolStrip);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormEditTypes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormEditTypes";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton FireCabinetsMenu;
        private System.Windows.Forms.ToolStripButton ExtinguishersMenu;
        private System.Windows.Forms.ToolStripButton HosesMenu;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader Label;
        private System.Windows.Forms.ColumnHeader Manufacturer;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnExport;
    }
}