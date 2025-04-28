using AP1_GSB_BTS_SIO;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;
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
    public partial class Comptable : Form
    {

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

        public Comptable()
        {
            InitializeComponent();

            // Remplissage des Combo box :
            #region

            // Combobox des ID de fiche
            ConnectionBDD();
            MySqlCommand cmd = new MySqlCommand("SELECT fdf.id_fiche_frais FROM `fiche_de_frais` fdf WHERE fdf.id_etat != 2 ;", Connection);

            MySqlDataReader LecteurDonnee = cmd.ExecuteReader();

            while (LecteurDonnee.Read())
            {
                ListeIdFiches.Items.Add(LecteurDonnee["id_fiche_frais"].ToString());
                ChoixID.Items.Add(LecteurDonnee["id_fiche_frais"].ToString());
            }
            DeconnectionBDD();
            //

            // Combobox des état de fiche
            ConnectionBDD();
            cmd = new MySqlCommand("SELECT e.etat FROM `etat` e WHERE e.etat != 'EN COURS';", Connection);

            LecteurDonnee = cmd.ExecuteReader();

            while (LecteurDonnee.Read())
            {
                ChoixEtat.Items.Add(LecteurDonnee["etat"].ToString());
            }
            DeconnectionBDD();
            //

            // Combobox des utilisateurs
            ConnectionBDD();
            cmd = new MySqlCommand("SELECT u.nom FROM `utilisateur` u WHERE u.id_role = 1;", Connection);

            LecteurDonnee = cmd.ExecuteReader();

            while (LecteurDonnee.Read())
            {
                TriUtilisateur.Items.Add(LecteurDonnee["nom"].ToString());
            }
            DeconnectionBDD();

            TriUtilisateur.Items.Add("General");
            //

            #endregion
            //

            Actualiser();
        }

        // Fonction Actualiser
        #region
        public void Actualiser()
        {

            ConnectionBDD();

            TTEFicheFrais.Items.Clear();

            MySqlCommand cmd = new MySqlCommand("" +
                "SELECT fdf.id_fiche_frais, e.etat, u.nom, fdf.date_creation, fdf.date_fin " +
                "FROM fiche_de_frais fdf " +
                "LEFT JOIN `utilisateur` u ON u.id_utilisateur = fdf.id_utilisateur " +
                "LEFT JOIN `etat` e ON e.id_etat = fdf.id_etat;", Connection);

            MySqlDataReader LecteurDonnee = cmd.ExecuteReader();

            while (LecteurDonnee.Read())
            {
                ListViewItem ObjetFicheFrais = new ListViewItem(

                LecteurDonnee["id_fiche_frais"].ToString());
                ObjetFicheFrais.SubItems.Add(LecteurDonnee["etat"].ToString());
                ObjetFicheFrais.SubItems.Add(LecteurDonnee["nom"].ToString());
                ObjetFicheFrais.SubItems.Add(LecteurDonnee["date_creation"].ToString());
                ObjetFicheFrais.SubItems.Add(LecteurDonnee["date_fin"].ToString());

                TTEFicheFrais.Items.Add(ObjetFicheFrais);
            }

            DeconnectionBDD();
        }
        #endregion
        //


        // Boutons simples
        #region

        private void Deconnexion(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BoutonFermer(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion
        //

        // Tri par Utilisateur
        #region
        private void TriUtilisateurChoisi(object sender, EventArgs e)
        {

            string UtilisateurChoisi = TriUtilisateur.Text;

            TTEFicheFrais.Items.Clear();

            ConnectionBDD();

            // Si utilisateur veut afficher toutes les fiches a nouveau
            if (UtilisateurChoisi == "General")
            {
                MySqlCommand cmdgen = new MySqlCommand("" +
                "SELECT fdf.id_fiche_frais, e.etat, u.nom, fdf.date_creation, fdf.date_fin " +
                "FROM fiche_de_frais fdf " +
                "LEFT JOIN `utilisateur` u ON u.id_utilisateur = fdf.id_utilisateur " +
                "LEFT JOIN `etat` e ON e.id_etat = fdf.id_etat;", Connection);

                MySqlDataReader LecteurDonnee = cmdgen.ExecuteReader();

                while (LecteurDonnee.Read())
                {
                    ListViewItem ObjetFicheFrais = new ListViewItem(

                    LecteurDonnee["id_fiche_frais"].ToString());
                    ObjetFicheFrais.SubItems.Add(LecteurDonnee["etat"].ToString());
                    ObjetFicheFrais.SubItems.Add(LecteurDonnee["nom"].ToString());
                    ObjetFicheFrais.SubItems.Add(LecteurDonnee["date_creation"].ToString());
                    ObjetFicheFrais.SubItems.Add(LecteurDonnee["date_fin"].ToString());

                    TTEFicheFrais.Items.Add(ObjetFicheFrais);
                }
            }
            //
            else
            {
                MySqlCommand cmd = new MySqlCommand("" +
                "SELECT fdf.id_fiche_frais, e.etat, u.nom, fdf.date_creation, fdf.date_fin " +
                "FROM fiche_de_frais fdf " +
                "LEFT JOIN `utilisateur` u ON u.id_utilisateur = fdf.id_utilisateur " +
                "LEFT JOIN `etat` e ON e.id_etat = fdf.id_etat " +
                "WHERE u.nom = @utilisateur;", Connection);

                cmd.Parameters.AddWithValue("@utilisateur", UtilisateurChoisi); 
                
                MySqlDataReader LecteurDonnee = cmd.ExecuteReader();

                while (LecteurDonnee.Read())
                {
                    ListViewItem ObjetFicheFrais = new ListViewItem(

                    LecteurDonnee["id_fiche_frais"].ToString());
                    ObjetFicheFrais.SubItems.Add(LecteurDonnee["etat"].ToString());
                    ObjetFicheFrais.SubItems.Add(LecteurDonnee["nom"].ToString());
                    ObjetFicheFrais.SubItems.Add(LecteurDonnee["date_creation"].ToString());
                    ObjetFicheFrais.SubItems.Add(LecteurDonnee["date_fin"].ToString());

                    TTEFicheFrais.Items.Add(ObjetFicheFrais);
                }

                DeconnectionBDD();
            }
        }
        #endregion
        //

        // Pour afficher les détails d'une fiche
        #region
        private void ListeIdFiches_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConnectionBDD();

            string selection = ListeIdFiches.Text;

            ListeFraisForfait.Items.Clear();

            // On Récupère donnee Frais Forfais de la fiche selectionné

            MySqlCommand cmd = new MySqlCommand("" +
            "SELECT ff.Valeur, ff.date_frais, tf.nom, ff.Motif " +
            "FROM `fiche_de_frais` fdf " +
            "left join `frais_forfait` ff on fdf.id_fiche_frais = ff.id_fiche_frais " +
            "left join `type_frais` tf on ff.id_type = tf.id_type " +
            "WHERE fdf.id_fiche_frais = @IDselection", Connection);

            cmd.Parameters.AddWithValue("@IDselection", selection);

            MySqlDataReader LecteurDonnee = cmd.ExecuteReader();

            while (LecteurDonnee.Read())
            {
                ListViewItem FraisForfait = new ListViewItem(

                LecteurDonnee["nom"].ToString());
                FraisForfait.SubItems.Add(LecteurDonnee["Valeur"].ToString());
                FraisForfait.SubItems.Add(LecteurDonnee["date_frais"].ToString());
                FraisForfait.SubItems.Add(LecteurDonnee["Motif"].ToString());

                ListeFraisForfait.Items.Add(FraisForfait);
            }

            LecteurDonnee.Close();

            ListeFraisHorsForfait.Items.Clear();

            // On récupère les donnee Frais Hors Forfait de la fiche selectionné

            cmd = new MySqlCommand("" +
            "SELECT fhf.nom, fhf.valeur, fhf.date_frais " +
            "FROM `fiche_de_frais` fdf " +
            "left join `frais_hors_forfait` fhf on fhf.id_fiche_frais = fdf.id_fiche_frais " +
            "WHERE fdf.id_fiche_frais = @IDselection", Connection);

            cmd.Parameters.AddWithValue("@IDselection", selection);

            try
            {
                LecteurDonnee = cmd.ExecuteReader();

                while (LecteurDonnee.Read())
                {
                    ListViewItem FraisHForfait = new ListViewItem(

                    LecteurDonnee["nom"].ToString());
                    FraisHForfait.SubItems.Add(LecteurDonnee["valeur"].ToString());
                    FraisHForfait.SubItems.Add(LecteurDonnee["date_frais"].ToString());

                    ListeFraisHorsForfait.Items.Add(FraisHForfait);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Aucun éléments");
            }

            LecteurDonnee.Close();

            DeconnectionBDD();
        }
        #endregion
        //

        // Valider les élèments pour la modification
        #region
        private void ValiderModification(object sender, EventArgs e)
        {
            ConnectionBDD();

            int selectionID = int.Parse(ChoixID.Text);
            int selectionEtat = 1;
            string EtatChoisit = ChoixEtat.Text;

            switch (EtatChoisit)
            {
                case "EN ATTENTE":
                    selectionEtat = 1;
                    break;

                case "VALIDER":
                    selectionEtat = 3;
                    break;

                case "REFUSER":
                    selectionEtat = 4;
                    break;
            }

            MySqlCommand cmd = new MySqlCommand("" +
                "UPDATE `fiche_de_frais` fdf SET id_etat = @selecEtat " +
                "WHERE fdf.id_fiche_frais = @selecID;", Connection);

            cmd.Parameters.AddWithValue("@selecID",selectionID);
            cmd.Parameters.AddWithValue("@selecEtat", selectionEtat);

            cmd.ExecuteNonQuery();

            DeconnectionBDD();

            Actualiser();
        }
        #endregion
        //
    }

}


