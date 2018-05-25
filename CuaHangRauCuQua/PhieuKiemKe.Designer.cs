namespace CuaHangRauCuQua
{
    partial class PhieuKiemKe
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.rbKhoangTG = new System.Windows.Forms.RadioButton();
            this.rbNam = new System.Windows.Forms.RadioButton();
            this.rbQuy = new System.Windows.Forms.RadioButton();
            this.rbThang = new System.Windows.Forms.RadioButton();
            this.rbNgay = new System.Windows.Forms.RadioButton();
            this.dtlNL = new System.Windows.Forms.DateTimePicker();
            this.dtlTuNgay = new System.Windows.Forms.DateTimePicker();
            this.dtlToiNgay = new System.Windows.Forms.DateTimePicker();
            this.cbNam = new System.Windows.Forms.ComboBox();
            this.cbQuy = new System.Windows.Forms.ComboBox();
            this.cbThang = new System.Windows.Forms.ComboBox();
            this.dtgvThongKe = new Bunifu.Framework.UI.BunifuCustomDataGrid();
            this.giaban_MaHH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.giaban_TenHH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.giaban_Gianhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_In = new System.Windows.Forms.Button();
            this.btn_ThongKe = new System.Windows.Forms.Button();
            this.lbTongTienNhap = new FlatUI.FlatLabel();
            this.lbTongTienBan = new FlatUI.FlatLabel();
            this.flatLabel7 = new FlatUI.FlatLabel();
            this.flatLabel6 = new FlatUI.FlatLabel();
            this.flatLabel5 = new FlatUI.FlatLabel();
            this.flatLabel4 = new FlatUI.FlatLabel();
            this.flatLabel3 = new FlatUI.FlatLabel();
            this.flatLabel2 = new FlatUI.FlatLabel();
            this.flatLabel1 = new FlatUI.FlatLabel();
            this.lblgiaban_MaHH = new FlatUI.FlatLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvThongKe)).BeginInit();
            this.SuspendLayout();
            // 
            // rbKhoangTG
            // 
            this.rbKhoangTG.AutoSize = true;
            this.rbKhoangTG.Font = new System.Drawing.Font("UTM Nokia Standard", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbKhoangTG.ForeColor = System.Drawing.Color.Black;
            this.rbKhoangTG.Location = new System.Drawing.Point(875, 86);
            this.rbKhoangTG.Name = "rbKhoangTG";
            this.rbKhoangTG.Size = new System.Drawing.Size(177, 23);
            this.rbKhoangTG.TabIndex = 80;
            this.rbKhoangTG.TabStop = true;
            this.rbKhoangTG.Text = "Theo khoảng thời gian";
            this.rbKhoangTG.UseVisualStyleBackColor = true;
            this.rbKhoangTG.CheckedChanged += new System.EventHandler(this.rbKhoangTG_CheckedChanged);
            // 
            // rbNam
            // 
            this.rbNam.AutoSize = true;
            this.rbNam.Font = new System.Drawing.Font("UTM Nokia Standard", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNam.ForeColor = System.Drawing.Color.Black;
            this.rbNam.Location = new System.Drawing.Point(671, 86);
            this.rbNam.Name = "rbNam";
            this.rbNam.Size = new System.Drawing.Size(157, 23);
            this.rbNam.TabIndex = 79;
            this.rbNam.TabStop = true;
            this.rbNam.Text = "Thống kê theo năm";
            this.rbNam.UseVisualStyleBackColor = true;
            this.rbNam.CheckedChanged += new System.EventHandler(this.rbNam_CheckedChanged);
            // 
            // rbQuy
            // 
            this.rbQuy.AutoSize = true;
            this.rbQuy.Font = new System.Drawing.Font("UTM Nokia Standard", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbQuy.ForeColor = System.Drawing.Color.Black;
            this.rbQuy.Location = new System.Drawing.Point(481, 86);
            this.rbQuy.Name = "rbQuy";
            this.rbQuy.Size = new System.Drawing.Size(151, 23);
            this.rbQuy.TabIndex = 78;
            this.rbQuy.TabStop = true;
            this.rbQuy.Text = "Thống kê theo quý";
            this.rbQuy.UseVisualStyleBackColor = true;
            this.rbQuy.CheckedChanged += new System.EventHandler(this.rbQuy_CheckedChanged);
            // 
            // rbThang
            // 
            this.rbThang.AutoSize = true;
            this.rbThang.Font = new System.Drawing.Font("UTM Nokia Standard", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbThang.ForeColor = System.Drawing.Color.Black;
            this.rbThang.Location = new System.Drawing.Point(270, 86);
            this.rbThang.Name = "rbThang";
            this.rbThang.Size = new System.Drawing.Size(166, 23);
            this.rbThang.TabIndex = 77;
            this.rbThang.TabStop = true;
            this.rbThang.Text = "Thống kê theo tháng";
            this.rbThang.UseVisualStyleBackColor = true;
            this.rbThang.CheckedChanged += new System.EventHandler(this.rbThang_CheckedChanged);
            // 
            // rbNgay
            // 
            this.rbNgay.AutoSize = true;
            this.rbNgay.Font = new System.Drawing.Font("UTM Nokia Standard", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNgay.ForeColor = System.Drawing.Color.Black;
            this.rbNgay.Location = new System.Drawing.Point(73, 86);
            this.rbNgay.Name = "rbNgay";
            this.rbNgay.Size = new System.Drawing.Size(159, 23);
            this.rbNgay.TabIndex = 76;
            this.rbNgay.TabStop = true;
            this.rbNgay.Text = "Thống kê theo ngày";
            this.rbNgay.UseVisualStyleBackColor = true;
            this.rbNgay.CheckedChanged += new System.EventHandler(this.rbNgay_CheckedChanged);
            // 
            // dtlNL
            // 
            this.dtlNL.CustomFormat = "dd/MM/yyyy";
            this.dtlNL.Font = new System.Drawing.Font("UTM Nokia Standard", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtlNL.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtlNL.Location = new System.Drawing.Point(334, 142);
            this.dtlNL.Name = "dtlNL";
            this.dtlNL.Size = new System.Drawing.Size(176, 26);
            this.dtlNL.TabIndex = 75;
            this.dtlNL.Value = new System.DateTime(2017, 10, 27, 15, 51, 21, 0);
            // 
            // dtlTuNgay
            // 
            this.dtlTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtlTuNgay.Font = new System.Drawing.Font("UTM Nokia Standard", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtlTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtlTuNgay.Location = new System.Drawing.Point(334, 178);
            this.dtlTuNgay.Name = "dtlTuNgay";
            this.dtlTuNgay.Size = new System.Drawing.Size(176, 26);
            this.dtlTuNgay.TabIndex = 74;
            this.dtlTuNgay.Value = new System.DateTime(2017, 10, 27, 15, 51, 21, 0);
            // 
            // dtlToiNgay
            // 
            this.dtlToiNgay.CustomFormat = "dd/MM/yyyy";
            this.dtlToiNgay.Font = new System.Drawing.Font("UTM Nokia Standard", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtlToiNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtlToiNgay.Location = new System.Drawing.Point(334, 216);
            this.dtlToiNgay.Name = "dtlToiNgay";
            this.dtlToiNgay.Size = new System.Drawing.Size(176, 26);
            this.dtlToiNgay.TabIndex = 73;
            this.dtlToiNgay.Value = new System.DateTime(2017, 10, 27, 15, 51, 21, 0);
            // 
            // cbNam
            // 
            this.cbNam.DisplayMember = "2017";
            this.cbNam.Font = new System.Drawing.Font("UTM Nokia Standard", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbNam.FormattingEnabled = true;
            this.cbNam.Items.AddRange(new object[] {
            "2015",
            "2016",
            "2017",
            "2018",
            "2019",
            "2020",
            "2021",
            "2022",
            "2023",
            "2024",
            "2025"});
            this.cbNam.Location = new System.Drawing.Point(671, 216);
            this.cbNam.Name = "cbNam";
            this.cbNam.Size = new System.Drawing.Size(176, 27);
            this.cbNam.TabIndex = 71;
            this.cbNam.Text = "2017";
            this.cbNam.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbNam_KeyPress);
            // 
            // cbQuy
            // 
            this.cbQuy.DisplayMember = "1";
            this.cbQuy.Font = new System.Drawing.Font("UTM Nokia Standard", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbQuy.FormattingEnabled = true;
            this.cbQuy.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cbQuy.Location = new System.Drawing.Point(671, 178);
            this.cbQuy.Name = "cbQuy";
            this.cbQuy.Size = new System.Drawing.Size(176, 27);
            this.cbQuy.TabIndex = 70;
            this.cbQuy.Text = "1";
            this.cbQuy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbQuy_KeyPress);
            // 
            // cbThang
            // 
            this.cbThang.DisplayMember = "1";
            this.cbThang.Font = new System.Drawing.Font("UTM Nokia Standard", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbThang.FormattingEnabled = true;
            this.cbThang.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cbThang.Location = new System.Drawing.Point(671, 141);
            this.cbThang.Name = "cbThang";
            this.cbThang.Size = new System.Drawing.Size(176, 27);
            this.cbThang.TabIndex = 72;
            this.cbThang.Text = "1";
            this.cbThang.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbThang_KeyPress);
            // 
            // dtgvThongKe
            // 
            this.dtgvThongKe.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("UTM Nokia Standard", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgvThongKe.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgvThongKe.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dtgvThongKe.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtgvThongKe.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(46)))), ((int)(((byte)(60)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("UTM Nokia Standard", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvThongKe.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dtgvThongKe.ColumnHeadersHeight = 35;
            this.dtgvThongKe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtgvThongKe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.giaban_MaHH,
            this.giaban_TenHH,
            this.giaban_Gianhap,
            this.Column1,
            this.Column2});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("UTM Nokia Standard", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(130)))), ((int)(((byte)(131)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgvThongKe.DefaultCellStyle = dataGridViewCellStyle3;
            this.dtgvThongKe.DoubleBuffered = true;
            this.dtgvThongKe.EnableHeadersVisualStyles = false;
            this.dtgvThongKe.HeaderBgColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(46)))), ((int)(((byte)(60)))));
            this.dtgvThongKe.HeaderForeColor = System.Drawing.Color.WhiteSmoke;
            this.dtgvThongKe.Location = new System.Drawing.Point(73, 336);
            this.dtgvThongKe.Name = "dtgvThongKe";
            this.dtgvThongKe.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dtgvThongKe.RowTemplate.Height = 30;
            this.dtgvThongKe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvThongKe.Size = new System.Drawing.Size(965, 224);
            this.dtgvThongKe.TabIndex = 69;
            // 
            // giaban_MaHH
            // 
            this.giaban_MaHH.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.giaban_MaHH.DataPropertyName = "MaHH";
            this.giaban_MaHH.HeaderText = "Mã Hàng";
            this.giaban_MaHH.Name = "giaban_MaHH";
            this.giaban_MaHH.ReadOnly = true;
            // 
            // giaban_TenHH
            // 
            this.giaban_TenHH.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.giaban_TenHH.DataPropertyName = "TenHH";
            this.giaban_TenHH.HeaderText = "Tên Hàng";
            this.giaban_TenHH.Name = "giaban_TenHH";
            this.giaban_TenHH.ReadOnly = true;
            // 
            // giaban_Gianhap
            // 
            this.giaban_Gianhap.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.giaban_Gianhap.DataPropertyName = "SoLuongBan";
            this.giaban_Gianhap.HeaderText = "Số lượng bán (kg)";
            this.giaban_Gianhap.Name = "giaban_Gianhap";
            this.giaban_Gianhap.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.DataPropertyName = "SoLuongNhap";
            this.Column1.HeaderText = "Số lượng nhập (kg)";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.DataPropertyName = "SoLuongTonKho";
            this.Column2.HeaderText = "Số lượng tồn kho (Tính đến thời điểm hiện tại)";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // btn_In
            // 
            this.btn_In.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(46)))), ((int)(((byte)(60)))));
            this.btn_In.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(46)))), ((int)(((byte)(60)))));
            this.btn_In.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_In.Font = new System.Drawing.Font("UTM Nokia Standard", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_In.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_In.Location = new System.Drawing.Point(577, 271);
            this.btn_In.Name = "btn_In";
            this.btn_In.Size = new System.Drawing.Size(85, 38);
            this.btn_In.TabIndex = 68;
            this.btn_In.Text = "IN";
            this.btn_In.UseVisualStyleBackColor = false;
            this.btn_In.Click += new System.EventHandler(this.btn_In_Click);
            // 
            // btn_ThongKe
            // 
            this.btn_ThongKe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(46)))), ((int)(((byte)(60)))));
            this.btn_ThongKe.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(46)))), ((int)(((byte)(60)))));
            this.btn_ThongKe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ThongKe.Font = new System.Drawing.Font("UTM Nokia Standard", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ThongKe.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_ThongKe.Location = new System.Drawing.Point(486, 271);
            this.btn_ThongKe.Name = "btn_ThongKe";
            this.btn_ThongKe.Size = new System.Drawing.Size(85, 38);
            this.btn_ThongKe.TabIndex = 67;
            this.btn_ThongKe.Text = "THỐNG KÊ";
            this.btn_ThongKe.UseVisualStyleBackColor = false;
            this.btn_ThongKe.Click += new System.EventHandler(this.btn_ThongKe_Click);
            // 
            // lbTongTienNhap
            // 
            this.lbTongTienNhap.AutoSize = true;
            this.lbTongTienNhap.BackColor = System.Drawing.Color.Transparent;
            this.lbTongTienNhap.Font = new System.Drawing.Font("UTM Nokia Standard", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTongTienNhap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(46)))), ((int)(((byte)(60)))));
            this.lbTongTienNhap.Location = new System.Drawing.Point(853, 572);
            this.lbTongTienNhap.Name = "lbTongTienNhap";
            this.lbTongTienNhap.Size = new System.Drawing.Size(44, 18);
            this.lbTongTienNhap.TabIndex = 65;
            this.lbTongTienNhap.Text = "0 VNĐ";
            // 
            // lbTongTienBan
            // 
            this.lbTongTienBan.AutoSize = true;
            this.lbTongTienBan.BackColor = System.Drawing.Color.Transparent;
            this.lbTongTienBan.Font = new System.Drawing.Font("UTM Nokia Standard", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTongTienBan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(46)))), ((int)(((byte)(60)))));
            this.lbTongTienBan.Location = new System.Drawing.Point(853, 598);
            this.lbTongTienBan.Name = "lbTongTienBan";
            this.lbTongTienBan.Size = new System.Drawing.Size(44, 18);
            this.lbTongTienBan.TabIndex = 64;
            this.lbTongTienBan.Text = "0 VNĐ";
            // 
            // flatLabel7
            // 
            this.flatLabel7.AutoSize = true;
            this.flatLabel7.BackColor = System.Drawing.Color.Transparent;
            this.flatLabel7.Font = new System.Drawing.Font("UTM Nokia Standard", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flatLabel7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(46)))), ((int)(((byte)(60)))));
            this.flatLabel7.Location = new System.Drawing.Point(743, 572);
            this.flatLabel7.Name = "flatLabel7";
            this.flatLabel7.Size = new System.Drawing.Size(104, 16);
            this.flatLabel7.TabIndex = 63;
            this.flatLabel7.Text = "Tổng tiền nhập:";
            // 
            // flatLabel6
            // 
            this.flatLabel6.AutoSize = true;
            this.flatLabel6.BackColor = System.Drawing.Color.Transparent;
            this.flatLabel6.Font = new System.Drawing.Font("UTM Nokia Standard", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flatLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(46)))), ((int)(((byte)(60)))));
            this.flatLabel6.Location = new System.Drawing.Point(743, 598);
            this.flatLabel6.Name = "flatLabel6";
            this.flatLabel6.Size = new System.Drawing.Size(96, 16);
            this.flatLabel6.TabIndex = 62;
            this.flatLabel6.Text = "Tổng tiền bán:";
            // 
            // flatLabel5
            // 
            this.flatLabel5.AutoSize = true;
            this.flatLabel5.BackColor = System.Drawing.Color.Transparent;
            this.flatLabel5.Font = new System.Drawing.Font("UTM Nokia Standard", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flatLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(46)))), ((int)(((byte)(60)))));
            this.flatLabel5.Location = new System.Drawing.Point(219, 216);
            this.flatLabel5.Name = "flatLabel5";
            this.flatLabel5.Size = new System.Drawing.Size(79, 20);
            this.flatLabel5.TabIndex = 61;
            this.flatLabel5.Text = "Đến ngày:";
            // 
            // flatLabel4
            // 
            this.flatLabel4.AutoSize = true;
            this.flatLabel4.BackColor = System.Drawing.Color.Transparent;
            this.flatLabel4.Font = new System.Drawing.Font("UTM Nokia Standard", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flatLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(46)))), ((int)(((byte)(60)))));
            this.flatLabel4.Location = new System.Drawing.Point(229, 179);
            this.flatLabel4.Name = "flatLabel4";
            this.flatLabel4.Size = new System.Drawing.Size(69, 20);
            this.flatLabel4.TabIndex = 60;
            this.flatLabel4.Text = "Từ ngày:";
            // 
            // flatLabel3
            // 
            this.flatLabel3.AutoSize = true;
            this.flatLabel3.BackColor = System.Drawing.Color.Transparent;
            this.flatLabel3.Font = new System.Drawing.Font("UTM Nokia Standard", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flatLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(46)))), ((int)(((byte)(60)))));
            this.flatLabel3.Location = new System.Drawing.Point(602, 220);
            this.flatLabel3.Name = "flatLabel3";
            this.flatLabel3.Size = new System.Drawing.Size(46, 20);
            this.flatLabel3.TabIndex = 59;
            this.flatLabel3.Text = "Năm:";
            // 
            // flatLabel2
            // 
            this.flatLabel2.AutoSize = true;
            this.flatLabel2.BackColor = System.Drawing.Color.Transparent;
            this.flatLabel2.Font = new System.Drawing.Font("UTM Nokia Standard", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flatLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(46)))), ((int)(((byte)(60)))));
            this.flatLabel2.Location = new System.Drawing.Point(607, 182);
            this.flatLabel2.Name = "flatLabel2";
            this.flatLabel2.Size = new System.Drawing.Size(41, 20);
            this.flatLabel2.TabIndex = 58;
            this.flatLabel2.Text = "Quý:";
            // 
            // flatLabel1
            // 
            this.flatLabel1.AutoSize = true;
            this.flatLabel1.BackColor = System.Drawing.Color.Transparent;
            this.flatLabel1.Font = new System.Drawing.Font("UTM Nokia Standard", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flatLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(46)))), ((int)(((byte)(60)))));
            this.flatLabel1.Location = new System.Drawing.Point(592, 145);
            this.flatLabel1.Name = "flatLabel1";
            this.flatLabel1.Size = new System.Drawing.Size(57, 20);
            this.flatLabel1.TabIndex = 66;
            this.flatLabel1.Text = "Tháng:";
            // 
            // lblgiaban_MaHH
            // 
            this.lblgiaban_MaHH.AutoSize = true;
            this.lblgiaban_MaHH.BackColor = System.Drawing.Color.Transparent;
            this.lblgiaban_MaHH.Font = new System.Drawing.Font("UTM Nokia Standard", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblgiaban_MaHH.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(46)))), ((int)(((byte)(60)))));
            this.lblgiaban_MaHH.Location = new System.Drawing.Point(248, 143);
            this.lblgiaban_MaHH.Name = "lblgiaban_MaHH";
            this.lblgiaban_MaHH.Size = new System.Drawing.Size(49, 20);
            this.lblgiaban_MaHH.TabIndex = 57;
            this.lblgiaban_MaHH.Text = "Ngày:";
            // 
            // PhieuKiemKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rbKhoangTG);
            this.Controls.Add(this.rbNam);
            this.Controls.Add(this.rbQuy);
            this.Controls.Add(this.rbThang);
            this.Controls.Add(this.rbNgay);
            this.Controls.Add(this.dtlNL);
            this.Controls.Add(this.dtlTuNgay);
            this.Controls.Add(this.dtlToiNgay);
            this.Controls.Add(this.cbNam);
            this.Controls.Add(this.cbQuy);
            this.Controls.Add(this.cbThang);
            this.Controls.Add(this.dtgvThongKe);
            this.Controls.Add(this.btn_In);
            this.Controls.Add(this.btn_ThongKe);
            this.Controls.Add(this.lbTongTienNhap);
            this.Controls.Add(this.lbTongTienBan);
            this.Controls.Add(this.flatLabel7);
            this.Controls.Add(this.flatLabel6);
            this.Controls.Add(this.flatLabel5);
            this.Controls.Add(this.flatLabel4);
            this.Controls.Add(this.flatLabel3);
            this.Controls.Add(this.flatLabel2);
            this.Controls.Add(this.flatLabel1);
            this.Controls.Add(this.lblgiaban_MaHH);
            this.Name = "PhieuKiemKe";
            this.Size = new System.Drawing.Size(1113, 647);
            this.Load += new System.EventHandler(this.PhieuKiemKe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvThongKe)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbKhoangTG;
        private System.Windows.Forms.RadioButton rbNam;
        private System.Windows.Forms.RadioButton rbQuy;
        private System.Windows.Forms.RadioButton rbThang;
        private System.Windows.Forms.RadioButton rbNgay;
        private System.Windows.Forms.DateTimePicker dtlNL;
        private System.Windows.Forms.DateTimePicker dtlTuNgay;
        private System.Windows.Forms.DateTimePicker dtlToiNgay;
        private System.Windows.Forms.ComboBox cbNam;
        private System.Windows.Forms.ComboBox cbQuy;
        private System.Windows.Forms.ComboBox cbThang;
        private Bunifu.Framework.UI.BunifuCustomDataGrid dtgvThongKe;
        private System.Windows.Forms.DataGridViewTextBoxColumn giaban_MaHH;
        private System.Windows.Forms.DataGridViewTextBoxColumn giaban_TenHH;
        private System.Windows.Forms.DataGridViewTextBoxColumn giaban_Gianhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button btn_In;
        private System.Windows.Forms.Button btn_ThongKe;
        private FlatUI.FlatLabel lbTongTienNhap;
        private FlatUI.FlatLabel lbTongTienBan;
        private FlatUI.FlatLabel flatLabel7;
        private FlatUI.FlatLabel flatLabel6;
        private FlatUI.FlatLabel flatLabel5;
        private FlatUI.FlatLabel flatLabel4;
        private FlatUI.FlatLabel flatLabel3;
        private FlatUI.FlatLabel flatLabel2;
        private FlatUI.FlatLabel flatLabel1;
        private FlatUI.FlatLabel lblgiaban_MaHH;
    }
}
