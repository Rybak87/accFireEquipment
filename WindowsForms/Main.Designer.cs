namespace WindowsForms
{
    partial class Main
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
            this.treeViewDB = new System.Windows.Forms.TreeView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.редактироватьТаблицыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // treeViewDB
            // 
            this.treeViewDB.Location = new System.Drawing.Point(12, 84);
            this.treeViewDB.Name = "treeViewDB";
            this.treeViewDB.Size = new System.Drawing.Size(290, 665);
            this.treeViewDB.TabIndex = 0;
            this.treeViewDB.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TreeViewDB_MouseDoubleClick);
            this.treeViewDB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TreeViewDB_MouseDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1414, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.редактироватьТаблицыToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(154, 24);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            // 
            // редактироватьТаблицыToolStripMenuItem
            // 
            this.редактироватьТаблицыToolStripMenuItem.Name = "редактироватьТаблицыToolStripMenuItem";
            this.редактироватьТаблицыToolStripMenuItem.Size = new System.Drawing.Size(250, 26);
            this.редактироватьТаблицыToolStripMenuItem.Text = "Редактировать таблицы";
            this.редактироватьТаблицыToolStripMenuItem.Click += new System.EventHandler(this.РедактироватьТаблицыToolStripMenuItem_Click);
            // 
            // picBox
            // 
            this.picBox.Location = new System.Drawing.Point(308, 84);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(1076, 665);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBox.TabIndex = 2;
            this.picBox.TabStop = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1414, 761);
            this.Controls.Add(this.picBox);
            this.Controls.Add(this.treeViewDB);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "Main";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewDB;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem редактироватьТаблицыToolStripMenuItem;
        private System.Windows.Forms.PictureBox picBox;
    }
}