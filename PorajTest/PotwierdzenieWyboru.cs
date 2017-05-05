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
    public partial class PotwierdzenieWyboru : Form
    {
        private string ean;
        private KlientDetailsForm detailsForm;
        public PotwierdzenieWyboru(KlientDetailsForm detailsForm)
        {
            InitializeComponent();
            this.ean = detailsForm.ean;
            this.detailsForm = detailsForm;
        }

        private void PrzyciskNie_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PrzyciskTak_Click(object sender, EventArgs e)
        {
            Utils.UsunKlienta(ean);
            detailsForm.Close();
            this.Close();
        }
    }
}
