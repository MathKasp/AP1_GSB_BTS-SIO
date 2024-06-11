using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AP1_GSB_BTS_SIO.Log_in;

namespace AP1_GSB_BTS_SIO
{
    public partial class Visiteur : Form
    {
        private int idUtilisateur;
        public Visiteur(int idUtilisateur)
        {
            this.idUtilisateur = idUtilisateur;
            InitializeComponent();

            ConnectionBDD();

            MySqlCommand cmd = new MySqlCommand("SELECT fdf.id_fiche_frais FROM `fiche_de_frais` fdf ;", Connection);

            MySqlDataReader LecteurDonnee = cmd.ExecuteReader();

            while (LecteurDonnee.Read())
            {
                ListeIdFiches.Items.Add(LecteurDonnee["id_fiche_frais"].ToString());
            }
            DeconnectionBDD();
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

        // Fonction Actualiser (actualise la fiche de frais actuel)
        #region
        public void Actualiser()
        {
            ConnectionBDD();

            ListviewFrais.Items.Clear();

            // On Récupère donnee Frais Forfais 
            MySqlCommand cmd = new MySqlCommand("" +
                "SELECT ff.Valeur, ff.date_frais, tf.nom, ff.Motif " +
                "FROM fiche_de_frais fdf left join frais_forfait ff on fdf.id_fiche_frais = ff.id_fiche_frais " +
                "left join type_frais tf on ff.id_type = tf.id_type " +
                "WHERE fdf.id_utilisateur = 1 " +
                "AND fdf.date_creation <= DATE_FORMAT(NOW(), '%Y-%m-%d') <= fdf.date_fin;", Connection);

            cmd.Parameters.AddWithValue("@utilisateur", idUtilisateur);

            MySqlDataReader LecteurDonnee = cmd.ExecuteReader();

            while (LecteurDonnee.Read())
            {
                ListViewItem FraisForfait = new ListViewItem(

                LecteurDonnee["nom"].ToString());
                FraisForfait.SubItems.Add(LecteurDonnee["valeur"].ToString());
                FraisForfait.SubItems.Add(LecteurDonnee["date_frais"].ToString());
                FraisForfait.SubItems.Add(LecteurDonnee["Motif"].ToString());

                ListviewFrais.Items.Add(FraisForfait);
            }

            LecteurDonnee.Close();
            //

            ListViewHorsForfait.Items.Clear();

            // On récupère les donnee Frais Hors Forfait
            cmd = new MySqlCommand("" +
            "SELECT fhf.nom, fhf.valeur, fhf.date_frais " +
            "FROM `fiche_de_frais` fdf left join `frais_hors_forfait` fhf " +
            "on fhf.id_fiche_frais = fdf.id_fiche_frais " +
            "WHERE fdf.id_utilisateur = @utilisateur " +
            "AND fdf.date_creation <= DATE_FORMAT(NOW(), '%Y-%m-%d') <= fdf.date_fin;", Connection);

            cmd.Parameters.AddWithValue("@utilisateur", idUtilisateur);

            LecteurDonnee = cmd.ExecuteReader();

            while (LecteurDonnee.Read())
            {
                ListViewItem FraisHForfait = new ListViewItem(

                LecteurDonnee["nom"].ToString());
                FraisHForfait.SubItems.Add(LecteurDonnee["valeur"].ToString());
                FraisHForfait.SubItems.Add(LecteurDonnee["date_frais"].ToString());
                //FraisForfait.SubItems.Add(LecteurDonnee["Motif"].ToString());


                ListViewHorsForfait.Items.Add(FraisHForfait);
            }

            LecteurDonnee.Close();

            DeconnectionBDD();
            //
        }
        #endregion
        //


        // - Outillage des composants du form - //


        // Bouton simples
        #region
        // Fermer tout
        private void BoutonFermer(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //

        // Retour au log in
        private void boutonRetour(object sender, EventArgs e)
        {
            this.Close();
        }
        // 
        #endregion
        //

        // Bouton pour consulter la fiche ACTUELLE (+ affichage des dates de la fiche)
        #region
        private void Consulter(object sender, EventArgs e)
        {
            Actualiser();

            ConnectionBDD();

            // Affichage de la date debut / Fin de la fiche observé
            MySqlCommand cmd = new MySqlCommand("" +
            "SELECT fdf.date_creation, fdf.date_fin " +
            "FROM `fiche_de_frais` fdf WHERE date_creation <= DATE_FORMAT(NOW(), '%Y-%m-%d') <= date_fin " +
            "AND fdf.id_utilisateur = @utilisateur;", Connection);

            cmd.Parameters.AddWithValue("@utilisateur", idUtilisateur);

            MySqlDataReader LecteurDonnee = cmd.ExecuteReader();

            while (LecteurDonnee.Read())
            {
                string DateDeb = LecteurDonnee["date_creation"].ToString();
                string DateFinn = LecteurDonnee["date_fin"].ToString();
                    
                DateDepart.Text = DateDeb;
                DateFin.Text = DateFinn;
            }

            LecteurDonnee.Close();
            //

            DeconnectionBDD();  
        }
        #endregion
        //

        // Bouton qui affiche toutes les fiches de cet utilisateur
        #region
        private void Historique(object sender, EventArgs e)
        {
            ConnectionBDD();

            ListViewFichesUtiliateur.Items.Clear();

            MySqlCommand cmd = new MySqlCommand("SELECT fdf.date_creation, fdf.date_fin, fdf.id_fiche_frais, e.etat FROM `fiche_de_frais` fdf LEFT JOIN `etat` e ON fdf.id_etat = e.id_etat AND fdf.id_utilisateur = @utilisateur;", Connection);

            cmd.Parameters.AddWithValue("@utilisateur", idUtilisateur);

            MySqlDataReader LecteurDonnee = cmd.ExecuteReader();

            while (LecteurDonnee.Read())
            {
                ListViewItem FicheTotales = new ListViewItem(

                LecteurDonnee["etat"].ToString());
                FicheTotales.SubItems.Add(LecteurDonnee["date_creation"].ToString());
                FicheTotales.SubItems.Add(LecteurDonnee["date_fin"].ToString());
                FicheTotales.SubItems.Add(LecteurDonnee["id_fiche_frais"].ToString());

                ListViewFichesUtiliateur.Items.Add(FicheTotales);
            }

            DeconnectionBDD();
        }
        #endregion
        //

        // Bouton qui envoie sur l'espace Demande de Frais
        #region
        private void Demande_Frais(object sender, EventArgs e)
        {
            ConnectionBDD();

            MySqlCommand cmd = new MySqlCommand("" +
                "SELECT fdf.id_fiche_frais " +
                "FROM `fiche_de_frais` fdf " +
                "WHERE fdf.id_utilisateur = @utilisateur;", Connection); // faut récupérer l'id de la fiche de frai en utilisant l'id utilisateur 
            cmd.Parameters.AddWithValue("@utilisateur", idUtilisateur);

            MySqlDataReader LecteurDonnee = cmd.ExecuteReader();

            if (LecteurDonnee.Read()) 
            {
                int id_FicheDeFrais = LecteurDonnee.GetInt32("id_fiche_frais");
                AjoutFrais OuvreDemandeFrais = new AjoutFrais(id_FicheDeFrais);

                OuvreDemandeFrais.ShowDialog();
            }

            DeconnectionBDD();

            Actualiser();
        }
        #endregion
        //

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DateDepart_Click(object sender, EventArgs e)
        {
            MessageBox.Show("hoho surprise;");
        }

        private void ListViewToutesLesFiches(object sender, EventArgs e)
        {
            MessageBox.Show("double clicks !!");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
