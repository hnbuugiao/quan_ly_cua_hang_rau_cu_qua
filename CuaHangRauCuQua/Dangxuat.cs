using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuaHangRauCuQua
{
    public partial class Dangxuat : UserControl
    {
        public trangchu tc;
        public Dangxuat(Form trangchu)
        {
            InitializeComponent();
            this.tc = tc;

        }

        public Dangxuat()
        {
            InitializeComponent();
        }
        public void LoadText(string string1,string string2)
        {
            /* PopupDangNhap login = new PopupDangNhap();
             string quyen = login.quyen;
             string ten = login.taikhoan; */
            string quyen;
            if (string1.Equals("NV"))
                quyen = "Nhân viên";
            else
                quyen = "Admin";
            flatLabel1.Text = quyen;
            flatLabel2.Text = string2;
        }

        private void btnDangxuat_Click(object sender, EventArgs e)
        {
            homeContent home = new homeContent();
            tc.enable_button();
            tc.DangnhapPanel.Controls.Clear();
            tc.DangnhapPanel.Controls.Add(tc.btnDangnhap);
            tc.contentPanelhome.Controls.Clear();
            tc.contentPanelhome.Controls.Add(home);
            tc.tieudeHeaderPanel.Controls.Clear();
        }
    }
}
