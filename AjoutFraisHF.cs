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
        int id_FicheDeFrais;
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

        private void retourhf_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void quitterhf_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
