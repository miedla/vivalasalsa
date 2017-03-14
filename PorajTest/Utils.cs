using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PorajTest
{
    public static class Utils
    {
        public static string conncection_string = "Server=sql.poraj.nazwa.pl; Database=poraj_29; Uid=poraj_29; Pwd=7Dfwn^#@sdh#@32; CharSet=utf8";

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
            }
            catch (MySqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        public static void UsunKlienta(string ean)//(int id)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(Utils.conncection_string);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                //cmd.CommandText = "DELETE FROM klienci WHERE id = @id;";
                cmd.CommandText = "DELETE FROM klienci WHERE ean = @ean;";
                //cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@ean", ean);

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

        public static void AddButtonToDataGridView(string colName, string btnText, int width, ref DataGridView dataGridView)
        {
            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.Name = colName;
            btnColumn.Text = btnText;
            btnColumn.HeaderText = "";
            btnColumn.Width = width;
            btnColumn.UseColumnTextForButtonValue = true;
            dataGridView.Columns.Add(btnColumn);
        }
    }

}
