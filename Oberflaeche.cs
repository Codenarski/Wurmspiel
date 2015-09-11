using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wurmspiel
{
    public partial class Oberflaeche : Form
    {
        private Steuerung _ichkennSteuerung;
        private int _aMeldung;

        public Oberflaeche()
        {
            _ichkennSteuerung = new Steuerung(this);
            InitializeComponent();
            _ichkennSteuerung.InitialisiereSpiel();

        }
        public void InitialisiereOberflaeche()
        {
            AktualisiereOberflaeche();
        }
        public void AktualisiereOberflaeche()
        {

            ZeichneFutterundWurm(pictureBox1.CreateGraphics());
        }

        public void ZeichneFutterundWurm(Graphics g)
        {
            g.Clear(this.BackColor);
            _ichkennSteuerung.Zeichne(g);
        }
        public void SetzeMeldung(string pMeldung)
        {
            MessageBox.Show(pMeldung);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Oberflaeche_KeyDown(object sender, KeyEventArgs e)
        {
            _ichkennSteuerung.VerarbeiteTasteDruck(e.KeyCode);
        }
    }
}
