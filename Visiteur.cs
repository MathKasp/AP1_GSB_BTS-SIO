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


        // Outillage des composants du form //

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

        // Bouton pour consulter la fiche ACTUELLE
        #region
        private void Consulter(object sender, EventArgs e)
        {
            ConnectionBDD();

            ListviewFrais.Items.Clear();

            // On Récupère donnee Frais Forfais 
            MySqlCommand cmd = new MySqlCommand("" +
                "SELECT ff.valeur_total, ff.date_frais, tf.nom " +
                "FROM `fiche_de_frais` fdf left join `frais_forfait` ff " +
                "on fdf.id_fiche_frais = ff.id_fiche_frais left join `type_frais` tf " +
                "on ff.id_type = tf.id_type WHERE fdf.id_utilisateur = @utilisateur " +
                "AND fdf.date_creation <= DATE_FORMAT(NOW(), '%Y-%m-%d') <= fdf.date_fin;", Connection);

            cmd.Parameters.AddWithValue("@utilisateur", idUtilisateur);

            MySqlDataReader LecteurDonnee = cmd.ExecuteReader();

            while (LecteurDonnee.Read())
            {
                ListViewItem FraisForfait = new ListViewItem(

                LecteurDonnee["nom"].ToString());
                FraisForfait.SubItems.Add(LecteurDonnee["valeur_total"].ToString());
                FraisForfait.SubItems.Add(LecteurDonnee["date_frais"].ToString());

                ListviewFrais.Items.Add(FraisForfait);
            }

            LecteurDonnee.Close();
            //

            ListViewHorsForfait.Items.Clear ();

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

                ListViewHorsForfait.Items.Add(FraisHForfait);
            }

            LecteurDonnee.Close();

            //

            // Affichage de la date debut / Fin de la fiche observé
            cmd = new MySqlCommand("" +
            "SELECT fdf.date_creation, fdf.date_fin " +
            "FROM `fiche_de_frais` fdf WHERE date_creation <= DATE_FORMAT(NOW(), '%Y-%m-%d') <= date_fin " +
            "AND fdf.id_utilisateur = @utilisateur;", Connection);

            cmd.Parameters.AddWithValue("@utilisateur", idUtilisateur);

            LecteurDonnee = cmd.ExecuteReader();

            if (LecteurDonnee.Read())
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
        // - //

        private void Historique(object sender, EventArgs e)
        {
            ConnectionBDD();



            DeconnectionBDD();
        }

        private void Demande_Frais(object sender, EventArgs e)
        {

        }

        private void Visiteur_Load(object sender, EventArgs e)
        {
            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DateDepart_Click(object sender, EventArgs e)
        {
            MessageBox.Show("fghj;");
        }
    }
}
