﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BL.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.3.0.0")]
    public sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("20")]
        public int RatioIconSize {
            get {
                return ((int)(this["RatioIconSize"]));
            }
            set {
                this["RatioIconSize"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ПК-#L.#N")]
        public string SampleNameFireCabinets {
            get {
                return ((string)(this["SampleNameFireCabinets"]));
            }
            set {
                this["SampleNameFireCabinets"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("#L.#F/#N")]
        public string SampleNameExtinguishers {
            get {
                return ((string)(this["SampleNameExtinguishers"]));
            }
            set {
                this["SampleNameExtinguishers"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Рукав №#N")]
        public string SampleNameHoses {
            get {
                return ((string)(this["SampleNameHoses"]));
            }
            set {
                this["SampleNameHoses"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ПК-#L.#N")]
        public string DefaultSampleNameFireCabinets {
            get {
                return ((string)(this["DefaultSampleNameFireCabinets"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("#L.#F/#N")]
        public string DefaultSampleNameExtinguishers {
            get {
                return ((string)(this["DefaultSampleNameExtinguishers"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Рукав №#N")]
        public string DefaultSampleNameHoses {
            get {
                return ((string)(this["DefaultSampleNameHoses"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Пожарный кран №#N")]
        public string SampleNameHydrants {
            get {
                return ((string)(this["SampleNameHydrants"]));
            }
            set {
                this["SampleNameHydrants"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Пожарный кран №#N")]
        public string DefaultSampleNameHydrants {
            get {
                return ((string)(this["DefaultSampleNameHydrants"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool UseTime {
            get {
                return ((bool)(this["UseTime"]));
            }
            set {
                this["UseTime"] = value;
            }
        }
    }
}
