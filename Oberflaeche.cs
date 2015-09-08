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
        private Steuerung ichkennSteuerung;
        private int aMeldung;

        public Oberflaeche()
        {
            ichkennSteuerung = new Steuerung(this);
            InitializeComponent();
            ichkennSteuerung.initialisiereSpiel();

        }
        public void initialisiereOberflaeche()
        {
            aktualisiereOberflaeche();
        }
        public void aktualisiereOberflaeche()
        {

            zeichneFutterundWurm(pictureBox1.CreateGraphics());
        }

        public void zeichneFutterundWurm(Graphics g)
        {
            g.Clear(this.BackColor);
            ichkennSteuerung.zeichne(g);
        }
        public void setzeMeldung(string pMeldung)
        {
            MessageBox.Show(pMeldung);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Oberflaeche_KeyDown(object sender, KeyEventArgs e)
        {
            ichkennSteuerung.verarbeiteTasteDruck(e.KeyCode);
        }
    }
}
