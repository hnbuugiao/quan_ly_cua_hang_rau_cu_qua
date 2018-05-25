namespace CuaHangRauCuQua
{
    partial class Dangxuat
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnDangxuat = new System.Windows.Forms.Button();
            this.lblhanghoa_MaHH = new FlatUI.FlatLabel();
            this.flatLabel1 = new FlatUI.FlatLabel();
            this.flatLabel2 = new FlatUI.FlatLabel();
            this.SuspendLayout();
            // 
            // btnDangxuat
            // 
            this.btnDangxuat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(46)))), ((int)(((byte)(60)))));
            this.btnDangxuat.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(46)))), ((int)(((byte)(60)))));
            this.btnDangxuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangxuat.Font = new System.Drawing.Font("UTM Nokia Standard", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangxuat.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnDangxuat.Location = new System.Drawing.Point(307, 2);
            this.btnDangxuat.Name = "btnDangxuat";
            this.btnDangxuat.Size = new System.Drawing.Size(99, 54);
            this.btnDangxuat.TabIndex = 5;
            this.btnDangxuat.Text = "ĐĂNG XUẤT";
            this.btnDangxuat.UseVisualStyleBackColor = false;
            this.btnDangxuat.Click += new System.EventHandler(this.btnDangxuat_Click);
            // 
            // lblhanghoa_MaHH
            // 
            this.lblhanghoa_MaHH.AutoSize = true;
            this.lblhanghoa_MaHH.BackColor = System.Drawing.Color.Transparent;
            this.lblhanghoa_MaHH.Font = new System.Drawing.Font("UTM Nokia Standard", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblhanghoa_MaHH.ForeColor = System.Drawing.Color.White;
            this.lblhanghoa_MaHH.Location = new System.Drawing.Point(4, 20);
            this.lblhanghoa_MaHH.Name = "lblhanghoa_MaHH";
            this.lblhanghoa_MaHH.Size = new System.Drawing.Size(74, 20);
            this.lblhanghoa_MaHH.TabIndex = 44;
            this.lblhanghoa_MaHH.Text = "Xin chào, ";
            // 
            // flatLabel1
            // 
            this.flatLabel1.AutoSize = true;
            this.flatLabel1.BackColor = System.Drawing.Color.Transparent;
            this.flatLabel1.Font = new System.Drawing.Font("UTM Nokia Standard", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flatLabel1.ForeColor = System.Drawing.Color.White;
            this.flatLabel1.Location = new System.Drawing.Point(70, 20);
            this.flatLabel1.Name = "flatLabel1";
            this.flatLabel1.Size = new System.Drawing.Size(78, 20);
            this.flatLabel1.TabIndex = 45;
            this.flatLabel1.Text = "Nhân viên";
            // 
            // flatLabel2
            // 
            this.flatLabel2.AutoSize = true;
            this.flatLabel2.BackColor = System.Drawing.Color.Transparent;
            this.flatLabel2.Font = new System.Drawing.Font("UTM Nokia Standard", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flatLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(130)))), ((int)(((byte)(131)))));
            this.flatLabel2.Location = new System.Drawing.Point(155, 20);
            this.flatLabel2.Name = "flatLabel2";
            this.flatLabel2.Size = new System.Drawing.Size(105, 20);
            this.flatLabel2.TabIndex = 46;
            this.flatLabel2.Text = "Nguyễn Văn A";
            this.flatLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Dangxuat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flatLabel2);
            this.Controls.Add(this.flatLabel1);
            this.Controls.Add(this.lblhanghoa_MaHH);
            this.Controls.Add(this.btnDangxuat);
            this.Name = "Dangxuat";
            this.Size = new System.Drawing.Size(406, 58);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDangxuat;
        private FlatUI.FlatLabel lblhanghoa_MaHH;
        public FlatUI.FlatLabel flatLabel1;
        public FlatUI.FlatLabel flatLabel2;
    }
}
