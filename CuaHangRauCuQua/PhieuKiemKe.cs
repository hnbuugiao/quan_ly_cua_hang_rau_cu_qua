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
using CrystalDecisions.CrystalReports.Engine;

namespace CuaHangRauCuQua
{
    public partial class PhieuKiemKe : UserControl
    {
        public PhieuKiemKe()
        {
            InitializeComponent();
            string myDate = DateTime.Now.ToString("yyyy.MM.dd");
            dtlNL.Text = myDate;
            dtlToiNgay.Text = myDate;
            dtlTuNgay.Text = myDate;
        }
        
        ChiTietPKK_DAL DAL_ctkk = new ChiTietPKK_DAL(); // tao doi tuong cua class ChiTietPKK chua cac ham ket noi, truy van CSDL

        DataTable dttk;  // bien luu du lieu cho data set cua report
        string kieutk = "";  // bien luu kieu thong ke len report

        // Hàm khoá đièu khiển
        public void KhoaDieuKhien()
        {
            dtlNL.Enabled = false;
            dtlToiNgay.Enabled = false;
            dtlTuNgay.Enabled = false;
            cbNam.Enabled = false;
            cbQuy.Enabled = false;
            cbThang.Enabled = false;
        }

        
        /// Xó dữ liệu trong datagridview
        public void clearDatagridview(DataGridView v)
        {
            while (v.Rows.Count > 0)
            {
               v.Rows.RemoveAt(0);
            }
            
        }

        // su kien load uc thong ke
        private void PhieuKiemKe_Load(object sender, EventArgs e)
        {
            KhoaDieuKhien();
        }

        // su kien nhan nut thong ke
        private void btn_ThongKe_Click(object sender, EventArgs e)
        {
            string sb = "", sn = "";  
            
            // cick vao radio button thong ke theo ngay
            if (rbNgay.Checked)
            {
                
                sb = "NgayBan='" + dtlNL.Value.ToString("yyyy-MM-dd") + "' ";
                sn = "NgayNhap='" + dtlNL.Value.ToString("yyyy-MM-dd") + "' ";
                kieutk = "NGÀY "+ dtlNL.Value.ToString("dd/MM/yyyy");
            }
            // cick vao radio button thong ke theo thang
            if (rbThang.Checked)
            {
                
                if (cbThang.Text == "")
                {
                    Alert.Show("CHƯA CHỌN THÁNG", Alert.AlertType.warning);
                    return;
                }
                if (cbNam.Text == "")
                {
                    Alert.Show("CHƯA CHỌN NĂM", Alert.AlertType.warning);
                    return;
                }

                if (Int32.Parse(cbNam.Text) < 1000 || Int32.Parse(cbNam.Text) > 9999)
                {
                    Alert.Show("KHÔNG CÓ NĂM NÀY", Alert.AlertType.error);
                    return;
                }

                
                sb = "MONTH(NgayBan)='" + cbThang.Text + "' AND YEAR(NgayBan)='" + cbNam.Text + "'";
                sn = "MONTH(NgayNhap)='" + cbThang.Text + "' AND YEAR(NgayNhap)='" + cbNam.Text + "'";
                kieutk = "THÁNG "+ cbThang.Text+"/"+cbNam.Text;
            }
            // cick vao radio button thong ke theo quy
            if (rbQuy.Checked)
            {
               
                if (cbQuy.Text == "")
                {
                    Alert.Show("CHƯA CHỌN QUÝ", Alert.AlertType.warning);
                    return;
                }

                if (cbNam.Text == "")
                {
                    Alert.Show("CHƯA CHỌN NĂM", Alert.AlertType.warning);
                    return;
                }
                if (Int32.Parse(cbQuy.Text) < 1 || Int32.Parse(cbQuy.Text) > 4)
                {
                    Alert.Show("KHÔNG CÓ QUÝ NÀY", Alert.AlertType.error);
                    return;
                }
                if (Int32.Parse(cbNam.Text) < 1000 || Int32.Parse(cbNam.Text) > 9999)
                {
                    Alert.Show("KHÔNG CÓ NĂM NÀY", Alert.AlertType.error);
                    return;
                }
               
                sb = "DATEPART(q,NgayBan)='" + cbQuy.Text + "' AND YEAR(NgayBan)='" + cbNam.Text + "'";
                sn = "DATEPART(q,NgayNhap)='" + cbQuy.Text + "' AND YEAR(NgayNhap)='" + cbNam.Text + "'";
                kieutk = "QUÝ " + cbQuy.Text+"/"+cbNam.Text;
            }
            // cick vao radio button thong ke theo nam
            if (rbNam.Checked)
            {

                if (cbNam.Text == "")
                {
                    Alert.Show("CHƯA CHỌN NĂM", Alert.AlertType.warning);
                    return;
                }
                if (Int32.Parse(cbNam.Text) < 1000 || Int32.Parse(cbNam.Text) > 9999)
                {
                    Alert.Show("KHÔNG CÓ NĂM NÀY", Alert.AlertType.error);
                    return;
                }
                sb = "YEAR(NgayBan)='" + cbNam.Text + "'";
                sn = "YEAR(NgayNhap)='" + cbNam.Text + "'";
                kieutk = "NĂM "+cbNam.Text;
            }
            // cick vao radio button thong ke theo khoang thoi gian
            if (rbKhoangTG.Checked)
            {
                
                sb = "NgayBan BETWEEN '" + dtlTuNgay.Value.ToString("yyyy-MM-dd") + "' AND '" + dtlToiNgay.Value.ToString("yyyy-MM-dd") + "'";
                sn = "NgayNhap BETWEEN '" + dtlTuNgay.Value.ToString("yyyy-MM-dd") + "' AND '" + dtlToiNgay.Value.ToString("yyyy-MM-dd") + "'";
                kieutk = "TỪ "+ dtlTuNgay.Value.ToString("dd/MM/yyyy") +" ĐẾN "+ dtlToiNgay.Value.ToString("dd/MM/yyyy");
            }

            if (rbNgay.Checked == false && rbThang.Checked == false && rbQuy.Checked == false && rbNam.Checked == false
                && rbKhoangTG.Checked == false)
            {
                Alert.Show("CHỌN HÌNH THỨC THỐNG KÊ", Alert.AlertType.warning);
            }
            else
            {

                dtgvThongKe.DataSource = DAL_ctkk.LaySoLuong(sb, sn); // gan gia tri cho datagridview
                // Gan gia tri cho textbox tong tien ban, tong tien nhap
                if (DAL_ctkk.LayGiaTriBan(sb) != "" && DAL_ctkk.LayGiaTriNhap(sn) != "")
                {
                    lbTongTienBan.Text = DAL_ctkk.LayGiaTriBan(sb) + " VNĐ";
                    lbTongTienNhap.Text = DAL_ctkk.LayGiaTriNhap(sn) + " VNĐ";

                }
                else
                {
                    lbTongTienBan.Text = "0 VNĐ";
                    lbTongTienNhap.Text = "0 VNĐ";
                }
            }

            // gan gia tri cho dttk
            dttk = new DataTable();
            dttk = DAL_ctkk.LaySoLuong(sb, sn);
        }

