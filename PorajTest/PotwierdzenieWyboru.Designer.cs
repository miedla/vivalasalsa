namespace PorajTest
{
    partial class PotwierdzenieWyboru
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
            this.UwagaText = new System.Windows.Forms.Label();
            this.PrzyciskTak = new System.Windows.Forms.Button();
            this.PrzyciskNie = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UwagaText
            // 
            this.UwagaText.AutoSize = true;
            this.UwagaText.Location = new System.Drawing.Point(53, 34);
            this.UwagaText.Name = "UwagaText";
            this.UwagaText.Size = new System.Drawing.Size(326, 17);
            this.UwagaText.TabIndex = 0;
            this.UwagaText.Text = "Czy jesteś pewien, że chcesz usunąć tego klienta?";
            // 
            // PrzyciskTak
            // 
            this.PrzyciskTak.Location = new System.Drawing.Point(86, 103);
            this.PrzyciskTak.Name = "PrzyciskTak";
            this.PrzyciskTak.Size = new System.Drawing.Size(75, 23);
            this.PrzyciskTak.TabIndex = 1;
            this.PrzyciskTak.Text = "TAK";
            this.PrzyciskTak.UseVisualStyleBackColor = true;
            this.PrzyciskTak.Click += new System.EventHandler(this.PrzyciskTak_Click);
            // 
            // PrzyciskNie
            // 
            this.PrzyciskNie.Location = new System.Drawing.Point(267, 103);
            this.PrzyciskNie.Name = "PrzyciskNie";
            this.PrzyciskNie.Size = new System.Drawing.Size(75, 23);
            this.PrzyciskNie.TabIndex = 2;
            this.PrzyciskNie.Text = "NIE";
            this.PrzyciskNie.UseVisualStyleBackColor = true;
            this.PrzyciskNie.Click += new System.EventHandler(this.PrzyciskNie_Click);
            // 
            // PotwierdzenieWyboru
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 145);
            this.Controls.Add(this.PrzyciskNie);
            this.Controls.Add(this.PrzyciskTak);
            this.Controls.Add(this.UwagaText);
            this.Name = "PotwierdzenieWyboru";
            this.Text = "PotwierdzenieWyboru";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label UwagaText;
        private System.Windows.Forms.Button PrzyciskTak;
        private System.Windows.Forms.Button PrzyciskNie;
    }
}