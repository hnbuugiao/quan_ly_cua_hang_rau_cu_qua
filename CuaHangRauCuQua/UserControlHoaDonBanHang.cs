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
using CuaHangRauCuQua;

namespace CuaHangRauCuQua
{
    public partial class UserControlHoaDonBanHang : UserControl
    {
    
        private SqlConnection conn;
        private SqlCommand cmd;
      
        private SqlDataAdapter adt;
        private DataTable bangcthd = new DataTable();
        private DataTable banghd;
        private String myDate;
        private String manv;
        private float tongtienhd;
        private string connection = @"Data Source=.\SqlExpress;Initial Catalog=quanlybanhang;Integrated Security=True";
        public UserControlHoaDonBanHang(string MaNV)
        {
            InitializeComponent();
            try
            {
                conn = new SqlConnection(connection);
                conn.Open();
              
            }

            catch
            {
        
                Alert.Show("KHÔNG THỂ KẾT NỐI ĐẾN CSDL", Alert.AlertType.error);
            }
           
            bangcthd.Columns.AddRange(new DataColumn[7] {
                new DataColumn("MaHD",typeof(string)),
                new DataColumn("MaHH",typeof(string)),
                new DataColumn("TenHH",typeof(string)),
                new DataColumn("SoLuong",typeof(float)),
                new DataColumn("DonGia",typeof(float)),
                new DataColumn("TiLeVat",typeof(float)),
                new DataColumn("thanhtien",typeof(float))
            });
            dg_CTHD.AllowUserToAddRows = false;
            bangcthd.Columns["MaHD"].ReadOnly = true;
         
            tb_MaHD.Text = TaoMaHD();
            myDate = DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss");
            dtp_NgayBan.Text = myDate;
            fillComboBox("SELECT * FROM HANG_HOA", cb_MaHH);
            cb_MaHH.DropDownHeight = cb_MaHH.ItemHeight * 5;
            banghd = loadTable("SELECT * FROM HOA_DON_BAN_HANG", dg_DSHoaDon);
            dg_DSHoaDon.Columns["NgayBan"].DefaultCellStyle.Format = "dd/MM/yyyy";
            tb_Vat.Text = "10";
            tb_Gia.Clear();
            tb_MaHD.Clear();
            cb_MaHH.Text = "";
            tb_SoLuong.Clear();
            tb_MaHD.Text = TaoMaHD();
            this.manv = MaNV;
            tb_MaNV.Text = manv;
            tb_TongTien.Text = "0";
            //labelLoi.Text = "";
        }
        

