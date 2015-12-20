using System.Drawing;

namespace Wurmspiel
{
    public class Wurmzelle : Zelle
    {
        public Wurmzelle(int pX, int pY) : base(pX, pY)
        {
        }

        public override void Zeichne(Graphics g)
        {
            g.FillEllipse(Brushes.GreenYellow, new Rectangle(X*10, Y*10, 10, 10));
        }
    }
}
