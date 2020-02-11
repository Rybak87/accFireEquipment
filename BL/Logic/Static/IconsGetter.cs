using BL.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BL
{
    /// <summary>
    /// Иконки.
    /// </summary>
    public static class IconsGetter
    {
        /// <summary>
        /// Коллекция индексов иконок по типу.
        /// </summary>
        private static Dictionary<Type, int> IconsImageIndex { get; } = new Dictionary<Type, int>()
        {
            [0.GetType()] = 0,
            [typeof(Location)] = 0,
            [typeof(FireCabinet)] = 1,
            [typeof(Extinguisher)] = 2,
            [typeof(Hose)] = 3,
            [typeof(Hydrant)] = 4
        };

        /// <summary>
        /// Констуктор. Заполение коллекции иконок.
        /// </summary>
        static IconsGetter()
        {
                IconsImageList = new ImageList();
                IconsImageList.TransparentColor = Color.Transparent;

                IconsImageList.Images.Add(Icons.Location);
                IconsImageList.Images.Add(Icons.FireCabinet);
                IconsImageList.Images.Add(Icons.Extinguisher);
                IconsImageList.Images.Add(Icons.Hose);
                IconsImageList.Images.Add(Icons.Hydrant);
        }

        /// <summary>
        /// Коллекция иконок.
        /// </summary>
        public static ImageList IconsImageList { get; }

        /// <summary>
        /// Возвращает индекс иконки по типу.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int GetIndexIcon(Type type) => IconsImageIndex[type];

        /// <summary>
        /// Возвращает иконку по типу.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Image GetIconImage(Type type) => IconsImageList.Images[IconsImageIndex[type]];
    }
}
