using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AP1_GSB_BTS_SIO
{
    public partial class AjoutFrais : Form
    {
        int id_FicheDeFrais;
        public AjoutFrais(int id_FicheDeFrais)
        {
            this.id_FicheDeFrais = id_FicheDeFrais;
            InitializeComponent();

            ChoixTypeFrais.Items.Add("Salut c'est l'option 1 ca gaz");
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
        // Fermer l'appli
        private void Fermer(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //

        // Nouton retour
        private void Retour(object sender, EventArgs e)
        {
            this.Close();
        }
        //

        // valider les critères saisis
        private void Ajouter(object sender, EventArgs e)
        {
            MessageBox.Show("heyyyy wont you waiiiit for me");
        }
        //

        // Pop up fenêtre HF même principe
        private void Frais_HF(object sender, EventArgs e)
        {
            ConnectionBDD();

            AjoutFraisHF OuvreDemandeFraisHF = new AjoutFraisHF(id_FicheDeFrais);

            OuvreDemandeFraisHF.ShowDialog();

            DeconnectionBDD();
        }
        //

        // Contient la chaine de caractère qui va devenir le motif du Frais
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }



        // INfo pour la combo box

        //SqlCommand Comande = new SqlCommand("INSERT INTO Livres (Titre, Auteur, AnneePublication, CategorieID, EditeurID) VALUES (@Titre, @Auteur, @AnneePublication, @CategorieID, @EditeurID);", cnn);
        //Comande.Parameters.AddWithValue("@Titre", OuvreFenetre2.texttitre);
        //Comande.Parameters.AddWithValue("@Auteur", OuvreFenetre2.textauteur);
        //Comande.Parameters.AddWithValue("@AnneePublication", OuvreFenetre2.textannee);
        //Comande.Parameters.AddWithValue("@CategorieID", OuvreFenetre2.textcatégorie);
        //Comande.Parameters.AddWithValue("@EditeurID", OuvreFenetre2.textediteur);
        //Comande.ExecuteNonQuery();

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //
        #endregion
        // - //
    }
}
