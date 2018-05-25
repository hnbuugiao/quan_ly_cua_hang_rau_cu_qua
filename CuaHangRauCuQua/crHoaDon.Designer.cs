namespace CuaHangRauCuQua
{
    partial class crHoaDon
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
            this.cr_HoaDon = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // cr_HoaDon
            // 
            this.cr_HoaDon.ActiveViewIndex = -1;
            this.cr_HoaDon.AutoSize = true;
            this.cr_HoaDon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cr_HoaDon.Cursor = System.Windows.Forms.Cursors.Default;
            this.cr_HoaDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cr_HoaDon.Font = new System.Drawing.Font("UTM Nokia Standard", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cr_HoaDon.Location = new System.Drawing.Point(0, 0);
            this.cr_HoaDon.Name = "cr_HoaDon";
            this.cr_HoaDon.Size = new System.Drawing.Size(680, 503);
            this.cr_HoaDon.TabIndex = 0;
            // 
            // crHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 503);
            this.Controls.Add(this.cr_HoaDon);
            this.Name = "crHoaDon";
            this.Text = "crHoaDon";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        public CrystalDecisions.Windows.Forms.CrystalReportViewer cr_HoaDon;
    }
}