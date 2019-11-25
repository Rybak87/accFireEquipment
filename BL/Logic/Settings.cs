using BL.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BL
{
    public static class Settings
    {
        public static int RatioIconSize { get; set; }
        public static Dictionary<Type, int> IconsImageIndex { get; }
        public static ImageList IconsImageList { get; }
        

        static Settings()
        {
            RatioIconSize = 50;
            IconsImageIndex = new Dictionary<Type, int>()
            {
                [0.GetType()] = (0),
                [typeof(Location)] = (0),
                [typeof(FireCabinet)] = (1),
                [typeof(Extinguisher)] = (2),
                [typeof(Hose)] = (3),
                [typeof(Hydrant)] = (4)
            };
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
        public static Image IconsImage(Type type) => IconsImageList.Images[IconsImageIndex[type]];

    }
}
