using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sett = BL.Properties.Settings;

namespace BL
{
    /// <summary>
    /// Данные типа.
    /// </summary>
    public static class GetterOfType
    {
        private static Dictionary<Type, DataOfType> dictOfType = new Dictionary<Type, DataOfType>()
        {
            [typeof(FireCabinet)] = new DataOfType("LF", "Пожарный шкаф", "Пожарные шкафы",
                () => Sett.Default.SampleNameFireCabinets,
                (str) => Sett.Default.SampleNameFireCabinets = str,
                () => Sett.Default.DefaultSampleNameFireCabinets),

            [typeof(Extinguisher)] = new DataOfType("LFE", "Огнетушитель", "Огнетушители",
                () => Sett.Default.SampleNameExtinguishers,
                (str) => Sett.Default.SampleNameExtinguishers = str,
                () => Sett.Default.DefaultSampleNameExtinguishers),

            [typeof(Hose)] = new DataOfType("LFH", "Рукав", "Рукава",
                () => Sett.Default.SampleNameHoses,
                (str) => Sett.Default.SampleNameHoses = str,
                () => Sett.Default.DefaultSampleNameHoses),

            [typeof(Hydrant)] = new DataOfType("LFD", "Пожарный кран", "Пожарные краны",
                () => Sett.Default.SampleNameHydrants,
                (str) => Sett.Default.SampleNameHydrants = str,
                () => Sett.Default.DefaultSampleNameHydrants),
        };
        static GetterOfType()
        {

        }

        /// <summary>
        /// Возвращает символы для проверки корректности шаблона именования.
        /// </summary>
        /// <param name="type">Тип.</param>
        /// <returns></returns>
        public static string GetSampleChars(Type type) => dictOfType[type].sampleChars;

        /// <summary>
        /// Возвращает русский перевод имени класса в единственном числе.
        /// </summary>
        /// <param name="type">Тип.</param>
        public static string GetTraslateOne(Type type) => dictOfType[type].traslateOne;

        /// <summary>
        /// Возвращает русский перевод имени класса во множественном числе.
        /// </summary>
        /// <param name="type">Тип.</param>
        public static string GetTraslateMany(Type type) => dictOfType[type].traslateMany;

        /// <summary>
        /// Возвращает шаблон именования.
        /// </summary>
        /// <param name="type">Тип.</param>
        public static string GetSampleNaming(Type type) => dictOfType[type].getSampleNaming();

        /// <summary>
        /// Устанавливает шаблон именования.
        /// </summary>
        /// <param name="type">Тип.</param>
        /// /// <param name="str">Строка.</param>
        public static void SetSampleNaming(Type type, string str) => dictOfType[type].setSampleNaming(str);

        /// <summary>
        /// Возвращает шаблон именования по умолчанию.
        /// </summary>
        /// <param name="type">Тип.</param>
        public static string GetDefaultSampleNaming(Type type) => dictOfType[type].getDefaultSampleNaming();
    }

    /// <summary>
    /// Данные типа.
    /// </summary>
    public struct DataOfType
    {
        /// <summary>
        /// Символы для проверки корректности шаблона именования.
        /// </summary>
        public string sampleChars;

        /// <summary>
        /// Русский перевод имени класса в единственном числе.
        /// </summary>
        public string traslateOne;

        /// <summary>
        /// Русский перевод имени класса во множественном числе.
        /// </summary>
        public string traslateMany;

        /// <summary>
        /// Функция возврата шаблона именования.
        /// </summary>
        public Func<string> getSampleNaming;

        /// <summary>
        /// Метод установки шаблона именования.
        /// </summary>
        public Action<string> setSampleNaming;

        /// <summary>
        /// Функция возврата шаблона именования по умолчанию.
        /// </summary>
        public Func<string> getDefaultSampleNaming;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="sampleChars"></param>
        /// <param name="traslateOne"></param>
        /// <param name="traslateMany"></param>
        /// <param name="getSampleNaming"></param>
        /// <param name="setSampleNaming"></param>
        /// <param name="getDefaultSampleNaming"></param>
        public DataOfType(string sampleChars, string traslateOne, string traslateMany,
            Func<string> getSampleNaming, Action<string> setSampleNaming, Func<string> getDefaultSampleNaming)
        {
            this.sampleChars = sampleChars;
            this.traslateOne = traslateOne;
            this.traslateMany = traslateMany;
            this.getSampleNaming = getSampleNaming;
            this.setSampleNaming = setSampleNaming;
            this.getDefaultSampleNaming = getDefaultSampleNaming;
        }
    }
}
