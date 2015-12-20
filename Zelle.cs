using System.Drawing;

namespace Wurmspiel
{
    public abstract class Zelle
    {
        public int X { get; set; }
        public int Y { get; set; }

        protected Zelle(int pX, int pY)
        {
            X = pX;
            Y = pY;
        }

        public bool HatGleicheXy(int pX, int pY)
        {
            return (X == pX) && (Y == pY);
        }

        public abstract void Zeichne(Graphics g);
    }

}
