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
    public partial class KlientDetailsForm : Form
    {
        private int id;
        private string imie;
        private string nazwisko;
        private string email;
        private string telefon;
        private string ean;
        private List<string> kursyNazwaList;
        private string[] dniTygodnia = { "Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota" };
        private DateTimePicker dtpRozp;
        private DateTimePicker dtpKonc;
        private int[] dateColumns = { 8, 9 };//{ 10, 11 };
        private Dictionary<int, string> kursyNazwaDict;

        DataGridViewComboBoxColumn comboBoxColumnKursyNazwa;
        DataGridViewComboBoxColumn comboBoxColumnDniTygodnia;
        public KlientDetailsForm(int id, string imie, string nazwisko, string email, string telefon, string ean)
        {
            InitializeComponent();
            labelKlient.Text = imie + " " + nazwisko + " " + email + " " + telefon;

            this.id = id;
            this.imie = imie;
            this.nazwisko = nazwisko;
            this.email = email;
            this.telefon = telefon;
            this.ean = ean;

            kursyNazwaList = new List<string>();

            comboBoxColumnKursyNazwa = PobierzKursyNazwa();
            comboBoxColumnDniTygodnia = new DataGridViewComboBoxColumn();
            comboBoxColumnDniTygodnia.Items.AddRange(dniTygodnia);
            comboBoxColumnDniTygodnia.Name = "Dzień tygodnia";
            comboBoxColumnDniTygodnia.DataPropertyName = "Dzień tygodnia";

            PobierzKursyKlienta(comboBoxColumnKursyNazwa, comboBoxColumnDniTygodnia);

            SetDatePicker(ref dtpRozp);
            SetDatePicker(ref dtpKonc);

            dataGridViewKlientKursy.AllowUserToAddRows = false;
            Debug.WriteLine("gridview column count: "+dataGridViewKlientKursy.Columns.Count);
        }

        private void SetDatePicker(ref DateTimePicker dtPicker)
        {
            dtPicker = new DateTimePicker();
            dtPicker.Format = DateTimePickerFormat.Short;
            dtPicker.Visible = false;
            dtPicker.Width = 100;
            dataGridViewKlientKursy.Controls.Add(dtPicker);
        }

        private DataGridViewComboBoxColumn PobierzKursyNazwa()
        {
            DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn();

            using (MySqlConnection conn = new MySqlConnection(Utils.conncection_string))
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM kursy";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                comboBoxColumn.DataSource = ds.Tables[0].AsEnumerable().Select(row => row.Field<string>("nazwa")).ToList();
                comboBoxColumn.Name = "Grupa";
                comboBoxColumn.DataPropertyName = "Grupa";

                kursyNazwaDict = ds.Tables[0].AsEnumerable().Select(r => r).ToDictionary(r=>r.Field<int>("id"), r=>r.Field<string>("nazwa"));
                Debug.WriteLine("kursyNazwDict: "+ kursyNazwaDict.FirstOrDefault(x=> x.Value=="Zumba").Key);
            }

            return comboBoxColumn;
        }

        private void PobierzKursyKlienta(DataGridViewComboBoxColumn kursyComboBoxColumn, DataGridViewComboBoxColumn comboBoxColumnDniTygodnia)
        {
            using (MySqlConnection conn = new MySqlConnection(Utils.conncection_string))
            {
                try
                {
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT k.nazwisko as Nazwisko, k.imie as Imię, k.email as Email, k.telefon as Telefon, kur.nazwa as Grupa, kk.dzien as \"Dzień tygodnia\", kk.ilosc_wejsc as \"Ilość wejść\", kk.poczatek_karnetu as \"Początek karnetu\", kk.koniec_karnetu as \"Koniec karnetu\", kk.cena as Cena FROM `klienci_kursy` kk JOIN klienci k ON (k.id = kk.klient_id) JOIN kursy kur ON (kur.id = kk.kurs_id) WHERE klient_id = " +id;
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    if (dataGridViewKlientKursy.InvokeRequired)
                    {
                        dataGridViewKlientKursy.Invoke(new Action(() =>
                        {
                            dataGridViewKlientKursy.DataSource = ds.Tables[0].DefaultView;
                        }));

                        if (ds.Tables[0].Rows.Count < 1)
                        {
                            ds.Tables[0].Rows.Add(nazwisko, imie);
                        }
                    }
                    else
                    {
                        if (ds.Tables[0].Rows.Count < 1)
                        {
                            ds.Tables[0].Rows.Add(nazwisko, imie, email, telefon);
                        }
                        dataGridViewKlientKursy.DataSource = ds.Tables[0].DefaultView;
                    }

                    dataGridViewKlientKursy.Columns["Grupa"].Visible = false;
                    dataGridViewKlientKursy.Columns["Dzień tygodnia"].Visible = false;

                    //kursyComboBoxColumn.DataPropertyName = "Grupa";
                    //comboBoxColumnDniTygodnia.DataPropertyName = "Dzień tygodnia";
                    dataGridViewKlientKursy.Columns.Insert(4, kursyComboBoxColumn);
                    dataGridViewKlientKursy.Columns.Insert(5, comboBoxColumnDniTygodnia);

                    Debug.WriteLine("kursy idx: " + dataGridViewKlientKursy.Columns.Count);

                    Utils.AddButtonToDataGridView("Zapisz", "Zapisz", 100, ref dataGridViewKlientKursy);
                    Utils.AddButtonToDataGridView("Usuń", "Usuń", 100, ref dataGridViewKlientKursy);
                    //DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
                    //btnColumn.Name = "Zapisz";
                    //btnColumn.HeaderText = "Zapisz";
                    //btnColumn.Text = "Zapisz";
                    //btnColumn.UseColumnTextForButtonValue = true;

                    //Debug.WriteLine("btnColumn idx: " + dataGridViewKlientKursy.Columns.Count);
                    //dataGridViewKlientKursy.Columns.Insert(dataGridViewKlientKursy.Columns.Count, btnColumn);
                    dataGridViewKlientKursy.Columns["Ilość wejść"].DisplayIndex = 6;
                    dataGridViewKlientKursy.Columns["Początek karnetu"].DisplayIndex = 7;
                    dataGridViewKlientKursy.Columns["Koniec karnetu"].DisplayIndex = 8;
                    dataGridViewKlientKursy.Columns["Cena"].DisplayIndex = 9;
                }
                catch (MySqlException e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }
        }

        private void buttonDodajKurs_Click(object sender, EventArgs e)
        {
            dataGridViewKlientKursy.AllowUserToAddRows = true;

            //DataGridViewRow row = dataGridViewKlientKursy.Rows[dataGridViewKlientKursy.Rows.Count -1];
            //row.Cells["Nazwisko"].Value = nazwisko;
            //row.Cells["Imię"].Value = imie;
            //row.Cells["Email"].Value = email;
            //row.Cells["Telefon"].Value = telefon;
            
            //dataGridViewKlientKursy.NotifyCurrentCellDirty(true);
            //dataGridViewKlientKursy.BeginEdit(true);
            //dataGridViewKlientKursy.EndEdit();
        }

        private void RemoveKlientKursy(int klient_id, int kurs_id)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(Utils.conncection_string);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM klienci_kursy WHERE klient_id = @klient_id AND kurs_id = @kurs_id";
                cmd.Parameters.AddWithValue("@klient_id", klient_id);
                cmd.Parameters.AddWithValue("@kurs_id", kurs_id);
                int resp = cmd.ExecuteNonQuery();
                Debug.WriteLine("resp: " + resp);
                conn.Close();
                //mainForm.RefreshKlientGridView("klienci");
                this.Close();
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("SaveKlientKursy, MySqlException");
                Debug.WriteLine(e.ToString());
            }
        }

        private void SaveKlientKursy(int klient_id, int kurs_id, string dzien, int ilosc_wejsc, DateTime pocz_karn, DateTime kon_karn, float cena)
        {
            Debug.WriteLine("SaveKlientKursy");
            Debug.WriteLine("kurs_id: "+ kurs_id);
            try
            {
                MySqlConnection conn = new MySqlConnection(Utils.conncection_string);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO klienci_kursy(klient_id, kurs_id, dzien, ilosc_wejsc, poczatek_karnetu, koniec_karnetu, cena) VALUES(@klient_id, @kurs_id, @dzien, @ilosc_wejsc, @pocz_karn, @kon_karn, @cena) ON DUPLICATE KEY UPDATE dzien = @dzien, ilosc_wejsc = @ilosc_wejsc, poczatek_karnetu = @pocz_karn, koniec_karnetu = @kon_karn, cena = @cena;";
                cmd.Parameters.AddWithValue("@klient_id", klient_id);
                cmd.Parameters.AddWithValue("@kurs_id", kurs_id);
                cmd.Parameters.AddWithValue("@dzien", dzien);
                cmd.Parameters.AddWithValue("@ilosc_wejsc", ilosc_wejsc);
                cmd.Parameters.AddWithValue("@pocz_karn", pocz_karn);
                cmd.Parameters.AddWithValue("@kon_karn", kon_karn);
                cmd.Parameters.AddWithValue("@cena", cena);

                int resp = cmd.ExecuteNonQuery();
                Debug.WriteLine("resp: "+resp);
                conn.Close();
                //mainForm.RefreshKlientGridView("klienci");
                this.Close();
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("SaveKlientKursy, MySqlException");
                Debug.WriteLine(e.ToString());
            }
        }

        private void dataGridViewKlientKursy_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (dataGridViewKlientKursy.Focused && dataGridViewKlientKursy.CurrentCell.ColumnIndex == dateColumns[0])
                {
                    dtpRozp.Location = dataGridViewKlientKursy.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
                    dtpRozp.Visible = true;

                    if (dataGridViewKlientKursy.CurrentCell.Value != DBNull.Value)
                    {
                        dtpRozp.Value = (DateTime)dataGridViewKlientKursy.CurrentCell.Value;
                    }
                    else
                    {
                        dtpRozp.Value = DateTime.Today;
                    }
                }
                else if (dataGridViewKlientKursy.Focused && dataGridViewKlientKursy.CurrentCell.ColumnIndex == dateColumns[1])
                {
                    dtpKonc.Location = dataGridViewKlientKursy.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
                    dtpKonc.Visible = true;

                    if (dataGridViewKlientKursy.CurrentCell.Value != DBNull.Value)
                    {
                        dtpKonc.Value = (DateTime)dataGridViewKlientKursy.CurrentCell.Value;
                    }
                    else
                    {
                        dtpKonc.Value = DateTime.Today;
                    }
                }
                else
                {
                    dtpRozp.Visible = false;
                    dtpKonc.Visible = false;
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void dataGridViewKlientKursy_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridViewKlientKursy.Focused && dataGridViewKlientKursy.CurrentCell.ColumnIndex == dateColumns[0])
                {
                    dataGridViewKlientKursy.CurrentCell.Value = dtpRozp.Value.Date;
                }
                if (dataGridViewKlientKursy.Focused && dataGridViewKlientKursy.CurrentCell.ColumnIndex == dateColumns[1])
                {
                    dataGridViewKlientKursy.CurrentCell.Value = dtpKonc.Value.Date;
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void dataGridViewKlientKursy_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                DateTime poczatekKarnetu;
                DateTime koniecKarnetu;
                float cena;
                int ilosc_wejsc;

                if (!string.IsNullOrEmpty(senderGrid.Rows[e.RowIndex].Cells["Nazwisko"].Value.ToString()) && !string.IsNullOrEmpty(senderGrid.Rows[e.RowIndex].Cells["Imię"].Value.ToString())
                    && !string.IsNullOrEmpty(senderGrid.Rows[e.RowIndex].Cells["Email"].Value.ToString()) && !string.IsNullOrEmpty(senderGrid.Rows[e.RowIndex].Cells["Telefon"].Value.ToString())
                    && !string.IsNullOrEmpty(senderGrid.Rows[e.RowIndex].Cells["Grupa"].Value.ToString()) && !string.IsNullOrEmpty(senderGrid.Rows[e.RowIndex].Cells["Dzień Tygodnia"].Value.ToString())
                    && int.TryParse(senderGrid.Rows[e.RowIndex].Cells["Ilość wejść"].Value.ToString(), out ilosc_wejsc) && float.TryParse(senderGrid.Rows[e.RowIndex].Cells["Cena"].Value.ToString(), out cena)
                    && DateTime.TryParse(senderGrid.Rows[e.RowIndex].Cells["Początek karnetu"].Value.ToString(), out poczatekKarnetu) && DateTime.TryParse(senderGrid.Rows[e.RowIndex].Cells["Koniec karnetu"].Value.ToString(), out koniecKarnetu))
                {
                    int kursId = kursyNazwaDict.FirstOrDefault(x => x.Value == senderGrid.Rows[e.RowIndex].Cells["Grupa"].Value.ToString()).Key;
                    string dzien = senderGrid.Rows[e.RowIndex].Cells["Dzień Tygodnia"].Value.ToString();

                    Debug.WriteLine("konieckarn: "+ koniecKarnetu);

                    DataGridViewButtonColumn btnCol = senderGrid.Columns[e.ColumnIndex] as DataGridViewButtonColumn;
                    if (btnCol.Text.Equals("Zapisz"))
                    {
                        SaveKlientKursy(id, kursId, dzien, ilosc_wejsc, poczatekKarnetu, koniecKarnetu, cena);
                    }
                    else if (btnCol.Text.Equals("Usuń"))
                    {
                        RemoveKlientKursy(id, kursId);
                        PobierzKursyKlienta(comboBoxColumnKursyNazwa, comboBoxColumnDniTygodnia);
                    }
                }
            }
        }

        private void DrawEan13(string eanProductCode)
        {
            Graphics g = pictureBoxEan.CreateGraphics();

            g.FillRectangle(
                 new SolidBrush(SystemColors.Control),
                 new Rectangle(0, 0, pictureBoxEan.Width, pictureBoxEan.Height));

            // Create an instance of the Ean13 Class.        
            Ean13 upc = new Ean13();

            upc.CountryCode = "590";//"12";
            upc.ManufacturerCode = "34567";//"34567";
            upc.ProductCode = eanProductCode;//"0001";//"89012";
            upc.Scale = 1.5f;//(float)Convert.ToDecimal(cboScale.Items[cboScale.SelectedIndex]);

            upc.DrawEan13Barcode(g, new Point(0, 0));

            pictureBoxEan.BackColor = Color.White;
            g.Dispose();
        }

        private void KlientDetailsForm_Paint(object sender, PaintEventArgs e)
        {
            DrawEan13(ean);
        }

        private void dataGridViewKlientKursy_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["Nazwisko"].Value = nazwisko;
            e.Row.Cells["Imię"].Value = imie;
            e.Row.Cells["Email"].Value = email;
            e.Row.Cells["Telefon"].Value = telefon;
        }
    }
}
