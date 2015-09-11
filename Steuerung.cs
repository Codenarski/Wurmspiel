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
        private readonly Oberflaeche _g;
        private Wurm _johnny;
        private FutterZelle _fluffy;
        private readonly Uhr _uhrski;

        public Steuerung(Oberflaeche g)
        {
            this._g = g;
            _uhrski = new Uhr(this);
        }
        public void VerarbeiteTasteDruck(Keys taste)
        {
            switch (taste)
            {
                case Keys.Up:
                    if (_johnny.ARichtung != Richtung.Runter)
                        SetzteRichtung(Richtung.Hoch);
                    break;
                case Keys.Down:
                    if (_johnny.ARichtung != Richtung.Hoch)
                    SetzteRichtung(Richtung.Runter);
                    break;
                case Keys.Left:
                    if (_johnny.ARichtung != Richtung.Rechts)
                    SetzteRichtung(Richtung.Links);
                    break;
                case Keys.Right:
                    if (_johnny.ARichtung != Richtung.Links)
                    SetzteRichtung(Richtung.Rechts);
                    break;
            }
         }
        public void VerarbeiteUhrTick()
        {
            if (!_johnny.Krieche())
            {
                _uhrski.Stoppe();
                _g.SetzeMeldung("Enter for Restart");
                InitialisiereSpiel();
                return;
            }
            _g.AktualisiereOberflaeche();
        }

        public void SetzteRichtung(Richtung richtung)
        {
            _johnny.ARichtung = richtung;

        }
        public void InitialisiereSpiel()
        {
            _johnny = new Wurm(25, 25, 50, 50);
            ErzeugeFutterAusserhalbWurm();
            _g.AktualisiereOberflaeche();
            _uhrski.Start();
        }

        public void ErzeugeFutterAusserhalbWurm()
        {
            int x, y;

            do
            {
                x = ErzeugeZUfallsZahl();
                y = ErzeugeZUfallsZahl();

            } while (LiegtAufWurm(x, y)); 
 
            _fluffy = new FutterZelle(x,y);
        }

        public int ErzeugeZUfallsZahl()
        {
            var rd1 = new Random();
            return rd1.Next(0, 50);
        }

        public bool LiegtAufWurm(int x, int y)
        {
            return _johnny.Zellen().Select(wurmzelle => wurmzelle.HatGleicheXy(x, y)).Any(hatGleicheXy => hatGleicheXy);
        }

        public void Zeichne(Graphics g)
        {
            if (FutterzelleGefressen())
            {
                _johnny.Wachse();
                ErzeugeFutterAusserhalbWurm();
            }
            ZeichneFutter(g);
            ZeichneWurm(g);
        }
        private bool FutterzelleGefressen()
        {

            return _johnny.Zellen().First().HatGleicheXy(_fluffy.X,_fluffy.Y); 
        }
        public void ZeichneFutter(Graphics g)
        {

            var futterColor = Brushes.Black;
            _fluffy.Zeichne(g);
            
        }

        public void ZeichneWurm(Graphics g)
        {
            var zellenColor = Brushes.Yellow;

            foreach (var wurmzelle in _johnny.Zellen())
            {
                wurmzelle.Zeichne(g);
            }

        }     
    }
}
