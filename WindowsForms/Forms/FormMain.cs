using BL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsForms
{
    /// <summary>
    /// Главная форма.
    /// </summary>
    public partial class FormMain : Form
    {
        private MyTreeView myTreeView;
        private Plan picContainer;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            MyInitializeComponent();

            ContextMenuGetter.TreeView = myTreeView;
            ContextMenuGetter.Plan = picContainer;

            Dialogs.Owner = this;
            Dialogs.TreeView = myTreeView;
            Dialogs.Plan = picContainer;

            using (var db = new BLContext())
            {
                db.Database.Initialize(false);
            }

            myTreeView.LoadFromContext();
            myTreeView.MouseClickSign += picContainer.LoadImage;
            ReportMenu.Click += (s, e) => new FormReport().Show(this);
            TypesEquipmentMenu.Click += (s, e) => new FormKinds().ShowDialog(this);
            StickersMenu.Click += (s, e) => new FormStickers().Show(this);
            SettingsMenu.Click += SettingsMenu_Click;
        }

        private void MyInitializeComponent()
        {
            this.myTreeView = new MyTreeView();
            this.picContainer = new Plan();
            this.Controls.Add(this.myTreeView);
            this.rightPanel.Controls.Add(this.picContainer);

            this.myTreeView.AllowDrop = true;
            this.myTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.myTreeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.myTreeView.Location = new System.Drawing.Point(0, 24);
            this.myTreeView.Name = "treeView";
            this.myTreeView.Size = new System.Drawing.Size(290, 720);
            this.myTreeView.TabIndex = 0;

            //this.picContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.picContainer.Location = new System.Drawing.Point(352, 221);
            this.picContainer.Name = "picContainer";
            this.picContainer.Size = new System.Drawing.Size(407, 209);
            this.picContainer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picContainer.TabIndex = 2;
            this.picContainer.TabStop = false;
        }

        /// <summary>
        /// Обработчик меню "Настройки".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsMenu_Click(object sender, EventArgs e)
        {
            var frm = new FormSettings();
            frm.ChangeSample += myTreeView.RenameNodesOfType;
            frm.ChangeSample += picContainer.DoRenameIcons;
            frm.ChangeIconSize += picContainer.DoCoerciveResize;
            frm.Show(this);
        }

        /// <summary>
        /// Обработчик события изменения размеров формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Resize(object sender, EventArgs e)
        {
            if (picContainer?.Image == null)
                return;
            picContainer.ResizeRelativePosition();
        }
    }
}
