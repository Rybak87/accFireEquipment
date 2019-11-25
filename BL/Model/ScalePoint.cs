using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace BL
{
    [ComplexType]
    public class ScalePoint
    {
        public double PercentLeft { get; set; }
        public double PercentTop { get; set; }
        public bool Empty { get; set; }

        public ScalePoint(double percentLeft, double percentTop, bool empty)
        {
            PercentLeft = percentLeft;
            PercentTop = percentTop;
            Empty = empty;
        }
        public ScalePoint()
        {
            PercentLeft = 0;
            PercentTop = 0;
            Empty = false;
        }
        public ScalePoint(Point mousePoint, Control control)
        {
            var point = control.PointToClient(mousePoint);
            PercentLeft = (double)point.X / control.Width;
            PercentTop = (double)point.Y / control.Height;
            Empty = true;
        }
    }
}
