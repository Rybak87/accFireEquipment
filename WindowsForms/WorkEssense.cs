//using BL;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Windows.Forms;

//namespace WindowsForms
//{
//    public partial class WorkEssense : Form
//    {
//        EssenseControl essControl;
//        public Dictionary<PropertyInfo, Control> ControlsData { get; private set; }
//        public WorkEssense(EssenseControl essControl)
//        {
//            InitializeComponent();
//            this.essControl = essControl;
//        }
//        public WorkEssense()
//        {
//            InitializeComponent();
//        }
//        private void AddEssense_Load(object sender, EventArgs e)
//        {
//            ControlsData = CreateControls(essControl.EssenseType);
//            GetPropEssense(ControlsData);
//        }
//        private void GetPropEssense(Dictionary<PropertyInfo, Control> controlsData)
//        {
//            foreach (var item in controlsData)
//            {
//                switch (GetPropAttribute(item.Key).Control)
//                {
//                    case "TextBox":
//                        {
//                            item.Value.Text = (string)item.Key.GetValue(essControl.essense);
//                            break;
//                        }
//                    case "ComboBox":
//                        {
//                            ((ComboBox)item.Value).SelectedItem = item.Key.GetValue(essControl.essense);
//                            break;
//                        }
//                    case "CheckBox":
//                        {
//                            ((CheckBox)item.Value).Checked = (bool)item.Key.GetValue(essControl.essense);
//                            break;
//                        }
//                    case "NumericUpDown":
//                        {
//                            ((NumericUpDown)item.Value).Value = (int)item.Key.GetValue(essControl.essense);
//                            break;
//                        }
//                    case "NumericUpDownDecimal":
//                        {
//                            ((NumericUpDown)item.Value).Value = (decimal)((double)item.Key.GetValue(essControl.essense));
//                            break;
//                        }
//                    case "DateTimePicker":
//                        {
//                            ((DateTimePicker)item.Value).Value = (DateTime)item.Key.GetValue(essControl.essense);
//                            break;
//                        }
//                    case "Image":
//                        {
//                            break;
//                        }
//                    case null:
//                        break;
//                    default:
//                        throw new ArgumentException();
//                }
//            }
//        }
//        private Dictionary<PropertyInfo, Control> CreateControls(Type myType)
//        {
//            var propControlsData = new Dictionary<PropertyInfo, Control>();
//            int yPosControl = 1;
//            foreach (PropertyInfo prop in myType.GetProperties())
//            {
//                PropAttribute attr = GetPropAttribute(prop);
//                if (attr != null)
//                {
//                    var lbl = new Label();
//                    lbl.Text = prop.Name;
//                    lbl.Location = new Point(25, 25 * yPosControl);
//                    Controls.Add(lbl);

//                    if (attr.IsRequired)
//                    {
//                        lbl = new Label();
//                        lbl.Text = "Обязательно заполнить";
//                        lbl.AutoSize = true;
//                        lbl.Location = new Point(400, 25 * yPosControl);
//                        Controls.Add(lbl);
//                    }

//                    switch (attr.Control)
//                    {
//                        case "TextBox":
//                            {
//                                var cntrl = new TextBox();
//                                cntrl.Location = new Point(200, 25 * yPosControl);
//                                propControlsData.Add(prop, cntrl);
//                                Controls.Add(cntrl);
//                                break;
//                            }
//                        case "ComboBox":
//                            {
//                                var cntrl = new ComboBox();
//                                cntrl.Location = new Point(200, 25 * yPosControl);
//                                cntrl.DataSource = essControl.GetListSourse(attr.NameClass);
//                                propControlsData.Add(prop, cntrl);
//                                Controls.Add(cntrl);
//                                break;
//                            }
//                        case "CheckBox":
//                            {
//                                var cntrl = new CheckBox();
//                                cntrl.Location = new Point(200, 25 * yPosControl);
//                                propControlsData.Add(prop, cntrl);
//                                Controls.Add(cntrl);
//                                break;
//                            }
//                        case "NumericUpDown":
//                            {
//                                var cntrl = new NumericUpDown();
//                                cntrl.Location = new Point(200, 25 * yPosControl);
//                                propControlsData.Add(prop, cntrl);
//                                Controls.Add(cntrl);
//                                break;
//                            }
//                        case "NumericUpDownDecimal":
//                            {
//                                var cntrl = new NumericUpDown();
//                                cntrl.DecimalPlaces = 2;
//                                cntrl.Location = new Point(200, 25 * yPosControl);
//                                propControlsData.Add(prop, cntrl);
//                                Controls.Add(cntrl);
//                                break;
//                            }
//                        case "DateTimePicker":
//                            {
//                                var cntrl = new DateTimePicker();
//                                cntrl.Location = new Point(200, 25 * yPosControl);
//                                propControlsData.Add(prop, cntrl);
//                                Controls.Add(cntrl);
//                                break;
//                            }
//                        case "Image":
//                            {
//                                var cntrl = new Button();
//                                cntrl.Location = new Point(200, 25 * yPosControl);
//                                cntrl.Text = "...";
//                                cntrl.Click += new EventHandler((s, e) => NewFileDialog(s, e, essControl, prop));
//                                Controls.Add(cntrl);
//                                break;
//                            }
//                        case null:
//                            break;
//                        default:
//                            throw new ArgumentException();
//                    }
//                    yPosControl++;
//                }
//            }
//            return propControlsData;
//        }

//        //private IEnumerable<PropAttribute> GetPropAttributes(Type myType)
//        //{
//        //    //foreach (PropertyInfo prop in myType.GetProperties())
//        //    //{
//        //    //    foreach (var item in prop.GetCustomAttributes())
//        //    //    {
//        //    //        if (item.GetType() == typeof(PropAttribute))
//        //    //        {
//        //    //            yield return (PropAttribute)item;
//        //    //        }
//        //    //    }
//        //    //}
//        //    return (IEnumerable<PropAttribute>)myType.GetProperties().Select(p => p.GetCustomAttributes()).Where(ca => ca.GetType() == typeof(PropAttribute));
//        //}

//        private void NewFileDialog(object sender, EventArgs e, EssenseControl essControl, PropertyInfo prop)
//        {
//            OpenFileDialog dialog = new OpenFileDialog();
//            if (dialog.ShowDialog() == DialogResult.OK)
//            {
//                var data = File.ReadAllBytes(dialog.FileName);
//                prop.SetValue(essControl.essense, data);
//            }
//        }

//        private PropAttribute GetPropAttribute(PropertyInfo pi)
//        {
//            foreach (var item in pi.GetCustomAttributes())
//            {
//                if (item.GetType() == typeof(PropAttribute))
//                {
//                    return (PropAttribute)item;
//                }
//            }
//            return null;
//        }
//    }
//}
