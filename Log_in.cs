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
using MySql.Data;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;

namespace AP1_GSB_BTS_SIO
{
    public partial class Log_in : Form
    {
        // Option Co et Deco de la  BDD
        #region
        public MySqlConnection Connection;
        public Log_in()
        {
            InitializeComponent();
        }

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

        // Outillage des composants du form 

        // Variables par défault de la selection de l'utilisateur
        public string nom = "";
        public string mdp = "";
        //

        // Validation NOM / MDP du log in
        #region
        private void BoutonValider(object sender, EventArgs e)
        {
            ConnectionBDD();

            MySqlCommand cmd = new MySqlCommand("" +
                "SELECT * FROM `utilisateur` " +
                "WHERE nom = '" + nom +"' AND mdp = '" +mdp+"';", Connection);

            MySqlDataReader Utilisateur = cmd.ExecuteReader();
            if (Utilisateur.Read())
            {
                int idUtilisateur = Utilisateur.GetInt32("id_utilisateur");
                int idRole = Utilisateur.GetInt32("id_role");

                switch (idRole)
                {
                    case 1 :

                        Visiteur OuvreEspaceVisiteur = new Visiteur(idUtilisateur);
                        OuvreEspaceVisiteur.ShowDialog();

                        break;

                    case 2:

                        Comptable OuvreEspaceComptable = new Comptable();
                        OuvreEspaceComptable.ShowDialog();

                        break;

                    case 3:

                        Administrateur OuvreEspaceAdmin = new Administrateur();
                        OuvreEspaceAdmin.ShowDialog();

                        break;
                }
            }

            DeconnectionBDD();
            #endregion
        }
        //

        // Outillage du form
        #region

        // contient NOM
        private void TextIdentifiant(object sender, EventArgs e)
        {
            nom = textnom.Text;
        }

        // contient MDP
        private void TextMDP(object sender, EventArgs e)
        {
            mdp = textmdpp.Text;
        }

        // Bouton pour fermer l'appli
        private void button_Close(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion
        //
    }
}
