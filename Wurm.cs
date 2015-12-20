using System;
using System.Collections.Generic;
using System.Linq;

namespace Wurmspiel
{
    public enum Richtung
    {
        Links,
        Rechts,
        Hoch,
        Runter
    }

    public class Wurm
    {
        public Richtung ARichtung;
        private readonly int _maxX;
        private readonly int _maxY;
        private readonly Queue<Wurmzelle> _wurmzellen;

        public Wurm(int kopfX, int kopfY, int maxX, int maxY)
        {
            ARichtung = Richtung.Hoch;
            _maxX = maxX;
            _maxY = maxY;
            _wurmzellen = new Queue<Wurmzelle>();
            var kopf = new Wurmzelle(kopfX, kopfY);
            _wurmzellen.Enqueue(kopf);
            ErweitereWurmUmWurmzellen(3);
        }

        public Queue<Wurmzelle> Zellen()
        {
            return _wurmzellen;
        }

        private void ErweitereWurmUmWurmzellen(int x)
        {
            var alteZelle = _wurmzellen.First();
            for (var i = 0; i < x; i++)
            {
                var neueZelle = ErstelleWurmzelle(alteZelle.X, alteZelle.Y);
                _wurmzellen.Enqueue(neueZelle);
                alteZelle = neueZelle;
            }
        }

        private Wurmzelle ErstelleWurmzelle(int x, int y)
        {
            int neuX = x, neuY = y;

            switch (ARichtung)
            {
                case Richtung.Links:
                    neuX = x + 1;
                    break;
                case Richtung.Rechts:
                    neuX = x - 1;
                    break;
                case Richtung.Hoch:
                    neuY = y + 1;
                    break;
                case Richtung.Runter:
                    neuY = y - 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return new Wurmzelle(neuX, neuY);
        }

        public int GibKopfPosX()
        {
            return _wurmzellen.First().X;
        }

        public int GibKopfPosY()
        {
            return _wurmzellen.First().Y;
        }

        public void SetzeRichtung(Richtung pRichtung)
        {
            ARichtung = pRichtung;
        }

        public bool Krieche()
        {
            var kopierterwurm = KopiereWurm();
            BewegeKopf();
            Bewegekoerper(kopierterwurm);
            return !GibtEsKollisionen();
        }

        public void Wachse()
        {
            ErweitereWurmUmWurmzellen(1);
        }

        private void BewegeKopf()
        {
            switch (ARichtung)
            {
                case Richtung.Links:
                    _wurmzellen.First().X--;
                    break;
                case Richtung.Rechts:
                    _wurmzellen.First().X++;
                    break;
                case Richtung.Hoch:
                    _wurmzellen.First().Y--;
                    break;
                case Richtung.Runter:
                    _wurmzellen.First().Y++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private Queue<Wurmzelle> KopiereWurm()
        {
            var kopierterwurm = new Queue<Wurmzelle>();

            foreach (var neuerWurm in _wurmzellen.Select(alterWurm => new Wurmzelle(alterWurm.X, alterWurm.Y)))
            {
                kopierterwurm.Enqueue(neuerWurm);
            }

            return kopierterwurm;
        }

        private void Bewegekoerper(Queue<Wurmzelle> kopierterwurm)
        {
            var kopierterWurmArr = kopierterwurm.ToArray();
            var neuerWirmArr = _wurmzellen.ToArray();

            for (var i = 0; i < neuerWirmArr.Length; i++)
            {
                if (UeberspringeKopf(i))
                    continue;

                var kopierteZelle = kopierterWurmArr[i - 1];
                var neueZelle = neuerWirmArr[i];
                neueZelle.X = kopierteZelle.X;
                neueZelle.Y = kopierteZelle.Y;
            }
        }

        private bool UeberspringeKopf(int position)
        {
            return position == 0;
        }

        private bool GibtEsKollisionen()
        {
            var beruehrtKoerper = BeruehrtKopfKoerper();
            var beruehrtWand = BeruehrtKopfWand();
            var endlaengeErreicht = _wurmzellen.Count >= 10;
            return (beruehrtKoerper) || (beruehrtWand) || (endlaengeErreicht);
        }

        private bool BeruehrtKopfKoerper()
        {
            var wurmzellenaray = _wurmzellen.ToArray();
            var kopf = _wurmzellen.First();
            for (var i = 0; i < wurmzellenaray.Length; i++)
            {
                if (UeberspringeKopf(i))
                    continue;
                if ((kopf.X == wurmzellenaray[i].X) && (kopf.Y == wurmzellenaray[i].Y))
                    return true;
            }
            return false;
        }

        private bool BeruehrtKopfWand()
        {
            var kopf = _wurmzellen.First();
            return (kopf.X > _maxX) || (kopf.Y > _maxY) || (kopf.X < 0) || (kopf.Y < 0);
        }
    }
}


