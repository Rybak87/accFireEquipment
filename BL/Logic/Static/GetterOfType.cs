using System;
using System.Collections.Generic;
using System.Linq;
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
            [typeof(FireCabinet)] = new DataOfType("NL", "Пожарный шкаф", "Пожарные шкафы",
                () => Sett.Default.SampleNameFireCabinets,
                (str) => Sett.Default.SampleNameFireCabinets = str,
                () => Sett.Default.DefaultSampleNameFireCabinets),

            [typeof(Extinguisher)] = new DataOfType("NLF", "Огнетушитель", "Огнетушители",
                () => Sett.Default.SampleNameExtinguishers,
                (str) => Sett.Default.SampleNameExtinguishers = str,
                () => Sett.Default.DefaultSampleNameExtinguishers),

            [typeof(Hose)] = new DataOfType("NLF", "Рукав", "Рукава",
                () => Sett.Default.SampleNameHoses,
                (str) => Sett.Default.SampleNameHoses = str,
                () => Sett.Default.DefaultSampleNameHoses),

            [typeof(Hydrant)] = new DataOfType("NLF", "Пожарный кран", "Пожарные краны",
                () => Sett.Default.SampleNameHydrants,
                (str) => Sett.Default.SampleNameHydrants = str,
                () => Sett.Default.DefaultSampleNameHydrants),
        };

        private static Dictionary<char, Func<Hierarchy, string>> dictSampleFunctions = new Dictionary<char, Func<Hierarchy, string>>()
        {
            ['N'] = ent => ent.Number.ToString(),
            ['L'] = ent => ent.GetLocation.Number.ToString(),
            ['F'] = ent => (ent as Equipment).Parent.Number.ToString()
        };

        /// <summary>
        /// Возвращает имя.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns></returns>
        public static string GetName(Hierarchy entity)
        {
            var sample = dictOfType[entity.GetType()].getSampleNaming();
            return GetName(entity, sample);
        }

        /// <summary>
        /// Возвращает имя.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// /// <param name="sample">Шаблон.</param>
        /// <returns></returns>
        public static string GetName(Hierarchy entity, string sample)
        {
            var chars = dictOfType[entity.GetType()].sampleChars;
            var result = sample;

            foreach (char ch in chars)
                result = result.Replace("#" + ch, dictSampleFunctions[ch].Invoke(entity));
            return result;
        }

        /// <summary>
        /// Проверяет корректность шаблона именования.
        /// </summary>
        public static bool CorrectSample(string text, Type type)
        {
            var chars = GetSampleChars(type);
            text = text.Trim();
            for (int i = 0; i < text.Length; i++)
            {
                var ch = text[i];
                if (ch == '#')
                {
                    if (i + 1 == text.Length)
                        return false;
                    if (!chars.Contains(text[i + 1]))
                        return false;
                }
            }
            return true;
        }

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
        /// Возвращает перевод имени класса в единственном числе.
        /// </summary>
        /// <param name="type">Тип.</param>
        public static string GetTraslateOne(Type type) => dictOfType[type].traslateOne;

        /// <summary>
        /// Возвращает перевод имени класса во множественном числе.
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
