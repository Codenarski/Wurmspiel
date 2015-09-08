using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private int aLaenge;        
        public Richtung aRichtung;
        private int maxX;
        private int maxY;
        
        Queue<Wurmzelle> Wurmzellen;

        public Wurm(int kopfX, int kopfY, int maxX, int maxY)
        {
            aRichtung = Richtung.Hoch;
            this.maxX = maxX;
            this.maxY = maxY;
            Wurmzellen = new Queue<Wurmzelle>();
            Wurmzelle kopf = new Wurmzelle(kopfX, kopfY);

            Wurmzellen.Enqueue(kopf);
            ErweitereWurmUmWurmzellen(3);
            aLaenge = Wurmzellen.Count;
        }

        public Queue<Wurmzelle> Zellen()
        {
            return Wurmzellen;
        }

        private void ErweitereWurmUmWurmzellen(int x)
        {
            Wurmzelle alteZelle = Wurmzellen.First();
            Wurmzelle neueZelle;
            for (int i = 0; i < x; i++)
            {
                neueZelle = ErstelleWurmzelle(alteZelle.X, alteZelle.Y);
                Wurmzellen.Enqueue(neueZelle);
                alteZelle = neueZelle;
            }
            
        }
        private Wurmzelle ErstelleWurmzelle(int x, int y)
        {
            int neuX = x, neuY = y;

            switch (aRichtung)
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


       public int gibKopfPosX()
        {
            return Wurmzellen.First<Wurmzelle>().X;
        }
        
        public int gibKopfPosY()
        {
            return Wurmzellen.First<Wurmzelle>().Y;
        }
        
        public void setzeRichtung(Richtung pRichtung)
        {
            aRichtung = pRichtung;                        
        }
        
        public Boolean krieche()
        {
            Queue<Wurmzelle> Kopierterwurm = KopiereWurm();
            BewegeKopf();
            Bewegekoerper(Kopierterwurm);
            return !GibtEsKollisionen();

        }
        public void wachse()
        {
            ErweitereWurmUmWurmzellen(1);
        }

        private void BewegeKopf()
        {
            switch (aRichtung)
            {
                case Richtung.Links:
                    Wurmzellen.First().X--; 
                    break;
                case Richtung.Rechts:
                    Wurmzellen.First().X++;
                    break;
                case Richtung.Hoch:
                    Wurmzellen.First().Y--;
                    break;
                case Richtung.Runter:
                    Wurmzellen.First().Y++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        private Queue<Wurmzelle> KopiereWurm()
        {
            Queue<Wurmzelle>kopierterwurm = new Queue<Wurmzelle>();        
            Wurmzelle neuerWurm;

            foreach (var alterWurm in Wurmzellen)
            {
                neuerWurm = new Wurmzelle(alterWurm.X, alterWurm.Y);
                kopierterwurm.Enqueue(neuerWurm);
            }

            return kopierterwurm;
        }

        private void Bewegekoerper(Queue<Wurmzelle> kopierterwurm)
        {
            Wurmzelle[] kopierterWurmArr = kopierterwurm.ToArray();
            Wurmzelle[] neuerWirmArr = Wurmzellen.ToArray();
            Wurmzelle kopierteZelle;
            Wurmzelle neueZelle;

            for (int i = 0; i < neuerWirmArr.Count(); i++)
            {
                if (WennDasDerKopfIstDannIgnorierenWirIhn(i))
                   continue;

                kopierteZelle = kopierterWurmArr[i - 1]; 
                neueZelle = neuerWirmArr[i];
                neueZelle.X = kopierteZelle.X;
                neueZelle.Y = kopierteZelle.Y;

            }
        }

        private Boolean WennDasDerKopfIstDannIgnorierenWirIhn(int position)
        {
            return position == 0;
        }

        private Boolean GibtEsKollisionen()
        {
            if ((BeruehrtKopfKoerper() == false) && (BeruehrtKopfWand() == false) && (Wurmzellen.Count < 10))
            {
                return false;
            }
            else
            {
                return true;
            }           
        }

        private Boolean BeruehrtKopfKoerper()
        {
            Wurmzelle[] Wurmzellenaray = Wurmzellen.ToArray();
            Wurmzelle kopf = Wurmzellen.First();
            for (int i = 0; i < Wurmzellenaray.Count(); i++)
            {
                if(WennDasDerKopfIstDannIgnorierenWirIhn(i))
                    continue;
                if ((kopf.X == Wurmzellenaray[i].X) && (kopf.Y == Wurmzellenaray[i].Y))
                {
                    return true;
                }
                
            }
            return false;
        }

        private Boolean BeruehrtKopfWand()
        {
            Wurmzelle kopf = Wurmzellen.First();
            if ((kopf.X > maxX) || (kopf.Y > maxY) || (kopf.X < 0) || (kopf.Y < 0))
            {
                return true;
            }
            return false;
        }
    }
}
