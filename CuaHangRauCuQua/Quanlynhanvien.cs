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
    public partial class Quanlynhanvien : UserControl
    {
        static public DataSet ds;
        static public SqlDataAdapter dap, dap1;
        static public SqlCommandBuilder sqlcm;
        static public DataTable table, table1;
        SqlCommand command;
        public string gt, kq;
        public int guima,value=0,dem=0;
       
        public Quanlynhanvien()
        {
            InitializeComponent();

            //Load combobox Quyen
            string strCmd = "select * from QUYEN";
            SqlDataAdapter da = new SqlDataAdapter(strCmd, conn);
            DataSet ds1 = new DataSet();
            da.Fill(ds1);

            cboQ.DataSource = ds1.Tables[0];
            cboQ.DisplayMember = "TenQuyen";
            cboQ.ValueMember = "MaQuyen";
            cboQ.Enabled = true;
            this.cboQ.SelectedIndex = -1;
        }
        SqlConnection conn = new SqlConnection(
            @"Data Source=.\SqlExpress;Initial Catalog=quanlybanhang;Integrated Security=True");
        /*
        /// <summary>
        /// Hàm lấy giá trị
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private String getValue(String sql)
        {
            try
            {
                conn.Open();
                SqlCommand cmd;
                cmd = new SqlCommand(sql, conn);
                var value = cmd.ExecuteScalar();
                conn.Close();
                return value.ToString();
            }
            catch
            {
                return "KhongTonTai";
            }
        }*/

        /// <summary>
        /// Hàm tự động tạo 1 mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        private string TaoMaNV()
        {

            conn.Open();
            string kq = "";
            string sql = "select count(*) from NHAN_VIEN";
            command = new SqlCommand(sql, conn);
            string ma = command.ExecuteScalar().ToString();
            conn.Close();
            if (ma == "0")
            {
                kq = "001";
            }
            else
            {
                int so = int.Parse(ma) + 1;
                kq = so.ToString("000");
            }
            return kq;
        }

        /// <summary>
        /// Hàm cắt khoản trắng của chuỗi
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public string strtrim(string Input)
        {
            string strPattern = "\\s+";
            Regex rgx = new Regex(strPattern);
            string Ouput = rgx.Replace(Input, " ");
            return Ouput;
        }

        /// <summary>
        /// Load dữ liệu lên Datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Quanlynhanvien_Load(object sender, EventArgs e)
        {
            this.txtMaNV.Clear();
            
            dap = new SqlDataAdapter("SELECT nv.MaNV,nv.TenNV,nv.GioiTinh,nv.NgaySinh,nv.SDT,QUYEN.TenQuyen,nv.TrangThai from NHAN_VIEN as nv, QUYEN WHERE nv.MaQuyen=QUYEN.MaQuyen", conn);
            
            table = new DataTable();
            dap.Fill(table);
            dgvNhanVien.DataSource = table;
            dgvNhanVien.Columns[3].DefaultCellStyle.Format="dd/MM/yyyy";
           
        }

        /// <summary>
        /// Hàm làm mới lại tất cả textbox,combobox,checkbox và enable ComboBox quyền
        /// </summary>
        public void xoaAll()
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            rbtNu.Checked = false;
            rbtNam.Checked = false;
            txtSDT.Text = "";
            txtMK.Text = "";
            chkTT.Checked = false;
            cboQ.Enabled = true;
            cboQ.Text = "";
            //cboQ.DataSource = null;
            

        }

        /// <summary>
        /// Bắt sự kiện checkbox trạng thái
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkTT_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTT.Checked == true)
            {
                value = 1;
            }
            else
            {
                value = 0;
            }
        }

        /// <summary>
        /// Thêm một nhân viên mới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.cboQ.Text == "")
                    Alert.Show("TÊN QUYỀN KHÔNG ĐƯỢC TRỐNG", Alert.AlertType.warning);
                else if (this.txtTenNV.TextLength == 0)
                    Alert.Show("TÊN NHÂN VIÊN KHÔNG ĐƯỢC TRỐNG", Alert.AlertType.warning);

                else if (this.txtSDT.TextLength == 0)
                    Alert.Show("SĐT NHÂN VIÊN KHÔNG ĐƯỢC TRỐNG", Alert.AlertType.warning);
                else if (this.txtSDT.TextLength > 11 || this.txtSDT.TextLength < 10)
                {
                    Alert.Show("SĐT PHẢI CÓ 10 HOẶC 11 SỐ", Alert.AlertType.info);
                }
                else if (this.txtMK.TextLength == 0)
                    Alert.Show("MẬT KHẨU KHÔNG ĐƯỢC TRỐNG", Alert.AlertType.warning);

                else if (this.rbtNu.Checked == false && this.rbtNam.Checked == false)
                    Alert.Show("CHƯA CHỌN GIỚI TÍNH", Alert.AlertType.warning);


                else
                {
                    //Kiểm tra SDT đã tồn tại hay chưa
                    DataTable dt1 = new DataTable();
                    string sqltk1 = "select * from NHAN_VIEN where SDT like N'" + this.txtSDT.Text + "'";
                    SqlDataAdapter ad1 = new SqlDataAdapter(sqltk1, conn);
                    ad1.Fill(dt1);

                    conn.Close();
                    if (dt1.Rows.Count != 0)
                    {
                        Alert.Show("SĐT ĐÃ ĐƯỢC SỬ DỤNG", Alert.AlertType.error);
                    }

                    else
                    {
                        //Kiểm tra Mã Nhân Viên đã tồn tại hay chưa
                        DataTable dt2 = new DataTable();
                        string sqltk2 = "select * from NHAN_VIEN where MaNV like N'" + this.txtMaNV.Text + "'";
                        SqlDataAdapter ad2 = new SqlDataAdapter(sqltk2, conn);
                        ad2.Fill(dt2);

                        conn.Close();
                        if (dt2.Rows.Count != 0)
                        {
                            Alert.Show("Mã NHÂN VIÊN ĐÃ ĐƯỢC SỬ DỤNG", Alert.AlertType.error);
                        }

                        else
                        {
                            conn.Open();
                            //string vDate = DateTime.Parse(datepickerNgaysinh.Value).ToString("yyyy-MM-dd");
                            string sql = "Insert into NHAN_VIEN (MaNV,TenNV,GioiTinh,NgaySinh,SDT,MaQuyen,MatKhau,TrangThai)  Values (@MaNV,@TenNV,@GioiTinh,@NgaySinh,@SDT,@MaQuyen,@MatKhau,@TrangThai)";
                            command = new SqlCommand(sql, conn);
                            command.Parameters.Add(new SqlParameter("@MaNV", txtMaNV.Text));
                            command.Parameters.Add(new SqlParameter("@TenNV", strtrim(txtTenNV.Text)));
                            command.Parameters.Add(new SqlParameter("@GioiTinh", kq));
                            //command.Parameters.Add(new SqlParameter("NgaySinh", vDate));
                            command.Parameters.Add(new SqlParameter("@NgaySinh", date.Value));
                            //command.Parameters.Add(new SqlParameter("NgaySinh", txtNgaySinh.Text));
                            command.Parameters.Add(new SqlParameter("@SDT", txtSDT.Text));
                            command.Parameters.Add(new SqlParameter("@MaQuyen", cboQ.SelectedValue.ToString()));
                            command.Parameters.Add(new SqlParameter("@MatKhau", txtMK.Text));
                            command.Parameters.Add(new SqlParameter("@TrangThai", value));
                            command.ExecuteNonQuery();
                            //Cập nhật Datagridview
                            dap = new SqlDataAdapter("SELECT nv.MaNV,nv.TenNV,nv.GioiTinh,nv.NgaySinh,nv.SDT,QUYEN.TenQuyen,nv.TrangThai from NHAN_VIEN as nv, QUYEN WHERE nv.MaQuyen=QUYEN.MaQuyen ", conn);
                            table = new DataTable();
                            dap.Fill(table);
                            dgvNhanVien.DataSource = table;
                            Alert.Show("THÊM NHÂN VIÊN THÀNH CÔNG", Alert.AlertType.success);
                            xoaAll();
                            //Quanlynhanvien_Load(sender, e);
                            conn.Close();
                        }
                    }
                }
                
            }
            catch
            {
                Alert.Show("LỖI KHÔNG XÁC ĐỊNH", Alert.AlertType.warning);
            }
        }
        /// <summary>
        /// Sưa thông tin nhân viên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaNV.Text == "")
                    Alert.Show("CHỌN MỘT NHÂN VIÊN ĐỂ SỬA", Alert.AlertType.warning);
                else if (this.txtTenNV.TextLength == 0)
                    Alert.Show("TÊN NHÂN VIÊN KHÔNG ĐƯỢC TRỐNG", Alert.AlertType.warning);

                else if (this.txtSDT.TextLength == 0)
                    Alert.Show("SĐT NHÂN VIÊN KHÔNG ĐƯỢC TRỐNG", Alert.AlertType.warning);

                else if (this.cboQ.Text == "")
                    Alert.Show("YÊU CẦU CHỌN QUYỀN", Alert.AlertType.warning);

                else if (this.rbtNu.Checked == false && this.rbtNam.Checked == false)
                    Alert.Show("CHƯA CHỌN GIỚI TÍNH", Alert.AlertType.warning);
                else if (this.txtSDT.TextLength > 11 || this.txtSDT.TextLength < 10)
                {
                    Alert.Show("SĐT PHẢI CÓ 10 HOẶC 11 SỐ", Alert.AlertType.info);
                }
                else if (this.txtMK.TextLength == 0)
                    Alert.Show("MẬT KHẨU KHÔNG ĐƯỢC TRỐNG", Alert.AlertType.warning);
                else
                {
                    
                        conn.Open();
                        //string vDate = DateTime.Parse(date.Text).ToString("yyyy-MM-dd");
                        string sql = "UPDATE NHAN_VIEN set TenNV=@TenNV,GioiTinh=@GioiTinh,NgaySinh=@NgaySinh,SDT=@SDT,MaQuyen=@MaQuyen,MatKhau=@MatKhau,TrangThai=@TrangThai  where MaNV=@MaNV ";
                        command = new SqlCommand(sql, conn);
                        command.Parameters.Add(new SqlParameter("@MaNV", txtMaNV.Text));
                        command.Parameters.Add(new SqlParameter("@TenNV", strtrim(txtTenNV.Text)));
                        command.Parameters.Add(new SqlParameter("@GioiTinh", kq));
                        //command.Parameters.Add(new SqlParameter("@NgaySinh", vDate));
                        command.Parameters.Add(new SqlParameter("@NgaySinh", date.Value));
                        //command.Parameters.Add(new SqlParameter("NgaySinh", txtNgaySinh.Text));
                        command.Parameters.Add(new SqlParameter("@SDT", txtSDT.Text));
                        command.Parameters.Add(new SqlParameter("@MaQuyen", cboQ.SelectedValue.ToString()));
                        command.Parameters.Add(new SqlParameter("@MatKhau", txtMK.Text));
                        command.Parameters.Add(new SqlParameter("@TrangThai", value));
                        command.ExecuteNonQuery();
                        xoaAll();
                        //Quanlynhanvien_Load(sender, e);
                    //Cập nhật Datagridview
                    dap = new SqlDataAdapter("SELECT nv.MaNV,nv.TenNV,nv.GioiTinh,nv.NgaySinh,nv.SDT,QUYEN.TenQuyen,nv.TrangThai from NHAN_VIEN as nv, QUYEN WHERE nv.MaQuyen=QUYEN.MaQuyen ", conn);
                        table = new DataTable();
                        dap.Fill(table);
                        dgvNhanVien.DataSource = table;
                        conn.Close();
                        Alert.Show("SỬA NHÂN VIÊN THÀNH CÔNG", Alert.AlertType.success);
                    }
                
            }
            catch
            {
                Alert.Show("Lỗi không xác định", Alert.AlertType.warning);
            }
            
        }

        /// <summary>
        /// Bắt sự kiện radio button thay đổi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtNam_CheckedChanged(object sender, EventArgs e)
        {
            gt = "Nam";
            kq = "";
        }

        /// <summary>
        /// Bắt sự kiện radio button thay đổi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtNu_CheckedChanged(object sender, EventArgs e)
        {
            gt = "Nu";
            kq = "X";
        }

        /// <summary>
        /// Tự động tạo 1 Mã NV mới khi đã chọn combox quyền
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboQ_TextChanged(object sender, EventArgs e)
        {
            //combobox Quyền là true thì mới thực hiện tạo mã nhân viên
            if (cboQ.Enabled == true)
            {
                if (this.cboQ.Text == "Admin")
                    this.txtMaNV.Text = "AD" + TaoMaNV();
                if (this.cboQ.Text == "Nhân Viên")
                    this.txtMaNV.Text = "NV" + TaoMaNV();
                //dem = 1;
            }
            

        }

        /// <summary>
        /// Chỉ cho nhập vào số 
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

        /// <summary>
        /// Chỉ cho phép nhập vào chữ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTenNV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cboQ_ValueMemberChanged(object sender, EventArgs e)
        {
            
        }
        
        /// <summary>
        /// Khi nhấp nút gọi hàm xoaAll() để xóa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNhaplai_Click(object sender, EventArgs e)
        {
 
            //Xóa dữ liệu và enable ComboBox quyền để có thể chọn quyền
            xoaAll();
            //Quanlynhanvien_Load(sender, e);
        }

        /// <summary>
        /// Nếu checkbox hiển thị được check thì sẽ hiện mật khẩu và ngược lại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Check_CheckedChanged(object sender, EventArgs e)
        {
            if (Check.Checked)
            {
   
                txtMK.UseSystemPasswordChar = false;
            }
            else
            {

                txtMK.UseSystemPasswordChar = true;
            }
        }

        /// <summary>
        /// Xử lí sự kiện khi click lên datagirdview sẽ truyền các giá trị từ datagridview hiển thị lên trên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvNhanVien_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                //mỗi lần click và datagridview thì ComboBox quyền bị disable
                this.cboQ.Enabled = false;

                //load dữ liệu từ datagridview
                DataGridViewRow row = this.dgvNhanVien.Rows[e.RowIndex];
                txtMaNV.Text = row.Cells[0].Value.ToString();

                this.txtTenNV.Text = row.Cells[1].Value.ToString();
                if (row.Cells[2].Value.ToString() == "X")
                    this.rbtNu.Checked = true;
                else this.rbtNam.Checked = true;
                this.date.Value = Convert.ToDateTime(row.Cells[3].Value);
                this.txtSDT.Text = row.Cells[4].Value.ToString();
                this.cboQ.Text = row.Cells[5].Value.ToString();
                Check.Checked = false;
                if (row.Cells[6].Value.ToString() == "1")
                    chkTT.Checked = true;
                else chkTT.Checked = false;

                //Lấy mật khẩu theo textbox mã nhân viên
                conn.Open();
                string sql = "SELECT MatKhau FROM Nhan_Vien WHERE MaNV= '" + txtMaNV.Text + "'";
                command = new SqlCommand(sql, conn);
                string kq = command.ExecuteScalar().ToString();
                txtMK.Text = kq.ToString();
                conn.Close();

                
            }
            catch
            {
                Alert.Show("Lỗi không xác định", Alert.AlertType.warning);
            }
        }

    }
}