        /// <summary>
        /// Tạo mã hóa đơn tự động 
        /// </summary>
        /// <returns>trả về chuỗi mã hóa đơn</returns>
        private string TaoMaHD()
        {

            string year = DateTime.Now.ToString("yyyy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string hour = DateTime.Now.ToString("hh");
            string minute = DateTime.Now.ToString("mm");
            string second = DateTime.Now.ToString("ss");
            return "HD"+year + month + day + hour + minute + second;

        }

        private void UserControlHoaDon_Load(Object sender, EventArgs e)
        {

            //  MessageBox.Show("You are in the UserControl.Load event.");
            myDate = DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss");
            dtp_NgayBan.Text = myDate;
            fillComboBox("SELECT * FROM HANG_HOA", cb_MaHH);
            banghd = loadTable("SELECT * FROM HOA_DON_BAN_HANG", dg_DSHoaDon);
            tb_Vat.Text = "10";
            tb_Gia.Clear();
            tb_MaHD.Clear();
            cb_MaHH.Text="";
            tb_SoLuong.Clear();
            tb_MaHD.Text = TaoMaHD();
           
            tb_TongTien.Text = "0";
        }

        /// <summary>
        /// lấy dữ liệu từ csdl thêm vào combobox
        /// </summary>
        /// <param name="sql">chuôi tury vấn sql</param>
        /// <param name="cb">tên combobox</param>
        /// <param name="conn">chuỗi kết nối</param>
        public void fillComboBox(String sql, ComboBox cb)
        {
            cb_MaHH.Items.Clear();

            cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0]);
            }
            dr.Close();

        }
        /// <summary>
        /// Đưa dữ liệu vào datagridview từ csdl
        /// </summary>
        /// <param name="sql">String: sql query</param>
        /// <param name="v">Datagriview</param>
        /// <returns></returns>
        public DataTable loadTable(String sql, DataGridView v)
        {
            DataTable bang = new DataTable();
            try
            {
                cmd = new SqlCommand(sql, conn);
                adt = new SqlDataAdapter();
                adt.SelectCommand = cmd;
                adt.Fill(bang);
                v.DataSource = bang;
                return bang;
                //  adt.Update(bang);
            }
            catch (Exception)
            {
                Alert.Show("KHÔNG LOAD ĐƯỢC BẢNG " + v, Alert.AlertType.error);
                return bang;
            }


        }
        
      
 

        /// <summary>
        /// lấy giá trị một ô nào đó torng csdl thông qua sql query
        /// </summary>
        /// <param name="sql">chuỗi truy vấn sql</param>
        /// <returns></returns>
        private String getValue(String sql)
        {
            try
            {
              
                cmd = new SqlCommand(sql, conn);
                var value = cmd.ExecuteScalar();
                return value.ToString();
            }
            catch
            {
                return "KhongTonTai";
            }
        }

     
        /// <summary>
        /// xác định mã có tồn tại trong datagridview hay chưa
        /// </summary>
        /// <param name="ma">chuỗi: mã cần xác định</param>
        /// <param name="dg">tên của datagridview</param>
        /// <returns></returns>
        public Boolean idExistedInDataGrid(String ma, DataGridView dg)
        {
            //int rowIndex = -1;
            //Boolean kq = false;
            int rowCount = dg.Rows.Count;
            try
            {
                for (int i = 0; i < rowCount; i++)
                {

                    if (dg.Rows[i].Cells[1].Value.ToString().Equals(ma))
                    {
                        return true;
                    }
                }

            }
            catch (NullReferenceException)
            {
                return false;
            }
            return false;
        }


  



        /// <summary>
        /// Khi chọn hàng khác trong combo box thì tb_SoLuong trống
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_MaHH_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_SoLuong.Text = "";

        }

        /// <summary>
        /// sự kiện thay đổi ô trong datagridview, điều chỉnh lại đơn giá, thành tiền khi số lượng , vat thay đổi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dg_CTHD_CellValueChanged(object sender, DataGridViewCellEventArgs e)

        {
            
            if (e.RowIndex == -1)
            {
                return;
            }
            string mahang = dg_CTHD["MaHH", e.RowIndex].Value.ToString();

            float tienchuasua = float.Parse(dg_CTHD["DonGia", e.RowIndex].Value.ToString());

            float soluongmoi = float.Parse(dg_CTHD["SoLuong", e.RowIndex].Value.ToString());

            float vat = float.Parse(dg_CTHD["TiLeVat", e.RowIndex].Value.ToString());
            try
            {
                float giasl1 = float.Parse(getValue("SELECT TOP 1 GiaBan FROM DON_GIA_BAN WHERE MaHH LIKE '" + mahang + "' ORDER BY NgayCapNhatBan DESC"));
                dg_CTHD["TenHH", e.RowIndex].Value = getValue("SELECT TenHH FROM HANG_HOA WHERE MaHH LIKE '"+mahang+"'");
                dg_CTHD["DonGia", e.RowIndex].Value = giasl1 * soluongmoi;
                dg_CTHD["thanhtien", e.RowIndex].Value = giasl1 * soluongmoi + (giasl1 * soluongmoi) * (vat / 100);
            }
            catch (Exception)
            {
                
                Alert.Show("MÃ HÀNG CHƯA CHO GIÁ BÁN", Alert.AlertType.error);
                return;
            }
            // if (dg_CTHD.Columns[e.ColumnIndex].Name == "SoLuong")

            
           

        }

    

  
        
        /// <summary>
        /// Báo lỗi khi giá trị của một ô trong cột số lượng không phải là số, số lượng =0 hoặc khống lậy được tồn trong datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dg_CTHD_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    float i;

                    if (!float.TryParse(Convert.ToString(e.FormattedValue), out i))
                    {
                        e.Cancel = true;
                        Alert.Show("TRỌNG LƯỢNG PHẢI LÀ SỐ", Alert.AlertType.error);

                    }
                    if (float.Parse(e.FormattedValue.ToString()) == 0)
                    {
                        e.Cancel = true;
                        Alert.Show("TRỌNG LƯỢNG PHẢI LỚN HƠN 0", Alert.AlertType.error);
                    }

                }

                if (e.ColumnIndex == 1)
                {

                    String mahh = e.FormattedValue.ToString();
                    string tontai = getValue("SELECT * FROM HANG_HOA WHERE MaHH LIKE '" + mahh + "'");

                    if (tontai.Equals("KhongTonTai"))
                    {
                        e.Cancel = true;
                        Alert.Show("MÃ HÀNG NÀY KHÔNG TỒN TẠI", Alert.AlertType.error);

                    }

                    String soluongtonkho = getValue("SELECT SoLuongTonKho FROM TON_KHO WHERE MaHH LIKE '" + mahh + "'");
                    if (soluongtonkho.Equals("KhongTonTai"))
                    {
                        e.Cancel = true;
                        Alert.Show("KHÔNG LẤY ĐƯỢC SỐ LƯỢNG TỒN\nNHẬP LẠI MÃ HÀNG", Alert.AlertType.error);
                    }

                }
            }
            catch (Exception)
            {
                e.Cancel = true;
                Alert.Show("PHẢI NHẬP SỐ", Alert.AlertType.error);
            }
        }

        
        private void cb_MaHH_Leave(object sender, EventArgs e)
        {
           

        }

        /// <summary>
        /// Mở vào giao diện sửa chi tiết hóa đơn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dg_hoadon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string mahd = dg_DSHoaDon.CurrentRow.Cells[0].Value.ToString();
            string ngay = dg_DSHoaDon.CurrentRow.Cells[1].Value.ToString();
            string manv = dg_DSHoaDon.CurrentRow.Cells[3].Value.ToString();
            string tongtien= dg_DSHoaDon.CurrentRow.Cells[2].Value.ToString();
            CuaHangRauCuQua.XemHoaDon xemhoadon = new XemHoaDon(manv);
            
            xemhoadon.lb_MaHD.Text = mahd;
            xemhoadon.dtp_NgayBan.Value = DateTime.Parse(ngay);
            xemhoadon.tb_MaNV.Text = manv;
            xemhoadon.tb_TongTien.Text = tongtien;
            xemhoadon.ShowDialog();
            loadTable("SELECT * FROM HOA_DON_BAN_HANG", dg_DSHoaDon);
        }

        

        /// <summary>
        /// nút lưu hóa đơn, lưu lại mã hóa đơn, chi tiết hóa đơn vào csdl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_LuuHD_Click(object sender, EventArgs e)
        {
            if (tb_MaNV.Text == "")
            {

                Alert.Show("CHƯA NHẬP MÃ NHÂN VIÊN", Alert.AlertType.warning);
                return;
            }
            if (dg_CTHD.Rows.Count==0)
            {
                Alert.Show("CHƯA NHẬP CHI TIẾT PHIẾU", Alert.AlertType.warning);
                return;
            }
            try
            {
                cmd = new SqlCommand("INSERT INTO hoa_don_ban_hang (MaHD, NgayBan,TongTienHD,MaNV) VALUES ('" + tb_MaHD.Text + "','" + dtp_NgayBan.Value.ToString("yyyy-MM-dd") + "','" + tb_TongTien.Text + "','" + tb_MaNV.Text + "')", conn);
                cmd.ExecuteNonQuery();// lưu hóa đơn
                                      // conn.Close();   
                tongtienhd = 0;
                foreach (DataGridViewRow row in dg_CTHD.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        string mahd = row.Cells[0].Value.ToString();
                        string mahh = row.Cells[1].Value.ToString();
                        float soluong = float.Parse(row.Cells[3].Value.ToString());
                        float giachuavat = float.Parse(row.Cells[4].Value.ToString());
                        float vat = float.Parse(row.Cells[5].Value.ToString());
                        float giadavat = float.Parse(row.Cells[6].Value.ToString());
                        SqlCommand cmd2 = new SqlCommand("INSERT INTO CHI_TIET_HOA_DON VALUES ('" + mahd + "','" + mahh + "','" + soluong + "','" + giachuavat + "','" + vat + "','" + giadavat + "')", conn);
                        cmd2.ExecuteNonQuery();
                        String soluongtonkho = getValue("SELECT SoLuongTonKho FROM TON_KHO WHERE MaHH LIKE '" + mahh + "'");

                        float soluongcapnhat = float.Parse(soluongtonkho) - soluong;
                        SqlCommand cmd3 = new SqlCommand("UPDATE TON_KHO SET SoLuongTonKHo='" + soluongcapnhat + "' WHERE MaHH='" + mahh + "'", conn);
                        cmd3.ExecuteNonQuery();//lưu chi tiết của hóa đơn vào csdl

                    }

                }

                
                Alert.Show("LƯU HÓA ĐƠN THÀNH CÔNG", Alert.AlertType.success);
                TaoBanInHoaDon();
                while (dg_CTHD.Rows.Count > 0)
                {
                    dg_CTHD.Rows.RemoveAt(0);
                }
                tb_TongTien.Text = "0";
                UserControlHoaDon_Load(sender, e);
            }
            catch (Exception)
            {
                Alert.Show("LƯU HÓA ĐƠN THẤT BẠI", Alert.AlertType.error);
            }
        }

        /// <summary>
        /// nút đưa hàng hóa vào datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btn_Themctp_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (tb_MaNV.Text == "")
                {

                    Alert.Show("CHƯA NHẬP MÃ NHÂN VIÊN", Alert.AlertType.warning);
                    return;
                }
                if (getValue("SELECT MaNV FROM NHAN_VIEN WHERE MaNV LIKE '" + tb_MaNV.Text + "'") == "KhongTonTai")
                {

                    Alert.Show("MÃ NHÂN VIÊN KHÔNG TỒN TẠI", Alert.AlertType.error);
                    return;
                }

                if (tb_MaHD.Text == "")
                {


                    Alert.Show("CHƯA NHẬP MÃ HÓA ĐƠN", Alert.AlertType.warning);
                    return;
                }

                if (cb_MaHH.Text == "")
                {

                    Alert.Show("CHƯA NHẬP MÃ HÀNG", Alert.AlertType.warning);
                    return;
                }
                if (idExistedInDataGrid(cb_MaHH.Text, dg_CTHD) == true)
                {
                    Alert.Show("ĐÃ TỒN TẠI MÃ HÀNG NÀY", Alert.AlertType.warning);
                    return;
                }
                if (tb_SoLuong.Text == "")
                {

                    Alert.Show("CHƯA NHẬP TRỌNG LƯỢNG", Alert.AlertType.warning);
                    return;
                }
                if (tb_SoLuong.Text == "0")
                {

                    Alert.Show("TRỌNG LƯỢNG PHẢI LỚN HƠN 0", Alert.AlertType.warning);
                    return;
                }

                try
                {
                    //bangcthd = (DataTable)dg_CTHD.DataSource;
                    Boolean tontai = idExistedInDataGrid(cb_MaHH.Text, dg_CTHD);

                    if (true == tontai)
                    {
                        Alert.Show("BẠN ĐÃ THÊM MÃ HÀNG NÀY", Alert.AlertType.warning);
                        return;
                    }
                    else
                    {
                        string mahd = tb_MaHD.Text;
                        string mahang = cb_MaHH.Text;
                        string tenhang = getValue("SELECT TenHH FROM HANG_HOA WHERE MaHH LIKE '" + mahang + "'");
                        float sl = float.Parse(tb_SoLuong.Text);
                        float giachuavat = float.Parse(tb_Gia.Text);
                        float vat = float.Parse(tb_Vat.Text);
                        float thanhtien = giachuavat + giachuavat * (vat / 100);
                        float tongtien = float.Parse(tb_TongTien.Text);

                        try
                        {
                            String soluongtonkho = getValue("SELECT SoLuongTonKho FROM TON_KHO WHERE MaHH LIKE '" + cb_MaHH.Text + "'");

                            if (float.Parse(soluongtonkho) < sl)
                            {
                                Alert.Show("KHÔNG ĐỦ HÀNG!!!", Alert.AlertType.warning);
                                return;
                            }
                        }
                        catch (Exception)
                        {
                            Alert.Show("KHÔNG CÓ DỮ LIỆU TỒN KHO\nNHẬP LẠI MÃ HÀNG", Alert.AlertType.error);
                            return;
                        }
                        tongtien += thanhtien;
                        tb_TongTien.Text = tongtien.ToString();
                        //tongtienhd += float.Parse(tb_TongTien.Text.ToString());
                        bangcthd.Rows.Add(mahd, mahang, tenhang, sl, giachuavat, vat, thanhtien);
                        this.dg_CTHD.DataSource = bangcthd;
                    }


                }
                catch (ArgumentException)
                {
                    //labelLoi.Text = "Số lượng và giá phải là số";
                    Alert.Show("Số lượng và giá phải là số", Alert.AlertType.warning);
                }
            }
            catch (Exception)
            {
                Alert.Show("THÊM THẤT BẠI", Alert.AlertType.error);
            }

        }


        /// <summary>
        /// Tạo hóa đơn mới, tìm xem mã hóa đơn đã tồn tại trong csdl hay chưa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_TaoHD_Click_1(object sender, EventArgs e)

        {
            tb_MaHD.Text = TaoMaHD();
            if (tb_MaHD.Text == "")
            {
                //labelLoi.Text = "Chưa nhập mã hóa đơn";
                Alert.Show("CHƯA NHẬP MÃ HÓA ĐƠN", Alert.AlertType.warning);
                return;
            }
            if (tb_MaNV.Text == "")
            {
                //labelLoi.Text = "Chưa nhập mã nhân viên";
                Alert.Show("CHƯA NHẬP MÃ NHÂN VIÊN", Alert.AlertType.warning);
                return;
            }

            string tontai = getValue("SELECT TOP 1 MaHD FROM hoa_don_ban_hang WHERE MaHD LIKE '" + tb_MaHD.Text + "'");
            if (tontai != "KhongTonTai")
            {
               
                Alert.Show("MÃ HÓA ĐƠN ĐÃ TỒN TẠI", Alert.AlertType.warning);
                return;
            }
            else
            {
                tb_MaHD.Text = tb_MaHD.Text;
                manv = tb_MaNV.Text;
            }
            
            
        }


       
        /// <summary>
        /// Sự kiện bấm nút làm mới, làm trống số lượng, combobox mã hàng, đặt giá =0
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_reset_Click(object sender, EventArgs e)
        {
            tb_Gia.Clear();
            tb_TongTien.Text = "0";
            cb_MaHH.Text = "";
            tb_SoLuong.Clear();
            //UserControlHoaDon_Load(sender, e);
        }

        /// <summary>
        /// sự kiện : khi ô số lượng thay đổi thì giá thay đổi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_SoLuong_TextChanged(object sender, EventArgs e)
        {
            string chuoigia = getValue("SELECT TOP 1 GiaBan FROM DON_GIA_BAN WHERE MaHH LIKE '" + cb_MaHH.Text + "' ORDER BY NgayCapNhatBan DESC");
            if(chuoigia == "KhongTonTai" && cb_MaHH.Text != "")
            {
                
                return;
            }
            float sl = 0;
            float gia = 0;
            float.TryParse(chuoigia, out gia);
            float.TryParse(tb_SoLuong.Text, out sl);
            float thanhtien = gia * sl;

            tb_Gia.Text = thanhtien.ToString();
        }

        /// <summary>
        /// Chỉ Nhập số trong tb_SoLuong
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_SoLuong_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        

        private void UserControlHoaDonBanHang_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Sự kiến xóa một hàng hóa trong chi tiết phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dg_CTHD_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            foreach(DataGridViewRow row in dg_CTHD.Rows)
            tongtienhd += float.Parse(row.Cells[6].Value.ToString()); //tổng tiền từ cột thành tiền
            tb_TongTien.Text = tongtienhd.ToString();
            tongtienhd = 0;
        }

        private void btn_TimHD_Click(object sender, EventArgs e)
        {
            Boolean found = false;
            /* try
             {
                 foreach (DataGridViewRow row in dg_DSHoaDon.Rows)
                 {
                     if (row.Cells[0].Value.ToString().Equals(tb_TimHD.Text))
                     {
                         found = true;
                         row.Selected = true;
                         dg_DSHoaDon.CurrentCell = row.Cells[0];
                         string mahd = dg_DSHoaDon.CurrentRow.Cells[0].Value.ToString();
                         string ngay = dg_DSHoaDon.CurrentRow.Cells[1].Value.ToString();
                         string manv = dg_DSHoaDon.CurrentRow.Cells[3].Value.ToString();
                         string tongtien = dg_DSHoaDon.CurrentRow.Cells[2].Value.ToString();
                         CuaHangRauCuQua.XemHoaDon xemhoadon = new XemHoaDon(manv);

                         xemhoadon.lb_MaHD.Text = mahd;
                         xemhoadon.dtp_NgayBan.Value = DateTime.Parse(ngay);
                         xemhoadon.tb_MaNV.Text = manv;
                         xemhoadon.tb_TongTien.Text = tongtien;
                         xemhoadon.ShowDialog();
                         loadTable("SELECT * FROM HOA_DON_BAN_HANG", dg_DSHoaDon);
                         break;
                     }
                 }
                 if (found == false) {
                     Alert.Show("KHÔNG TÌM THẤY MÃ HÓA ĐƠN", Alert.AlertType.error);
                 }
             }
             catch (Exception)
             {
                 Alert.Show("KHÔNG TÌM THẤY MÃ HÓA ĐƠN", Alert.AlertType.error);
             }*/
            loadTable("SELECT * FROM HOA_DON_BAN_HANG WHERE MaHD LIKE '%"+tb_TimHD.Text+"%'", dg_DSHoaDon);
        }

        /// <summary>
        /// Tạo bảng in háo đơn bằng crystal report
        /// </summary>
        private void TaoBanInHoaDon()
        {
            crHoaDon hoadon = new crHoaDon();
            DataSet ds = new DataSet();
            ds.Tables.Add(bangcthd.Copy());
            // ds.WriteXmlSchema("Sample.xml");
            HoaDonRP rp = new HoaDonRP();
            rp.SetDataSource(ds);
            rp.SetParameterValue("tbNgayBan", dtp_NgayBan.Value.ToString("dd-MM-yyyy"));
            rp.SetParameterValue("tbMaHD", tb_MaHD.Text);
            crHoaDon cr = new crHoaDon();
            cr.cr_HoaDon.ReportSource = rp;
            cr.Show();
        }

        /// <summary>
        /// textbox Vat chỉ được nhập số
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_Vat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Chỉ được nhập số ở cột số lượng và Vat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dg_CTHD_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column_KeyPress);
            if (dg_CTHD.CurrentCell.ColumnIndex == 3 || dg_CTHD.CurrentCell.ColumnIndex == 5)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column_KeyPress);
                }
            }
        }

        /// <summary>
        /// Không cho gõ chữ, chỉ cho phép gõ cac phím control, số, dấu '.'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Column_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Khi mã hàng hóa trong combobox thay đổi thì giá thay đổi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_MaHH_TextChanged(object sender, EventArgs e)
        {
            string chuoigia = "";
            if (cb_MaHH.Text != "")
            {
                chuoigia = getValue("SELECT TOP 1 GiaBan FROM DON_GIA_BAN WHERE MaHH LIKE '" + cb_MaHH.Text + "' ORDER BY NgayCapNhatBan DESC");
            }
            if (chuoigia == "KhongTonTai")
            {
               // Alert.Show("MÃ HÀNG CHƯA CÓ GIÁ BÁN", Alert.AlertType.error);
                return;
            }
            float sl = 0;
            float gia = 0;
            float.TryParse(chuoigia, out gia);
            float.TryParse(tb_SoLuong.Text, out sl);
            float thanhtien = gia * sl;

            tb_Gia.Text = thanhtien.ToString();
        }

        /// <summary>
        /// Xóa hàng khi chưa lưu hóa đơn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Xoactp_Click(object sender, EventArgs e)
        {
            try
            {
                dg_CTHD.Rows.RemoveAt(dg_CTHD.CurrentRow.Index);
            }
            catch (NullReferenceException)
            {
                Alert.Show("CHƯA CHỌN HÀNG ĐỂ XÓA", Alert.AlertType.error);
            }
        }

        
    }

}