        //Su kien ckick vao radio button thong ke theo ngay
        private void rbNgay_CheckedChanged(object sender, EventArgs e)
        {
            KhoaDieuKhien();
            dtlNL.Enabled = true;
            clearDatagridview(dtgvThongKe);
        }
        //Su kien ckick vao radio button thong ke theo thang
        private void rbThang_CheckedChanged(object sender, EventArgs e)
        {
            KhoaDieuKhien();
            cbThang.Enabled = true;
            cbNam.Enabled = true;
            clearDatagridview(dtgvThongKe);
        }
        //Su kien ckick vao radio button thong ke theo quy
        private void rbQuy_CheckedChanged(object sender, EventArgs e)
        {
            KhoaDieuKhien();
            cbNam.Enabled = true;
            cbQuy.Enabled = true;
            clearDatagridview(dtgvThongKe);
        }
        //Su kien ckick vao radio button thong ke theo nam
        private void rbNam_CheckedChanged(object sender, EventArgs e)
        {
            KhoaDieuKhien();
            cbNam.Enabled = true;
            clearDatagridview(dtgvThongKe);
        }
        //Su kien ckick vao radio button thong ke theo khoang thoi gian
        private void rbKhoangTG_CheckedChanged(object sender, EventArgs e)
        {
            KhoaDieuKhien();
            dtlTuNgay.Enabled = true;
            dtlToiNgay.Enabled = true;
            clearDatagridview(dtgvThongKe);
        }

        // Khong cho nhap chu vao combobox
        private void cbThang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void cbQuy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void cbNam_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        //su kien click vao button In
        private void btn_In_Click(object sender, EventArgs e)
        {
           
            crThongKe hoadon = new crThongKe();  // tao form chua report
            DataSet ds = new DataSet(); // tao data cho report
            try
            {
                ds.Tables.Add(dttk.Copy());
            }
            catch (Exception)
            {
                Alert.Show("CHƯA CÓ DỮ LIỆU THỐNG KÊ", Alert.AlertType.error);
                return;
            }
            if (dtgvThongKe.Rows.Count == 0)
            {
                Alert.Show("CHƯA CÓ DỮ LIỆU THỐNG KÊ", Alert.AlertType.error);
                return;
            }

            // gan cac gia tri cho report
            ds.WriteXmlSchema("tk.xml");
            ThongKe rp = new ThongKe();
            rp.SetDataSource(ds);
            crThongKe cr = new crThongKe();
            cr.cr_ThongKe.ReportSource = rp;
            rp.SetParameterValue("tbKieuTK", kieutk);
            rp.SetParameterValue("tbNgaybd", dtlNL.Text); //tên textbox rồi giá trị gán phái sau 
            rp.SetParameterValue("tbNgaykt", "");
            rp.SetParameterValue("tbTongTienNhap", lbTongTienNhap.Text);
            rp.SetParameterValue("tbTongTienBan", lbTongTienBan.Text);
            cr.ShowDialog();
        }
    }

