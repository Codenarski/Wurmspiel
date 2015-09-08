using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wurmspiel
{
    public abstract class Zelle
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Zelle(int pX, int pY)
        {
            X = pX;
            Y = pY;
        }
        public Boolean hatGleicheXY(int pX, int pY)
        {
            return (X == pX) && (Y == pY);
        }
        public abstract void zeichne(Graphics g);
        
    }
}
