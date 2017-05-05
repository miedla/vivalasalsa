using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PorajTest
{
    public partial class WybierzKurs : Form
    {
        private List<int> idKursow;
        private string ean;
        private Form1 form1;


        public WybierzKurs(List<int> idKursow, string ean, List<string> kursy, Form1 form1)
        {
            this.idKursow = idKursow;
            InitializeComponent();
            listBox1.DataSource = kursy;
            this.ean = ean;
            this.form1 = form1;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id_kursu = idKursow[listBox1.SelectedIndex];
            this.form1.rejestracjaPrzyjsciaNaKurs(ean, id_kursu);
            Close();
        }
    }
}
