using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AP1_GSB_BTS_SIO
{
    public partial class KiloUtilFrais : Form
    {

        // Var par def
        public int ValeurK {  get; set; }
        //
        public KiloUtilFrais()
        {
            InitializeComponent();
        }

        private void TextnmbK(object sender, EventArgs e)
        {
            string Valeur = TextNmbKilometres.Text;
            ValeurK = 0;

            try
            {
                ValeurK = Convert.ToInt32(Valeur); // on converti l'entrée de l'utilisateur en double
            }
            catch (Exception msg) // si ca ne fonctionne pas réinitialisation
            {
                MessageBox.Show("La valeur entrée est incorrecte, n'est attendu que les chiffres / nombres avec ou sans virgule.");
                TextNmbKilometres.Text = "";
            }
        }

        private void Valider(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
