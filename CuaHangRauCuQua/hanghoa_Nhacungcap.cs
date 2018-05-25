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
    public partial class hanghoa_Nhacungcap : UserControl
    {
        public hanghoa_Nhacungcap()
        {
            InitializeComponent();
        }
        string ma_ncc;
        SqlConnection sc = new SqlConnection(@"Data Source=.\SqlExpress;Initial Catalog=quanlybanhang;Integrated Security=True");
        /// <summary>
        /// Xử lí các thành phần khi load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hanghoa_Nhacungcap_Load(object sender, EventArgs e)
        {
            this.txtTenNCC.Focus();
            sc.Open();
            SqlCommand cmd = sc.CreateCommand();
            cmd.CommandText = "select * from NHA_CUNG_CAP";
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dtRecord = new DataTable();
            adap.Fill(dtRecord);
            dgvNhacungcap.DataSource = dtRecord;
            cmd.ExecuteNonQuery();
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
        /// Cắt bỏ tất cả các khoảng trắng có trong email
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public string catKhoangtrangEmail(string Input)
        {
            string strPattern = "\\s+";
            Regex rgx = new Regex(strPattern);
            string Ouput = rgx.Replace(Input, "");
            return Ouput;
        }
        /// <summary>
        /// Xử lí sự kiện khi click lên datagirdview sẽ truyền các giá trị từ datagridview hiển thị lên textbox 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvNhacungcap_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ma_ncc = dgvNhacungcap.CurrentRow.Cells["MaNCC"].Value.ToString();
            this.txtTenNCC.Text = dgvNhacungcap.CurrentRow.Cells["TenNCC"].Value.ToString();
            this.txtSDT.Text = dgvNhacungcap.CurrentRow.Cells["SDT"].Value.ToString();
            this.txtEmail.Text = dgvNhacungcap.CurrentRow.Cells["Email"].Value.ToString();
            this.txtDiachi.Text = dgvNhacungcap.CurrentRow.Cells["DiaChi"].Value.ToString();
        }
        /// <summary>
        /// Thêm mới nhà cung cấp vào cơ sở dữ liệu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (this.txtTenNCC.TextLength == 0 || this.txtSDT.TextLength == 0 || this.txtEmail.TextLength == 0 || this.txtDiachi.TextLength == 0 || catKhoangtrang(this.txtTenNCC.Text)==" " || catKhoangtrang(this.txtSDT.Text) == " " || catKhoangtrang(this.txtDiachi.Text) == " " || catKhoangtrang(this.txtEmail.Text) == " ")
            {
                Alert.Show("CHƯA NHẬP ĐẦY ĐỦ THÔNG TIN", Alert.AlertType.warning);
            }
            else if (this.txtTenNCC.TextLength > 30)
            {
                Alert.Show("TÊN VƯỢT QUÁ 30 KÝ TỰ", Alert.AlertType.info);
            }
            else if (this.txtSDT.TextLength > 11 || this.txtSDT.TextLength < 10)
            {
                Alert.Show("SĐT PHẢI CÓ 10 HOẶC 11 SỐ", Alert.AlertType.info);
            }
            else if (this.txtDiachi.TextLength > 100)
            {
                Alert.Show("ĐỊA CHỈ VƯỢT QUÁ 100 KÝ TỰ", Alert.AlertType.info);
            }
            else if (this.txtEmail.TextLength > 30)
            {
                Alert.Show("EMAIL VƯỢT QUÁ 30 KÝ TỰ", Alert.AlertType.info);
            }
            else
            {
                try
                {
                    sc.Open();
                    
                    //Tìm SĐT muốn thêm mới đã có trong cơ sở dữ liệu hay chưa
                    DataTable dt1 = new DataTable();
                    string sqltk1 = "select * from NHA_CUNG_CAP where SDT like N'" + this.txtSDT.Text + "'";
                    SqlDataAdapter ad1 = new SqlDataAdapter(sqltk1, sc);
                    ad1.Fill(dt1);
                    //Tìm ĐỊA CHỈ muốn thêm mới đã có trong cơ sở dữ liệu hay chưa
                    DataTable dt2 = new DataTable();
                    string sqltk2 = "select * from NHA_CUNG_CAP where DiaChi like N'" + catKhoangtrang(this.txtDiachi.Text) + "'";
                    SqlDataAdapter ad2 = new SqlDataAdapter(sqltk2, sc);
                    ad2.Fill(dt2);
                    //Tìm EMAIL muốn thêm mới đã có trong cơ sở dữ liệu hay chưa
                    DataTable dt3 = new DataTable();
                    string sqltk3 = "select * from NHA_CUNG_CAP where Email like N'" + catKhoangtrangEmail(this.txtEmail.Text) + "'";
                    SqlDataAdapter ad3 = new SqlDataAdapter(sqltk3, sc);
                    ad3.Fill(dt3);
                    sc.Close();
                    if (dt1.Rows.Count != 0)
                    {
                        Alert.Show("SĐT ĐÃ ĐƯỢC SỬ DỤNG", Alert.AlertType.error);
                    }
                    else if (dt2.Rows.Count != 0)
                    {
                        Alert.Show("ĐỊA CHỈ ĐÃ ĐƯỢC SỬ DỤNG", Alert.AlertType.error);
                    }
                    else if (dt3.Rows.Count != 0)
                    {
                        Alert.Show("EMAIL ĐÃ ĐƯỢC SỬ DỤNG", Alert.AlertType.error);
                    }
                    else
                    {
                        sc.Open();
                        string sql = "insert NHA_CUNG_CAP values(N'" + catKhoangtrang(this.txtTenNCC.Text) + "',N'" + this.txtSDT.Text + "',N'" + catKhoangtrang(this.txtDiachi.Text) + "',N'" + catKhoangtrangEmail(this.txtEmail.Text) + "')"; // N phòng trường hợp lỗi font tiếng việt                   
                        SqlCommand cmd = new SqlCommand(sql, sc);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        sc.Close();
                        Alert.Show("ĐÃ THÊM THÀNH CÔNG", Alert.AlertType.success);
                        hanghoa_Nhacungcap_Load(sender, e);
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
        /// Xóa các dữ liệu có sẵn tại các textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNhaplai_Click(object sender, EventArgs e)
        {
            this.txtTenNCC.Clear();
            this.txtSDT.Clear();
            this.txtEmail.Clear();
            this.txtDiachi.Clear();
            this.txtTenNCC.Focus();
            this.txtTimkiem.Clear();
            hanghoa_Nhacungcap_Load(sender, e);
        } 
        /// <summary>
        /// Sửa lại giá bán của một nhà cung cấp nếu có sai sót
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (this.txtTenNCC.TextLength == 0 || this.txtSDT.TextLength == 0 || this.txtEmail.TextLength == 0 || this.txtDiachi.TextLength == 0 || catKhoangtrang(this.txtTenNCC.Text) == " " || catKhoangtrang(this.txtSDT.Text) == " " || catKhoangtrang(this.txtDiachi.Text) == " " || catKhoangtrang(this.txtEmail.Text) == " ")
            {
                Alert.Show("CHƯA NHẬP ĐẦY ĐỦ THÔNG TIN", Alert.AlertType.warning);
            }
            else if (this.txtTenNCC.TextLength > 30)
            {
                Alert.Show("TÊN VƯỢT QUÁ 30 KÝ TỰ", Alert.AlertType.info);
            }
            else if (this.txtSDT.TextLength > 11 || this.txtSDT.TextLength < 10)
            {
                Alert.Show("SĐT PHẢI CÓ 10 HOẶC 11 SỐ", Alert.AlertType.info);
            }
            else if (this.txtDiachi.TextLength > 100)
            {
                Alert.Show("ĐỊA CHỈ VƯỢT QUÁ 100 KÝ TỰ", Alert.AlertType.info);
            }
            else if (this.txtEmail.TextLength > 30)
            {
                Alert.Show("EMAIL VƯỢT QUÁ 30 KÝ TỰ", Alert.AlertType.info);
            }
            else
            {
                try
                {
                    sc.Open();
                    string sql = "update NHA_CUNG_CAP set TenNCC=N'" + catKhoangtrang(this.txtTenNCC.Text) + "',SDT=N'" + this.txtSDT.Text + "',Email=N'" + catKhoangtrangEmail(this.txtEmail.Text) + "',DiaChi=N'" + catKhoangtrang(this.txtDiachi.Text) + "'where MaNCC=N'" + int.Parse(ma_ncc) + "'";

                    SqlCommand cmd = new SqlCommand(sql, sc);

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    sc.Close();
                    Alert.Show("ĐÃ SỬA THÀNH CÔNG", Alert.AlertType.success);
                    hanghoa_Nhacungcap_Load(sender, e);
                }
                catch
                {
                    Alert.Show("SỬA KHÔNG THÀNH CÔNG", Alert.AlertType.error);
                }
            }
        }
        /// <summary>
        /// Xóa một nhà cung cấp ra khỏi cơ sở dữ liệu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtTenNCC.TextLength == 0 || this.txtSDT.TextLength == 0 || this.txtEmail.TextLength == 0 || this.txtDiachi.TextLength == 0)
                {
                    Alert.Show("VUI LÒNG CHỌN NHÀ CUNG CẤP", Alert.AlertType.warning);
                }
                else
                {
                    sc.Open();
                    string sql = "delete NHA_CUNG_CAP where MaNCC=N'" + int.Parse(ma_ncc) + "'";
                    SqlCommand cmd = new SqlCommand(sql, sc);

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    sc.Close();
                    Alert.Show("ĐÃ XÓA THÀNH CÔNG", Alert.AlertType.success);
                    hanghoa_Nhacungcap_Load(sender, e);
                }
            }
            catch
            {
                Alert.Show("XÓA KHÔNG THÀNH CÔNG", Alert.AlertType.error);
            }
        }
        /// <summary>
        /// Tìm kiếm một nhà cung cấp theo tên hoặc số điện thoại
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
                string sql = "select * from NHA_CUNG_CAP where TenNCC like N'%" + this.txtTimkiem.Text + "%' or SDT like N'%" + this.txtTimkiem.Text + "%'";
                SqlDataAdapter ad = new SqlDataAdapter(sql, sc);
                ad.Fill(dt);
                sc.Close();

                dgvNhacungcap.DataSource = dt;
                if (dt.Rows.Count == 0)
                {
                    Alert.Show("KHÔNG TÌM THẤY DỮ LIỆU", Alert.AlertType.info);
                }
            }
        }
        /// <summary>
        /// Kiểm tra nhập liệu, không được nhập số
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTenNCC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// Kiểm tra nhập liệu, chỉ được nhập số, không cho nhập chữ cái và các ký tự đặc biệt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
