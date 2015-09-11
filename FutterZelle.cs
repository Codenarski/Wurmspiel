using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wurmspiel
{
    class FutterZelle : Zelle
    {
        public FutterZelle(int pX, int pY) : base(pX, pY)
        {

        }

        public override void Zeichne(Graphics g)
        {
            g.FillEllipse(Brushes.DeepPink, new Rectangle(X * 10, Y * 10, 10, 10));
        }
    }
}
