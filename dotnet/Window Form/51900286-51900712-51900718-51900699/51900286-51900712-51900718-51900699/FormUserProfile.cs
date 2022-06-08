using RestaurentDAO;
using RestaurentDTO;
using System;
using System.Windows.Forms;

namespace _51900286_51900712_51900718_51900699
{
    public partial class FormUserProfile : Form
    {
        public FormUserProfile(NhanVien nhanvien)
        {
            InitializeComponent();
            this.Tag = nhanvien;
            LoadAccountInformation();
        }

        private void LoadAccountInformation()
        {
            NhanVien nhanvien = (this.Tag as NhanVien);

            textBox_Username.Text = AccountDAO.GetInstance().GetAccountByEmployeeID(nhanvien.MaNhanVien).TenTaiKhoan;
            textBox_EmployeeID.Text = nhanvien.MaNhanVien;
            textBox_EmployeeName.Text = nhanvien.TenNhanVien;
            textBox_EmployeeRole.Text = nhanvien.ChucVu;
            textBox_EmployeeEmail.Text = nhanvien.Email;
        }

        private void button_UpdateAccount_Click(object sender, EventArgs e)
        {
            // TODO: Đổi mật khẩu
            string oldPassword = textBox_oldPassword.Text;
            string newPassword = textBox_newPassword.Text;
            string newPasswordConfirm = textBox_newPasswordConfirm.Text;

            if ( oldPassword.Equals("") || newPassword.Equals("") || newPasswordConfirm.Equals(""))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin !!!", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            string username = textBox_Username.Text;

            if ( !AccountDAO.GetInstance().ValidateLogin(username, oldPassword) )
            {
                MessageBox.Show("Mật khẩu cũ không chính xác vui lòng nhập lại !!!", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            if ( !newPassword.Equals(newPasswordConfirm) )
            {
                MessageBox.Show("Mật khẩu mới không khớp vui lòng nhập lại !!!", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            if ( AccountDAO.GetInstance().UpdateNewPassword(username, newPassword))
            {
                MessageBox.Show("Đổi mật khẩu thành công !!!", "Thông báo", MessageBoxButtons.OK);
                return;
            } else
            {
                MessageBox.Show("Đổi mật khẩu không thành công !!!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
