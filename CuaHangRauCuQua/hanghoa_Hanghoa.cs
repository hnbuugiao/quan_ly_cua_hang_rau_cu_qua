using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace CuaHangRauCuQua
{
    public partial class hanghoa_Hanghoa : UserControl
    {
        public hanghoa_Hanghoa()
        {
            InitializeComponent();
        }
        // Kết nối
        SqlConnection sc = new SqlConnection(@"Data Source=.\SqlExpress;Initial Catalog=quanlybanhang;Integrated Security=True");

        /// <summary>
        /// Hàm lấy giá trị từ một câu truy vấn
        /// </summary>
        /// <param name="sql">Chuỗi truy vấn</param>
        /// <returns></returns>
        private String getValue(String sql)
        {
            try
            {
                sc.Open();
                SqlCommand cmd;
                cmd = new SqlCommand(sql, sc);
                var value = cmd.ExecuteScalar();
                sc.Close();
                return value.ToString();
            }
            catch
            {
                return "KhongTonTai";
            }
        }
        /// <summary>
        /// Hàm tạo mã tự động
        /// </summary>
        /// <returns></returns>
        private string TaoMaHH()
        {
            string kq = "";
            string ma = getValue("SELECT TOP 1 MaHH FROM HANG_HOA ORDER BY MaHH DESC");
            if (ma == "KhongTonTai")
            {
                kq = "001";
            }
            else
            {
                int so = int.Parse(ma.Substring(2)) + 1;
                kq = so.ToString("000");
            }
            return kq;
        }
        /// <summary>
        /// Xử lí các thành phần khi load 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hanghoa_Hanghoa_Load(object sender, EventArgs e)
        {
            this.txthanghoa_MaHH.Text = TaoMaHH();
            this.txthanghoa_TenHH.Focus();
            sc.Open();

            //LOAD DATA dgvhanghoa
            SqlCommand cmd1 = sc.CreateCommand();
            cmd1.CommandText = "select HANG_HOA.MaHH, HANG_HOA.TenHH, NHOM_HANG.TenNH from HANG_HOA, NHOM_HANG where NHOM_HANG.MaNH=HANG_HOA.MaNH";
            SqlDataAdapter adap1 = new SqlDataAdapter(cmd1);
            DataTable dtRecord1 = new DataTable();
            adap1.Fill(dtRecord1);
            dgvhanghoa.DataSource = dtRecord1;
            cmd1.ExecuteNonQuery();

            //LOAD COMBOBOX NHOMHANG
           
            string strCmd = "select * from NHOM_HANG";
            SqlDataAdapter da = new SqlDataAdapter(strCmd, sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1);
            cbhanghoa_Nhomhang.DataSource = ds1.Tables[0];
            cbhanghoa_Nhomhang.DisplayMember = "TenNH";
            cbhanghoa_Nhomhang.ValueMember = "MaNH";
            cbhanghoa_Nhomhang.Enabled = true;
            this.cbhanghoa_Nhomhang.SelectedIndex = -1;

            //LOAD DATA dgvHanghoadaydu
            SqlCommand cmd = sc.CreateCommand();
            cmd.CommandText = "select HANG_HOA.MaHH, NHOM_HANG.TenNH, HANG_HOA.TenHH, ngayupdate.Giaban, ngayupdate.NgayCapNhatBan from HANG_HOA,NHOM_HANG,DON_GIA_BAN as ngayupdate where ngayupdate.NgayCapNhatBan = (select MAX(NgayCapNhatBan) from DON_GIA_BAN where MaHH = ngayupdate.MaHH) and HANG_HOA.MaHH = ngayupdate.MaHH and NHOM_HANG.MaNH=HANG_HOA.MaNH";           
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dtRecord = new DataTable();
            adap.Fill(dtRecord);
            dgvhanghoadaydu.DataSource = dtRecord;
            dgvhanghoadaydu.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy hh:mm tt";
            cmd.ExecuteNonQuery();

            //LOAD DATA dgvgiaban
            SqlCommand cmd2 = sc.CreateCommand();
            cmd2.CommandText = "select HANG_HOA.MaHH, HANG_HOA.TenHH, ngayupdate.Gianhap, ngayupdate.NgayCapNhatNhap from HANG_HOA,DON_GIA_NHAP as ngayupdate where ngayupdate.NgayCapNhatNhap = (select MAX(NgayCapNhatNhap) from DON_GIA_NHAP where MaHH = ngayupdate.MaHH) and HANG_HOA.MaHH = ngayupdate.MaHH";
            SqlDataAdapter adap2 = new SqlDataAdapter(cmd2);
            DataTable dtRecord2 = new DataTable();
            adap2.Fill(dtRecord2);
            dgvgiaban.DataSource = dtRecord2;
            dgvgiaban.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy hh:mm tt";
            cmd2.ExecuteNonQuery();
            sc.Close();

        }
        /// <summary>
        /// Cắt bỏ khoảng trắng dư thừa trong chuỗi đưa vào và chỉ giữ lại một khoảng trắng duy nhất giữa các chuỗi ký tự
        /// </summary>
        /// <param name="Input">Chuỗi cần bỏ khoảng trắng</param>
        /// <returns></returns>
        public string catKhoangtrang(string Input)
        {
            string strPattern = "\\s+";
            Regex rgx = new Regex(strPattern);
            string Ouput = rgx.Replace(Input, " ");
            return Ouput;
        }
        /// <summary>
        /// Cắt bỏ ký tự HH trong mã hàng hóa
        /// </summary>
        /// <param name="Input">Chuỗi cần bỏ ký tự HH</param>
        /// <returns></returns>
        public string catMaHH(string Input)
        {
            string strPattern = "HH";
            Regex rgx = new Regex(strPattern);
            string Ouput = rgx.Replace(Input, "");
            return Ouput;
        }
        /// <summary>
        /// Xử lí sự kiện khi click lên datagirdview sẽ truyền các giá trị từ datagridview hiển thị lên textbox phần hàng hóa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvhanghoa_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.txthanghoa_MaHH.Text = catMaHH(dgvhanghoa.CurrentRow.Cells["hanghoa_MaHH"].Value.ToString());
            this.txthanghoa_TenHH.Text = dgvhanghoa.CurrentRow.Cells["hanghoa_TenHH"].Value.ToString();
            this.cbhanghoa_Nhomhang.Text = dgvhanghoa.CurrentRow.Cells["hanghoa_TenNH"].Value.ToString();
        }
        /// <summary>
        /// Xóa các dữ liệu có sẵn tại 2 ô textbox và 1 combobox bên phần hàng hóa thành rỗng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnhanghoaNhaplai_Click(object sender, EventArgs e)
        {
            this.txthanghoa_MaHH.Clear();
            this.txthanghoa_TenHH.Clear();
            this.cbhanghoa_Nhomhang.DataSource = null;
            this.txthanghoa_TenHH.Focus();
            hanghoa_Hanghoa_Load(sender, e);
        }
        /// <summary>
        /// Thêm mới hàng hóa vào cơ sở dữ liệu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnhanghoaThem_Click(object sender, EventArgs e)
        {
            if (this.txthanghoa_MaHH.TextLength == 0 || this.txthanghoa_TenHH.TextLength == 0 || this.cbhanghoa_Nhomhang.Text=="" || catKhoangtrang(this.cbhanghoa_Nhomhang.Text) == " " || catKhoangtrang(this.txthanghoa_TenHH.Text)==" ")
            {
                Alert.Show("CHƯA NHẬP ĐẦY ĐỦ THÔNG TIN", Alert.AlertType.warning);
            }
            else if (this.txthanghoa_MaHH.TextLength > 5)
            {
                Alert.Show("MÃ HÀNG VƯỢT QUÁ 5 KÝ TỰ", Alert.AlertType.info);
            }
            else if (this.txthanghoa_TenHH.TextLength > 30)
            {
                Alert.Show("TÊN HÀNG HÓA VƯỢT QUÁ 30 KÝ TỰ", Alert.AlertType.info);
            }
            else
            {
                try
                {
                    //Tìm tên hàng hóa thêm mới đã có trong cơ sở dữ liệu chưa
                    sc.Open();
                    DataTable dt = new DataTable();
                    string sqltk = "select * from HANG_HOA where TenHH like N'" + this.txthanghoa_TenHH.Text + "'";
                    SqlDataAdapter ad = new SqlDataAdapter(sqltk, sc);
                    ad.Fill(dt);
                    sc.Close();
                    if (dt.Rows.Count != 0)
                    {
                        Alert.Show("TÊN HÀNG ĐÃ TỒN TẠI", Alert.AlertType.error);
                    }
                    else
                    {
                        sc.Open();
                        string sql = "insert HANG_HOA values(N'HH" + this.txthanghoa_MaHH.Text + "',N'" + catKhoangtrang(this.txthanghoa_TenHH.Text) + "',N'" + this.cbhanghoa_Nhomhang.SelectedValue.ToString() + "')"; // N phòng trường hợp lỗi font tiếng việt  
                        string sql1 = "insert TON_KHO values(N'HH" + this.txthanghoa_MaHH.Text + "',0)";               
                        SqlCommand cmd = new SqlCommand(sql, sc);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        SqlCommand cmd1 = new SqlCommand(sql1, sc);
                        cmd1.ExecuteNonQuery();
                        cmd1.Dispose();
                        sc.Close();
                        Alert.Show("ĐÃ THÊM THÀNH CÔNG", Alert.AlertType.success);
                        this.txthanghoa_MaHH.Clear();
                        this.txthanghoa_TenHH.Clear();
                        this.txthanghoa_TenHH.Focus();
                        hanghoa_Hanghoa_Load(sender, e);
                    }
                    
                }
                catch
                {
                    Alert.Show("THÊM KHÔNG THÀNH CÔNG", Alert.AlertType.error);
                    sc.Close();
                }
            }
        }
        /// <summary>
        /// Sửa thông tin hàng hóa nếu có sai sót 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnhanghoaSua_Click(object sender, EventArgs e)
        {
            if (this.txthanghoa_MaHH.TextLength == 0 || this.txthanghoa_TenHH.TextLength == 0 || this.cbhanghoa_Nhomhang.Text == "" || catKhoangtrang(this.cbhanghoa_Nhomhang.Text) == " " || catKhoangtrang(this.txthanghoa_TenHH.Text) == " ")
            {
                Alert.Show("CHƯA NHẬP ĐẦY ĐỦ THÔNG TIN", Alert.AlertType.warning);
            }
            else if (this.txthanghoa_MaHH.TextLength > 5)
            {
                Alert.Show("MÃ HÀNG VƯỢT QUÁ 5 KÝ TỰ", Alert.AlertType.info);
            }
            else if (this.txthanghoa_TenHH.TextLength > 30)
            {
                Alert.Show("TÊN HÀNG HÓA VƯỢT QUÁ 30 KÝ TỰ", Alert.AlertType.info);
            }
            else
            {
                try
                {
                    // Kiểm tra xem mã hàng hóa cần sửa đã có trong cơ sở dữ liệu hay chưa
                    DataTable dt = new DataTable();
                    string sqltk = "select * from HANG_HOA where MaHH like N'HH" + this.txthanghoa_MaHH.Text + "'";
                    SqlDataAdapter ad = new SqlDataAdapter(sqltk, sc);
                    ad.Fill(dt);
                    sc.Close();
                    if (dt.Rows.Count == 0)
                    {
                        Alert.Show("MÃ KHÔNG TỒN TẠI", Alert.AlertType.error);
                    }
                    else
                    {
                        sc.Open();
                        string sql = "update HANG_HOA set TenHH=N'" + catKhoangtrang(this.txthanghoa_TenHH.Text) + "',MaNH=N'" + this.cbhanghoa_Nhomhang.SelectedValue.ToString() + "'where MaHH=N'HH" + this.txthanghoa_MaHH.Text + "'";
                        SqlCommand cmd = new SqlCommand(sql, sc);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();

                        sc.Close();
                        Alert.Show("ĐÃ SỬA THÀNH CÔNG", Alert.AlertType.success);
                        this.txthanghoa_MaHH.Clear();
                        this.txthanghoa_TenHH.Clear();
                        this.txthanghoa_TenHH.Focus();
                        hanghoa_Hanghoa_Load(sender, e);
                    }
                    
                }
                catch
                {
                    Alert.Show("SỬA KHÔNG THÀNH CÔNG", Alert.AlertType.error);
                    sc.Close();
                }
            }
        }
        /// <summary>
        /// Thêm giá bán cho một mã hàng hóa khi đã có giá nhập
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btngiabanThem_Click(object sender, EventArgs e)
        {
            if (this.txtgiaban_MaHH.TextLength == 0 || this.txtgiaban_Giaban.TextLength == 0)
            {
                Alert.Show("CHƯA NHẬP ĐẦY ĐỦ THÔNG TIN", Alert.AlertType.warning);
            }
            else if (this.txtgiaban_MaHH.TextLength>5){
                Alert.Show("MÃ HÀNG VƯỢT QUÁ 5 KÝ TỰ", Alert.AlertType.info);
            }
            else if (this.txtgiaban_Giaban.TextLength > 10)
            {
                Alert.Show("GIÁ BÁN VƯỢT QUÁ 10 KÝ TỰ", Alert.AlertType.info);
            }
            else
            {
                try
                {
                    //Tìm mã hàng muốn nhập giá bán, đã có giá nhập hay chưa
                    sc.Open();
                    DataTable dt = new DataTable();
                    string sqltk = "select HANG_HOA.MaHH, HANG_HOA.TenHH, ngayupdate.Gianhap, ngayupdate.NgayCapNhatNhap from HANG_HOA,DON_GIA_NHAP as ngayupdate where ngayupdate.NgayCapNhatNhap = (select MAX(NgayCapNhatNhap) from DON_GIA_NHAP where MaHH = ngayupdate.MaHH) and HANG_HOA.MaHH = ngayupdate.MaHH and ngayupdate.MaHH  like N'HH" + this.txtgiaban_MaHH.Text + "'";
                    SqlDataAdapter ad = new SqlDataAdapter(sqltk, sc);
                    ad.Fill(dt);
                    sc.Close();
                    if (dt.Rows.Count == 0)
                    {
                        Alert.Show("MÃ HÀNG KHÔNG HỢP LỆ", Alert.AlertType.error);
                    }
                    else
                    {
                        sc.Open();     
                        string sql1 = "insert DON_GIA_BAN values(N'HH" + this.txtgiaban_MaHH.Text + "','" + float.Parse(this.txtgiaban_Giaban.Text) + "',GETDATE())"; // N phòng trường hợp lỗi font tiếng việt                                                                                                                                                                               //SqlCommand cmd = new SqlCommand(sql, sc);
                        SqlCommand cmd1 = new SqlCommand(sql1, sc);
                        cmd1.ExecuteNonQuery();
                        cmd1.Dispose();
                        sc.Close();
                        Alert.Show("ĐÃ THÊM THÀNH CÔNG", Alert.AlertType.success);
                        hanghoa_Hanghoa_Load(sender, e);
                    }
                }
                catch
                {
                    Alert.Show("THÊM KHÔNG THÀNH CÔNG", Alert.AlertType.error);
                    sc.Close();
                }
            }     
        }
        /// <summary>
        /// Sửa lại giá bán của một mã hàng hóa nếu có sai sót
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btngiabanSua_Click(object sender, EventArgs e)
        {
            if (this.txtgiaban_MaHH.TextLength == 0 || this.txtgiaban_Giaban.TextLength == 0)
            {
                Alert.Show("CHƯA NHẬP ĐẦY ĐỦ THÔNG TIN", Alert.AlertType.warning);
            }
            else if (this.txtgiaban_MaHH.TextLength > 5)
            {
                Alert.Show("MÃ HÀNG VƯỢT QUÁ 5 KÝ TỰ", Alert.AlertType.info);
            }
            else if (this.txtgiaban_Giaban.TextLength > 10)
            {
                Alert.Show("GIÁ BÁN VƯỢT QUÁ 10 KÝ TỰ", Alert.AlertType.info);
            }
            else
            {
                try
                {
                    //Kiểm tra mã hàng muốn sửa có tồn tại hay không
                    sc.Open();
                    DataTable dt = new DataTable();
                    string sqltk = "select HANG_HOA.MaNH, HANG_HOA.MaHH, HANG_HOA.TenHH, ngayupdate.Giaban, ngayupdate.NgayCapNhatBan from HANG_HOA,DON_GIA_BAN as ngayupdate where ngayupdate.NgayCapNhatBan = (select MAX(NgayCapNhatBan) from DON_GIA_BAN where MaHH = ngayupdate.MaHH) and HANG_HOA.MaHH = ngayupdate.MaHH and ngayupdate.MaHH like N'HH" + this.txtgiaban_MaHH.Text + "'";
                    SqlDataAdapter ad = new SqlDataAdapter(sqltk, sc);
                    ad.Fill(dt);
                    sc.Close();
                    if (dt.Rows.Count == 0)
                    {
                        Alert.Show("MÃ HÀNG HÓA KHÔNG HỢP LỆ", Alert.AlertType.error);
                    }
                    else
                    {
                        sc.Open();
                        string sql1 = "update DON_GIA_BAN set GiaBan=N'" + this.txtgiaban_Giaban.Text + "'where MaHH=N'HH" + this.txtgiaban_MaHH.Text + "'";
                        SqlCommand cmd2 = new SqlCommand(sql1, sc);
                        cmd2.ExecuteNonQuery();
                        cmd2.Dispose();
                        sc.Close();
                        Alert.Show("ĐÃ SỬA THÀNH CÔNG", Alert.AlertType.success);
                        hanghoa_Hanghoa_Load(sender, e);
                    }
                }
                catch
                {
                    Alert.Show("SỬA KHÔNG THÀNH CÔNG", Alert.AlertType.error);
                    sc.Close();
                }
            }
        }
        /// <summary>
        /// Xóa các dữ liệu có sẵn tại 2 ô textbox bên phần giá bán thành textbox rỗng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btngiabanNhaplai_Click(object sender, EventArgs e)
        {
            this.txtgiaban_MaHH.Clear();
            this.txtgiaban_Giaban.Clear();
            this.txtgiaban_MaHH.Focus();
            hanghoa_Hanghoa_Load(sender, e);
        }
        /// <summary>
        /// Xử lí sự kiện khi click lên datagirdview sẽ truyền mã hàng hóa từ datagridview hiển thị lên textbox phần giá bán
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvgiaban_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.txtgiaban_MaHH.Text = catMaHH(dgvgiaban.CurrentRow.Cells["giaban_MaHH"].Value.ToString());
        }
        /// <summary>
        /// Xử lí sự kiện khi click lên datagirdview sẽ truyền mã hàng hóa từ datagridview hiển thị lên textbox phần giá bán
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvhanghoadaydu_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.txtgiaban_MaHH.Text = catMaHH(dgvhanghoadaydu.CurrentRow.Cells["MaHH"].Value.ToString());
            this.txtgiaban_Giaban.Text = dgvhanghoadaydu.CurrentRow.Cells["Giaban"].Value.ToString();
            //this.datepickerNgayCNGiaBan.Value = Convert.ToDateTime(dgvhanghoadaydu.CurrentRow.Cells["NgayCapNhatBan"].Value);
        }
        /// <summary>
        /// Tìm kiếm theo các mã hàng hóa đã có đầy đủ giá nhập và giá bán, theo mã hàng hóa hoặc tên hàng hóa  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnhanghoaTimkiem_Click(object sender, EventArgs e)
        {
            if (this.txthanghoaTimkiem.TextLength == 0)
            {
                Alert.Show("NHẬP TỪ KHÓA TÌM KIẾM", Alert.AlertType.warning);
            }
            else
            {
                sc.Open();
                DataTable dt = new DataTable();
                string sql = "select HANG_HOA.MaHH, NHOM_HANG.TenNH, HANG_HOA.TenHH, ngayupdate.Giaban, ngayupdate.NgayCapNhatBan from HANG_HOA,NHOM_HANG,DON_GIA_BAN as ngayupdate where ngayupdate.NgayCapNhatBan = (select MAX(NgayCapNhatBan) from DON_GIA_BAN where MaHH = ngayupdate.MaHH) and HANG_HOA.MaHH = ngayupdate.MaHH and NHOM_HANG.MaNH=HANG_HOA.MaNH and (HANG_HOA.TenHH like N'%" + this.txthanghoaTimkiem.Text + "%' or HANG_HOA.MaNH like N'%" + this.txthanghoaTimkiem.Text + "%')";
                SqlDataAdapter ad = new SqlDataAdapter(sql, sc);
                ad.Fill(dt);
                sc.Close();

                dgvhanghoadaydu.DataSource = dt;
                if (dt.Rows.Count == 0)
                {
                    Alert.Show("KHÔNG TÌM THẤY DỮ LIỆU", Alert.AlertType.info);

                }
            }
        }
        /// <summary>
        /// Hàm kiểm tra nhập liệu, chỉ được nhập số, không cho nhập chữ cái và các ký tự đặc biệt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void kiemtraNhapkytu(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// Xử lí sự kiện KeyPress gọi tới hàm kiemtraNhapkytu để bẫy lỗi nhập liệu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txthanghoa_MaHH_KeyPress(object sender, KeyPressEventArgs e)
        {
            kiemtraNhapkytu(sender, e);
        }
        /// <summary>
        /// Xử lí sự kiện KeyPress gọi tới hàm kiemtraNhapkytu để bẫy lỗi nhập liệu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtgiaban_MaHH_KeyPress(object sender, KeyPressEventArgs e)
        {
            kiemtraNhapkytu(sender, e);
        }
        /// <summary>
        /// Xử lí sự kiện KeyPress gọi tới hàm kiemtraNhapkytu để bẫy lỗi nhập liệu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtgiaban_Giaban_KeyPress(object sender, KeyPressEventArgs e)
        {
            kiemtraNhapkytu(sender, e);
        }
        /// <summary>
        /// Xử lí sự kiện KeyPress kiểm tra không cho nhập số
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txthanghoa_TenHH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
