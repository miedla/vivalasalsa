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
using System.Diagnostics;

namespace PorajTest
{
    public partial class DodajKursForm : Form
    {
        private Form1 mainForm;
        public DodajKursForm(Form1 mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        void DodajKurs(string nazwa)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(Utils.conncection_string);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO kursy(nazwa) VALUES(@nazwa);";
                cmd.Parameters.AddWithValue("@nazwa", nazwa);

                cmd.ExecuteNonQuery();
                conn.Close();
                mainForm.RefreshKlientGridView("kursy");
                this.Close();
            }
            catch (MySqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        private void buttonDodaj_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxKurs.Text))
            {
                DodajKurs(textBoxKurs.Text);
            }
        }
    }
}
