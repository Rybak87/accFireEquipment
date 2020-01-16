using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sett = BL.Properties.Settings;

namespace BL
{
    public static class GetterOfType
    {
        private static Dictionary<Type, DataOfType> dictOfType = new Dictionary<Type, DataOfType>()
        {
            [typeof(FireCabinet)] = new DataOfType("LF", "Пожарный шкаф", "Пожарные шкафы"),
            [typeof(Extinguisher)] = new DataOfType("LFE", "Огнетушитель", "Огнетушители"),
            [typeof(Hose)] = new DataOfType("LFH", "Рукав", "Рукава"),
            [typeof(Hydrant)] = new DataOfType("LFD", "Пожарный кран", "Пожарные краны"),
        };
        static GetterOfType()
        {

        }
        /// <summary>
        /// Символы для проверки корректности шаблона именования.
        /// </summary>
        public static string GetSampleChars(Type type) => dictOfType[type].sampleChars;
        public static string GetTraslateOne(Type type) => dictOfType[type].traslateOne;
        public static string GetTraslateMany(Type type) => dictOfType[type].traslateMany;
        public static void SetSample(Type type, string str)
        {
            switch (type.Name)
            {
                case "FireCabinet":
                    {
                        Sett.Default.SampleNameFireCabinets = str;
                        break;
                    }
                case "Extinguisher":
                    {
                        Sett.Default.SampleNameExtinguishers = str;
                        break;
                    }
                case "Hose":
                    {
                        Sett.Default.SampleNameHoses = str;
                        break;
                    }
                case "Hydrant":
                    {
                        Sett.Default.SampleNameHydrants = str;
                        break;
                    }
            }
        }
        public static string GetSample(Type type)
        {
            switch (type.Name)
            {
                case "FireCabinet":
                    {
                        return Sett.Default.SampleNameFireCabinets;
                    }
                case "Extinguisher":
                    {
                        return Sett.Default.SampleNameExtinguishers;
                    }
                case "Hose":
                    {
                        return Sett.Default.SampleNameHoses;
                    }
                case "Hydrant":
                    {
                        return Sett.Default.SampleNameHydrants;
                    }
            }
            return string.Empty;
        }
        public static string GetDefaultSampleName(Type type)
        {
            switch (type.Name)
            {
                case "FireCabinet":
                    {
                        return Sett.Default.DefaultSampleNameFireCabinets;
                    }
                case "Extinguisher":
                    {
                        return Sett.Default.DefaultSampleNameExtinguishers;
                    }
                case "Hose":
                    {
                        return Sett.Default.DefaultSampleNameHoses;
                    }
                case "Hydrant":
                    {
                        return Sett.Default.DefaultSampleNameHydrants;
                    }
            }
            return string.Empty;
        }
    }

    public class DataOfType
    {
        /// <summary>
        /// Символы для проверки корректности шаблона именования.
        /// </summary>
        public string sampleChars;
        public string traslateOne;
        public string traslateMany;

        public DataOfType(string sampleChars, string traslateOne, string traslateMany)
        {
            this.sampleChars = sampleChars;
            this.traslateOne = traslateOne;
            this.traslateMany = traslateMany;
        }
    }
}
