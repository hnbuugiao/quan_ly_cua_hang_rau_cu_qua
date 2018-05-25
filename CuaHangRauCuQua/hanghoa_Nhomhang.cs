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
    public partial class hanghoa_Nhomhang : UserControl
    {
        public hanghoa_Nhomhang()
        {
            InitializeComponent();
        }

        SqlConnection sc = new SqlConnection(@"Data Source=.\SqlExpress;Initial Catalog=quanlybanhang;Integrated Security=True");
        /// <summary>
        /// Hàm lấy giá trị từ một câu truy vấn
        /// </summary>
        /// <param name="sql"></param>
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
        private string TaoMaNH()
        {
            string kq = "";
            string ma = getValue("SELECT TOP 1 MaNH FROM NHOM_HANG ORDER BY MaNH DESC");
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
        private void hanghoa_Nhomhang_Load(object sender, EventArgs e)
        {
            this.txtMaNH.Text = TaoMaNH();
            this.txtTenNH.Focus();
            sc.Open();
            SqlCommand cmd = sc.CreateCommand();
            cmd.CommandText = "select MaNH, TenNH from NHOM_HANG ";
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dtRecord = new DataTable();
            adap.Fill(dtRecord);
            dgvnhomhang.DataSource = dtRecord;
            sc.Close();
        }
        /// <summary>
        /// Cắt bỏ khoảng trắng dư thừa trong chuỗi đưa vào và chỉ giữ lại một khoảng trắng duy nhất giữa các chuỗi ký tự
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public string catKhoangtrang(string Input)
        {
            string strPattern = "\\s+";
            Regex rgx = new Regex(strPattern);
            string Ouput = rgx.Replace(Input, " ");
            return Ouput;
        }
        /// <summary>
        /// Cắt bỏ ký tự NH trong mã nhóm hàng
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public string catMaNH(string Input)
        {
            string strPattern = "NH";
            Regex rgx = new Regex(strPattern);
            string Ouput = rgx.Replace(Input, "");
            return Ouput;
        }
        /// <summary>
        /// Xử lí sự kiện khi click lên datagirdview sẽ truyền các giá trị từ datagridview hiển thị lên textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvnhomhang_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.txtMaNH.Text = catMaNH(dgvnhomhang.CurrentRow.Cells["MaNH"].Value.ToString());
            this.txtTenNH.Text = dgvnhomhang.CurrentRow.Cells["TenNH"].Value.ToString();
        }
        /// <summary>
        /// Thêm mới nhóm hàng vào cơ sở dữ liệu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (this.txtMaNH.TextLength == 0 || this.txtTenNH.TextLength == 0 || catKhoangtrang(this.txtTenNH.Text)==" ")
            {
                Alert.Show("CHƯA NHẬP ĐẦY ĐỦ THÔNG TIN", Alert.AlertType.warning);
            }
            else if (this.txtMaNH.TextLength > 5)
            {
                Alert.Show("MÃ VƯỢT QUÁ 5 KÝ TỰ", Alert.AlertType.info);
            }
            else if (this.txtTenNH.TextLength > 30)
            {
                Alert.Show("TÊN VƯỢT QUÁ 30 KÝ TỰ", Alert.AlertType.info);
            }
            else
            {
                try
                {
                    //Kiểm tra tên nhóm hàng này đã tồn tại trong cơ sở dữ liệu hay chưa
                    sc.Open();
                    DataTable dt = new DataTable();
                    string sqltk = "select * from NHOM_HANG where TenNH like N'" + this.txtTenNH.Text + "'";
                    SqlDataAdapter ad = new SqlDataAdapter(sqltk, sc);
                    ad.Fill(dt);
                    sc.Close();
                    if (dt.Rows.Count != 0)
                    {
                        Alert.Show("TÊN ĐÃ TỒN TẠI", Alert.AlertType.error);
                    }
                    else
                    {
                        sc.Open();
                        string sql = "insert NHOM_HANG values(N'NH" + this.txtMaNH.Text + "',N'" + catKhoangtrang(this.txtTenNH.Text) + "')"; // N phòng trường hợp lỗi font tiếng việt                   
                        SqlCommand cmd = new SqlCommand(sql, sc);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        sc.Close();
                        Alert.Show("ĐÃ THÊM THÀNH CÔNG", Alert.AlertType.success);
                        this.txtMaNH.Clear();
                        this.txtTenNH.Clear();
                        this.txtTenNH.Focus();
                        hanghoa_Nhomhang_Load(sender, e);
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
        /// Sửa thông tin nhóm hàng nếu có sai sót
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (this.txtMaNH.TextLength == 0 || this.txtTenNH.TextLength == 0 || catKhoangtrang(this.txtTenNH.Text) == " ")
            {
                Alert.Show("CHƯA NHẬP ĐẦY ĐỦ THÔNG TIN", Alert.AlertType.warning);
            }
            else if (this.txtMaNH.TextLength > 5)
            {
                Alert.Show("MÃ VƯỢT QUÁ 5 KÝ TỰ", Alert.AlertType.info);
            }
            else if (this.txtTenNH.TextLength > 30)
            {
                Alert.Show("TÊN VƯỢT QUÁ 30 KÝ TỰ", Alert.AlertType.info);
            }
            else
            {

                try
                {
                    sc.Open();
                    //Kiểm tra xem mã cần sửa đã có trong cơ sở dữ liệu hay chưa
                    DataTable dt1 = new DataTable();
                    string sqltk1 = "select * from NHOM_HANG where MaNH like N'NH" + this.txtMaNH.Text + "'";
                    SqlDataAdapter ad1 = new SqlDataAdapter(sqltk1, sc);
                    ad1.Fill(dt1);
                    //Kiểm tra xem tên cần sửa đã có trong cơ sở dữ liệu hay chưa
                    DataTable dt = new DataTable();
                    string sqltk = "select * from NHOM_HANG where TenNH like N'" + this.txtTenNH.Text + "'";
                    SqlDataAdapter ad = new SqlDataAdapter(sqltk, sc);
                    ad.Fill(dt);
                    sc.Close();
                    if (dt.Rows.Count != 0)
                    {
                        Alert.Show("TÊN ĐÃ TỒN TẠI", Alert.AlertType.error);
                    }
                    else if (dt1.Rows.Count == 0)
                    {
                        Alert.Show("MÃ KHÔNG TỒN TẠI", Alert.AlertType.error);
                    }
                    else
                    {
                        sc.Open();
                        string sql = "update NHOM_HANG set TenNH=N'" + catKhoangtrang(this.txtTenNH.Text) + "'where MaNH=N'NH" + this.txtMaNH.Text + "'";

                        SqlCommand cmd = new SqlCommand(sql, sc);

                        cmd.ExecuteNonQuery();
                        cmd.Dispose();

                        sc.Close();
                        Alert.Show("ĐÃ SỬA THÀNH CÔNG", Alert.AlertType.success);
                        this.txtMaNH.Clear();
                        this.txtTenNH.Clear();
                        this.txtTenNH.Focus();
                        hanghoa_Nhomhang_Load(sender, e);
                    }
                }
                catch
                {
                    Alert.Show("SỬA KHÔNG THÀNH CÔNG", Alert.AlertType.error);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (this.txtMaNH.TextLength == 0)
            {
                Alert.Show("NHẬP MÃ CẦN XÓA", Alert.AlertType.warning);
            }
            else if (this.txtMaNH.TextLength > 5)
            {
                Alert.Show("MÃ VƯỢT QUÁ 5 KÝ TỰ", Alert.AlertType.info);
            }
            else
            {
                try
                {
                    //Kiểm tra mã cần xóa có trong cơ sở dữ liệu hay không
                    sc.Open();
                    DataTable dt = new DataTable();
                    string sqltk = "select * from NHOM_HANG where MaNH like N'NH" + this.txtMaNH.Text + "'";
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
                        string sql = "delete NHOM_HANG where MaNH=N'NH" + this.txtMaNH.Text + "'";
                        SqlCommand cmd = new SqlCommand(sql, sc);

                        cmd.ExecuteNonQuery();
                        cmd.Dispose();

                        sc.Close();
                        Alert.Show("ĐÃ XÓA THÀNH CÔNG", Alert.AlertType.success);
                        this.txtMaNH.Clear();
                        this.txtTenNH.Clear();
                        this.txtTenNH.Focus();
                        hanghoa_Nhomhang_Load(sender, e);
                    }
                }
                catch
                {
                    Alert.Show("XÓA KHÔNG THÀNH CÔNG", Alert.AlertType.error);
                }
            }
        }
        /// <summary>
        /// Xóa dữ liệu có sẵn tại textbox thành textbox rỗng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNhaplai_Click(object sender, EventArgs e)
        {
            this.txtMaNH.Clear();
            this.txtTenNH.Clear();
            this.txtTenNH.Focus();
            this.txtTimkiem.Clear();
            hanghoa_Nhomhang_Load(sender, e);
        }
        /// <summary>
        /// Tìm kiếm nhóm hàng theo tên nhóm hàng và mã nhóm hàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            if (this.txtTimkiem.TextLength == 0)
            {
                Alert.Show("NHẬP TỪ KHÓA TÌM KIẾM", Alert.AlertType.warning);
            }
            else
            {
                sc.Open();
                DataTable dt = new DataTable();
                string sql = "select * from NHOM_HANG where TenNH like N'%" + this.txtTimkiem.Text + "%' or MaNH like N'%" + this.txtTimkiem.Text + "%'";
                SqlDataAdapter ad = new SqlDataAdapter(sql, sc);
                ad.Fill(dt);
                sc.Close();

                dgvnhomhang.DataSource = dt;
                if (dt.Rows.Count == 0)
                {
                    Alert.Show("KHÔNG TÌM THẤY DỮ LIỆU", Alert.AlertType.info);
                }

            }
        }
        /// <summary>
        /// Kiểm tra nhập liệu, chỉ được nhập số
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMaNH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// Kiểm tra nhập liệu, không được nhập số
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTenNH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

    }
}
