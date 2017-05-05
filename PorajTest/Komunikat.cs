using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PorajTest
{
    public partial class Komunikat : Form
    {
        private Form1 form1 = null;
        public Komunikat(Form1 form1)
        {
            this.form1 = form1;
            InitializeComponent();
        }

        public Komunikat(string tekst, Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
            labelUwaga.Text = tekst;
        }
       public Komunikat(string tekst, string tekstTytulowy)
        {
            InitializeComponent();
            label1.Text = tekstTytulowy;
            labelUwaga.Text = tekst;
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (form1 != null)
            {
                form1.textBoxEan.Text = "";
            }
            Close();
        }

        private void labelUwaga_Click(object sender, EventArgs e)
        {
        }
    }
}