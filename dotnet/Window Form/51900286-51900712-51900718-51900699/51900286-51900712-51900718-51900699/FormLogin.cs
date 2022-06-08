using RestaurentDAO;
using System;
using System.Windows.Forms;

namespace _51900286_51900712_51900718_51900699
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private bool ValidateLogin()
        {
            // TODO: Validate user name and password then show warning like password's length lower than 6 character..
            string username = textBox_Username.Text;
            string password = textBox_Password.Text;

            return AccountDAO.GetInstance().ValidateLogin(username, password);
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn thoát chương trình ?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (ValidateLogin())
            {
                FormTableManager formTableManager = new FormTableManager();
                this.Hide();
                // ShowDialog để ghi đè form login like z-index

                string employeeID = AccountDAO.GetInstance().GetAccountByUsername(textBox_Username.Text).MaNhanVien;
                formTableManager.Tag = EmployeeDAO.GetInstance().GetEmployeeByID(employeeID);
                formTableManager.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu ! Vui lòng nhập lại !", "Thông báo", MessageBoxButtons.OK);
            };
        }
    }
}
