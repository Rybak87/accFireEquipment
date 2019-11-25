using BL;

namespace WindowsForms
{
    partial class FormMain
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьПроектToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditDatabaseMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AddMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AddTypeFireCabinetContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AddTypeExtinguisherContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AddTypeHoseContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TypesEquipmentMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.RechargeExtinguishersReportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FullReportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.FireCabinetsReportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ExtinguishersReportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HosesReportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HydrantsReportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.нормативныеАктыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuFireCabinet = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AddExtinguisherContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AddHoseContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AddHydrantContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.EditContextMenu2 = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteContextMenu2 = new System.Windows.Forms.ToolStripMenuItem();
            this.EditContextMenu1 = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuProject = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddLocationContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AddLocationsContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuLocation = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddFireCabinetContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteContextMenu1 = new System.Windows.Forms.ToolStripMenuItem();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.contextMenuEquipment = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EditContextMenu3 = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteContextMenu3 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.contextMenuFireCabinet.SuspendLayout();
            this.contextMenuProject.SuspendLayout();
            this.contextMenuLocation.SuspendLayout();
            this.contextMenuEquipment.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.нормативныеАктыToolStripMenuItem,
            this.настройкиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1528, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьПроектToolStripMenuItem,
            this.EditDatabaseMenu,
            this.AddMenu,
            this.TypesEquipmentMenu});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(77, 24);
            this.toolStripMenuItem1.Text = "Главная";
            // 
            // открытьПроектToolStripMenuItem
            // 
            this.открытьПроектToolStripMenuItem.Name = "открытьПроектToolStripMenuItem";
            this.открытьПроектToolStripMenuItem.Size = new System.Drawing.Size(250, 26);
            this.открытьПроектToolStripMenuItem.Text = "Открыть проект...";
            // 
            // EditDatabaseMenu
            // 
            this.EditDatabaseMenu.Name = "EditDatabaseMenu";
            this.EditDatabaseMenu.Size = new System.Drawing.Size(250, 26);
            this.EditDatabaseMenu.Text = "Редактировать таблицы";
            // 
            // AddMenu
            // 
            this.AddMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddTypeFireCabinetContextMenu,
            this.AddTypeExtinguisherContextMenu,
            this.AddTypeHoseContextMenu});
            this.AddMenu.Name = "AddMenu";
            this.AddMenu.Size = new System.Drawing.Size(250, 26);
            this.AddMenu.Text = "Добавить";
            // 
            // AddTypeFireCabinetContextMenu
            // 
            this.AddTypeFireCabinetContextMenu.Name = "AddTypeFireCabinetContextMenu";
            this.AddTypeFireCabinetContextMenu.Size = new System.Drawing.Size(210, 26);
            this.AddTypeFireCabinetContextMenu.Text = "Тип шкафа";
            // 
            // AddTypeExtinguisherContextMenu
            // 
            this.AddTypeExtinguisherContextMenu.Name = "AddTypeExtinguisherContextMenu";
            this.AddTypeExtinguisherContextMenu.Size = new System.Drawing.Size(210, 26);
            this.AddTypeExtinguisherContextMenu.Text = "Тип огнетушителя";
            // 
            // AddTypeHoseContextMenu
            // 
            this.AddTypeHoseContextMenu.Name = "AddTypeHoseContextMenu";
            this.AddTypeHoseContextMenu.Size = new System.Drawing.Size(210, 26);
            this.AddTypeHoseContextMenu.Text = "Тип рукава";
            // 
            // TypesEquipmentMenu
            // 
            this.TypesEquipmentMenu.Name = "TypesEquipmentMenu";
            this.TypesEquipmentMenu.Size = new System.Drawing.Size(250, 26);
            this.TypesEquipmentMenu.Text = "Типы оснащения";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RechargeExtinguishersReportMenu,
            this.отчетыToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(71, 24);
            this.toolStripMenuItem2.Text = "Отчеты";
            // 
            // RechargeExtinguishersReportMenu
            // 
            this.RechargeExtinguishersReportMenu.Name = "RechargeExtinguishersReportMenu";
            this.RechargeExtinguishersReportMenu.Size = new System.Drawing.Size(284, 26);
            this.RechargeExtinguishersReportMenu.Text = "Перезарядка огнетушителей";
            // 
            // отчетыToolStripMenuItem
            // 
            this.отчетыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FullReportMenu,
            this.FireCabinetsReportMenu,
            this.ExtinguishersReportMenu,
            this.HosesReportMenu,
            this.HydrantsReportMenu});
            this.отчетыToolStripMenuItem.Name = "отчетыToolStripMenuItem";
            this.отчетыToolStripMenuItem.Size = new System.Drawing.Size(284, 26);
            this.отчетыToolStripMenuItem.Text = "Отчеты";
            // 
            // FullReportMenu
            // 
            this.FullReportMenu.Name = "FullReportMenu";
            this.FullReportMenu.Size = new System.Drawing.Size(216, 26);
            this.FullReportMenu.Text = "Полный отчет";
            // 
            // FireCabinetsReportMenu
            // 
            this.FireCabinetsReportMenu.Name = "FireCabinetsReportMenu";
            this.FireCabinetsReportMenu.Size = new System.Drawing.Size(216, 26);
            this.FireCabinetsReportMenu.Text = "Шкафы";
            this.FireCabinetsReportMenu.Click += new System.EventHandler(this.FireCabinetsReportMenu_Click);
            // 
            // ExtinguishersReportMenu
            // 
            this.ExtinguishersReportMenu.Name = "ExtinguishersReportMenu";
            this.ExtinguishersReportMenu.Size = new System.Drawing.Size(216, 26);
            this.ExtinguishersReportMenu.Text = "Огнетушители";
            // 
            // HosesReportMenu
            // 
            this.HosesReportMenu.Name = "HosesReportMenu";
            this.HosesReportMenu.Size = new System.Drawing.Size(216, 26);
            this.HosesReportMenu.Text = "Рукава";
            // 
            // HydrantsReportMenu
            // 
            this.HydrantsReportMenu.Name = "HydrantsReportMenu";
            this.HydrantsReportMenu.Size = new System.Drawing.Size(216, 26);
            this.HydrantsReportMenu.Text = "Гидранты";
            // 
            // нормативныеАктыToolStripMenuItem
            // 
            this.нормативныеАктыToolStripMenuItem.Name = "нормативныеАктыToolStripMenuItem";
            this.нормативныеАктыToolStripMenuItem.Size = new System.Drawing.Size(156, 24);
            this.нормативныеАктыToolStripMenuItem.Text = "Нормативные акты";
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(96, 24);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // contextMenuFireCabinet
            // 
            this.contextMenuFireCabinet.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuFireCabinet.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddContextMenu,
            this.EditContextMenu2,
            this.DeleteContextMenu2});
            this.contextMenuFireCabinet.Name = "contextMenuEquipment";
            this.contextMenuFireCabinet.Size = new System.Drawing.Size(181, 76);
            // 
            // AddContextMenu
            // 
            this.AddContextMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddExtinguisherContextMenu,
            this.AddHoseContextMenu,
            this.AddHydrantContextMenu});
            this.AddContextMenu.Name = "AddContextMenu";
            this.AddContextMenu.Size = new System.Drawing.Size(180, 24);
            this.AddContextMenu.Text = "Добавить";
            // 
            // AddExtinguisherContextMenu
            // 
            this.AddExtinguisherContextMenu.Name = "AddExtinguisherContextMenu";
            this.AddExtinguisherContextMenu.Size = new System.Drawing.Size(198, 26);
            this.AddExtinguisherContextMenu.Text = "Огнетушитель";
            // 
            // AddHoseContextMenu
            // 
            this.AddHoseContextMenu.Name = "AddHoseContextMenu";
            this.AddHoseContextMenu.Size = new System.Drawing.Size(198, 26);
            this.AddHoseContextMenu.Text = "Рукав";
            // 
            // AddHydrantContextMenu
            // 
            this.AddHydrantContextMenu.Name = "AddHydrantContextMenu";
            this.AddHydrantContextMenu.Size = new System.Drawing.Size(198, 26);
            this.AddHydrantContextMenu.Text = "Пожарный кран";
            // 
            // EditContextMenu2
            // 
            this.EditContextMenu2.Name = "EditContextMenu2";
            this.EditContextMenu2.Size = new System.Drawing.Size(180, 24);
            this.EditContextMenu2.Text = "Редактировать";
            // 
            // DeleteContextMenu2
            // 
            this.DeleteContextMenu2.Name = "DeleteContextMenu2";
            this.DeleteContextMenu2.Size = new System.Drawing.Size(180, 24);
            this.DeleteContextMenu2.Text = "Удалить";
            // 
            // EditContextMenu1
            // 
            this.EditContextMenu1.Name = "EditContextMenu1";
            this.EditContextMenu1.Size = new System.Drawing.Size(265, 24);
            this.EditContextMenu1.Text = "Редактировать";
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Location.ico");
            this.imageList.Images.SetKeyName(1, "FireCabinet.ico");
            this.imageList.Images.SetKeyName(2, "Extinguisher.ico");
            this.imageList.Images.SetKeyName(3, "Hose.ico");
            this.imageList.Images.SetKeyName(4, "Hydrant.ico");
            // 
            // contextMenuProject
            // 
            this.contextMenuProject.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuProject.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddLocationContextMenu,
            this.AddLocationsContextMenu});
            this.contextMenuProject.Name = "contextMenuLocations";
            this.contextMenuProject.Size = new System.Drawing.Size(212, 52);
            // 
            // AddLocationContextMenu
            // 
            this.AddLocationContextMenu.Name = "AddLocationContextMenu";
            this.AddLocationContextMenu.Size = new System.Drawing.Size(211, 24);
            this.AddLocationContextMenu.Text = "Добавить локацию";
            // 
            // AddLocationsContextMenu
            // 
            this.AddLocationsContextMenu.Name = "AddLocationsContextMenu";
            this.AddLocationsContextMenu.Size = new System.Drawing.Size(211, 24);
            this.AddLocationsContextMenu.Text = "Добавить локации";
            // 
            // contextMenuLocation
            // 
            this.contextMenuLocation.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuLocation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddFireCabinetContextMenu,
            this.EditContextMenu1,
            this.DeleteContextMenu1});
            this.contextMenuLocation.Name = "contextMenuFireCabinets";
            this.contextMenuLocation.Size = new System.Drawing.Size(266, 76);
            // 
            // AddFireCabinetContextMenu
            // 
            this.AddFireCabinetContextMenu.Name = "AddFireCabinetContextMenu";
            this.AddFireCabinetContextMenu.Size = new System.Drawing.Size(265, 24);
            this.AddFireCabinetContextMenu.Text = "Добавить пожарный шкаф";
            // 
            // DeleteContextMenu1
            // 
            this.DeleteContextMenu1.Name = "DeleteContextMenu1";
            this.DeleteContextMenu1.Size = new System.Drawing.Size(265, 24);
            this.DeleteContextMenu1.Text = "Удалить";
            // 
            // rightPanel
            // 
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(0, 28);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(1528, 720);
            this.rightPanel.TabIndex = 4;
            // 
            // contextMenuEquipment
            // 
            this.contextMenuEquipment.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuEquipment.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditContextMenu3,
            this.DeleteContextMenu3});
            this.contextMenuEquipment.Name = "contextMenuEquipment";
            this.contextMenuEquipment.Size = new System.Drawing.Size(181, 52);
            // 
            // EditContextMenu3
            // 
            this.EditContextMenu3.Name = "EditContextMenu3";
            this.EditContextMenu3.Size = new System.Drawing.Size(180, 24);
            this.EditContextMenu3.Text = "Редактировать";
            // 
            // DeleteContextMenu3
            // 
            this.DeleteContextMenu3.Name = "DeleteContextMenu3";
            this.DeleteContextMenu3.Size = new System.Drawing.Size(180, 24);
            this.DeleteContextMenu3.Text = "Удалить";
            // 
            // FormMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1528, 748);
            this.Controls.Add(this.rightPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuFireCabinet.ResumeLayout(false);
            this.contextMenuProject.ResumeLayout(false);
            this.contextMenuLocation.ResumeLayout(false);
            this.contextMenuEquipment.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private void MyInitializeComponent()
        {
            this.picContainer = new PictureContainer(/*dictMenu*/);
            this.myTreeView = new MyTreeView(dictMenu);
            this.Controls.Add(this.myTreeView);
            this.rightPanel.Controls.Add(this.picContainer);

            this.picContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.picContainer.Location = new System.Drawing.Point(352, 221);
            this.picContainer.Name = "picContainer";
            this.picContainer.Size = new System.Drawing.Size(407, 209);
            this.picContainer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picContainer.TabIndex = 2;
            this.picContainer.TabStop = false;

            this.myTreeView.AllowDrop = true;
            this.myTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.myTreeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.myTreeView.Location = new System.Drawing.Point(0, 28);
            this.myTreeView.Name = "treeView";
            this.myTreeView.Size = new System.Drawing.Size(290, 720);
            this.myTreeView.TabIndex = 0;

            AddLocationContextMenu.Tag = typeof(Location);
            AddFireCabinetContextMenu.Tag = typeof(FireCabinet);
            AddExtinguisherContextMenu.Tag = typeof(Extinguisher);
            AddHoseContextMenu.Tag = typeof(Hose);
            AddHydrantContextMenu.Tag = typeof(Hydrant);

            AddLocationContextMenu.Click += MenuAdd_MouseClick;
            AddFireCabinetContextMenu.Click += MenuAdd_MouseClick;
            AddExtinguisherContextMenu.Click += MenuAdd_MouseClick;
            AddHoseContextMenu.Click += MenuAdd_MouseClick;
            AddHydrantContextMenu.Click += MenuAdd_MouseClick;

            EditContextMenu1.Click += MenuEdit_MouseClick;
            EditContextMenu2.Click += MenuEdit_MouseClick;
            EditContextMenu3.Click += MenuEdit_MouseClick;
            DeleteContextMenu1.Click += MenuRemove_MouseClick;
            DeleteContextMenu2.Click += MenuRemove_MouseClick;
            DeleteContextMenu3.Click += MenuRemove_MouseClick;
        }

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem EditDatabaseMenu;
        private System.Windows.Forms.ContextMenuStrip contextMenuFireCabinet;
        private System.Windows.Forms.ToolStripMenuItem AddContextMenu;
        private System.Windows.Forms.ToolStripMenuItem AddExtinguisherContextMenu;
        private System.Windows.Forms.ToolStripMenuItem AddHoseContextMenu;
        private System.Windows.Forms.ToolStripMenuItem AddHydrantContextMenu;
        private System.Windows.Forms.ContextMenuStrip contextMenuProject;
        private System.Windows.Forms.ContextMenuStrip contextMenuLocation;
        private System.Windows.Forms.ToolStripMenuItem AddLocationContextMenu;
        private System.Windows.Forms.ToolStripMenuItem AddFireCabinetContextMenu;
        private System.Windows.Forms.ToolStripMenuItem EditContextMenu1;
        private System.Windows.Forms.ToolStripMenuItem DeleteContextMenu1;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.ImageList imageList;

        public MyTreeView myTreeView;
        public PictureContainer picContainer;
        private System.Windows.Forms.ToolStripMenuItem EditContextMenu2;
        private System.Windows.Forms.ToolStripMenuItem DeleteContextMenu2;
        private System.Windows.Forms.ContextMenuStrip contextMenuEquipment;
        private System.Windows.Forms.ToolStripMenuItem EditContextMenu3;
        private System.Windows.Forms.ToolStripMenuItem DeleteContextMenu3;
        private System.Windows.Forms.ToolStripMenuItem открытьПроектToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem RechargeExtinguishersReportMenu;
        private System.Windows.Forms.ToolStripMenuItem отчетыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExtinguishersReportMenu;
        private System.Windows.Forms.ToolStripMenuItem FireCabinetsReportMenu;
        private System.Windows.Forms.ToolStripMenuItem HosesReportMenu;
        private System.Windows.Forms.ToolStripMenuItem HydrantsReportMenu;
        private System.Windows.Forms.ToolStripMenuItem нормативныеАктыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddLocationsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem AddMenu;
        private System.Windows.Forms.ToolStripMenuItem AddTypeFireCabinetContextMenu;
        private System.Windows.Forms.ToolStripMenuItem AddTypeExtinguisherContextMenu;
        private System.Windows.Forms.ToolStripMenuItem AddTypeHoseContextMenu;
        private System.Windows.Forms.ToolStripMenuItem TypesEquipmentMenu;
        private System.Windows.Forms.ToolStripMenuItem FullReportMenu;
    }
}