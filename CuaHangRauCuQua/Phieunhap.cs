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
    public partial class Phieunhap : UserControl
    {
        // Tạo 2 kết nối để dễ dàng truy cập nhiều bảng trong quá trình sử dụng
        SqlConnection cnn;
        SqlConnection cnn2;
        // Biến lưu giá trị Mã nhân viên đang đăng nhập vào hệ thống 
        public string MaNVtemp;
        // Biến lưu giá trị cũ Số lượng của từng loại hàng hoá -> cập nhật lại bảng TON_KHO
        Int32[] OldValue = new Int32[100]; 

        /// <summary>
        /// Khai báo khởi tạo Phiếu Nhập với MaNV hiện tại đang đăng nhập vào hệ thống
        /// </summary>
        /// <param name="MaNV"></param>
        public Phieunhap(string MaNV)
        {
            InitializeComponent();
            // Tạo kết nối tới server
            string connetionString = null;
            connetionString = @"Data Source=.\SqlExpress;Initial Catalog=quanlybanhang;Integrated Security=True";
            cnn = new SqlConnection(connetionString);
            cnn2 = new SqlConnection(connetionString);
            //Mở kết nối 
            cnn.Open();
            cnn2.Open();
            // Gán Mã nhân viên vào TextBox Mã nhân viên
            this.MaNVtemp = MaNV;
            tb_MaNV.Text = MaNVtemp;

            // Load combobox Nhà cung cấp
            string Query = "SELECT * FROM NHA_CUNG_CAP;";
            SqlCommand createCommand = new SqlCommand(Query, cnn);
            SqlDataReader dr = createCommand.ExecuteReader();
            while (dr.Read())
            {
                string name = dr.GetString(1);
                cb_MaNCC.Items.Add(name);
            }
            dr.Close();
            // Tắt chỉnh sửa Timeinput, chỉ mở khi nào chỉnh sửa
            timeInput.Enabled = false;
            // Đặt thời gian mặc định cho Timeinput là Now
            timeInput.Value = DateTime.Now;
        }
        
        /// <summary>
        /// Hàm cắt chuỗi Phiếu Nhập, cắt từ PN khỏi các kí tự còn lại
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public string catMaPN(string Input)
        {
            string strPattern = "PN";
            Regex rgx = new Regex(strPattern);
            string Ouput = rgx.Replace(Input, "");
            return Ouput;
        }

        /// <summary>
        /// Hàm tạo tự động Mã Phiếu Nhập theo cấu trúc ( Năm + Tháng + Ngày + Giờ + Phút + Giây)
        /// </summary>
        /// <returns></returns>
        private string TaoMaPN()
        {
            // Lấy các giá trị năm tháng ngày giờ phút giây
            string year = DateTime.Now.ToString("yyyy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string hour = DateTime.Now.ToString("hh");
            string minute = DateTime.Now.ToString("mm");
            string second = DateTime.Now.ToString("ss");

            return year + month + day + hour + minute + second;
        }

        /// <summary>
        /// Làm reset lại các TextBox, Combobox, TimeInput nếu có dữ liệu mới được cập nhật
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Phieunhap_Load(object sender, EventArgs e)
        {
            // Lấy chuỗi tự động Mã Phiếu Nhập
            string kq = TaoMaPN();
            tb_MaPN.Text = kq;
            tb_MaPN.Focus();

            // Giá trị mặc định Tổng tiền trả = 0
            tb_TongPhaiTrainput.Text = "0";

            // Bắt sự kiện cho Datagridview
            dataGridViewX1.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            dataGridViewX1.EditingControlShowing += dataGridViewX1_EditingControlShowing;

            // Load Datagridview Phiếu Nhập
            String Query3 = "SELECT MaPN,NgayNhap,TongTienTra FROM PHIEU_NHAP";
            SqlDataAdapter addapter = new SqlDataAdapter(Query3, cnn);
            DataTable dt = new DataTable();
            addapter.Fill(dt);
            dataGridViewX2.DataSource = dt;
            dataGridViewX2.Columns[1].DefaultCellStyle.Format = "dd/MM/yyyy";
        }
        
        /// <summary>
        /// Làm reset lại các TextBox, Combobox, TimeInput nếu có dữ liệu mới được cập nhật
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PhieuMoi_Click(object sender, EventArgs e)
        {
            dataGridViewX1.Rows.Clear();
            timeInput.ResetText();
            timeInput.Update();
            timeInput.Refresh();
            cb_MaNCC.ResetText();
            tb_TongPhaiTrainput.Clear();
            Phieunhap_Load(sender, e);
            // Tắt chỉnh sửa Timeinput, chỉ mở khi nào chỉnh sửa
            timeInput.Enabled = false;
        }
   

        /// <summary>
        /// Hàm cho nút Sửa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Sua_Click(object sender, EventArgs e)
        {
            // Enable nút Sửa, để sửa ngày tháng của Phiếu Nhập khi cần chỉnh sửa một phiếu nhập nào đó
            timeInput.Enabled = true;
            // Reset lại các datagridview,combobox để tránh tình trạng ghi đè dữ liệu
            dataGridViewX1.Rows.Clear();
            tb_TongPhaiTrainput.Clear();
            cb_MaNCC.ResetText();

            // Lấy vị trí của ô đã chọn trên Datagridview -> Truy tìm trong CSDL Mã Phiếu Nhập đã chọn
            int vitri = this.dataGridViewX2.SelectedCells[0].RowIndex;
            string MaPN = this.dataGridViewX2.Rows[vitri].Cells[0].Value.ToString();
            string Query = "SELECT * FROM PHIEU_NHAP WHERE MaPN = '" + MaPN + "'";
            string Query2 = "SELECT * FROM CHI_TIET_PHIEU_NHAP WHERE MaPN = '" + MaPN + "'";

            SqlCommand cmd = new SqlCommand(Query, cnn);
            SqlCommand cmd2 = new SqlCommand(Query2, cnn);

            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            // Gán các giá trị đã Select từ CSDL vào Textbox, .... 
            tb_MaPN.Text = catMaPN(dr.GetString(0));
            DateTime date = dr.GetDateTime(1);
            string temp = date.ToShortDateString();
            timeInput.Text = temp;
            tb_MaNV.Text = dr.GetString(4);
            float temp2 = float.Parse(dr[2].ToString());
            tb_TongPhaiTrainput.Text = temp2.ToString();
            int temp3 = dr.GetInt32(3);
            dr.Close();
            // Từ tên Nhà cung cấp -> Tìm được Mã nhà cung cấp
            string Query3 = "SELECT TenNCC FROM NHA_CUNG_CAP WHERE MaNCC = '" + temp3 + "'";
            SqlCommand cmd3 = new SqlCommand(Query3, cnn);
            SqlDataReader dr2 = cmd3.ExecuteReader();
            dr2.Read();
            string TenNCC = dr2[0].ToString();
            cb_MaNCC.Text = TenNCC;
            dr2.Close();

            // Load các thông tin từ CHI_TIET_PHIEU_NHAP vào Datagridview ( Chi tiết mỗi Phiếu Nhập sẽ có loại hàng hoá nào, số lượng, v.v. )
            SqlDataReader dr3 = cmd2.ExecuteReader();
            while (dr3.Read())
            {
                float SoLuongtemp = float.Parse(dr3[2].ToString());
                float DonGiatemp = float.Parse(dr3[3].ToString());
                float ThanhTientemp = SoLuongtemp * DonGiatemp;

                string MaHH = dr3.GetString(1);
                string SoLuong = dr3[2].ToString();
                string DonGia = dr3[3].ToString();

                string QueryX = "SELECT TenHH FROM HANG_HOA WHERE MaHH = '" + dr3.GetString(1) + "';";

                SqlCommand cmdX = new SqlCommand(QueryX, cnn2);
                SqlDataReader drX = cmdX.ExecuteReader();
                drX.Read();
                string TenHH = drX.GetString(0);
                string[] row = { MaHH, TenHH, SoLuong, DonGia, ThanhTientemp.ToString() };
                dataGridViewX1.Rows.Add(row);
                drX.Close();

            }
            dr3.Close();
        }
         
        /// <summary>
        /// Hàm để xoá Phiếu Nhập
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            // biến kiểm tra lỗi trong quá trình vận hành
            int check = 0;
            int sohang = dataGridViewX1.RowCount;
            for (int i = 0; i < sohang; i++)
            {

                string MaHHtemp = "";
                float SoLuongtemp = 0;
                float DonGiatemp = 0;
                String ThanhTien = "0";
     
                // check lỗi sơ bộ các thông tin Mã hàng hoá, số lượng, đơn giá
                try
                {
                    MaHHtemp = dataGridViewX1.Rows[i].Cells["MaHH"].Value.ToString();
                    SoLuongtemp = float.Parse(dataGridViewX1.Rows[i].Cells["SoLuong"].Value.ToString());
                    DonGiatemp = float.Parse(dataGridViewX1.Rows[i].Cells["Dongia"].Value.ToString());
                    ThanhTien = dataGridViewX1.Rows[i].Cells["ThanhTien"].Value.ToString();
                }
                catch
                {
                    check = 1;
                    Alert.Show("LỖI THÔNG TIN HÀNG HOÁ", Alert.AlertType.error);
                }

                // Nếu thông qua sẽ thực hiện các bước tiếp theo
                if(check == 0)
                {
                    string QuerySoLuongOld = "SELECT SoLuongTonKho FROM TON_KHO WHERE MaHH = '" + MaHHtemp + "'";
                    SqlCommand cmdSoLuongOld = new SqlCommand(QuerySoLuongOld, cnn);
                    cmdSoLuongOld.ExecuteNonQuery();
                    double SoLuong = (double)cmdSoLuongOld.ExecuteScalar();

                    try
                    {
                        string QueryUpdateTonKho = "UPDATE TON_KHO SET SoLuongTonKho -= '" + SoLuongtemp + "' WHERE MaHH = '" + MaHHtemp + "';";
                        SqlCommand cmdUpdateTonKho = new SqlCommand(QueryUpdateTonKho, cnn);
                        cmdUpdateTonKho.ExecuteNonQuery();
                    }
                    catch
                    {
                        Alert.Show("LỖI THÔNG TIN HÀNG HOÁ", Alert.AlertType.error);
                        check = 1;
                    }

                }
            }
            if(check == 0)
            {
                try
                {
                    // Nếu còn chọn trên Datagridview thì tiếp tục Xoá 
                    if (this.dataGridViewX2.SelectedCells.Count > 0)
                    {
                        // Lấy vị trí của ô trên Datagridview -> Truy tìm Mã Phiếu -> Xoá Mã Phiếu đoá khỏi CSDL
                        int vitri = this.dataGridViewX2.SelectedCells[0].RowIndex;
                        string MaPN = this.dataGridViewX2.Rows[vitri].Cells[0].Value.ToString();

                        string Query = "DELETE FROM PHIEU_NHAP WHERE MaPN = '" + MaPN + "'";
                        string Query2 = "DELETE FROM CHI_TIET_PHIEU_NHAP WHERE MaPN = '" + MaPN + "'";

                        SqlCommand cmd = new SqlCommand(Query, cnn);
                        SqlCommand cmd2 = new SqlCommand(Query2, cnn);

                        cmd2.ExecuteNonQuery();
                        cmd.ExecuteNonQuery();

                        dataGridViewX2.Rows.RemoveAt(this.dataGridViewX2.SelectedCells[0].RowIndex);

                        dataGridViewX2.Update();
                        dataGridViewX2.Refresh();
                    }
                }
                catch
                {
                    Alert.Show("XOÁ PHIẾU NHẬP THẤT BẠI", Alert.AlertType.error);
                }
            }else
            {
                Alert.Show("XOÁ PHIẾU NHẬP THẤT BẠI", Alert.AlertType.error);
            }
         

        }



        /// <summary>
        /// Hàm Lưu có 2 chức năng là Lưu phiếu nhập mới và Cập nhật lại phiếu nhập cũ 
        /// Nếu không chưa tồn tại Mã Phiếu trong CSDL, những câu lệnh trong try {} sẽ thực thi thành công
        /// Còn nếu đã tồn tại Mã Phiệu trong CSDL thì sẽ thực hiện các câu lệnh trong catch {}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Luu_Click(object sender, EventArgs e)
        {
            // Biến check = 0 -> dữ liệu tốt -> thực thi các câu lệnh 
            // Biến check = 1 -> dữ liệu có sai sót nhập liệu -> cảnh báo lỗi
            int check = 0;
            string TenNCC = "", MaPN = "", NgayNhap = "", MaNV = "";
            float TongTienTra = 0;

            // Lấy dữ liệu từ Textbox
            MaPN = tb_MaPN.Text.ToString();
            NgayNhap = timeInput.Value.ToString("yyyy/MM/dd");
            MaNV = tb_MaNV.Text.ToString();

            TongTienTra = float.Parse(tb_TongPhaiTrainput.Text.ToString());


            //Check lỗi dữ liệu nhập vào
            if (string.IsNullOrEmpty(tb_MaPN.Text))
            {
                check = 1;
                Alert.Show("CHƯA NHẬP MÃ PHIẾU NHẬP", Alert.AlertType.error);
            }

            if (string.IsNullOrEmpty(tb_MaNV.Text))
            {
                check = 1;
                Alert.Show("CHƯA NHẬP MÃ NHÂN VIÊN", Alert.AlertType.error);
            }
            // Câu lệnh SQL để đảm bảo nhân viên có tồn tại trong CSDL
            String testmanv = tb_MaNV.Text.ToString();
            String querytestmanv = "SELECT TenNV FROM NHAN_VIEN WHERE MaNV = '" + testmanv + "'";
            SqlCommand cmdtest = new SqlCommand(querytestmanv, cnn);
            String testtennv = (String)cmdtest.ExecuteScalar();

            if (testtennv == null || testtennv == String.Empty)
            {
                check = 1;
                Alert.Show("MÃ NHÂN VIÊN CHƯA ĐÚNG", Alert.AlertType.error);
            }


            if (timeInput.Value.Equals(""))
            {
                check = 1;
                Alert.Show("CHƯA NHẬP NGÀY THÁNG", Alert.AlertType.error);
            }

            if (string.IsNullOrEmpty(cb_MaNCC.Text))
            {
                check = 1;
                Alert.Show("CHƯA CHỌN NHÀ CUNG CẤP", Alert.AlertType.error);
            }
            else
            {
                TenNCC = cb_MaNCC.SelectedItem.ToString();
            }

            int testcount = dataGridViewX1.Rows.Count;
            if (testcount <= 0)
            {
                check = 1;
                Alert.Show("CHƯA NHẬP HÀNG HOÁ", Alert.AlertType.error);
            }

         
            // Nếu thông qua bước kiểm tra dữ liệu nhập vào thì thực hiện các thao tác Lưu, Cập nhật
            if (check != 1)
            {

                // Đọc dữ liệu từ combobox ( Tên nhà cung cấp) -> Tìm ra MãNCC
                int MaNCC = 0;
                string Query = "SELECT MaNCC FROM NHA_CUNG_CAP WHERE TenNCC LIKE N'" + TenNCC + "';";
                SqlCommand cmd = new SqlCommand(Query, cnn);
                MaNCC = (int)cmd.ExecuteScalar();

                // Lưu dữ liệu mới
                try
                {
                    int sohang = dataGridViewX1.RowCount;
                    int checkinsert = 0;
                    int checkinsertnext = 0;

                    for (int i = 0; i < sohang; i++)
                    {
                        String ThanhTien = dataGridViewX1.Rows[i].Cells["ThanhTien"].Value.ToString();
                        string loihang = i.ToString();
                        if (ThanhTien.ToString().Equals("0"))
                        {
                            Alert.Show("SAI THÔNG TIN HÀNG HOÁ HÀNG " + loihang, Alert.AlertType.error);
                            checkinsert = 1;

                        }
                    }

                    if (checkinsert != 1)
                    {
                        string AutoMaPN = "PN" + MaPN;
                        // Insert data vào bảng PHIEU_NHAP
                        string Query2 = "INSERT INTO PHIEU_NHAP VALUES('" + AutoMaPN + "','" + NgayNhap + "','" + TongTienTra + "','" + MaNCC + "','" + MaNV + "') ;";
                        SqlCommand cmd2 = new SqlCommand(Query2, cnn);
                        cmd2.ExecuteNonQuery();
     

                        string NgayCapNhat = DateTime.Now.ToString("yyyy/MM/dd H:mm:ss");

                        // Insert vào bảng CHI_TIET_PHIEU_NHAP
                        for (int i = 0; i < sohang; i++)
                        {
                            try
                            {
                                string MaHHtemp = "";
                                float SoLuongtemp = 0;
                                float DonGiatemp = 0;
                                String ThanhTien = "0";
                                try
                                {
                                     MaHHtemp = dataGridViewX1.Rows[i].Cells["MaHH"].Value.ToString();
                                     SoLuongtemp = float.Parse(dataGridViewX1.Rows[i].Cells["SoLuong"].Value.ToString());
                                     DonGiatemp = float.Parse(dataGridViewX1.Rows[i].Cells["Dongia"].Value.ToString());
                                     ThanhTien = dataGridViewX1.Rows[i].Cells["ThanhTien"].Value.ToString();
                                }
                                catch {
                                    checkinsertnext = 1;
                                    string QueryDeletePN = "DELETE FROM PHIEU_NHAP WHERE MaPN = '" + AutoMaPN + "'";
                                    SqlCommand cmdDeletePN = new SqlCommand(QueryDeletePN, cnn);
                                    cmdDeletePN.ExecuteNonQuery();
                                }
     
                                if(checkinsertnext == 0)
                                {
                                    string Query3 = "INSERT INTO CHI_TIET_PHIEU_NHAP VALUES('" + AutoMaPN + "','" + MaHHtemp + "','" + SoLuongtemp + "','" + DonGiatemp + "','" + NgayCapNhat + "');";
                                    SqlCommand cmd3 = new SqlCommand(Query3, cnn);
                                    cmd3.ExecuteNonQuery();

                                    // Insert vào bảng ĐƠN GIÁ NHẬP
                                    string Query4 = "INSERT INTO DON_GIA_NHAP VALUES('" + MaHHtemp + "','" + DonGiatemp + "','" + NgayCapNhat + "' );";
                                    SqlCommand cmd4 = new SqlCommand(Query4, cnn);
                                    cmd4.ExecuteNonQuery();

                                    // Update bảng TỒN KHO
                                    string Query5 = "UPDATE TON_KHO SET SoLuongTonKho += '" + SoLuongtemp + "' WHERE MaHH = '" + MaHHtemp + "';";
                                    SqlCommand cmd5 = new SqlCommand(Query5, cnn);
                                    cmd5.ExecuteNonQuery();
                                }else
                                {
                                    Alert.Show("LỖI THÔNG TIN HÀNG HOÁ", Alert.AlertType.error);
                                    checkinsertnext = 1;
                                    string QueryDeletePN = "DELETE FROM PHIEU_NHAP WHERE MaPN = '" + AutoMaPN + "'";
                                    SqlCommand cmdDeletePN = new SqlCommand(QueryDeletePN, cnn);
                                    cmdDeletePN.ExecuteNonQuery();
                                }

                            }
                            catch
                            {
                                Alert.Show("LỖI THÔNG TIN HÀNG HOÁ", Alert.AlertType.error);
                                checkinsertnext = 1;
                                string QueryDeletePN = "DELETE FROM PHIEU_NHAP WHERE MaPN = '" + AutoMaPN + "'";
                                SqlCommand cmdDeletePN = new SqlCommand(QueryDeletePN, cnn);
                                cmdDeletePN.ExecuteNonQuery();
                            }
     
                   


                        }

                        if (checkinsertnext == 0)
                            Alert.Show("ĐÃ THÊM THÀNH CÔNG", Alert.AlertType.success);
                        else
                            Alert.Show("THÊM THẤT BẠI", Alert.AlertType.error);
                        string refresh = TaoMaPN();
                    }

                }
                // Cập nhật lại Phiếu Nhập đã tồn tại
                catch
                {
                    // Biến checkupdate để bắt lỗi các sự kiến update
                    // checkupdate = 0 -> Các lệnh thực thi tốt
                    // checkupdate = 1 -> Có sai sót trong lúc thực thi
                    int checkupdate = 0;
                    string MaPNtemp = "PN" + MaPN;
                    // Update data vào bảng PHIEU_NHAP
                    string Query2 = "UPDATE PHIEU_NHAP SET NgayNhap = '" + NgayNhap + "',TongTienTra = '" + TongTienTra + "', MaNCC = '" + MaNCC + "', MaNV = '" + MaNV + "' WHERE MaPN = '" + MaPNtemp + "' ;";
                    SqlCommand cmd2 = new SqlCommand(Query2, cnn);
                    cmd2.ExecuteNonQuery();

                    int sohang = dataGridViewX1.RowCount;
                    string NgayCapNhat = DateTime.Now.ToString("yyyy/MM/dd H:mm:ss");
 

                    //Update bảng CHI_TIET_PHIEU_NHAP
                    for (int i = 0; i < sohang; i++)
                    {
                        string MaHHtemp = "";
                        float SoLuongtemp = 0;
                        float DonGiatemp = 0;
                        double SoLuongold = 0;

                        //  Bắt lỗi nếu Mã hàng hoá để trống
                        try
                        {
                             MaHHtemp = dataGridViewX1.Rows[i].Cells["MaHH"].Value.ToString();
                        }
                        catch
                        {
                            Alert.Show("CHƯA NHẬP MÃ HÀNG HOÁ", Alert.AlertType.warning);
                            checkupdate = 1;
                        }

                        // Nếu mã hàng hoá nhập đúng -> checkudpate = 0 -> sẽ thực hiện các bước tiếp theo
                        if(checkupdate == 0)
                        {
                            try
                            {
                                // Bắt lỗi nếu Tên hàng hoá để trống -> Mã hàng hoá bị đê trống
                                string TenHHtest = dataGridViewX1.Rows[i].Cells["TenHH"].Value.ToString();
                                if (string.IsNullOrEmpty("run here" + TenHHtest))
                                    checkupdate = 1;

                                // Nếu hàng hoá nhập đúng thì sẽ tiếp tục update bảng CHI_TIET_PHIEU_NHAP
                                SoLuongtemp = float.Parse(dataGridViewX1.Rows[i].Cells["SoLuong"].Value.ToString());
                                DonGiatemp = float.Parse(dataGridViewX1.Rows[i].Cells["Dongia"].Value.ToString());
        
                                string Query3 = "UPDATE CHI_TIET_PHIEU_NHAP SET SoLuong = '" + SoLuongtemp + "', DonGia = '" + DonGiatemp + "', NgayCapNhat = '" + NgayCapNhat + "' WHERE MaPN = '" + MaPNtemp + "' AND MaHH = '" + MaHHtemp + "';";
                                SqlCommand cmd3 = new SqlCommand(Query3, cnn);
                                cmd3.ExecuteNonQuery();
                            }
                            catch
                            {
                                Alert.Show("CHƯA NHẬP MÃ HÀNG HOÁ", Alert.AlertType.warning);
                                checkupdate = 1;
                            }


                            try
                            {
                                // Insert vào bảng ĐƠN GIÁ NHẬP
                                string Query4 = "INSERT INTO DON_GIA_NHAP VALUES('" + MaHHtemp + "','" + DonGiatemp + "','" + NgayCapNhat + "' );";
                                SqlCommand cmd4 = new SqlCommand(Query4, cnn);
                                cmd4.ExecuteNonQuery();
                            }
                            catch
                            {
                                Alert.Show("XIN ĐỪNG NHẤN QUÁ NHANH", Alert.AlertType.warning);
                                checkupdate = 1;
                            }

                            
                            try
                            {
                                // Lấy số lượng tồn kho cũ
                                string QueryS = "SELECT SoLuongTonKho FROM TON_KHO WHERE MaHH = '" + MaHHtemp + "'  ";
                                SqlCommand cmdS = new SqlCommand(QueryS, cnn);
                                SoLuongold = (double)cmdS.ExecuteScalar();
                            }
                            catch
                            {
                                SoLuongold = 0.0;
                                checkupdate = 1;
                            }

                        }

                        if (checkupdate == 0)
                        {
                            // Giá trị Số lượng hàng hoá trước khi Edit
                            float SoLuongoldrow = OldValue[i];
                            // Cập nhật lại số lượng hàng hoá -> tồn kho
                            string SoLuongoldconvert = SoLuongold.ToString();
                            float SoLuongoldconvert2 = float.Parse(SoLuongoldconvert);
                            float SoLuongnew = SoLuongoldconvert2 - SoLuongoldrow + SoLuongtemp;
                            // Update bảng TỒN KHO
                            string Query5 = "UPDATE TON_KHO SET SoLuongTonKho = '" + SoLuongnew + "' WHERE MaHH = '" + MaHHtemp + "';";
                            SqlCommand cmd5 = new SqlCommand(Query5, cnn);
                            cmd5.ExecuteNonQuery();
                        }else
                        {
                            Alert.Show("LỖI THÔNG TIN HÀNG HOÁ", Alert.AlertType.error);
                        }


                    }

                    // Trạng thái sau khi cập nhật
                    if (checkupdate == 0)
                        Alert.Show("CẬP NHẬT THÀNH CÔNG", Alert.AlertType.success);
                    else
                        Alert.Show("NHẬP SAI THÔNG TIN HÀNG HOÁ", Alert.AlertType.error);

                }


                // Load Datagridview Phiếu Nhập
                String QueryZ = "SELECT MaPN,NgayNhap,TongTienTra FROM PHIEU_NHAP";
                SqlDataAdapter addapter = new SqlDataAdapter(QueryZ, cnn);
                DataTable dt = new DataTable();
                addapter.Fill(dt);
                dataGridViewX2.DataSource = dt;

            }

            // Sau khi Lưu trong phải tắt tính tăng sửa Timeinput
            timeInput.Enabled = false;

        }


        /// <summary>
        /// Hàm thêm một hàng rỗng dữ liệu vào dataGridViewX1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ThemRow_Click(object sender, EventArgs e)
        {
            dataGridViewX1.Rows.Add(" ", "", "0", "0", "0");
        }


        /// <summary>
        /// Hàm xoá hàng ở dataGridViewX1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_XoaRow_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewX1.SelectedCells.Count > 0)
            {
                    
                    int vitri = this.dataGridViewX1.SelectedCells[0].RowIndex;
                    string MaHH = dataGridViewX1.Rows[vitri].Cells["MaHH"].Value.ToString();
                      int check = 0;
                    string MaHHtemp = "";
                    float SoLuongtemp = 0;


                    // check lỗi sơ bộ các thông tin Mã hàng hoá, số lượng, đơn giá
                    try
                    {
                        MaHHtemp = dataGridViewX1.Rows[vitri].Cells["MaHH"].Value.ToString();
                        SoLuongtemp = float.Parse(dataGridViewX1.Rows[vitri].Cells["SoLuong"].Value.ToString());
                    }
                    catch
                    {
                        Alert.Show("LỖI THÔNG TIN HÀNG HOÁ", Alert.AlertType.error);
                        check = 1;
                    }
                    if(check == 0)
                {
                    // Nếu thông qua sẽ thực hiện các bước tiếp theo

                    string QuerySoLuongOld = "SELECT SoLuongTonKho FROM TON_KHO WHERE MaHH = '" + MaHHtemp + "'";
                    SqlCommand cmdSoLuongOld = new SqlCommand(QuerySoLuongOld, cnn);
                    cmdSoLuongOld.ExecuteNonQuery();
                    try
                    {
                        double SoLuong = (double)cmdSoLuongOld.ExecuteScalar();
                    }
                    catch
                    {
                        Alert.Show("LỖI SỐ LƯỢNG TỒN", Alert.AlertType.error);
                    }

                    try
                    {
                        string QueryUpdateTonKho = "UPDATE TON_KHO SET SoLuongTonKho -= '" + SoLuongtemp + "' WHERE MaHH = '" + MaHHtemp + "';";
                        SqlCommand cmdUpdateTonKho = new SqlCommand(QueryUpdateTonKho, cnn);
                        cmdUpdateTonKho.ExecuteNonQuery();
                    }
                    catch
                    {
                        Alert.Show("LỖI THÔNG TIN HÀNG HOÁ", Alert.AlertType.error);
                    }


                    dataGridViewX1.Rows.RemoveAt(this.dataGridViewX1.SelectedCells[0].RowIndex);
                }
                else
                {
                    Alert.Show("LỖI THÔNG TIN HÀNG HOÁ", Alert.AlertType.error);
                }

            }


        }

        

        float tong = 0;

        /// <summary>
        /// Hàm lưu trữ dữ liệu Số lượng đã sửa đổi trước đó -> giúp ích cho việc update lại bảng TON_KHO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewX1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridViewX1.CurrentCell.ColumnIndex == 2)
            {

                try {
                    String oldValue = dataGridViewX1[e.ColumnIndex, e.RowIndex].Value.ToString();
                    // MessageBox.Show(oldValue.ToString());
                    int hangindex = dataGridViewX1.CurrentCell.RowIndex;
                    OldValue[hangindex] = Int32.Parse(oldValue);
                }
                catch
                {
                    dataGridViewX1[e.ColumnIndex, e.RowIndex].Value = "0";
                    String oldValue = "0";
                    // MessageBox.Show(oldValue.ToString());
                    int hangindex = dataGridViewX1.CurrentCell.RowIndex;
                    OldValue[hangindex] = Int32.Parse(oldValue);
                }

            }

            if(dataGridViewX1.CurrentCell.ColumnIndex == 0)
            {
                dataGridViewX1[e.ColumnIndex, e.RowIndex].Value = "Nhập mã hàng hoá";
            }
        }


        /// <summary>
        ///  Hàm này chịu trách nhiệm : 
        /// Từ Mã hàng hoá -> Tìm ra tên Hàng hoá 
        /// Từ Số lượng, đơn giá -> Tự tính ra ô Thành tiền và Tổng thành tiền
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewX1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int check = 0;
            tong = 0;
            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                string MaHH = "";
                string temp = "";
                try
                {
                    int vitri = this.dataGridViewX1.SelectedCells[0].RowIndex;
                    temp = dataGridViewX1.Rows[vitri].Cells["MaHH"].Value.ToString();
                }
                catch
                {
                    Alert.Show("CẦN NHẬP MÃ HÀNG HOÁ", Alert.AlertType.warning);
                }


                if (temp == " ")
                {

                    Alert.Show("NHẬP DỮ LIỆU HÀNG HOÁ SAI!", Alert.AlertType.error);
                    check = 1;
                }
                else
                {
                    try
                    {
                        MaHH = row.Cells[dataGridViewX1.Columns["MaHH"].Index].Value.ToString();
                    }
                    catch
                    {
                        Alert.Show("CẦN NHẬP MÃ HÀNG HOÁ", Alert.AlertType.warning);
                    }
                    
                }

                if (check != 1)
                {
                    string Query = "SELECT TenHH FROM HANG_HOA where MaHH = '" + MaHH + "'";
                    SqlCommand cmd = new SqlCommand(Query, cnn);
                    string TenHH = (String)cmd.ExecuteScalar();

                    if (TenHH == String.Empty || TenHH == null)
                    {
                        Alert.Show("NHẬP SAI MÃ HÀNG HOÁ!", Alert.AlertType.error);
                    }

                    row.Cells[dataGridViewX1.Columns["TenHH"].Index].Value = TenHH;
                    try
                    {
                        row.Cells[dataGridViewX1.Columns["ThanhTien"].Index].Value = (Convert.ToDouble(row.Cells[dataGridViewX1.Columns["SoLuong"].Index].Value)) * (Convert.ToDouble(row.Cells[dataGridViewX1.Columns["DonGia"].Index].Value));
                        tong = tong + float.Parse(row.Cells[dataGridViewX1.Columns["ThanhTien"].Index].Value.ToString());
                        tb_TongPhaiTrainput.Text = tong.ToString();
                    }
                    catch
                    {
                        Alert.Show("LỖI THÔNG TIN HÀNG HOÁ", Alert.AlertType.error);
                    }
               
        

                }
            }

        }



        /// <summary>
        /// Bắt lỗi không được nhập  chữ vào ô Số lượng, đơn giá
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
                 && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        
        /// <summary>
        /// Bắt lỗi không được Copy và Paste 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C | e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }


        /// <summary>
        /// Hàm thực thi không được nhập chữ vào ô Số lượng và Đơn giá và Bắt lỗi không được Copy và Paste
        /// </summary>
        /// <param name="sender"></param>sdf
        /// <param name="e"></param>
        private void dataGridViewX1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Bắt buộc nhập số ở Số lượng và đơn giá
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (dataGridViewX1.CurrentCell.ColumnIndex == 2 || dataGridViewX1.CurrentCell.ColumnIndex == 3)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                    tb.ContextMenuStrip = new ContextMenuStrip();
                    tb.KeyDown -= TextBox_KeyDown;
                    tb.KeyDown += TextBox_KeyDown;
                }
            }
            // Bắt buộc viết hoa chữ "HH" ở mã hàng hoá
            if (dataGridViewX1.CurrentCell.ColumnIndex == 0)
            {
                if (e.Control is TextBox)
                    ((TextBox)e.Control).CharacterCasing = CharacterCasing.Upper;
            }



            }
  
    }
}
