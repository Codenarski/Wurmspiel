using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wurmspiel
{
    class Steuerung
    {
        private Oberflaeche g;
        private Wurm Johnny;
        private FutterZelle fluffy;
        private Uhr Uhrski;

        public Steuerung(Oberflaeche g)
        {
            this.g = g;
            Uhrski = new Uhr(this);
        }
        public void verarbeiteTasteDruck(Keys Taste)
        {
            switch (Taste)
            {
                case Keys.Up:
                    if (Johnny.aRichtung != Richtung.Runter)
                        setzteRichtung(Richtung.Hoch);
                    break;
                case Keys.Down:
                    if (Johnny.aRichtung != Richtung.Hoch)
                    setzteRichtung(Richtung.Runter);
                    break;
                case Keys.Left:
                    if (Johnny.aRichtung != Richtung.Rechts)
                    setzteRichtung(Richtung.Links);
                    break;
                case Keys.Right:
                    if (Johnny.aRichtung != Richtung.Links)
                    setzteRichtung(Richtung.Rechts);
                    break;
            }
         }
        public void verarbeiteUhrTick()
        {
            if (!Johnny.krieche())
            {
                Uhrski.stoppe();
                g.setzeMeldung("Enter for Restart");
                initialisiereSpiel();
                return;
            }
            
            g.aktualisiereOberflaeche();

        }

        public void setzteRichtung(Richtung Richtung)
        {
            Johnny.aRichtung = Richtung;

        }
        public void initialisiereSpiel()
        {
            Johnny = new Wurm(25, 25, 50, 50);
            erzeugeFutterAusserhalbWurm();
            g.aktualisiereOberflaeche();
            Uhrski.Start();
            
        }

        public void erzeugeFutterAusserhalbWurm()
        {
            int x, y;

            do
            {
                x = erzeugeZUfallsZahl();
                y = erzeugeZUfallsZahl();

            } while (liegtAufWurm(x, y)); 
 
            fluffy = new FutterZelle(x,y);
        }

        public int erzeugeZUfallsZahl()
        {
            Random rd1 = new Random();
            return rd1.Next(0, 50);
           

        }

        public bool liegtAufWurm(int x, int y)
        {
            foreach (var wurmzelle in Johnny.Zellen())
            {
                bool jein = wurmzelle.hatGleicheXY(x, y);
                if (jein)
                    return true;
                
            }
            return false;
            
        }

        public void zeichne(Graphics g)
        {
            if (FutterzelleGefressen())
            {
                Johnny.wachse();
                erzeugeFutterAusserhalbWurm();
            }
            zeichneFutter(g);
            zeichneWurm(g);

            
        }
        private bool FutterzelleGefressen()
        {

            return Johnny.Zellen().First().hatGleicheXY(fluffy.X,fluffy.Y); 
        }
        public void zeichneFutter(Graphics g)
        {

            Brush futterColor = Brushes.Black;
            fluffy.zeichne(g);
            
        }

        public void zeichneWurm(Graphics g)
        {
            Brush zellenColor = Brushes.Yellow;

            foreach (var wurmzelle in Johnny.Zellen())
            {
                wurmzelle.zeichne(g);
            }

        }     
    }
}
