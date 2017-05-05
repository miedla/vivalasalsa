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
using System.Drawing.Imaging;

namespace PorajTest
{
    public struct Kurs
    {
        public int id;
        public string nazwa;
        public float cena;

        public Kurs(int id, string nazwa, float cena)
        {
            this.id = id;
            this.nazwa = nazwa;
            this.cena = cena;
        }
    }

    public partial class KlientDetailsForm : Form
    {

        protected override void WndProc(ref Message message)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch (message.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = message.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                        return;
                    break;
            }

            base.WndProc(ref message);
        }
        private int id;
        private string imie;
        private string nazwisko;
        private string email;
        private string telefon;
        public string ean;
        private List<string> kursyNazwaList;
        private string[] dniTygodnia = {"Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota"};
        private DateTimePicker dtpRozp;
        private DateTimePicker dtpKonc;
        private int[] dateColumns = {9, 10}; //{ 10, 11 };
        private int kursColumnIdx = 6;
        private List<Kurs> kursyNazwaDict; //Dictionary<int, string> kursyNazwaDict;
        private Ean13 ean13 = null;
        DataGridViewComboBoxColumn comboBoxColumnKursyNazwa;
        DataGridViewComboBoxColumn comboBoxColumnDniTygodnia;

        public KlientDetailsForm(int id, string imie, string nazwisko, string email, string telefon, string ean)
        {
            InitializeComponent();
            dataGridViewKlientKursy.MultiSelect = false;
            //            this.Height = 600;
            //            this.Width = 1300;
            //            this.StartPosition = FormStartPosition.CenterScreen;
            //            MinimizeBox = false;
            MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
//            this.TopMost = true;
//            this.ControlBox = false;
            //            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            //            this.FormBorderStyle = FormBorderStyle.;
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

            //for(int i=0; i<dataGridViewKlientKursy.Rows.Count; i++)
            //{
            //    CheckExpire(DateTime.Parse(dataGridViewKlientKursy.Rows[i].Cells["Koniec karnetu"].Value.ToString()), i);
            //}
            //foreach (DataGridViewRow row in dataGridViewKlientKursy.Rows)
            //{
            //    CheckExpire(DateTime.Parse(row.Cells["Koniec karnetu"].Value.ToString()), row.Index);
            //}
            Debug.WriteLine("gridview column count: " + dataGridViewKlientKursy.Columns.Count);
        }

        private void SetDatePicker(ref DateTimePicker dtPicker)
        {
            dtPicker = new DateTimePicker();
            dtPicker.Format = DateTimePickerFormat.Short;
            dtPicker.Visible = false;
            dtPicker.Width = 100;
            dataGridViewKlientKursy.Controls.Add(dtPicker);
        }

        private void UnSetDatePicker(ref DateTimePicker dtPicker, DateTime date)
        {
            dtpKonc.Value = date;
            dataGridViewKlientKursy.Controls.Remove(dtPicker);//Add(dtPicker);
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
                comboBoxColumn.DataSource =
                    ds.Tables[0].AsEnumerable().Select(row => row.Field<string>("nazwa")).ToList();
                comboBoxColumn.Name = "Grupa";
                comboBoxColumn.DataPropertyName = "Grupa";

                kursyNazwaDict = ds.Tables[0].AsEnumerable().Select(k => new Kurs
                    {
                        id = k.Field<int>("id"),
                        nazwa = k.Field<string>("nazwa"),
                        cena = k.Field<float>("cena")
                    })
                    .ToList();
                //kursyNazwaDict = ds.Tables[0].AsEnumerable().Select(r => r).ToDictionary(r=>r.Field<int>("id"), new Kurs { nazwa = r => r.Field<string>("nazwa")});
                //Debug.WriteLine("kursyNazwDict: "+ kursyNazwaDict.FirstOrDefault(x=> x.Value=="Zumba").Key);
                Debug.WriteLine("kursyNazwDict: " + kursyNazwaDict.FirstOrDefault(k => k.nazwa == "Zumba").cena);
            }

