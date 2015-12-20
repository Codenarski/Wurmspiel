using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Wurmspiel
{
    internal class Steuerung
    {
        private readonly Oberflaeche _oberflaeche;
        private Wurm _wurm;
        private Futterzelle _futterzelle;
        private readonly Uhr _uhr;

        public Steuerung(Oberflaeche oberflaeche)
        {
            _oberflaeche = oberflaeche;
            _uhr = new Uhr(this);
        }

        public void VerarbeiteTasteDruck(Keys taste)
        {
            switch (taste)
            {
                case Keys.Up:
                    if (_wurm.ARichtung != Richtung.Runter)
                        SetzteRichtung(Richtung.Hoch);
                    break;
                case Keys.Down:
                    if (_wurm.ARichtung != Richtung.Hoch)
                        SetzteRichtung(Richtung.Runter);
                    break;
                case Keys.Left:
                    if (_wurm.ARichtung != Richtung.Rechts)
                        SetzteRichtung(Richtung.Links);
                    break;
                case Keys.Right:
                    if (_wurm.ARichtung != Richtung.Links)
                        SetzteRichtung(Richtung.Rechts);
                    break;
            }
        }

        public void VerarbeiteUhrTick()
        {
            if (!_wurm.Krieche())
            {
                _uhr.Stop();
                _oberflaeche.SetzeMeldung("OK for Restart");
                InitialisiereSpiel();
                return;
            }
            _oberflaeche.AktualisiereOberflaeche();
        }

        public void SetzteRichtung(Richtung richtung)
        {
            _wurm.ARichtung = richtung;
        }

        public void InitialisiereSpiel()
        {
            _wurm = new Wurm(25, 25, 50, 50);
            ErzeugeFutterAusserhalbWurm();
            _oberflaeche.AktualisiereOberflaeche();
            _uhr.Start();
        }

        public void ErzeugeFutterAusserhalbWurm()
        {
            int x, y;

            do
            {
                x = ErzeugeZufallsZahl();
                y = ErzeugeZufallsZahl();
            } while (LiegtAufWurm(x, y));

            _futterzelle = new Futterzelle(x, y);
        }

        public int ErzeugeZufallsZahl()
        {
            var rd1 = new Random();
            return rd1.Next(0, 50);
        }

        public bool LiegtAufWurm(int x, int y)
        {
            return _wurm.Zellen().Select(wurmzelle => wurmzelle.HatGleicheXy(x, y)).Any(hatGleicheXy => hatGleicheXy);
        }

        public void Zeichne(Graphics g)
        {
            if (FutterzelleGefressen())
            {
                _wurm.Wachse();
                ErzeugeFutterAusserhalbWurm();
            }
            ZeichneFutter(g);
            ZeichneWurm(g);
        }

        private bool FutterzelleGefressen()
        {
            return _wurm.Zellen().First().HatGleicheXy(_futterzelle.X, _futterzelle.Y);
        }

        public void ZeichneFutter(Graphics g)
        {
            _futterzelle.Zeichne(g);
        }

        public void ZeichneWurm(Graphics g)
        {
            foreach (var wurmzelle in _wurm.Zellen())
            {
                wurmzelle.Zeichne(g);
            }
        }
    }
}
