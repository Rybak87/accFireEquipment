using BL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class FormEditEntities : Form
    {
        EntityController ec;
        Type typeEntity;
        List<Control> requiredControls = new List<Control>();
        public NumericUpDown num;
        public ComboBox cbx;
        public PropertyInfo pi;

        public FormEditEntities(Type typeEntity, EntityController ec)
        {
            InitializeComponent();
            this.typeEntity = typeEntity;
            this.ec = ec;
            CreateControls(typeEntity);
        }

        private List<(PropertyInfo, ControlAttribute, string)> GetProperties2(Type entityType)
        {
            var result = new List<(PropertyInfo, ControlAttribute, string)>();
            foreach (PropertyInfo prop in entityType.GetProperties())
            {
                var controlAttr = GetControlAttribute(prop);
                if (controlAttr == null)
                    continue;

                var nameAttr = GetColumnAttribute(prop)?.Name;
                result.Add((prop, controlAttr, nameAttr));
            }
            return result;

            ControlAttribute GetControlAttribute(PropertyInfo pi)
            {
                foreach (var item in pi.GetCustomAttributes())
                    if (item.GetType() == typeof(ControlAttribute))
                        return (ControlAttribute)item;
                return null;
            }
            ColumnAttribute GetColumnAttribute(PropertyInfo pi)
            {
                foreach (var item in pi.GetCustomAttributes())
                    if (item.GetType() == typeof(ColumnAttribute))
                        return (ColumnAttribute)item;
                return null;
            }
        }
        private void CreateControls(Type typeEntity)
        {
            var propType = GetProperties2(typeEntity).FirstOrDefault(vt => vt.Item1.PropertyType.GetInterface("ITypes")!=null);

            var lbl = new Label
            {
                Text = "Количество",
                Location = new Point(25, 25),
                Size = new Size(175, 25)
            };
            Controls.Add(lbl);

            num = new NumericUpDown
            {
                Location = new Point(200, 25),
                Size = new Size(150, 25),
                Maximum = 50
            };
            Controls.Add(num);

            if (propType.Item1 == null)
                return;
            if (propType.Item3 == null)
                propType.Item3 = propType.Item1.Name;

            var lbl2 = new Label
            {
                Text = propType.Item3,
                Location = new Point(25, 50),
                Size = new Size(175, 25)
            };

            cbx = new ComboBox
            {
                Location = new Point(200, 50),
                Size = new Size(150, 25),
                Sorted = true
            };
            var parentEntity = ec.GetTableList(propType.Item1.PropertyType);
            pi = propType.Item1;
            foreach (var item2 in parentEntity)
                cbx.Items.Add(item2);

            Controls.Add(cbx);
            cbx.SelectedIndex = -1;

            this.Height = 175;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            foreach (var cntrl in requiredControls)
            {
                switch (cntrl.GetType().Name)
                {

                    case "ComboBox":
                        if (((ComboBox)cntrl).SelectedIndex == -1)
                        {
                            AbortDialogResult();
                            return;
                        }
                        break;
                    case "NumericUpDown":
                        if (((NumericUpDown)cntrl).Value == 0)
                        {
                            AbortDialogResult();
                            return;
                        }
                        break;
                    case "TextBox":
                        if (((TextBox)cntrl).Text == "")
                        {
                            AbortDialogResult();
                            return;
                        }
                        break;
                }
            }
            void AbortDialogResult()
            {
                DialogResult = DialogResult.None;
                MessageBox.Show("Необходимо заполнить все поля");
            }
        }
        private void FormEditEntity_Load(object sender, EventArgs e)
        {

        }
    }
}
