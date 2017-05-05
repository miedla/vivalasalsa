namespace PorajTest
{
    partial class Komunikat
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelUwaga = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Zarejestrowano klienta pomyślnie!";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(90, 121);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // labelUwaga
            // 
            this.labelUwaga.AutoSize = true;
            this.labelUwaga.BackColor = System.Drawing.Color.Red;
            this.labelUwaga.Location = new System.Drawing.Point(56, 70);
            this.labelUwaga.Name = "labelUwaga";
            this.labelUwaga.Size = new System.Drawing.Size(0, 17);
            this.labelUwaga.TabIndex = 2;
            this.labelUwaga.Click += new System.EventHandler(this.labelUwaga_Click);
            // 
            // Komunikat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 174);
            this.Controls.Add(this.labelUwaga);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.label1);
            this.Name = "Komunikat";
            this.Text = "Komunikat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelUwaga;
    }
}