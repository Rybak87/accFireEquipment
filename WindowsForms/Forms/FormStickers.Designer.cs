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
            this.btnOpenExcel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numColumns = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numRows = new System.Windows.Forms.NumericUpDown();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRows)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FireCabinetsMenu,
            this.ExtinguishersMenu});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(573, 25);
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
            this.chkWithoutStickers.CheckedChanged += new System.EventHandler(this.chkWithoutStickers_CheckedChanged);
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.FullRowSelect = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(13, 52);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(367, 398);
            this.listView.TabIndex = 2;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
            // 
            // btnOpenExcel
            // 
            this.btnOpenExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenExcel.Location = new System.Drawing.Point(444, 121);
            this.btnOpenExcel.Name = "btnOpenExcel";
            this.btnOpenExcel.Size = new System.Drawing.Size(120, 23);
            this.btnOpenExcel.TabIndex = 3;
            this.btnOpenExcel.Text = "Открыть в Excel";
            this.btnOpenExcel.UseVisualStyleBackColor = true;
            this.btnOpenExcel.Click += new System.EventHandler(this.btnOpenExcel_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(386, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Колонок";
            // 
            // numColumns
            // 
            this.numColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numColumns.Location = new System.Drawing.Point(444, 52);
            this.numColumns.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numColumns.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numColumns.Name = "numColumns";
            this.numColumns.Size = new System.Drawing.Size(120, 20);
            this.numColumns.TabIndex = 5;
            this.numColumns.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(386, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Строк";
            // 
            // numRows
            // 
            this.numRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numRows.Location = new System.Drawing.Point(444, 84);
            this.numRows.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numRows.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRows.Name = "numRows";
            this.numRows.Size = new System.Drawing.Size(120, 20);
            this.numRows.TabIndex = 5;
            this.numRows.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // FormStickers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 450);
            this.Controls.Add(this.numRows);
            this.Controls.Add(this.numColumns);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOpenExcel);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.chkWithoutStickers);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormStickers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormStickers";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRows)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton FireCabinetsMenu;
        private System.Windows.Forms.ToolStripButton ExtinguishersMenu;
        private System.Windows.Forms.CheckBox chkWithoutStickers;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Button btnOpenExcel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numColumns;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numRows;
    }
}