using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AP1_GSB_BTS_SIO
{
    public partial class AjoutFrais : Form
    {


        // Variables par défauts


        int id_FicheDeFrais;

        // vars pour ajouter
        string motif_frais = "";

        string type_f = ""; // sert a récup id_type
        string type_id = ""; // id_type va ici 

        string Datefrais = "tout ca c'est de ma faute";
        //


        public AjoutFrais(int id_FicheDeFrais)
        {
            this.id_FicheDeFrais = id_FicheDeFrais;
            InitializeComponent();

            // remplissage du combobox sur frais 
            #region
            ConnectionBDD();

            MySqlCommand cmd = new MySqlCommand("SELECT tf.nom FROM `type_frais`tf;", Connection);

            MySqlDataReader LecteurDonnee = cmd.ExecuteReader();

            while (LecteurDonnee.Read())
            {
                ChoixTypeFrais.Items.Add(LecteurDonnee["nom"].ToString());
            }
            DeconnectionBDD();
            #endregion
            //
        }

        // Option co et deco de la bdd
        #region
        public MySqlConnection Connection;

        private void ConnectionBDD()
        {
            try
            {
                string connectionString = "server=127.0.0.1;uid=root;pwd=root;database=ap1_sql";
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

        //  Outillage des composants du form //
        #region


        // Fermer l'appli/ retour
        #region
        private void Fermer(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Retour(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        //

        // valider les critères saisis
        #region
        private void Ajouter(object sender, EventArgs e)
        {
            ConnectionBDD();

            try
            {
                if (type_f == "" || motif_frais == "" || Datefrais == "")
                {
                    MessageBox.Show("Certaines valeurs essentiels sont manquantes, assurez vous d'avoir rempli correctement tout les chants.");
                }
                else
                {
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO `frais_forfait` (date_frais, id_fiche_frais, id_type, Motif) VALUES (@Datefrais, @id_FicheDeFrais, @type_id, @motif_frais);", Connection);

                    cmd.Parameters.AddWithValue("@Datefrais", Datefrais);
                    cmd.Parameters.AddWithValue("@id_FicheDeFrais", id_FicheDeFrais);
                    cmd.Parameters.AddWithValue("@type_id", type_id);
                    cmd.Parameters.AddWithValue("@motif_frais", motif_frais);

                    cmd.ExecuteNonQuery();
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

        // redirection vers fenêtre frais HF (même principe)
        private void Frais_HF(object sender, EventArgs e)
        {
            ConnectionBDD();

            AjoutFraisHF OuvreDemandeFraisHF = new AjoutFraisHF(id_FicheDeFrais);

            OuvreDemandeFraisHF.ShowDialog();

            DeconnectionBDD();
        }
        //

        // 3 valeurs entrées par le visiteur pour son ajout ( motif_frais, type_f, Datefrais )
        #region
        // Contient la chaine de caractère qui va devenir le motif du Frais 
        private void TextMotif(object sender, EventArgs e)
        {
            motif_frais = TextMotifFrais.Text;
        }
        //

        // Combox box du type de Frais 
        private void Type_Frais (object sender, EventArgs e)
        {
            ConnectionBDD();

            type_f = ChoixTypeFrais.Text;

            // requete pour récupérer la valeur qui va avec le type de frais
            MySqlCommand cmd = new MySqlCommand("" +
                "SELECT tf.id_type FROM `type_frais`tf " +
                "LEFT JOIN `frais_forfait` ff ON ff.id_type = tf.id_type " +
                "WHERE ff.id_fiche_frais = @id_FicheFrais AND tf.nom = @type_f;", Connection);

            cmd.Parameters.AddWithValue("@id_FicheFrais", id_FicheDeFrais);
            cmd.Parameters.AddWithValue("@type_f", type_f);

            MySqlDataReader LecteurDonnee = cmd.ExecuteReader();

            if (LecteurDonnee.Read())
            {
                type_id = LecteurDonnee["id_type"].ToString();
            }

            DeconnectionBDD();
        }
        //

        // TimePicker de la date du Frais 
        public void SelectionDeValeurDate(object sender, EventArgs e)
        {
            ConnectionBDD();

            DateTime DateChoisi = DateFrais.Value;

            MySqlCommand cmd = new MySqlCommand("" +
            "SELECT fdf.date_creation, fdf.date_fin " +
            "FROM `fiche_de_frais` fdf " +
            "WHERE fdf.id_fiche_frais = @id_FicheFrais " +
            "AND fdf.date_creation <= DATE_FORMAT(NOW(), '%Y-%m-%d')<= fdf.date_fin;", Connection);

            cmd.Parameters.AddWithValue("@id_FicheFrais", id_FicheDeFrais);

            MySqlDataReader LecteurDonnee = cmd.ExecuteReader();

            if (LecteurDonnee.Read())
            {
                // On récupère les deux dates de fdf
                DateTime date_creation = LecteurDonnee.GetDateTime(LecteurDonnee.GetOrdinal("date_creation"));
                DateTime date_fin = LecteurDonnee.GetDateTime(LecteurDonnee.GetOrdinal("date_fin"));

                if (date_creation <= DateChoisi && DateChoisi <= date_fin)
                {
                    Datefrais = DateChoisi.ToString("yyyy-MM-dd");
                    MessageBox.Show(Datefrais);
                }
                else
                {
                    MessageBox.Show("Veuillez selectionner une date comprise entre la date de début et la date de fin d'effet de la fiche de frais actuelle.");
                    Datefrais = "";
                }
            }

            DeconnectionBDD();
        }
        //
        #endregion
        //


        #endregion
        // - //
    }
}
