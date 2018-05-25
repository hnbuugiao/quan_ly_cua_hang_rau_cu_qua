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
using System.Text.RegularExpressions;
namespace CuaHangRauCuQua
{
    public partial class PopupDangNhap : Form
    {
        Form tc;
        public PopupDangNhap(Form trangchu)
        {
            InitializeComponent();
            this.tc = trangchu;
        }
        public int dangnhap = 0;
        public string quyen = "";
        public string taikhoan = "";
        public trangchu trangchu = new trangchu();
        public Dangxuat dangxuat = new Dangxuat(ActiveForm);
        SqlConnection conn = new SqlConnection(@"Data Source=.\SqlExpress;Initial Catalog=quanlybanhang;Integrated Security=True");
        SqlCommand command;
        private void btnclosednhap_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void ckHienmatkhau_CheckedChanged(object sender, EventArgs e)
        {
            if (ckHienmatkhau.Checked)
            {
                txtdangnhap_Matkhau.UseSystemPasswordChar = false;
            }
            else
            {
                txtdangnhap_Matkhau.UseSystemPasswordChar = true;
            }
        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            
            if (this.txtdangnhap_Taikhoan.TextLength == 0)
                Alert.Show("CHƯA NHẬP TÀI KHOẢN", Alert.AlertType.warning);
            else if (this.txtdangnhap_Matkhau.TextLength == 0)
                Alert.Show("CHƯA NHẬP MẬT KHẨU", Alert.AlertType.warning);
            else
            {
                conn.Open();
                string sql = "select COUNT(*) From NHAN_VIEN where MaNV=@id and MatKhau=@pass";
                command = new SqlCommand(sql, conn);
                command.Parameters.Add(new SqlParameter("@id", strtrim(txtdangnhap_Taikhoan.Text)));
             
                command.Parameters.Add(new SqlParameter("@pass", txtdangnhap_Matkhau.Text));
                int x = (int)command.ExecuteScalar();

                    if (x == 1)
                    {

                        string id = txtdangnhap_Taikhoan.Text.ToString();
                        string pass = txtdangnhap_Matkhau.Text.ToString();

                        string sqltt = "SELECT TrangThai FROM NHAN_VIEN WHERE MaNV= '" +id + "' and MatKhau= '" + pass+ "' ";
                        SqlCommand cmdtt = new SqlCommand(sqltt, conn);
                        int TrangThai = (int)cmdtt.ExecuteScalar();
                    if (TrangThai == 1)
                    {

                        string sql2 = "select TenNV,MaQuyen From NHAN_VIEN where MaNV= '" + id + "' and MatKhau= '" + pass + "'";
                        SqlCommand cmd2 = new SqlCommand(sql2, conn);
                        SqlDataReader dr = cmd2.ExecuteReader();
                        while (dr.Read())
                        {
                            taikhoan = dr.GetString(0);
                            quyen = dr.GetString(1);

                        }


                        this.Hide();
                        Alert.Show("ĐĂNG NHẬP THÀNH CÔNG", Alert.AlertType.success);
                        dangxuat.LoadText(quyen, taikhoan);
                        //   ActiveForm.Hide();
                        tc.TopLevel = false;
                        tc.AddOwnedForm(trangchu);
                        dangxuat.tc = trangchu;
                        trangchu.MaNVtemp = id;
                        trangchu.disable_button(quyen);
                        trangchu.DangnhapPanel.Controls.Clear();
                        trangchu.DangnhapPanel.Controls.Add(dangxuat);
                        //  trangchu.test(trangchu);
                        this.trangchu.Show();
                    }else
                    {
                        Alert.Show("TÀI KHOẢN BỊ KHÓA", Alert.AlertType.error);
                    }


                    }
                    else
                    {
                        Alert.Show("SAI TÀI KHOẢN HOẶC MẬT KHẨU", Alert.AlertType.error);
                    }
                    conn.Close();
     

            }
        }

        private void PopupDangNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtdangnhap_Matkhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                //btnDangnhap.Focus();
                btnDangnhap_Click(sender, e);

        }

        private void txtdangnhap_Taikhoan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                this.txtdangnhap_Taikhoan.Text = strtrim(txtdangnhap_Taikhoan.Text);
                txtdangnhap_Matkhau.Focus();
                
            }
        }

        private void txtdangnhap_Taikhoan_TextChanged(object sender, EventArgs e)
        {
            txtdangnhap_Taikhoan.CharacterCasing = CharacterCasing.Upper;
        }
    }
}
