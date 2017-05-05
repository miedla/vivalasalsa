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
    public partial class DodajKlientaForm : Form
    {
        private Form1 mainForm;
        private int eanIndex;
        public DodajKlientaForm(Form1 mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.eanIndex = mainForm.dataGridViewKlienci.Rows.Count + 1;
            Debug.WriteLine("index: " + mainForm.dataGridViewKlienci.Rows.Count);
        }

        public static void DodajKlienta(string ean, string imie, string nazwisko, string email, string telefon)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(Utils.conncection_string);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO klienci(ean, imie, nazwisko, email, telefon) VALUES(@ean, @imie, @nazwisko, @email, @telefon) ON DUPLICATE KEY UPDATE ean = @ean, imie = @imie, nazwisko = @nazwisko, email = @email, telefon = @telefon;";
                cmd.Parameters.AddWithValue("@ean", ean);
                cmd.Parameters.AddWithValue("@imie", imie);
                cmd.Parameters.AddWithValue("@nazwisko", nazwisko);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@telefon", telefon);

                cmd.ExecuteNonQuery();
                conn.Close();
                //mainForm.RefreshKlientGridView("klienci");
                //this.Close();
            }catch(MySqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        public static void UsunKlienta(int id)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(Utils.conncection_string);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM klienci WHERE id = @id;";
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
                conn.Close();
                //mainForm.RefreshKlientGridView("klienci");
                //this.Close();
            }
            catch (MySqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        private void buttonDodaj_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textBoxImie.Text) && !string.IsNullOrEmpty(textBoxNazwisko.Text) 
                && !string.IsNullOrEmpty(textBoxEmail.Text) && !string.IsNullOrEmpty(textBoxTelefon.Text))
            {
                string eanCountryCode = Utils.eanCountryCode;
                string eanManuCode = Utils.eanManuCode;
                string eanProductCode = (eanIndex).ToString().PadLeft(5, '0');//(4, '0');

                Ean13 ean13 = new Ean13(eanCountryCode, eanManuCode, eanProductCode);
                string ean = ean13.CountryCode + ean13.ManufacturerCode + ean13.ProductCode + ean13.ChecksumDigit;
                Debug.WriteLine(ean);
                DodajKlienta(ean, textBoxImie.Text, textBoxNazwisko.Text, textBoxEmail.Text, textBoxTelefon.Text);
                mainForm.RefreshKlientGridView("klienci");
                this.Close();

            }
        }
    }
}
