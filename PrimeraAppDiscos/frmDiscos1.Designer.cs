namespace PrimeraAppDiscos
{
    partial class frmDiscos1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvDisco = new System.Windows.Forms.DataGridView();
            this.pbxDisco = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisco)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDisco)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDisco
            // 
            this.dgvDisco.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDisco.Location = new System.Drawing.Point(31, 12);
            this.dgvDisco.Name = "dgvDisco";
            this.dgvDisco.RowHeadersWidth = 51;
            this.dgvDisco.RowTemplate.Height = 24;
            this.dgvDisco.Size = new System.Drawing.Size(750, 312);
            this.dgvDisco.TabIndex = 0;
            this.dgvDisco.SelectionChanged += new System.EventHandler(this.dgvDisco_SelectionChanged);
            // 
            // pbxDisco
            // 
            this.pbxDisco.Location = new System.Drawing.Point(820, 12);
            this.pbxDisco.Name = "pbxDisco";
            this.pbxDisco.Size = new System.Drawing.Size(405, 312);
            this.pbxDisco.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxDisco.TabIndex = 1;
            this.pbxDisco.TabStop = false;
            // 
            // frmDiscos1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1256, 357);
            this.Controls.Add(this.pbxDisco);
            this.Controls.Add(this.dgvDisco);
            this.Name = "frmDiscos1";
            this.Text = "Discos";
            this.Load += new System.EventHandler(this.frmDiscos1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisco)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDisco)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDisco;
        private System.Windows.Forms.PictureBox pbxDisco;
    }
}

