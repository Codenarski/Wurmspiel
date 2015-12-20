using System.Drawing;

namespace Wurmspiel
{
    class Futterzelle : Zelle
    {
        public Futterzelle(int pX, int pY) : base(pX, pY)
        {

        }

        public override void Zeichne(Graphics g)
        {
            g.FillEllipse(Brushes.DeepPink, new Rectangle(X * 10, Y * 10, 10, 10));
        }
    }
}
