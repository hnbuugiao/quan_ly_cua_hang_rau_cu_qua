using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using CuaHangRauCuQua;

namespace CuaHangRauCuQua
{
    public partial class XemHoaDon : Form
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataAdapter adt;
        DataTable bang;
        float slcu;
        private string manv;
        string mahhcu;


        public XemHoaDon(string MaNV)
        {
            InitializeComponent();
            conn = new SqlConnection(@"Data Source=.\SqlExpress;Initial Catalog=quanlybanhang;Integrated Security=True");
            conn.Open();
            fillComboBox("SELECT * FROM HANG_HOA", cb_MaHH);
            cb_MaHH.DropDownHeight = cb_MaHH.ItemHeight * 5;
            this.manv = MaNV;
        }

        /// <summary>
        /// Load hóa đơn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XemHoaDon_Load(object sender, EventArgs e)
        {
            this.Top = Screen.PrimaryScreen.Bounds.Height - this.Height - 240;
            this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width - 370;
            try
            {
                cmd = new SqlCommand("SELECT CHI_TIET_HOA_DON.MaHH,TenHH,SoLuong,DonGia,TiLE_VAT,ThanhTien FROM CHI_TIET_HOA_DON,HANG_HOA WHERE MaHD = '" + lb_MaHD.Text + "' AND CHI_TIET_HOA_DON.MaHH=HANG_HOA.MaHH", conn);
                adt = new SqlDataAdapter();
                adt.SelectCommand = cmd;
                bang = new DataTable();
                adt.Fill(bang);
                dg_HoaDon.DataSource = bang;
            }
            catch (Exception)
            {
                Alert.Show("KẾT NỐI ĐẾN CHI TIẾT HÓA ĐƠN THẤT BẠI", Alert.AlertType.error);
                return;
            }

        }

        /// <summary>
        /// làm trống textbox
        /// </summary>
        private void LamMoi()
        {
            cb_MaHH.Text = "";
            tb_SoLuong.Text = "";
            tb_Vat.Text = "";
        }

