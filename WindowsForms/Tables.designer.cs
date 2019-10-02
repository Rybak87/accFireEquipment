namespace WindowsForms
{
    partial class DbTables
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvTables = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.TablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LocationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FireCabinetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExtinguihersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HosesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HydrantesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TypeExtinguisherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TypeHoseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.TypeFireCabinetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTables)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvTables
            // 
            this.dgvTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTables.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvTables.Location = new System.Drawing.Point(12, 31);
            this.dgvTables.Name = "dgvTables";
            this.dgvTables.RowTemplate.Height = 24;
            this.dgvTables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTables.Size = new System.Drawing.Size(923, 518);
            this.dgvTables.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TablesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(947, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // TablesToolStripMenuItem
            // 
            this.TablesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LocationsToolStripMenuItem,
            this.FireCabinetsToolStripMenuItem,
            this.ExtinguihersToolStripMenuItem,
            this.HosesToolStripMenuItem,
            this.HydrantesToolStripMenuItem,
            this.TypeExtinguisherToolStripMenuItem,
            this.TypeHoseToolStripMenuItem,
            this.TypeFireCabinetToolStripMenuItem});
            this.TablesToolStripMenuItem.Name = "TablesToolStripMenuItem";
            this.TablesToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.TablesToolStripMenuItem.Text = "Таблицы";
            // 
            // LocationsToolStripMenuItem
            // 
            this.LocationsToolStripMenuItem.Name = "LocationsToolStripMenuItem";
            this.LocationsToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.LocationsToolStripMenuItem.Text = "Локации";
            // 
            // FireCabinetsToolStripMenuItem
            // 
            this.FireCabinetsToolStripMenuItem.Name = "FireCabinetsToolStripMenuItem";
            this.FireCabinetsToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.FireCabinetsToolStripMenuItem.Text = "Пожарные шкафы";
            // 
            // ExtinguihersToolStripMenuItem
            // 
            this.ExtinguihersToolStripMenuItem.Name = "ExtinguihersToolStripMenuItem";
            this.ExtinguihersToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.ExtinguihersToolStripMenuItem.Text = "Огнетушители";
            // 
            // HosesToolStripMenuItem
            // 
            this.HosesToolStripMenuItem.Name = "HosesToolStripMenuItem";
            this.HosesToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.HosesToolStripMenuItem.Text = "Рукава";
            // 
            // HydrantesToolStripMenuItem
            // 
            this.HydrantesToolStripMenuItem.Name = "HydrantesToolStripMenuItem";
            this.HydrantesToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.HydrantesToolStripMenuItem.Text = "Пожарные краны";
            // 
            // TypeExtinguisherToolStripMenuItem
            // 
            this.TypeExtinguisherToolStripMenuItem.Name = "TypeExtinguisherToolStripMenuItem";
            this.TypeExtinguisherToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.TypeExtinguisherToolStripMenuItem.Text = "Типы огнетушителей";
            // 
            // TypeHoseToolStripMenuItem
            // 
            this.TypeHoseToolStripMenuItem.Name = "TypeHoseToolStripMenuItem";
            this.TypeHoseToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.TypeHoseToolStripMenuItem.Text = "Типы рукавов";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(244, 578);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(122, 34);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(404, 578);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(122, 34);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Изменить";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(565, 578);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(122, 34);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "Удалить";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.BtnRemove_Click);
            // 
            // TypeFireCabinetToolStripMenuItem
            // 
            this.TypeFireCabinetToolStripMenuItem.Name = "TypeFireCabinetToolStripMenuItem";
            this.TypeFireCabinetToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.TypeFireCabinetToolStripMenuItem.Text = "Типы шкафов";
            // 
            // DbTables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 636);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvTables);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DbTables";
            this.Text = "Таблицы базы данных";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTables)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTables;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem TablesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LocationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FireCabinetsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExtinguihersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HosesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HydrantesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TypeExtinguisherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TypeHoseToolStripMenuItem;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ToolStripMenuItem TypeFireCabinetToolStripMenuItem;
    }
}