    /// Kết nối đến dsdl
    public class KetNoiDB
    {
        public static SqlConnection connect;
        
    
        /// Hàm mở kết nối tới cơ sở dữ liệu
        public void MoKetNoi()
        {
            if (KetNoiDB.connect == null)
                KetNoiDB.connect = new SqlConnection(@"Data Source=.\SqlExpress;Initial Catalog=quanlybanhang;Integrated Security=True");
            if (KetNoiDB.connect.State != ConnectionState.Open) // nếu trạng thái kết nối khác trạng thái mở thì mở kết nối
                KetNoiDB.connect.Open();
        }

    
        ///  Hàm đóng kết nối tới cơ sở dữ liệu
        public void DongKetNoi()
        {
            if (KetNoiDB.connect != null)
            {
                if (KetNoiDB.connect.State == ConnectionState.Open)
                    KetNoiDB.connect.Close();
            }
        }


     
        /// Hàm thực thi các câu lện sql
        public void ThucThiCauLenhSQL(string strSQL)
        {
            try
            {
                MoKetNoi();
                SqlCommand sqlcmd = new SqlCommand(strSQL, connect);
                sqlcmd.ExecuteNonQuery();
                DongKetNoi();
            }
            catch
            {

            }
        }
        /// Hàm lấy nguyên bảng trong csdl đổ vào dataTable
        public DataTable GetDataTable(string strSQL)
        {
            try
            {
                MoKetNoi();
                DataTable dt = new DataTable();
                SqlDataAdapter sqlda = new SqlDataAdapter(strSQL, connect);
                sqlda.Fill(dt);

                DongKetNoi();
                return dt;
            }
            catch
            {
                return null;
            }

        }


 
        /// Hàm lấy một giá trị trong bảng
        public string GetValue(string strSQL)
        {
            string temp = null;
            MoKetNoi();
            SqlCommand sqlcmd = new SqlCommand(strSQL, connect);
            SqlDataReader sqldr = sqlcmd.ExecuteReader();
            while (sqldr.Read())
                temp = sqldr[0].ToString();
            DongKetNoi();
            return temp;
        }
    }

    public class ChiTietPKK_DAL
    {
        KetNoiDB kn = new KetNoiDB();


   
       
       /// Lấy số lượng bán, số lượng nhập, số lượng tồn kho
        public DataTable LaySoLuong(string thoigianban, string thoigiannhap)
        {
            return kn.GetDataTable(@"select bang5.MaHH,bang5.TenHH,bang5.SoLuongBan,bang5.SoLuongNhap,tk.SoLuongTonKho from (
                                    SELECT bang1.MaHH,bang1.TenHH,SLB as SoLuongBan,SLN as SoLuongNhap FROM (SELECT HANG_HOA.MaHH,TenHH,ISNULL(SoLuongBan,0)
                                    as SLB FROM dbo.HANG_HOA LEFT JOIN
                                    (SELECT ct.MaHH,SUM(SoLuong)SoLuongBan FROM HOA_DON_BAN_HANG hd JOIN CHI_TIET_HOA_DON ct
                                    ON hd.MaHD = ct.MaHD  
                                    WHERE " + thoigianban + @" GROUP BY ct.MaHH) as bang  ON HANG_HOA.MaHH=bang.MaHH) as
                                    bang1,(SELECT HANG_HOA.MaHH,TenHH,ISNULL(SoLuongNhap,0)as SLN FROM dbo.HANG_HOA LEFT JOIN
                                    (SELECT ct.MaHH,SUM(SoLuong)SoLuongNhap FROM PHIEU_NHAP hd JOIN CHI_TIET_PHIEU_NHAP ct
                                    ON hd.MaPN = ct.MaPN  
                                    WHERE " + thoigiannhap + @" 
                                    GROUP BY ct.MaHH) as bang2
                                    ON HANG_HOA.MaHH = bang2.MaHH
                                    ) as bang3 WHERE bang1.MaHH = bang3.MaHH) as bang5 join TON_KHO tk on bang5.MaHH = tk.MaHH");
        }
        
        /// Lấy tỗng tiền bán từ hóa đơn bán hàng
        public string LayGiaTriBan(string dk)
        {
            string kq = kn.GetValue("select SUM(TongTienHD) from HOA_DON_BAN_HANG where " + dk);
            return kq;
        }


        /// Lấy tỗng tiền nhập từ phiếu nhập
        public string LayGiaTriNhap(string dk)
        {
            string kq = kn.GetValue(@"select SUM(SoLuong*DonGia) from CHI_TIET_PHIEU_NHAP ct join PHIEU_NHAP pn 
                                        On ct.MaPN = pn.MaPN
                                         where " + dk);
            return kq;
        }


    }
}
