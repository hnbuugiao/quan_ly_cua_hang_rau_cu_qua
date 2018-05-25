using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuaHangRauCuQua
{
    public partial class trangchu : Form
    {
        public int dangnhap = 0;
        public string MaNVtemp;
        public trangchu()
        {
            InitializeComponent();
            enable_button();
        }
        public void test(Form okform)
        {

            ActiveForm.Close();
           // okform.Show();

            //  okform.Show();
        }
        public void enable_button()
        {
            btnQuanlynhanvien.Enabled = false;
            btnQuanlyhanghoa.Enabled = false;
            btnPhieunhap.Enabled = false;
            btnPhieuXuat.Enabled = false;
            btnKiemKeThongKe.Enabled = false;
        }
        public void disable_button(string string1)
        {
            if(string1.Equals("AD")){
                btnQuanlynhanvien.Enabled = true;
                btnQuanlyhanghoa.Enabled = true;
                btnPhieunhap.Enabled = true;
                btnPhieuXuat.Enabled = true;
                btnKiemKeThongKe.Enabled = true;
            }else
            {
                btnQuanlyhanghoa.Enabled = true;
                btnPhieunhap.Enabled = true;
                btnPhieuXuat.Enabled = true;
            }

        }

        private void btnQuanlynhanvien_Click(object sender, EventArgs e)
        {
            Quanlynhanvien quanlinhanvien = new Quanlynhanvien();
            contentPanelhome.Controls.Clear();
            contentPanelhome.Controls.Add(quanlinhanvien);

            TieudeHeaderQLNhanvien tieudeQLNhanvien = new TieudeHeaderQLNhanvien();
            tieudeHeaderPanel.Controls.Clear();
            tieudeHeaderPanel.Controls.Add(tieudeQLNhanvien);
        }

        private void btnQuanlyhanghoa_Click(object sender, EventArgs e)
        {
            hanghoahome quanlyhanghoa = new hanghoahome(); // khởi tạo user control
            contentPanelhome.Controls.Clear();// xóa bỏ những gì nằm trên panelNhacungcap
            contentPanelhome.Controls.Add(quanlyhanghoa);

            tieudeHeaderQLHanghoa tieudeQLHanghoa = new tieudeHeaderQLHanghoa();
            tieudeHeaderPanel.Controls.Clear();
            tieudeHeaderPanel.Controls.Add(tieudeQLHanghoa);

            hanghoa_Hanghoa hanghoa = new hanghoa_Hanghoa();
            quanlyhanghoa.hanghoaPanelHome.Controls.Clear();
            quanlyhanghoa.hanghoaPanelHome.Controls.Add(hanghoa);

            tieudeQLHHHanghoa tieudeHH = new tieudeQLHHHanghoa();
            quanlyhanghoa.tieudeQLHHPanel.Controls.Clear();
            quanlyhanghoa.tieudeQLHHPanel.Controls.Add(tieudeHH);
        }

        private void btnPhieunhap_Click(object sender, EventArgs e)
        {
            Phieunhap phieunhap = new Phieunhap(MaNVtemp);
            contentPanelhome.Controls.Clear();
            contentPanelhome.Controls.Add(phieunhap);


            tieudeHeaderPhieunhap tieudePhieunhap = new tieudeHeaderPhieunhap();
            tieudeHeaderPanel.Controls.Clear();
            tieudeHeaderPanel.Controls.Add(tieudePhieunhap);
        }

        private void trangchu_Load(object sender, EventArgs e)
        {
            homeContent noidungHome = new homeContent();
            contentPanelhome.Controls.Clear();
            contentPanelhome.Controls.Add(noidungHome);
            
        }

        private void btnheadmenu_Click(object sender, EventArgs e)
        {
            homeContent noidungHome = new homeContent();
            contentPanelhome.Controls.Clear();
            contentPanelhome.Controls.Add(noidungHome);

            tieudeHeaderPanel.Controls.Clear();
        }

        private void btnIconhome_Click(object sender, EventArgs e)
        {
            homeContent noidungHome = new homeContent();
            contentPanelhome.Controls.Clear();
            contentPanelhome.Controls.Add(noidungHome);
            
            tieudeHeaderPanel.Controls.Clear();
        }

        public void btnDangnhap_Click_1(object sender, EventArgs e)
        {
            PopupDangNhap popupdangnhap = new PopupDangNhap(ActiveForm);
            popupdangnhap.Show();
        }

        private void btnKiemKeThongKe_Click(object sender, EventArgs e)
        {
            PhieuKiemKe phieukiemke = new PhieuKiemKe();
            contentPanelhome.Controls.Clear();
            contentPanelhome.Controls.Add(phieukiemke);

            TieudeHeaderPhieuKiemKe tieudePhieukiemke = new TieudeHeaderPhieuKiemKe();
            tieudeHeaderPanel.Controls.Clear();
            tieudeHeaderPanel.Controls.Add(tieudePhieukiemke);
        }

        private void btnPhieuXuat_Click(object sender, EventArgs e)
        {
            UserControlHoaDonBanHang hoadonbanhang = new UserControlHoaDonBanHang(MaNVtemp);
            contentPanelhome.Controls.Clear();
            contentPanelhome.Controls.Add(hoadonbanhang);

            tieudeHeaderHoadon tieudehoadon = new tieudeHeaderHoadon();
            tieudeHeaderPanel.Controls.Clear();
            tieudeHeaderPanel.Controls.Add(tieudehoadon);
        }
    }
}
