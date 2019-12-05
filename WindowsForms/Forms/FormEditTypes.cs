﻿using BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class FormEditTypes : Form
    {
        Type saveType;
        public FormEditTypes()
        {
            InitializeComponent();
            FireCabinetsMenu.Image = ImageSettings.IconsImage(typeof(FireCabinet));
            ExtinguishersMenu.Image = ImageSettings.IconsImage(typeof(Extinguisher));
            HosesMenu.Image = ImageSettings.IconsImage(typeof(Hose));
        }

        private void FireCabinetsMenu_Click(object sender, EventArgs e) => LoadTypes(typeof(TypeFireCabinet));
        private void ExtinguishersMenu_Click(object sender, EventArgs e) => LoadTypes(typeof(TypeExtinguisher));
        private void HosesMenu_Click(object sender, EventArgs e) => LoadTypes(typeof(TypeHose));
        private void LoadTypes(Type type)
        {
            listView.Items.Clear();
            using (var ec = new EntityController())
            {
                foreach (ITypes t in ec.GetTable(type))
                {
                    var item = new ListViewItem(t.Name);
                    var subItem = new ListViewItem.ListViewSubItem(item, t.Manufacturer);
                    item.SubItems.Add(subItem);
                    item.Tag = ((EntityBase)t).GetSign();
                    listView.Items.Add(item);
                }
            }
            saveType = type;
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (saveType == null)
                return;
            var ec = new EntityController();
            var entity = ec.CreateEntity(saveType);

            var AddEssForm = new FormEditEntity(entity, ec);
            DialogResult result = AddEssForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;

            ec.AddNewEntity(entity);
            LoadTypes(saveType);
        }
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
                return;
            var sign = (EntitySign)listView.SelectedItems[0].Tag;

            var ec = new EntityController();
            var AddEssForm = new FormEditEntity(ec.GetEntity(sign), ec);
            DialogResult result = AddEssForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;

            ec.EditEntity(sign);
            LoadTypes(saveType);
        }
        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
                return;
            var sign = (EntitySign)listView.SelectedItems[0].Tag;

            var ec = new EntityController();
            ec.RemoveEntity(sign);
            LoadTypes(saveType);
        }
        private void listView_DoubleClick(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
                return;
            var item = listView.SelectedItems[0];
            var sign = (EntitySign)item.Tag;
            BtnEdit_Click(null, EventArgs.Empty);
        }
        private void btnImport_Click(object sender, EventArgs e)
        {
            using (var od = new OpenFileDialog())
            {
                od.Filter = "Текстовые файлы (*.txt,*.csv)|*.txt;*.csv|Все файлы (*.*)|*.*";
                od.RestoreDirectory = true;
                var pathDownLoads = Microsoft.Win32.Registry.GetValue(
                                    @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders",
                                    "{FDD39AD0-238F-46AF-ADB4-6C85480369C7}",
                                    string.Empty);
                od.InitialDirectory = Convert.ToString(pathDownLoads);
                if (od.ShowDialog() == DialogResult.OK)
                {
                    var fileStream = od.OpenFile();
                    AddTypesFromFile(fileStream);
                }
            }
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (saveType == null)
                return;
            using (var sd = new SaveFileDialog())
            {
                Stream fileStream;
                sd.Filter = "Файл csv (*.csv*)|*.csv*|Текстовый файл (*.txt)|*.txt|Все файлы (*.*)|*.*";
                sd.RestoreDirectory = true;
                sd.AddExtension = true;
                sd.DefaultExt = "csv";
                var pathDocuments = Microsoft.Win32.Registry.GetValue(
                                    @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders",
                                    "{FDD39AD0-238F-46AF-ADB4-6C85480369C7}",
                                    string.Empty);
                sd.InitialDirectory = Convert.ToString(pathDocuments);
                if (sd.ShowDialog() == DialogResult.OK)
                {
                    if ((fileStream = sd.OpenFile()) == null)
                        return;
                    WriteTypesToFile(fileStream);
                    fileStream.Close();
                }
            }
        }
        private void AddTypesFromFile(Stream fileStream)
        {
            using (StreamReader sr = new StreamReader(fileStream, Encoding.Default))
            {
                if (CheckingFile(sr) != 0)
                {
                    MessageBox.Show("Неккоректный файл");
                    return;
                }
                sr.BaseStream.Seek(0, SeekOrigin.Begin);

                string typeString = sr.ReadLine().Split(';')[0];
                var type = Type.GetType("BL." + typeString + ", BL");
                string line;
                string headersLine = sr.ReadLine();
                var headers = headersLine.Split(';');
                var properties = type.GetProperties().Where(p => headers.Contains(p.Name)).ToList();

                using (var ec = new EntityController())
                {
                    var table = ec.GetTable(type);
                    while ((line = sr.ReadLine()) != null)
                    {
                        var curr = ec.CreateEntity(type);
                        var values = line.Split(';');
                        for (int i = 0; i < properties.Count; i++)
                        {
                            if (properties[i].PropertyType == typeof(string))
                                properties[i].SetValue(curr, values[i]);
                            else if (properties[i].PropertyType == typeof(double))
                                properties[i].SetValue(curr, Double.Parse(values[i]));
                        }
                        if (!ec.ToList(table).Any(ent => ((ITypes)ent).EqualsValues(curr)))
                            table.Add(curr);
                    }
                    ec.SaveChanges();
                }
            }
            int CheckingFile(StreamReader sr)
            {
                Type[] typesEquipment = new Type[] { typeof(TypeFireCabinet), typeof(TypeExtinguisher), typeof(TypeHose) };
                string typeString = sr.ReadLine().Split(';')[0];
                var typeExist = typesEquipment.Select(t => t.Name).Any(s => s == typeString);
                if (!typeExist)
                    return 1;

                var type = Type.GetType("BL." + typeString + ", BL");
                var headers = sr.ReadLine().Split(';');
                var properties = type.GetProperties().Where(p => headers.Contains(p.Name)).ToArray();
                var propertiesName = properties.Select(p => p.Name);
                if (!(propertiesName.Union(headers).Count() == propertiesName.Count()))
                    return 2;

                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var values = line.Split(';');
                    if (values.Length != properties.Count())
                        return 3;
                    for (int i = 0; i < values.Length; i++)
                    {
                        if (properties[i].PropertyType == typeof(double))
                            if (!Double.TryParse(values[i], out double x))
                                return 4;
                    }
                }
                return 0;
            }
            //TODO: изза перестановок заголовков не будет правильно читаться
        }
        private void WriteTypesToFile(Stream fileStream)
        {
            using (StreamWriter sw = new StreamWriter(fileStream, Encoding.Default))
            {
                var properties = saveType.GetProperties().Where(p => p.GetCustomAttribute(typeof(ControlAttribute)) != null);
                var countProperties = properties.Count();
                sw.Write(saveType.Name);
                for (int i = 0; i < countProperties - 1; i++)
                    sw.Write(';');
                sw.WriteLine();
                foreach (var prop in properties)
                    sw.Write(prop.Name + ";");
                sw.WriteLine();
                using (var ec = new EntityController())
                {
                    var table = ec.GetTable(saveType);
                    foreach (var item in table)
                    {
                        foreach (var prop in properties)
                            sw.Write(prop.GetValue(item) + ";");
                        sw.WriteLine();
                    }
                }
            }
        }
    }
}
