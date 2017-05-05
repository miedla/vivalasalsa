namespace PorajTest
{
    partial class KlientDetailsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KlientDetailsForm));
            this.labelKlient = new System.Windows.Forms.Label();
            this.dataGridViewKlientKursy = new System.Windows.Forms.DataGridView();
            this.buttonDodajKurs = new System.Windows.Forms.Button();
            this.pictureBoxEan = new System.Windows.Forms.PictureBox();
            this.labelEan = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonZapiszZdjecie = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKlientKursy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEan)).BeginInit();
            this.SuspendLayout();
            // 
            // labelKlient
            // 
            this.labelKlient.AutoSize = true;
            this.labelKlient.Location = new System.Drawing.Point(16, 25);
            this.labelKlient.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelKlient.Name = "labelKlient";
            this.labelKlient.Size = new System.Drawing.Size(46, 17);
            this.labelKlient.TabIndex = 0;
            this.labelKlient.Text = "label1";
            // 
            // dataGridViewKlientKursy
            // 
            this.dataGridViewKlientKursy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewKlientKursy.Location = new System.Drawing.Point(16, 121);
            this.dataGridViewKlientKursy.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewKlientKursy.Name = "dataGridViewKlientKursy";
            this.dataGridViewKlientKursy.Size = new System.Drawing.Size(1639, 219);
            this.dataGridViewKlientKursy.TabIndex = 1;
            this.dataGridViewKlientKursy.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridViewKlientKursy_CellBeginEdit);
            this.dataGridViewKlientKursy.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewKlientKursy_CellContentClick);
            this.dataGridViewKlientKursy.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewKlientKursy_CellEndEdit);
            this.dataGridViewKlientKursy.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewKlientKursy_CellFormatting);
            this.dataGridViewKlientKursy.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewKlientKursy_CellValueChanged);
            this.dataGridViewKlientKursy.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridViewKlientKursy_DefaultValuesNeeded);
            this.dataGridViewKlientKursy.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridViewKlientKursy_EditingControlShowing);
            // 
            // buttonDodajKurs
            // 
            this.buttonDodajKurs.Location = new System.Drawing.Point(16, 64);
            this.buttonDodajKurs.Margin = new System.Windows.Forms.Padding(4);
            this.buttonDodajKurs.Name = "buttonDodajKurs";
            this.buttonDodajKurs.Size = new System.Drawing.Size(100, 28);
            this.buttonDodajKurs.TabIndex = 2;
            this.buttonDodajKurs.Text = "Dodaj kurs";
            this.buttonDodajKurs.UseVisualStyleBackColor = true;
            this.buttonDodajKurs.Click += new System.EventHandler(this.buttonDodajKurs_Click);
            // 
            // pictureBoxEan
            // 
            this.pictureBoxEan.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxEan.Image")));
            this.pictureBoxEan.Location = new System.Drawing.Point(19, 372);
            this.pictureBoxEan.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxEan.Name = "pictureBoxEan";
            this.pictureBoxEan.Size = new System.Drawing.Size(360, 213);
            this.pictureBoxEan.TabIndex = 3;
            this.pictureBoxEan.TabStop = false;
            // 
            // labelEan
            // 
            this.labelEan.AutoSize = true;
            this.labelEan.Location = new System.Drawing.Point(1513, 101);
            this.labelEan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelEan.Name = "labelEan";
            this.labelEan.Size = new System.Drawing.Size(46, 17);
            this.labelEan.TabIndex = 4;
            this.labelEan.Text = "label1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(141, 64);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 5;
            this.button1.Text = "Usuń klienta";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonZapiszZdjecie
            // 
            this.buttonZapiszZdjecie.Location = new System.Drawing.Point(386, 471);
            this.buttonZapiszZdjecie.Name = "buttonZapiszZdjecie";
            this.buttonZapiszZdjecie.Size = new System.Drawing.Size(156, 23);
            this.buttonZapiszZdjecie.TabIndex = 6;
            this.buttonZapiszZdjecie.Text = "Zapisz zdjęcie";
            this.buttonZapiszZdjecie.UseVisualStyleBackColor = true;
            this.buttonZapiszZdjecie.Click += new System.EventHandler(this.buttonZapiszZdjecie_Click);
            // 
            // KlientDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1900, 753);
            this.Controls.Add(this.buttonZapiszZdjecie);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelEan);
            this.Controls.Add(this.pictureBoxEan);
            this.Controls.Add(this.buttonDodajKurs);
            this.Controls.Add(this.dataGridViewKlientKursy);
            this.Controls.Add(this.labelKlient);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "KlientDetailsForm";
            this.Text = "KlientDetailsForm";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.KlientDetailsForm_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKlientKursy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelKlient;
        private System.Windows.Forms.DataGridView dataGridViewKlientKursy;
        private System.Windows.Forms.Button buttonDodajKurs;
        private System.Windows.Forms.PictureBox pictureBoxEan;
        private System.Windows.Forms.Label labelEan;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonZapiszZdjecie;
    }
}