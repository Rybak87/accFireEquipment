using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Windows.Forms;

namespace BL
{
    /// <summary>
    /// Относительная точка.
    /// </summary>
    [ComplexType]
    public class ScalePoint
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="percentLeft">Относительное расположение слева от 0 до 1.</param>
        /// <param name="percentTop">Относительное расположение сверху от 0 до 1.</param>
        /// <param name="displayed">Условие отображения.</param>
        public ScalePoint(double percentLeft, double percentTop, bool displayed)
        {
            PercentLeft = percentLeft;
            PercentTop = percentTop;
            Displayed = displayed;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public ScalePoint()
        {
            PercentLeft = 0;
            PercentTop = 0;
            Displayed = false;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="mousePoint"></param>
        /// <param name="control"></param>
        public ScalePoint(Point mousePoint, Control control)
        {
            var point = control.PointToClient(mousePoint);
            PercentLeft = (double)point.X / control.Width;
            PercentTop = (double)point.Y / control.Height;
            Displayed = true;
        }

        /// <summary>
        /// Относительное расположение слева от 0 до 1.
        /// </summary>
        public double PercentLeft { get; set; }

        /// <summary>
        /// Относительное расположение слева от 0 до 1.
        /// </summary>
        public double PercentTop { get; set; }

        /// <summary>
        /// Условие отображения.
        /// </summary>
        public bool Displayed { get; set; }
    }
}
