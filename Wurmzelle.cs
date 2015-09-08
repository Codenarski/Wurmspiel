using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wurmspiel
{
    public class Wurmzelle : Zelle
    {        
        public Wurmzelle(int pX, int pY) : base(pX, pY) { }

        public override void zeichne(Graphics g)
        {
            g.FillEllipse(Brushes.GreenYellow, new Rectangle(X * 10, Y * 10, 10, 10));
        }
    }
}
