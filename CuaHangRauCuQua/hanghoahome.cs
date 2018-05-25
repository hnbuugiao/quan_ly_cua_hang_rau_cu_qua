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
    public partial class hanghoahome : UserControl
    {
        public hanghoahome()
        {
            InitializeComponent();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnNhacungcap_Click(object sender, EventArgs e)
        {
            hanghoa_Nhacungcap nhacungcap = new hanghoa_Nhacungcap();
            hanghoaPanelHome.Controls.Clear();
            hanghoaPanelHome.Controls.Add(nhacungcap);

            tieudeQLHHNhacungcap tieudeNCC = new tieudeQLHHNhacungcap();
            tieudeQLHHPanel.Controls.Clear();
            tieudeQLHHPanel.Controls.Add(tieudeNCC);
        }

        private void btnHanghoa_Click(object sender, EventArgs e)
        {
            hanghoa_Hanghoa hanghoa = new hanghoa_Hanghoa();
            hanghoaPanelHome.Controls.Clear();
            hanghoaPanelHome.Controls.Add(hanghoa);

            tieudeQLHHHanghoa tieudeHH = new tieudeQLHHHanghoa();
            tieudeQLHHPanel.Controls.Clear();
            tieudeQLHHPanel.Controls.Add(tieudeHH);
        }

        private void btnNhomhang_Click(object sender, EventArgs e)
        {
            hanghoa_Nhomhang nhomhang = new hanghoa_Nhomhang();
            hanghoaPanelHome.Controls.Clear();
            hanghoaPanelHome.Controls.Add(nhomhang);

            tieudeQLHHNhomhang tieudeNH = new tieudeQLHHNhomhang();
            tieudeQLHHPanel.Controls.Clear();
            tieudeQLHHPanel.Controls.Add(tieudeNH);
        }
    }
}
