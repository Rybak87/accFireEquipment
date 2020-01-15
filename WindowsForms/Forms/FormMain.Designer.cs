using BL;
using System;

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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TypesEquipmentMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ReportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.StickersMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.ReportMenu,
            this.StickersMenu,
            this.SettingsMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1222, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenProjectToolStripMenuItem,
            this.TypesEquipmentMenu});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(63, 20);
            this.toolStripMenuItem1.Text = "Главная";
            // 
            // OpenProjectToolStripMenuItem
            // 
            this.OpenProjectToolStripMenuItem.Name = "OpenProjectToolStripMenuItem";
            this.OpenProjectToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.OpenProjectToolStripMenuItem.Text = "Открыть проект...";
            // 
            // TypesEquipmentMenu
            // 
            this.TypesEquipmentMenu.Name = "TypesEquipmentMenu";
            this.TypesEquipmentMenu.Size = new System.Drawing.Size(171, 22);
            this.TypesEquipmentMenu.Text = "Типы оснащения";
            // 
            // ReportMenu
            // 
            this.ReportMenu.Name = "ReportMenu";
            this.ReportMenu.Size = new System.Drawing.Size(60, 20);
            this.ReportMenu.Text = "Отчеты";
            // 
            // StickersMenu
            // 
            this.StickersMenu.Name = "StickersMenu";
            this.StickersMenu.Size = new System.Drawing.Size(73, 20);
            this.StickersMenu.Text = "Наклейки";
            // 
            // SettingsMenu
            // 
            this.SettingsMenu.Name = "SettingsMenu";
            this.SettingsMenu.Size = new System.Drawing.Size(79, 20);
            this.SettingsMenu.Text = "Настройки";
            // 
            // rightPanel
            // 
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(0, 24);
            this.rightPanel.Margin = new System.Windows.Forms.Padding(2);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(1222, 574);
            this.rightPanel.TabIndex = 4;
            // 
            // FormMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1222, 598);
            this.Controls.Add(this.rightPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(642, 486);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

            //this.rightPanel.Controls.Add(this.picContainer);
        }

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.ToolStripMenuItem OpenProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ReportMenu;
        private System.Windows.Forms.ToolStripMenuItem SettingsMenu;
        private System.Windows.Forms.ToolStripMenuItem TypesEquipmentMenu;
        private System.Windows.Forms.ToolStripMenuItem StickersMenu;
    }
}