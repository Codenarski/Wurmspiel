using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wurmspiel
{
    class Uhr
    {
        readonly Timer _meinTimer = new Timer();
        private double _aTakt = 0.1;
        private readonly Steuerung _ichKennDieSteuerung;

        public Uhr(Steuerung ichkennSteuerung)
        {
            _ichKennDieSteuerung = ichkennSteuerung;
            _meinTimer.Tick += delegate(object sender, EventArgs e) { _ichKennDieSteuerung.VerarbeiteUhrTick(); };
            _meinTimer.Interval = Convert.ToInt32(_aTakt*1000);
        }
        public void Stoppe()
        {
            _meinTimer.Stop();
        }

        public void Start()
        {
            _meinTimer.Start();
        }
    }

   
}