        /// <summary>
        /// lấy dữ liệu từ csdl thêm vào combobox
        /// </summary>
        /// <param name="sql">sql query</param>
        /// <param name="cb">tên combobox</param>
        /// <param name="conn">chuỗi kết nối</param>
        public void fillComboBox(String sql, ComboBox cb)
        {

            cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0]);
            }
            dr.Close();

        }

        /// <summary>
        /// lấy giá trị một ô nào đó torng csdl
        /// </summary>
        /// <param name="sql">sql query</param>
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

        private void flatClose1_Click(object sender, EventArgs e)
        {
            //this.Close();
          
        }

        private void flatMini1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void lb_ThongTin_Click(object sender, EventArgs e)
        {
           
        }


        /// <summary>
        /// Sửa chi tiết hoá đơn, chọn hàng để sửa từ các textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Suactp_Click_1(object sender, EventArgs e)
        {
            if (cb_MaHH.Text == "")
            {

                Alert.Show("CHƯA NHẬP MÃ HÀNG", Alert.AlertType.warning);
                return;
            }

            if (tb_SoLuong.Text == "")
            {
                
                Alert.Show("CHƯA NHẬP TRỌNG LƯỢNG", Alert.AlertType.warning);
                return;
            }
            if (tb_SoLuong.Text == "0")
            {

                Alert.Show("TRỌNG LƯỢNG LÀ SỐ THỰC > 0", Alert.AlertType.warning);
                return;
            }
            if (tb_Vat.Text == "")
            {
                
                Alert.Show("CHƯA NHẬP VAT", Alert.AlertType.warning);
                return;
            }
            String mahd = lb_MaHD.Text;
            String mahang = cb_MaHH.Text;
            float sl = 0;
            try
            {
                sl = float.Parse(tb_SoLuong.Text);
            }
            catch (Exception)
            {
                Alert.Show("TRONG LƯỢNG LÀ SỐ THỰC >0", Alert.AlertType.warning);
                return;
            }
            float giachuavat=0, vat=0, thanhtien = 0;
            try
            {
                string chuoigia = getValue("SELECT TOP 1 GiaBan FROM DON_GIA_BAN WHERE MaHH LIKE '" + cb_MaHH.Text + "' ORDER BY NgayCapNhatBan DESC");
                giachuavat = float.Parse(chuoigia) * sl;
                try
                {
                    vat = float.Parse(tb_Vat.Text.ToString());
                }
                catch (Exception)
                {
                    Alert.Show("VAT LÀ SỐ THỰC", Alert.AlertType.warning);
                    return;
                }
                thanhtien = giachuavat + giachuavat * (vat / 100);
            }
            catch (Exception)
            {
                Alert.Show("MÃ HÀNG KHÔNG TỒN TẠI\nHOẶC MÃ CHƯA CÓ GIÁ BÁN", Alert.AlertType.error);
                return;
            }
            String soluongtonkho = getValue("SELECT SoLuongTonKho FROM TON_KHO WHERE MaHH LIKE '" + cb_MaHH.Text + "'");
            if (mahhcu != cb_MaHH.Text)
            {
                slcu = 0;
            }
            float soluongcapnhat = float.Parse(soluongtonkho) + slcu - sl;
            if (soluongcapnhat < 0)
            {
                Alert.Show("KHÔNG ĐỦ HÀNG ", Alert.AlertType.warning);
                return;
            }
            try
            {
                SqlCommand cmd = new SqlCommand(@"UPDATE CHI_TIET_HOA_DON SET MaHH='" + cb_MaHH.Text +
                    @"', SoLuong='" + tb_SoLuong.Text +
                    @"',DonGia='" + giachuavat +
                    @"', TiLe_Vat='" + vat +
                    @"',ThanhTien='" + thanhtien +
                    @"' WHERE MaHD='" + lb_MaHD.Text +
                    @"' AND MaHH='" + mahhcu + "'", conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Alert.Show("CẬP NHẬT CHI TIẾT HD THẤT BẠI ", Alert.AlertType.error);
                return;
            }
            float tong = 0;

            try
            {
                SqlCommand cmd3 = new SqlCommand("UPDATE TON_KHO SET SoLuongTonKHo='" + soluongcapnhat + "' WHERE MaHH='" + cb_MaHH.Text + "'", conn);
                cmd3.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Alert.Show("CẬP NHẬT TỒN KHO THẤT BẠI ", Alert.AlertType.error);
                return;
            }
            slcu = sl;
            XemHoaDon_Load(sender, e);
            foreach (DataGridViewRow row in dg_HoaDon.Rows)
            {
                tong += float.Parse(row.Cells["thanhtien"].Value.ToString());
            }
            try
            {
                tb_TongTien.Text = tong.ToString();
                SqlCommand cmd2 = new SqlCommand("UPDATE HOA_DON_BAN_HANG SET TongTienHD='" + tong + "' WHERE MaHD='" + lb_MaHD.Text + "'", conn);
                cmd2.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Alert.Show("CẬP NHẬT TỔNG TIỀN THẤT BẠI ", Alert.AlertType.error);
                return;
            }
            LamMoi();
            Alert.Show("CẬP NHẬT THÀNH CÔNG ", Alert.AlertType.success);
        }

        private void dg_HoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cb_MaHH.Text = dg_HoaDon.CurrentRow.Cells["MaHH"].Value.ToString();
            mahhcu = dg_HoaDon.CurrentRow.Cells["MaHH"].Value.ToString();
            slcu = float.Parse(dg_HoaDon.CurrentRow.Cells["SoLuong"].Value.ToString());
            tb_SoLuong.Text = dg_HoaDon.CurrentRow.Cells["SoLuong"].Value.ToString();
            tb_Vat.Text = dg_HoaDon.CurrentRow.Cells["TiLeVat"].Value.ToString();
        }

        private void tb_Vat_TextChanged(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// Xóa hàng trong chi tiết phiếu, chọn hàng để xóa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Xoactp_Click(object sender, EventArgs e)
        {
            if (cb_MaHH.Text == "")
            {
                //labelLoi.Text = ("Chưa nhập mã hàng");
                Alert.Show("CHƯA NHẬP MÃ HÀNG CẦN XÓA", Alert.AlertType.warning);
                return;
            }
            cmd = new SqlCommand("DELETE FROM CHI_TIET_HOA_DON WHERE MaHD='" + lb_MaHD.Text + "' AND MaHH Like '" + cb_MaHH.Text + "'", conn);
            cmd.ExecuteNonQuery();
            String soluongtonkho = getValue("SELECT SoLuongTonKho FROM TON_KHO WHERE MaHH LIKE '" + cb_MaHH.Text + "'");
            if (soluongtonkho == "KhongTonTai")
            {
                Alert.Show("MÃ HÀNG KHÔNG TỒN TẠI", Alert.AlertType.error);
                return;
            }
            float soluongcapnhat = float.Parse(soluongtonkho) + slcu;
            SqlCommand cmd3 = new SqlCommand("UPDATE TON_KHO SET SoLuongTonKHo='" + soluongcapnhat + "' WHERE MaHH='" + cb_MaHH.Text + "'", conn);
            cmd3.ExecuteNonQuery();
            XemHoaDon_Load(sender, e);
            float tong = 0;
            foreach (DataGridViewRow row in dg_HoaDon.Rows)
            {
                tong += float.Parse(row.Cells["thanhtien"].Value.ToString());
            }
            try
            {
                tb_TongTien.Text = tong.ToString();
                SqlCommand cmd2 = new SqlCommand("UPDATE HOA_DON_BAN_HANG SET TongTienHD='" + tong + "' WHERE MaHD='" + lb_MaHD.Text + "'", conn);
                cmd2.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Alert.Show("CẬP NHẬT TỔNG TIỀN THẤT BẠI ", Alert.AlertType.error);
                return;
            }
            LamMoi();

        }

        /// <summary>
        /// Nút sửa thông tin háo đơn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SuaTT_Click(object sender, EventArgs e)
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
            try
            {

                cmd = new SqlCommand("UPDATE HOA_DON_BAN_HANG SET NgayBan='" + dtp_NgayBan.Value.ToString("yyyy.MM.dd HH:mm:ss") + "',MaNV='" + tb_MaNV.Text + "' WHERE MaHD='" + lb_MaHD.Text + "'", conn);
                cmd.ExecuteNonQuery();

                UserControlHoaDonBanHang ucHDBH = new UserControlHoaDonBanHang(manv);
                ucHDBH.loadTable("SELECT * FROM HOA_DON_BAN_HANG", ucHDBH.dg_DSHoaDon);
                Alert.Show("ĐÃ SỬA THÀNH CÔNG", Alert.AlertType.success);
            }
            catch (Exception)
            {
                Alert.Show("SỬA THẤT BẠI", Alert.AlertType.error);
                return;
            }

        }
        /// <summary>
        /// Xóa hóa đơn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_XoaHD_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("BẠN CÓ CHẮC MUỐN XÓA HÓA ĐƠN NÀY", "XOA HD", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                try
                {
                    int sodong = dg_HoaDon.Rows.Count;
                    if (0 < sodong)
                    {
                        foreach (DataGridViewRow row in dg_HoaDon.Rows)
                        {
                            string mahh = row.Cells["MaHH"].Value.ToString();

                            float soluong = float.Parse(row.Cells["SoLuong"].Value.ToString());
                            float soluongtonkho = float.Parse(getValue("SELECT SoLuongTonKho FROM TON_KHO WHERE MaHH LIKE '" + mahh + "'"));
                            float soluongcapnhat = soluongtonkho + soluong;
                            cmd = new SqlCommand("DELETE FROM CHI_TIET_HOA_DON WHERE MaHD='" + lb_MaHD.Text + "' AND MaHH Like '" + mahh + "'", conn);
                            cmd.ExecuteNonQuery();
                            cmd = new SqlCommand("UPDATE TON_KHO SET SoLuongTonKHo='" + soluongcapnhat + "' WHERE MaHH='" + mahh + "'", conn);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    cmd = new SqlCommand("DELETE FROM HOA_DON_BAN_HANG WHERE MaHD='" + lb_MaHD.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    Alert.Show("XÓA THÀNH CÔNG", Alert.AlertType.success);
                    this.Close();
                }
                catch (Exception)
                {
                    Alert.Show("XÓA HÓA ĐƠN THẤT BẠI", Alert.AlertType.error);
                    return;
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }


        }

        /// <summary>
        /// tb_SoLuong chi duoc nhap so 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_SoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void tb_Vat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void btnclosed_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Thêm vào hàng vào hóa đơn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Them_Click(object sender, EventArgs e)
        {
            if (cb_MaHH.Text == "")
            {

                Alert.Show("CHƯA NHẬP MÃ HÀNG", Alert.AlertType.warning);
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
            if (tb_Vat.Text == "")
            {
                
                Alert.Show("CHƯA NHẬP VAT", Alert.AlertType.warning);
                return;
            }
            String mahd = lb_MaHD.Text;
            String mahang = cb_MaHH.Text;
            float sl = 0;
            try
            {
                sl = float.Parse(tb_SoLuong.Text);
            }
            catch (Exception)
            {
                Alert.Show("TRONG LƯỢNG LÀ SỐ THỰC >0", Alert.AlertType.warning);
                return;
            }
            float giachuavat = 0;
            float vat = 0;

            float thanhtien = 0;
            try
            {
                string chuoigia = getValue("SELECT TOP 1 GiaBan FROM DON_GIA_BAN WHERE MaHH LIKE '" + cb_MaHH.Text + "' ORDER BY NgayCapNhatBan DESC");
                giachuavat = float.Parse(chuoigia) * sl;
                try
                {
                    vat = float.Parse(tb_Vat.Text.ToString());
                }
                catch (Exception)
                {
                    Alert.Show("VAT LÀ SỐ THỰC", Alert.AlertType.warning);
                    return;
                }
                thanhtien = giachuavat + giachuavat * (vat / 100);
                
            }
            catch (Exception)
            {
                Alert.Show("MÃ HÀNG KHÔNG TỒN TẠI\nHOẶC MÃ CHƯA CÓ GIÁ BÁN", Alert.AlertType.error);
                return;
            }
            float soluongcapnhat = 0;
            try
            {
                String soluongtonkho = getValue("SELECT SoLuongTonKho FROM TON_KHO WHERE MaHH LIKE '" + cb_MaHH.Text + "'");
                soluongcapnhat = float.Parse(soluongtonkho) - sl;
                if (float.Parse(soluongtonkho) < sl)
                {
                    Alert.Show("KHÔNG ĐỦ HÀNG ", Alert.AlertType.warning);
                    return;
                }
            }
            catch (Exception)
            {
                Alert.Show("KHÔNG CÓ DỮ LIỆU TỒN KHO", Alert.AlertType.error);
                return;
            }
            try
            {

                SqlCommand cmd = new SqlCommand("INSERT INTO CHI_TIET_HOA_DON VALUES ('" + mahd + "','" + mahang + "','" + sl + "','" + giachuavat + "','" + vat + "','" + thanhtien + "')", conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Alert.Show("MÃ HÀNG ĐÃ TỒN TẠI", Alert.AlertType.error);
                return;
            }
            float tong = 0;

            try
            {
                SqlCommand cmd3 = new SqlCommand("UPDATE TON_KHO SET SoLuongTonKHo='" + soluongcapnhat + "' WHERE MaHH='" + cb_MaHH.Text + "'", conn);
                cmd3.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Alert.Show("CẬP NHẬT TỒN KHO THẤT BẠI ", Alert.AlertType.error);
                return;
            }
            slcu = sl;
            XemHoaDon_Load(sender, e);
            foreach (DataGridViewRow row in dg_HoaDon.Rows)
            {
                tong += float.Parse(row.Cells["thanhtien"].Value.ToString());
            }
            try
            {
                tb_TongTien.Text = tong.ToString();
                SqlCommand cmd2 = new SqlCommand("UPDATE HOA_DON_BAN_HANG SET TongTienHD='" + tong + "' WHERE MaHD='" + lb_MaHD.Text + "'", conn);
                cmd2.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Alert.Show("CẬP NHẬT TỔNG TIỀN THẤT BẠI ", Alert.AlertType.error);
                return;
            }
            LamMoi();
            Alert.Show("CẬP NHẬT THÀNH CÔNG ", Alert.AlertType.success);
        }

        /// <summary>
        /// Tạo lại bản in của hóa đơn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_InLai_Click(object sender, EventArgs e)
        {
            crHoaDon hoadon = new crHoaDon();
            DataSet ds = new DataSet();
            ds.Tables.Add(bang.Copy());
            //ds.WriteXmlSchema("Sample.xml");
            HoaDonRP rp = new HoaDonRP();
            rp.SetDataSource(ds);
            rp.SetParameterValue("tbNgayBan", dtp_NgayBan.Value.ToString("dd-MM-yyyy"));
            rp.SetParameterValue("tbMaHD", lb_MaHD.Text);
            crHoaDon cr = new crHoaDon();
            cr.cr_HoaDon.ReportSource = rp;
            
            cr.Show();
        }
    }


}
        

