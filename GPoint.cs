using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTFCreator
{
  [Serializable]
    public class GPoint
    {
        private Point p;
        private bool oncrv;

        public GPoint(Point p, bool oncrv)
        {
            this.p = p;
            this.oncrv = oncrv;
        }

        public int X
        {
            get { return p.X; }
        }

        public int Y
        {
            get { return p.Y; }
        }

        public bool OnCurve
        {
            get { return oncrv; }
        }
    }
}
