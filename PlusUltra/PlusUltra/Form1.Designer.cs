namespace PlusUltra
{
    partial class wfPlusUltra
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbReponse = new System.Windows.Forms.TextBox();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.VoyantAlt = new System.Windows.Forms.Label();
            this.VoyantControl = new System.Windows.Forms.Label();
            this.VoyantMajuscule = new System.Windows.Forms.Label();
            this.lblReponse = new System.Windows.Forms.Label();
            this.btnDemarrerArreter = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblReponse2 = new PlusUltra.RevealLabel();
            this.SuspendLayout();
            // 
            // tbReponse
            // 
            this.tbReponse.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbReponse.Location = new System.Drawing.Point(32, 105);
            this.tbReponse.Name = "tbReponse";
            this.tbReponse.Size = new System.Drawing.Size(395, 29);
            this.tbReponse.TabIndex = 0;
            this.tbReponse.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.tbReponse.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.tbReponse.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // lblQuestion
            // 
            this.lblQuestion.BackColor = System.Drawing.Color.White;
            this.lblQuestion.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion.Location = new System.Drawing.Point(33, 34);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(394, 60);
            this.lblQuestion.TabIndex = 1;
            this.lblQuestion.Text = "Cliquer sur le bouton \"Démarrer\" pour commencer l\'épreuve";
            // 
            // VoyantAlt
            // 
            this.VoyantAlt.AutoSize = true;
            this.VoyantAlt.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.VoyantAlt.Location = new System.Drawing.Point(486, 185);
            this.VoyantAlt.Name = "VoyantAlt";
            this.VoyantAlt.Size = new System.Drawing.Size(19, 13);
            this.VoyantAlt.TabIndex = 2;
            this.VoyantAlt.Text = "Alt";
            // 
            // VoyantControl
            // 
            this.VoyantControl.AutoSize = true;
            this.VoyantControl.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.VoyantControl.Location = new System.Drawing.Point(539, 186);
            this.VoyantControl.Name = "VoyantControl";
            this.VoyantControl.Size = new System.Drawing.Size(40, 13);
            this.VoyantControl.TabIndex = 3;
            this.VoyantControl.Text = "Control";
            // 
            // VoyantMajuscule
            // 
            this.VoyantMajuscule.AutoSize = true;
            this.VoyantMajuscule.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.VoyantMajuscule.Location = new System.Drawing.Point(607, 186);
            this.VoyantMajuscule.Name = "VoyantMajuscule";
            this.VoyantMajuscule.Size = new System.Drawing.Size(24, 13);
            this.VoyantMajuscule.TabIndex = 4;
            this.VoyantMajuscule.Text = "Maj";
            // 
            // lblReponse
            // 
            this.lblReponse.BackColor = System.Drawing.Color.White;
            this.lblReponse.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReponse.Location = new System.Drawing.Point(33, 142);
            this.lblReponse.Name = "lblReponse";
            this.lblReponse.Size = new System.Drawing.Size(394, 40);
            this.lblReponse.TabIndex = 5;
            // 
            // btnDemarrerArreter
            // 
            this.btnDemarrerArreter.BackColor = System.Drawing.Color.LimeGreen;
            this.btnDemarrerArreter.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDemarrerArreter.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDemarrerArreter.Location = new System.Drawing.Point(504, 26);
            this.btnDemarrerArreter.Name = "btnDemarrerArreter";
            this.btnDemarrerArreter.Size = new System.Drawing.Size(112, 38);
            this.btnDemarrerArreter.TabIndex = 6;
            this.btnDemarrerArreter.Text = "Démarrer";
            this.btnDemarrerArreter.UseVisualStyleBackColor = false;
            this.btnDemarrerArreter.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(489, 77);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(155, 23);
            this.progressBar1.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Location = new System.Drawing.Point(-2, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(461, 503);
            this.panel1.TabIndex = 8;
            // 
            // lblReponse2
            // 
            this.lblReponse2.BackColor = System.Drawing.Color.White;
            this.lblReponse2.Enabled = false;
            this.lblReponse2.Location = new System.Drawing.Point(32, 224);
            this.lblReponse2.Name = "lblReponse2";
            this.lblReponse2.Size = new System.Drawing.Size(395, 41);
            this.lblReponse2.TabIndex = 5;
            this.lblReponse2.Text = "coucou";
            this.lblReponse2.Visible = false;
            // 
            // wfPlusUltra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 502);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnDemarrerArreter);
            this.Controls.Add(this.lblReponse);
            this.Controls.Add(this.VoyantMajuscule);
            this.Controls.Add(this.VoyantControl);
            this.Controls.Add(this.VoyantAlt);
            this.Controls.Add(this.lblQuestion);
            this.Controls.Add(this.tbReponse);
            this.Controls.Add(this.lblReponse2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "wfPlusUltra";
            this.Text = "PlusUltra";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbReponse;
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.Label VoyantAlt;
        private System.Windows.Forms.Label VoyantControl;
        private System.Windows.Forms.Label VoyantMajuscule;
        private System.Windows.Forms.Label lblReponse;
        private System.Windows.Forms.Button btnDemarrerArreter;
        private System.Windows.Forms.ProgressBar progressBar1;
        private RevealLabel lblReponse2;
        private System.Windows.Forms.Panel panel1;
    }
}

