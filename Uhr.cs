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
        Timer meinTimer = new Timer();
        private double aTakt = 0.1;
        private Steuerung fucka;

        public Uhr(Steuerung ichkennSteuerung)
        {
            fucka = ichkennSteuerung;
            meinTimer.Tick += delegate(object sender, EventArgs e) { fucka.verarbeiteUhrTick(); };
            meinTimer.Interval = Convert.ToInt32(aTakt*1000);
        }
        public void stoppe()
        {
            meinTimer.Stop();
        }

        public void Start()
        {
            meinTimer.Start();
        }
             
            

    }

   
}

