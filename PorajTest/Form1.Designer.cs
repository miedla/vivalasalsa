﻿namespace PorajTest
{
    partial class Form1
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
            this.dataGridViewKlienci = new System.Windows.Forms.DataGridView();
            this.buttonDodajKlienta = new System.Windows.Forms.Button();
            this.buttonDodajKurs = new System.Windows.Forms.Button();
            this.buttonKlienci = new System.Windows.Forms.Button();
            this.buttonKursy = new System.Windows.Forms.Button();
            this.buttonKlienciKursy = new System.Windows.Forms.Button();
            this.buttonMode = new System.Windows.Forms.Button();
            this.textBoxEan = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKlienci)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewKlienci
            // 
            this.dataGridViewKlienci.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewKlienci.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewKlienci.Location = new System.Drawing.Point(69, 89);
            this.dataGridViewKlienci.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewKlienci.Name = "dataGridViewKlienci";
            this.dataGridViewKlienci.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewKlienci.Size = new System.Drawing.Size(1007, 300);
            this.dataGridViewKlienci.TabIndex = 0;
            this.dataGridViewKlienci.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridKlient_CellContentClick);
            // 
            // buttonDodajKlienta
            // 
            this.buttonDodajKlienta.Location = new System.Drawing.Point(69, 41);
            this.buttonDodajKlienta.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonDodajKlienta.Name = "buttonDodajKlienta";
            this.buttonDodajKlienta.Size = new System.Drawing.Size(149, 28);
            this.buttonDodajKlienta.TabIndex = 1;
            this.buttonDodajKlienta.Text = "Dodaj klienta";
            this.buttonDodajKlienta.UseVisualStyleBackColor = true;
            this.buttonDodajKlienta.Click += new System.EventHandler(this.buttonDodajKlienta_Click);
            // 
            // buttonDodajKurs
            // 
            this.buttonDodajKurs.Location = new System.Drawing.Point(240, 41);
            this.buttonDodajKurs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonDodajKurs.Name = "buttonDodajKurs";
            this.buttonDodajKurs.Size = new System.Drawing.Size(149, 28);
            this.buttonDodajKurs.TabIndex = 2;
            this.buttonDodajKurs.Text = "Dodaj kurs";
            this.buttonDodajKurs.UseVisualStyleBackColor = true;
            this.buttonDodajKurs.Click += new System.EventHandler(this.buttonDodajKurs_Click);
            // 
            // buttonKlienci
            // 
            this.buttonKlienci.Location = new System.Drawing.Point(1084, 127);
            this.buttonKlienci.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonKlienci.Name = "buttonKlienci";
            this.buttonKlienci.Size = new System.Drawing.Size(100, 28);
            this.buttonKlienci.TabIndex = 3;
            this.buttonKlienci.Text = "Klienci";
            this.buttonKlienci.UseVisualStyleBackColor = true;
            this.buttonKlienci.Click += new System.EventHandler(this.buttonKlienci_Click);
            // 
            // buttonKursy
            // 
            this.buttonKursy.Location = new System.Drawing.Point(1084, 207);
            this.buttonKursy.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonKursy.Name = "buttonKursy";
            this.buttonKursy.Size = new System.Drawing.Size(100, 28);
            this.buttonKursy.TabIndex = 4;
            this.buttonKursy.Text = "Kursy";
            this.buttonKursy.UseVisualStyleBackColor = true;
            this.buttonKursy.Click += new System.EventHandler(this.buttonKursy_Click);
            // 
            // buttonKlienciKursy
            // 
            this.buttonKlienciKursy.Location = new System.Drawing.Point(1084, 282);
            this.buttonKlienciKursy.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonKlienciKursy.Name = "buttonKlienciKursy";
            this.buttonKlienciKursy.Size = new System.Drawing.Size(100, 28);
            this.buttonKlienciKursy.TabIndex = 5;
            this.buttonKlienciKursy.Text = "Klienci kursy";
            this.buttonKlienciKursy.UseVisualStyleBackColor = true;
            this.buttonKlienciKursy.Click += new System.EventHandler(this.buttonKlienciKursy_Click);
            // 
            // buttonMode
            // 
            this.buttonMode.Location = new System.Drawing.Point(397, 41);
            this.buttonMode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonMode.Name = "buttonMode";
            this.buttonMode.Size = new System.Drawing.Size(100, 28);
            this.buttonMode.TabIndex = 6;
            this.buttonMode.Text = "Edycja";
            this.buttonMode.UseVisualStyleBackColor = true;
            this.buttonMode.Visible = false;
            this.buttonMode.Click += new System.EventHandler(this.buttonMode_Click);
            // 
            // textBoxEan
            // 
            this.textBoxEan.Location = new System.Drawing.Point(839, 57);
            this.textBoxEan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxEan.Name = "textBoxEan";
            this.textBoxEan.Size = new System.Drawing.Size(234, 22);
            this.textBoxEan.TabIndex = 7;
            this.textBoxEan.TextChanged += new System.EventHandler(this.textBoxEan_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 444);
            this.Controls.Add(this.textBoxEan);
            this.Controls.Add(this.buttonMode);
            this.Controls.Add(this.buttonKlienciKursy);
            this.Controls.Add(this.buttonKursy);
            this.Controls.Add(this.buttonKlienci);
            this.Controls.Add(this.buttonDodajKurs);
            this.Controls.Add(this.buttonDodajKlienta);
            this.Controls.Add(this.dataGridViewKlienci);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKlienci)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView dataGridViewKlienci;
        private System.Windows.Forms.Button buttonDodajKlienta;
        private System.Windows.Forms.Button buttonDodajKurs;
        private System.Windows.Forms.Button buttonKlienci;
        private System.Windows.Forms.Button buttonKursy;
        private System.Windows.Forms.Button buttonKlienciKursy;
        private System.Windows.Forms.Button buttonMode;
        public System.Windows.Forms.TextBox textBoxEan;
    }
}

