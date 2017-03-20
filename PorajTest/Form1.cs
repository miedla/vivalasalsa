using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace PorajTest
{
    public partial class Form1 : Form
    {
        private struct TableMode
        {
            public bool isEditMode;
            public string tableName;
        }

        private TableMode tblMode;
        public Form1()
        {
            InitializeComponent();
            RefreshKlientGridView("klienci");

            if (buttonMode.Text.Equals("Edycja"))
            {
                dataGridViewKlienci.CellDoubleClick += new DataGridViewCellEventHandler(this.SelectedKlient);
                dataGridViewKlienci.ReadOnly = true;
            }

            tblMode = new TableMode();
            tblMode.isEditMode = false;
            tblMode.tableName = "klienci";

            dataGridViewKlienci.AllowUserToAddRows = false;
        }

        public void RefreshKlientGridView(string tableName)
        {
            using (MySqlConnection conn = new MySqlConnection(Utils.conncection_string))
            {
                try
                {
                    MySqlCommand cmd = conn.CreateCommand();
                    if (!tableName.Equals("klienci_kursy"))
                    {
                        cmd.CommandText = "SELECT * FROM " + tableName;
                    }else if (tableName.Equals("klienci_kursy"))
                    {
                        cmd.CommandText = "SELECT k.nazwisko as Nazwisko, k.imie as Imię, k.email as Email, k.telefon as Telefon, kur.nazwa as Grupa, kk.dzien as \"Dzień tygodnia\", kk.ilosc_wejsc as \"Ilość wejść\", kk.poczatek_karnetu as \"Początek karnetu\", kk.koniec_karnetu as \"Koniec karnetu\", kk.cena as Cena FROM `klienci_kursy` kk JOIN klienci k ON (k.id = kk.klient_id) JOIN kursy kur ON (kur.id = kk.kurs_id)";
                    }
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    if (dataGridViewKlienci.InvokeRequired)
                    {
                        dataGridViewKlienci.Invoke(new Action(() =>
                        {
                            dataGridViewKlienci.DataSource = ds.Tables[0].DefaultView;
                            if (dataGridViewKlienci.Columns["id"] != null)
                            {
                                dataGridViewKlienci.Columns["id"].Visible = false;
                            }
                            if (dataGridViewKlienci.Columns["ean"] != null)
                            {
                                dataGridViewKlienci.Columns["ean"].Visible = false;
                            }
                        }));
                    }
                    else
                    {
                        dataGridViewKlienci.DataSource = ds.Tables[0].DefaultView;
                        if (dataGridViewKlienci.Columns["id"] != null)
                        {
                            dataGridViewKlienci.Columns["id"].Visible = false;
                        }
                        if (dataGridViewKlienci.Columns["ean"] != null)
                        {
                            dataGridViewKlienci.Columns["ean"].Visible = false;
                        }
                    }

                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        conn.Open();
                        Debug.WriteLine("ds.Tables == null");
                        cmd.CommandText = "ALTER TABLE " + tableName + " AUTO_INCREMENT = 1";
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        conn.Open();
                        cmd.CommandText = "ALTER TABLE " + tableName + " AUTO_INCREMENT = @value";
                        cmd.Parameters.AddWithValue("@value", ds.Tables[0].Rows.Count);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        Debug.WriteLine("ds.Tables !!!!!= null");
                    }

                }
                catch(MySqlException e)
                {
                    Debug.WriteLine(e.ToString());
                }
                conn.Close();
            }
        }

        private void SelectedKlient(object sender, DataGridViewCellEventArgs e)
        {
            if (tblMode.tableName.Equals("klienci"))
            {
                try
                {
                    Debug.WriteLine("sk: " + dataGridViewKlienci.Rows[e.RowIndex].Cells[0].Value.ToString());
                    int id = int.Parse(dataGridViewKlienci.Rows[e.RowIndex].Cells["id"].Value.ToString());
                    string imie = dataGridViewKlienci.Rows[e.RowIndex].Cells["imie"].Value.ToString();
                    string nazwisko = dataGridViewKlienci.Rows[e.RowIndex].Cells["nazwisko"].Value.ToString();
                    string email = dataGridViewKlienci.Rows[e.RowIndex].Cells["email"].Value.ToString();
                    string telefon = dataGridViewKlienci.Rows[e.RowIndex].Cells["telefon"].Value.ToString();
                    string ean = dataGridViewKlienci.Rows[e.RowIndex].Cells["ean"].Value.ToString();

                    new KlientDetailsForm(id, imie, nazwisko, email, telefon, ean).Show();
                }catch(Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
        }

        private void buttonDodajKlienta_Click(object sender, EventArgs e)
        {
            new DodajKlientaForm(this).Show();
        }

        private void buttonDodajKurs_Click(object sender, EventArgs e)
        {
            new DodajKursForm(this).Show();
        }

        private void EdycjaMode()
        {
            Utils.AddButtonToDataGridView("Zmień", "Zapisz", 100, ref dataGridViewKlienci);
            Utils.AddButtonToDataGridView("Usuń", "Usuń", 100, ref dataGridViewKlienci);

            dataGridViewKlienci.CellDoubleClick -= new DataGridViewCellEventHandler(this.SelectedKlient);
            dataGridViewKlienci.ReadOnly = false;
            dataGridViewKlienci.AllowUserToAddRows = true;

            tblMode.isEditMode = false;

            buttonMode.Text = "Wyświetlanie";
        }

        private void WyswietlanieMode()
        {
            try
            {
                dataGridViewKlienci.Columns.RemoveAt(dataGridViewKlienci.Columns.IndexOf(dataGridViewKlienci.Columns["Zmień"]));
                dataGridViewKlienci.Columns.RemoveAt(dataGridViewKlienci.Columns.IndexOf(dataGridViewKlienci.Columns["Usuń"]));

                dataGridViewKlienci.CellDoubleClick += new DataGridViewCellEventHandler(this.SelectedKlient);
                dataGridViewKlienci.ReadOnly = true;
                dataGridViewKlienci.AllowUserToAddRows = false;

                buttonMode.Text = "Edycja";
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void buttonMode_Click(object sender, EventArgs e)
        {
            if (buttonMode.Text.Equals("Edycja") && !tblMode.tableName.Equals("klienci_kursy"))
            {
                //Utils.AddButtonToDataGridView("Zmień", "Zapisz", 100, ref dataGridViewKlienci);
                //Utils.AddButtonToDataGridView("Usuń", "Usuń", 100, ref dataGridViewKlienci);

                //dataGridViewKlienci.CellDoubleClick -= new DataGridViewCellEventHandler(this.SelectedKlient);
                //dataGridViewKlienci.ReadOnly = false;
                //dataGridViewKlienci.AllowUserToAddRows = true;

                //tblMode.isEditMode = false;

                //buttonMode.Text = "Wyświetlanie";

                EdycjaMode();

                return;
            }

            if (buttonMode.Text.Equals("Wyświetlanie"))
            {
                //dataGridViewKlienci.Columns.RemoveAt(dataGridViewKlienci.Columns.IndexOf(dataGridViewKlienci.Columns["Zmień"]));
                //dataGridViewKlienci.Columns.RemoveAt(dataGridViewKlienci.Columns.IndexOf(dataGridViewKlienci.Columns["Usuń"]));

                //dataGridViewKlienci.CellDoubleClick += new DataGridViewCellEventHandler(this.SelectedKlient);
                //dataGridViewKlienci.ReadOnly = true;
                //dataGridViewKlienci.AllowUserToAddRows = false;

                //buttonMode.Text = "Edycja";

                WyswietlanieMode();

                return;
            }
        }

        private void dataGridViewKlienci_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonKlienciKursy_Click(object sender, EventArgs e)
        {
            RefreshKlientGridView("klienci_kursy");
            tblMode.tableName = "klienci_kursy";

            if (!tblMode.isEditMode)
            {
                WyswietlanieMode();
            }
        }

        private void buttonKursy_Click(object sender, EventArgs e)
        {
            RefreshKlientGridView("kursy");
            tblMode.tableName = "kursy";
            if (dataGridViewKlienci.Columns["Zmień"] != null)
            {
                dataGridViewKlienci.Columns["Zmień"].DisplayIndex = dataGridViewKlienci.Columns.Count - 1;
                dataGridViewKlienci.Columns["Zmień"].Width = 100;

                dataGridViewKlienci.Columns["Usuń"].DisplayIndex = dataGridViewKlienci.Columns.Count - 1;
                dataGridViewKlienci.Columns["Usuń"].Width = 100;
            }
        }

        private void buttonKlienci_Click(object sender, EventArgs e)
        {
            RefreshKlientGridView("klienci");
            tblMode.tableName = "klienci";
            if (dataGridViewKlienci.Columns["Zmień"] != null)
            {
                dataGridViewKlienci.Columns["Zmień"].DisplayIndex = dataGridViewKlienci.Columns.Count - 1;
                dataGridViewKlienci.Columns["Zmień"].Width = 100;

                dataGridViewKlienci.Columns["Usuń"].DisplayIndex = dataGridViewKlienci.Columns.Count - 1;
                dataGridViewKlienci.Columns["Usuń"].Width = 100;
            }
        }

        private void dataGridKlient_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                if (tblMode.tableName.Equals("klienci"))
                {
                    if (!string.IsNullOrEmpty(senderGrid.Rows[e.RowIndex].Cells["nazwisko"].Value.ToString()) && !string.IsNullOrEmpty(senderGrid.Rows[e.RowIndex].Cells["imie"].Value.ToString())
                        && !string.IsNullOrEmpty(senderGrid.Rows[e.RowIndex].Cells["email"].Value.ToString()) && !string.IsNullOrEmpty(senderGrid.Rows[e.RowIndex].Cells["telefon"].Value.ToString()))
                    {
                        string ean = senderGrid.Rows[e.RowIndex].Cells["ean"].Value.ToString();
                        if (string.IsNullOrEmpty(ean))
                        {
                            string eanCountryCode = Utils.eanCountryCode;
                            string eanManuCode = Utils.eanManuCode;
                            string eanProductCode = (senderGrid.Rows.Count - 1).ToString().PadLeft(5, '0');//(4, '0');

                            Ean13 ean13 = new Ean13(eanCountryCode, eanManuCode, eanProductCode);
                            ean = ean13.CountryCode + ean13.ManufacturerCode + ean13.ProductCode + ean13.ChecksumDigit;

                            Debug.WriteLine("ean: "+ean);
                        }

                        //int id;
                        string imie = senderGrid.Rows[e.RowIndex].Cells["imie"].Value.ToString();
                        string nazwisko = senderGrid.Rows[e.RowIndex].Cells["nazwisko"].Value.ToString();
                        string email = senderGrid.Rows[e.RowIndex].Cells["email"].Value.ToString();
                        string telefon = senderGrid.Rows[e.RowIndex].Cells["telefon"].Value.ToString();

                        DataGridViewButtonColumn btnCol = senderGrid.Columns[e.ColumnIndex] as DataGridViewButtonColumn;
                        if (btnCol.Text.Equals("Zapisz"))
                        {
                            Debug.WriteLine("Dodaj klienta");
                            Utils.DodajKlienta(ean, imie, nazwisko, email, telefon);
                            RefreshKlientGridView(tblMode.tableName);
                        }else if (btnCol.Text.Equals("Usuń")) //&& int.TryParse(senderGrid.Rows[e.RowIndex].Cells["id"].Value.ToString(), out id))
                        {
                            Debug.WriteLine("Usun klienta");
                                //Utils.UsunKlienta(id);
                                Utils.UsunKlienta(ean);
                                RefreshKlientGridView(tblMode.tableName);
                        }
                    }
                }
                if (tblMode.tableName.Equals("kursy"))
                {
                    float cena;
                    string nazwa = senderGrid.Rows[e.RowIndex].Cells["nazwa"].Value.ToString();
                    if (!string.IsNullOrEmpty(nazwa) && float.TryParse(senderGrid.Rows[e.RowIndex].Cells["cena"].Value.ToString(), out cena))
                    {
                        DataGridViewButtonColumn btnCol = senderGrid.Columns[e.ColumnIndex] as DataGridViewButtonColumn;
                        if (btnCol.Text.Equals("Zapisz"))
                        {
                            Debug.WriteLine("Dodaj kurs");
                            int id = int.Parse(senderGrid.Rows[e.RowIndex].Cells["id"].Value.ToString());
                            Utils.DodajKurs(nazwa, cena);
                            RefreshKlientGridView(tblMode.tableName);
                        }
                        else if (btnCol.Text.Equals("Usuń")) //&& int.TryParse(senderGrid.Rows[e.RowIndex].Cells["id"].Value.ToString(), out id))
                        {
                            Debug.WriteLine("Usun klienta");
                            int id = int.Parse(senderGrid.Rows[e.RowIndex].Cells["id"].Value.ToString());
                            Utils.UsunKurs(id);
                            RefreshKlientGridView(tblMode.tableName);
                        }
                    }
                }
                if (tblMode.tableName.Equals("klienci_kursy"))
                {

                }
            }
        }

        private void textBoxEan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string ean = textBoxEan.Text;
                string eanProductCode = ean.Substring(ean.Length - 5, 4);
                Debug.WriteLine("eanProductCode: " + eanProductCode);
                DataGridViewRow row = dataGridViewKlienci.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["ean"].Value.ToString().Equals(ean)).FirstOrDefault();
                Debug.WriteLine("search row: " + row);
                dataGridViewKlienci.ClearSelection();

                if (row != null)
                {
                    row.Selected = true;
                    dataGridViewKlienci.FirstDisplayedScrollingRowIndex = dataGridViewKlienci.SelectedRows[0].Index;
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
           
        }
    }
}
