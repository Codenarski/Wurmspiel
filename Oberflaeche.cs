using System.Drawing;
using System.Windows.Forms;

namespace Wurmspiel
{
    public partial class Oberflaeche : Form
    {
        private readonly Steuerung _steuerung;

        public Oberflaeche()
        {
            _steuerung = new Steuerung(this);
            InitializeComponent();
            _steuerung.InitialisiereSpiel();
        }

        public void AktualisiereOberflaeche()
        {
            ZeichneFutterUndWurm(pictureBox1.CreateGraphics());
        }

        public void ZeichneFutterUndWurm(Graphics g)
        {
            g.Clear(BackColor);
            _steuerung.Zeichne(g);
        }

        public void SetzeMeldung(string pMeldung)
        {
            MessageBox.Show(pMeldung);
        }

        private void Oberflaeche_KeyDown(object sender, KeyEventArgs e)
        {
            _steuerung.VerarbeiteTasteDruck(e.KeyCode);
        }
    }
}