            return comboBoxColumn;
        }

        private void PobierzKursyKlienta(DataGridViewComboBoxColumn kursyComboBoxColumn,
            DataGridViewComboBoxColumn comboBoxColumnDniTygodnia)
        {
            using (MySqlConnection conn = new MySqlConnection(Utils.conncection_string))
            {
                try
                {
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText =
                        "SELECT k.nazwisko as Nazwisko, k.imie as Imię, k.email as Email, k.telefon as Telefon, kur.nazwa as Grupa, kk.dzien as \"Dzień tygodnia\", kk.ilosc_wejsc as \"Ilość wejść\", kk.poczatek_karnetu as \"Początek karnetu\", kk.koniec_karnetu as \"Koniec karnetu\", kk.cena as Cena FROM `klienci_kursy` kk JOIN klienci k ON (k.id = kk.klient_id) JOIN kursy kur ON (kur.id = kk.kurs_id) WHERE klient_id = " +
                        id;
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    if (dataGridViewKlientKursy.InvokeRequired)
                    {
                        dataGridViewKlientKursy.Invoke(
                            new Action(() => { dataGridViewKlientKursy.DataSource = ds.Tables[0].DefaultView; }));

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

        private void SaveKlientKursy(int klient_id, int kurs_id, string dzien, int ilosc_wejsc, DateTime pocz_karn,
            DateTime kon_karn, float cena)
        {
            Debug.WriteLine("SaveKlientKursy");
            Debug.WriteLine("kurs_id: " + kurs_id);
            Debug.WriteLine("kon_karn: "+ kon_karn);
            try
            {
                MySqlConnection conn = new MySqlConnection(Utils.conncection_string);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText =
                    "INSERT INTO klienci_kursy(klient_id, kurs_id, dzien, ilosc_wejsc, poczatek_karnetu, koniec_karnetu, cena) VALUES(@klient_id, @kurs_id, @dzien, @ilosc_wejsc, @pocz_karn, @kon_karn, @cena) ON DUPLICATE KEY UPDATE dzien = @dzien, ilosc_wejsc = @ilosc_wejsc, poczatek_karnetu = @pocz_karn, koniec_karnetu = @kon_karn, cena = @cena;";
                cmd.Parameters.AddWithValue("@klient_id", klient_id);
                cmd.Parameters.AddWithValue("@kurs_id", kurs_id);
                cmd.Parameters.AddWithValue("@dzien", dzien);
                cmd.Parameters.AddWithValue("@ilosc_wejsc", ilosc_wejsc);
                cmd.Parameters.AddWithValue("@pocz_karn", pocz_karn);
                cmd.Parameters.AddWithValue("@kon_karn", kon_karn);
                cmd.Parameters.AddWithValue("@cena", cena);

                int resp = cmd.ExecuteNonQuery();
                Debug.WriteLine("resp: " + resp);
                conn.Close();
                //mainForm.RefreshKlientGridView("klienci");
//                this.Close();
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("SaveKlientKursy, MySqlException");
                Debug.WriteLine(e.ToString());
            }
        }

        private void dataGridViewKlientKursy_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Debug.WriteLine("beginedit");
            try
            {
                if (dataGridViewKlientKursy.Focused && dataGridViewKlientKursy.CurrentCell.ColumnIndex == dateColumns[0])
                {
                    dtpRozp.Location =
                        dataGridViewKlientKursy.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
                    dtpRozp.Visible = true;

                    if (dataGridViewKlientKursy.CurrentCell.Value != DBNull.Value)
                    {
                        dtpRozp.Value = (DateTime) dataGridViewKlientKursy.CurrentCell.Value;
                    }
                    else
                    {
                        dtpRozp.Value = DateTime.Today;
                    }
                }
                else if (dataGridViewKlientKursy.Focused && dataGridViewKlientKursy.CurrentCell.ColumnIndex == dateColumns[1])
                {
                    dtpKonc.Location =
                        dataGridViewKlientKursy.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
                    dtpKonc.Visible = true;

                    if (dataGridViewKlientKursy.CurrentCell.Value != DBNull.Value)
                    {
                        dtpKonc.Value = (DateTime) dataGridViewKlientKursy.CurrentCell.Value;
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
            }
            catch (Exception ex)
            {
                Debug.WriteLine("beginedit: "+ex.ToString());
            }
        }

        private void dataGridViewKlientKursy_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridViewKlientKursy.Focused && dataGridViewKlientKursy.CurrentCell.ColumnIndex == dateColumns[0])
                {
                    Debug.WriteLine("CurrentCell.ColumnIndex == dateColumns[0]");
                    dataGridViewKlientKursy.CurrentCell.Value = dtpRozp.Value.Date;
                }
                if (dataGridViewKlientKursy.Focused && dataGridViewKlientKursy.CurrentCell.ColumnIndex == dateColumns[1])
                {
                    Debug.WriteLine("CurrentCell.ColumnIndex == dateColumns[1]");
                    dataGridViewKlientKursy.CurrentCell.Value = dtpKonc.Value.Date;
                }

                Debug.WriteLine("dtpKonc.Value.Date: " + dtpKonc.Value.Date);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("dataGridViewKlientKursy_CellEndEdit: " + ex.ToString());
            }
        }

        private void dataGridViewKlientKursy_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridViewKlientKursy.Focused && dataGridViewKlientKursy.CurrentCell.ColumnIndex == dateColumns[0])
                {
                    Debug.WriteLine("CurrentCell.ColumnIndex == dateColumns[0]");
                    dataGridViewKlientKursy.CurrentCell.Value = dtpRozp.Value.Date;
                }
                if (dataGridViewKlientKursy.Focused && dataGridViewKlientKursy.CurrentCell.ColumnIndex == dateColumns[1])
                {
                    Debug.WriteLine("CurrentCell.ColumnIndex == dateColumns[1]");
                    dataGridViewKlientKursy.CurrentCell.Value = dtpKonc.Value.Date;
                }

                Debug.WriteLine("dtpKonc.Value.Date: " + dtpKonc.Value.Date);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("dataGridViewKlientKursy_CellEndEdit: " + ex.ToString());
            }
        }

        private void dataGridViewKlientKursy_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView) sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                DateTime poczatekKarnetu;
                DateTime koniecKarnetu;
                float cena;
                int ilosc_wejsc;

                if (!string.IsNullOrEmpty(senderGrid.Rows[e.RowIndex].Cells["Nazwisko"].Value.ToString()) &&
                    !string.IsNullOrEmpty(senderGrid.Rows[e.RowIndex].Cells["Imię"].Value.ToString())
                    && !string.IsNullOrEmpty(senderGrid.Rows[e.RowIndex].Cells["Email"].Value.ToString()) &&
                    !string.IsNullOrEmpty(senderGrid.Rows[e.RowIndex].Cells["Telefon"].Value.ToString())
                    && !string.IsNullOrEmpty(senderGrid.Rows[e.RowIndex].Cells["Grupa"].Value.ToString()) &&
                    !string.IsNullOrEmpty(senderGrid.Rows[e.RowIndex].Cells["Dzień Tygodnia"].Value.ToString())
                    && int.TryParse(senderGrid.Rows[e.RowIndex].Cells["Ilość wejść"].Value.ToString(), out ilosc_wejsc) &&
                    float.TryParse(senderGrid.Rows[e.RowIndex].Cells["Cena"].Value.ToString(), out cena)
                    &&
                    DateTime.TryParse(senderGrid.Rows[e.RowIndex].Cells["Początek karnetu"].Value.ToString(),
                        out poczatekKarnetu) &&
                    DateTime.TryParse(senderGrid.Rows[e.RowIndex].Cells["Koniec karnetu"].Value.ToString(),
                        out koniecKarnetu))
                {
                    //int kursId = kursyNazwaDict.FirstOrDefault(x => x.Value == senderGrid.Rows[e.RowIndex].Cells["Grupa"].Value.ToString()).Key;
                    int kursId =
                        kursyNazwaDict.FirstOrDefault(
                            k => k.nazwa == senderGrid.Rows[e.RowIndex].Cells["Grupa"].Value.ToString()).id;
                    string dzien = senderGrid.Rows[e.RowIndex].Cells["Dzień Tygodnia"].Value.ToString();

                    Debug.WriteLine("konieckarn: " + koniecKarnetu);
                    Debug.WriteLine("senderGrid.Rows[e.RowIndex].Cells[\"Koniec karnetu\"].Value.ToString(): "+ senderGrid.Rows[e.RowIndex].Cells["Koniec karnetu"].Value.ToString());

                    DataGridViewButtonColumn btnCol = senderGrid.Columns[e.ColumnIndex] as DataGridViewButtonColumn;
                    if (btnCol.Text.Equals("Zapisz"))
                    {
                        
                        //Debug.WriteLine("edit mode: "+senderGrid.Rows[e.RowIndex].Cells["Koniec karnetu"].IsInEditMode);
                        SaveKlientKursy(id, kursId, dzien, ilosc_wejsc, poczatekKarnetu, koniecKarnetu, cena);
                        UnSetDatePicker(ref dtpRozp, poczatekKarnetu);
                        UnSetDatePicker(ref dtpKonc, koniecKarnetu);
                        SetDatePicker(ref dtpRozp);
                        SetDatePicker(ref dtpKonc);
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
            // Graphics g = pictureBoxEan.CreateGraphics();
            //
            // g.FillRectangle(
            //      new SolidBrush(SystemColors.Control),
            //      new Rectangle(0, 0, pictureBoxEan.Width, pictureBoxEan.Height));
            //
            // // Create an instance of the Ean13 Class.        
            // Ean13 upc = new Ean13();
            //
            // upc.CountryCode = Utils.eanCountryCode;//"12";
            // upc.ManufacturerCode = Utils.eanManuCode;//"34567";
            // upc.ProductCode = "00001";//ean.Substring(eanProductCode.Length - 5, 4);//eanProductCode;//"0001";//"89012";
            // upc.Scale = 1f;//(float)Convert.ToDecimal(cboScale.Items[cboScale.SelectedIndex]);
            //
            // upc.DrawEan13Barcode(g, new Point(0, 0));
            //
            // Debug.WriteLine("upc name: "+upc.ChecksumDigit);
            //
            // labelEan.Text = upc.CountryCode + " " + upc.ManufacturerCode + " " + upc.ProductCode + " " + upc.ChecksumDigit;
            //
            // pictureBoxEan.BackColor = Color.White;
            // g.Dispose();
            CreateEan13(ean.Substring(eanProductCode.Length - 6, 5));
            ean13.Scale = 1.5f;

            System.Drawing.Bitmap bmp = ean13.CreateBitmap();
            pictureBoxEan.Image = bmp;
        }

        private void CreateEan13(string productCode)
        {
            ean13 = new Ean13();
            ean13.CountryCode = Utils.eanCountryCode;
            ean13.ManufacturerCode = Utils.eanManuCode;
            ean13.ProductCode = productCode;

            Debug.WriteLine("ean13.checksum " + ean13.ChecksumDigit);
            //ean13.ChecksumDigit ="8";
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

        private void CheckExpire(DateTime koncDate, int ilosc_wejsc, DataGridViewRow rowIdx)
        {
            int result = DateTime.Compare(koncDate, DateTime.Now.AddDays(-1));
            Debug.WriteLine("date result: " + result);
            if (result < 0 || ilosc_wejsc <= 0)
            {
                Debug.WriteLine("koncDate is earlier, row idx: " + rowIdx);
                //dataGridViewKlientKursy.Rows[rowIdx].DefaultCellStyle.BackColor = Color.Red;
                rowIdx.DefaultCellStyle.BackColor = Color.Red;
            }
            else
            {
                Debug.WriteLine("koncDate is Later!");
                rowIdx.DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void dataGridViewKlientKursy_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewRow row = dataGridViewKlientKursy.Rows[e.RowIndex];
            //row.DefaultCellStyle.BackColor = Color.Red;
            try
            {
                CheckExpire(DateTime.Parse(row.Cells["Koniec karnetu"].Value.ToString()),
                    int.Parse(row.Cells["Ilość wejść"].Value.ToString()), row);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void dataGridViewKlientKursy_EditingControlShowing(object sender,
            DataGridViewEditingControlShowingEventArgs e)
        {
            Debug.WriteLine("dataGridViewKlientKursy_EditingControlShowing()");
            if (e.Control is ComboBox)
            {
                ComboBox comboBox = e.Control as ComboBox;
                comboBox.SelectedIndexChanged -= KursComboSelectionChanged;
                if (dataGridViewKlientKursy.CurrentCell.ColumnIndex == kursColumnIdx)
                {
                    Debug.WriteLine(
                        "dataGridViewKlientKursy.CurrentCell.ColumnIndex == kursColumnIdx && e.Control is ComboBox");
                    //ComboBox comboBox = e.Control as ComboBox;
                    comboBox.SelectedIndexChanged += KursComboSelectionChanged;
                }
            }
        }

        private void KursComboSelectionChanged(object sender, EventArgs e)
        {
            var currentcell = dataGridViewKlientKursy.CurrentCellAddress;
            var sendingCB = sender as DataGridViewComboBoxEditingControl;
            string kurs = sendingCB.EditingControlFormattedValue.ToString();
            float cena = kursyNazwaDict.FirstOrDefault(k => k.nazwa == kurs).cena;
            if (cena != 0)
            {
                Debug.WriteLine("sendingCB: " + sendingCB.Name);
                Debug.WriteLine("kurs: " + kurs);
                Debug.WriteLine(
                    "kursyNazwaDict.FirstOrDefault(k => k.nazwa == dataGridViewKlientKursy.Rows[currentcell.Y].Cells[\"Grupa\"].Value.ToString()).cena: " +
                    kursyNazwaDict.FirstOrDefault(
                            k => k.nazwa == dataGridViewKlientKursy.Rows[currentcell.Y].Cells["Grupa"].Value.ToString())
                        .cena);

                dataGridViewKlientKursy.Rows[currentcell.Y].Cells["cena"].Value = cena;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new PotwierdzenieWyboru(this).Show();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
//            mainForm.RefreshKlientGridView("klienci");
        }

        private void buttonZapiszZdjecie_Click(object sender, EventArgs e)
        {
            //            pictureBoxEan.Image.Save(@"Zdjecie",ImageFormat.Jpeg);
//            Bitmap bmp = new Bitmap(pictureBoxEan.Image);
//            bmp.Save(@"C:\costam.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                int width = Convert.ToInt32(pictureBoxEan.Width);
                int height = Convert.ToInt32(pictureBoxEan.Height);

                Bitmap bmp = new Bitmap(width, height);
                pictureBoxEan.DrawToBitmap(bmp, new Rectangle(0, 0, width, height));
                bmp.Save(dialog.FileName + ".png", ImageFormat.Png);
            }
        }
    }
}