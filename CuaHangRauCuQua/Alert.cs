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
    public partial class Alert : Form
    {
        /// <summary>
        /// Đặt thuộc tính cho các loại thông báo
        /// </summary>
        /// <param name="_message">Chuỗi thông báo</param>
        /// <param name="type">Kiểu thông báo</param>
        public Alert(string _message, AlertType type)
        {
            InitializeComponent();

            this.message.Text = _message;

            switch (type) {
                case AlertType.success:
                    this.BackColor = Color.FromArgb(0, 185, 161);
                    icon.Image = imageList1.Images[0];
                    break;
                case AlertType.warning:
                    this.BackColor = Color.FromArgb(255, 165, 0);
                    icon.Image = imageList1.Images[1];
                    break;
                case AlertType.error:
                    this.BackColor = Color.FromArgb(200, 51, 51);
                    icon.Image = imageList1.Images[2];
                    break;
                case AlertType.info:
                    this.BackColor = Color.FromArgb(25, 159, 220);
                    icon.Image = imageList1.Images[3];
                    break;
            }
        }
        /// <summary>
        /// Xử lí khi load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Alert_Load(object sender, EventArgs e)
        {
            // Đặt vị trí bot left cho thông báo
            this.Top = Screen.PrimaryScreen.Bounds.Height - this.Height - 176;
            this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width - 276;
            //Bắt đầu chạy timer show 
            show.Start();
        }
        /// <summary>
        /// Hàm gọi lại thông báo 
        /// </summary>
        /// <param name="message">Chuỗi thông báo</param>
        /// <param name="type">Loại thông báo</param>
        public static void Show(string message, AlertType type)
        {
            new CuaHangRauCuQua.Alert(message, type).Show();
        }
        /// <summary>
        /// Các kiểu thông báo 
        /// </summary>
        public enum AlertType
        {
            success, warning, error, info
        }
        /// <summary>
        /// Khi bấm vào nút close thì sẽ bắt đầu gọi timer close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnclose_Click(object sender, EventArgs e)
        {
            close.Start();
        }

        /// <summary>
        /// Xử lí sự kiện cho timer show xảy ra khi chạy hết thời gian interval
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void show_Tick(object sender, EventArgs e)
        {       
            //Dừng timer show
            show.Stop();
            //Bắt đầu timer close
            close.Start();
   
        }
        /// <summary>
        /// Hàm xử lí sự kiện cho timer close, làm mờ hết thời gian interval thì đóng thông báo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0)
            {
                //Giảm opacity tới 0
                this.Opacity -= 0.1;
            }
            else
            {
                // Đóng lại
                this.Close(); 
            }
        }

        
    }

}
