using MySql.Data.MySqlClient;
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
    public partial class AjoutFraisHF : Form
    {

        // Variables par défaut
        #region
        DateTime dateActuel = DateTime.Now;
        int id_FicheDeFrais;

        // vars pour ajouter
        string motif_frais = "";
        string datefrais = "";
        double montantfrais = 0;
        #endregion
        //
        public AjoutFraisHF(int id_FicheDeFrais)
        {
            this.id_FicheDeFrais = id_FicheDeFrais;
            InitializeComponent();
        }

        // Option co et deco de la bdd
        #region
        public MySqlConnection Connection;

        private void ConnectionBDD()
        {
            try
            {
                string connectionString = "server=127.0.0.1;port=3307;uid=root;pwd=root;database=ap1bdd";
                Connection = new MySqlConnection(connectionString);
                Connection.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void DeconnectionBDD()
        {
            Connection.Close();
        }
        #endregion
        //

        // Fermer l'appli + bouton retour
        #region
        private void retourhf_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void quitterhf_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion
        //

        // Valider les critères saisis
        #region
        private void AjouterFHF(object sender, EventArgs e)
        {
            ConnectionBDD();

            string Valeur = ValeurDuFrais.Text;

            try
            {
                montantfrais = Convert.ToDouble(Valeur); // on converti l'entrée de l'utilisateur en double
            }
            catch (Exception msg) // si ca ne fonctionne pas réinitialisation
            {
                MessageBox.Show("La valeur entrée est incorrecte, n'est attendu que les chiffres / nombres avec ou sans virgule.");
                ValeurDuFrais.Text = "";
            }

            try
            {
                if (montantfrais == 0 || motif_frais == "" || datefrais == "")
                {
                    MessageBox.Show("Certaines valeurs sont invalides, assurez vous d'avoir rempli correctement tout les chants.");
                }
                else
                {
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO `frais_hors_forfait` (nom, valeur, date_frais, id_fiche_frais) VALUES (@motif_frais, @Montant, @Datefrais, @id_FicheDeFrais);", Connection);

                    cmd.Parameters.AddWithValue("@Datefrais", datefrais);
                    cmd.Parameters.AddWithValue("@id_FicheDeFrais", id_FicheDeFrais);
                    cmd.Parameters.AddWithValue("@Montant", montantfrais);
                    cmd.Parameters.AddWithValue("@motif_frais", motif_frais);

                    cmd.ExecuteNonQuery();

                    this.Close();
                }
            }
            catch (Exception msg)
            {
                MessageBox.Show("Une erreur est survenue => " + msg.Message);
            }

            DeconnectionBDD();
        }
        #endregion
        //


        // 3 valeurs entrées par le visiteur pour son ajout  (Datefrais, Montantfrais) => La troisième entrée se fait sur l'ajout
        #region 

        // Contient le Motif
        private void TextMotif(object sender, EventArgs e)
        {
            motif_frais = TextMotifFrais.Text;
        }
        //

        // Contient la date ou le frais a été effectué
        private void SelectionDeValeurDate(object sender, EventArgs e)
        {
            ConnectionBDD();

            // Date actuel en yyyy MM dd

            DateTime DateChoisi = DateDuFrais.Value;

            MySqlCommand cmd = new MySqlCommand("" +
            "SELECT fdf.date_creation, fdf.date_fin " +
            "FROM `fiche_de_frais` fdf " +
            "WHERE fdf.id_fiche_frais = @id_FicheFrais ", Connection);

            cmd.Parameters.AddWithValue("@id_FicheFrais", id_FicheDeFrais);

            MySqlDataReader LecteurDonnee = cmd.ExecuteReader();

            if (LecteurDonnee.Read())
            {
                // On récupère les deux dates de fdf
                DateTime date_creation = LecteurDonnee.GetDateTime(LecteurDonnee.GetOrdinal("date_creation"));
                DateTime date_fin = LecteurDonnee.GetDateTime(LecteurDonnee.GetOrdinal("date_fin"));

                if (date_creation <= DateChoisi && DateChoisi <= date_fin)
                {
                    datefrais = DateChoisi.ToString("yyyy-MM-dd");
                }
                else
                {
                    MessageBox.Show("Veuillez selectionner une date comprise entre la date de début et la date de fin d'effet de la fiche de frais actuelle." + date_creation + " - " + date_fin + id_FicheDeFrais);
                    datefrais = "";
                }
            }

            DeconnectionBDD();
        }

        #endregion
        //
    }
}
