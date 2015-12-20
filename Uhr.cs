using System;
using System.Windows.Forms;

namespace Wurmspiel
{
    internal class Uhr
    {
        private readonly Timer _timer = new Timer();
        private readonly double _takt = 0.1;
        private readonly Steuerung _steuerung;

        public Uhr(Steuerung steuerung)
        {
            _steuerung = steuerung;
            _timer.Tick += delegate { _steuerung.VerarbeiteUhrTick(); };
            _timer.Interval = Convert.ToInt32(_takt*1000);
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void Start()
        {
            _timer.Start();
        }
    }


}

