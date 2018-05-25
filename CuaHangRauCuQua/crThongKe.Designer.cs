namespace CuaHangRauCuQua
{
    partial class crThongKe
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
            this.cr_ThongKe = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // cr_ThongKe
            // 
            this.cr_ThongKe.ActiveViewIndex = -1;
            this.cr_ThongKe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cr_ThongKe.Cursor = System.Windows.Forms.Cursors.Default;
            this.cr_ThongKe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cr_ThongKe.Location = new System.Drawing.Point(0, 0);
            this.cr_ThongKe.Name = "cr_ThongKe";
            this.cr_ThongKe.Size = new System.Drawing.Size(780, 506);
            this.cr_ThongKe.TabIndex = 0;
            // 
            // crThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 506);
            this.Controls.Add(this.cr_ThongKe);
            this.Name = "crThongKe";
            this.Text = "crThongKe";
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer cr_ThongKe;
    }
}