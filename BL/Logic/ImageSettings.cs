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
    //[Serializable]
    public static class ImageSettings
    {
        /// <summary>
        /// Коллекция индексов иконок по типу.
        /// </summary>
        public static Dictionary<Type, int> IconsImageIndex { get; } = new Dictionary<Type, int>()
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
        static ImageSettings()
        {
            if (IconsImageList == null)
            {
                IconsImageList = new ImageList();
                IconsImageList.TransparentColor = Color.Transparent;

                IconsImageList.Images.Add("Location.ico", Icons.Location);
                IconsImageList.Images.Add("FireCabinet.ico", Icons.FireCabinet);
                IconsImageList.Images.Add("Extinguisher.ico", Icons.Extinguisher);
                IconsImageList.Images.Add("Hose.ico", Icons.Hose);
                IconsImageList.Images.Add("Hydrant.ico", Icons.Hydrant);

                IconsImageList.Images.SetKeyName(0, "Location.ico");
                IconsImageList.Images.SetKeyName(1, "FireCabinet.ico");
                IconsImageList.Images.SetKeyName(2, "Extinguisher.ico");
                IconsImageList.Images.SetKeyName(3, "Hose.ico");
                IconsImageList.Images.SetKeyName(4, "Hydrant.ico");
            }
        }

        /// <summary>
        /// Коллекция иконок.
        /// </summary>
        public static ImageList IconsImageList { get; }

        /// <summary>
        /// Возвращает иконку по типу.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Image IconsImage(Type type) => IconsImageList.Images[IconsImageIndex[type]];
    }
}
