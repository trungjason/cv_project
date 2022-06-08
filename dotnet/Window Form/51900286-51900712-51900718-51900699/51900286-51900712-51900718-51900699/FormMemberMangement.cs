using RestaurentDAO;
using RestaurentDTO;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Windows.Forms;

namespace _51900286_51900712_51900718_51900699
{
    public partial class FormMemberMangement : Form
    {
        public FormMemberMangement(NhanVien nhanvien)
        {
            InitializeComponent();
            this.Tag = nhanvien;

            button_UpdateMember.Enabled = false;
            LoadMemberList();
        }

        #region Method
        private void LoadMemberList()
        {
            List<HoiVien> memberList = MemberDAO.GetInstance().GetAllMember();
            listView_MemberList.Items.Clear();

            foreach (HoiVien member in memberList)
            {
                ListViewItem listViewItem = new ListViewItem(member.TenHoiVien);
                listViewItem.Tag = member;
                listViewItem.SubItems.Add(member.SoDienThoai);
                listViewItem.SubItems.Add(member.TongSoTienThanhToan.ToString());
                listViewItem.SubItems.Add(member.Email.ToString());
                listViewItem.SubItems.Add(member.DiaChi.ToString());
                listViewItem.SubItems.Add(member.QuyenLoi.ToString());

                listView_MemberList.Items.Add(listViewItem);
            }
        }

        private bool ValidateMemberInput()
        {
            string memberName = textBox_MemberName.Text;
            string memberPhoneNumber = textBox_MemberPhoneNumber.Text;
            string memberEmail = textBox_MemberEmail.Text;

            if (memberName == "" || memberPhoneNumber == "" || memberEmail.ToString() == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return false;
            }

            try
            {
                MailAddress validEmail = new MailAddress(memberEmail);
            }
            catch (FormatException)
            {
                MessageBox.Show("Email không hợp lệ.");
                return false;
            }

            if (!long.TryParse(memberPhoneNumber, out long result))
            {
                MessageBox.Show("Số điện thoại không hợp lệ");
                return false;
            }

            return true;
        }

        private void Clear()
        {
            textBox_MemberName.Text = "";
            textBox_MemberPhoneNumber.Text = "";
            textBox_MemberPhoneNumber.Enabled = true;

            textBox_MemberAddress.Text = "";
            textBox_MemberEmail.Text = "";
            textBox_MemberBenefit.Text = "";

            button_AddMember.Enabled = true;
            button_UpdateMember.Enabled = false;
        }

        #endregion

        #region Events

        private void button_UpdateMember_Click(object sender, EventArgs e)
        {
            if (ValidateMemberInput())
            {
                string memberName = textBox_MemberName.Text;
                string memberPhoneNumber = textBox_MemberPhoneNumber.Text;
                string memberAddress = textBox_MemberAddress.Text;
                string memberEmail = textBox_MemberEmail.Text;
                string memberBenefit = textBox_MemberBenefit.Text;

                HoiVien hoiVien = new HoiVien(memberName, memberPhoneNumber, memberAddress, memberEmail, memberBenefit);

                if (MemberDAO.GetInstance().UpdateMember(hoiVien))
                {
                    LoadMemberList();
                    Clear();
                    MessageBox.Show("Sửa thông tin hội viên thành công !!!");
                }
                else
                {
                    MessageBox.Show("Sửa thông tin hội viên không thành công !!!");
                }
            }
        }

        private void button_AddMember_Click(object sender, EventArgs e)
        {
            if ( ValidateMemberInput() )
            {
                string memberName = textBox_MemberName.Text;
                string memberPhoneNumber = textBox_MemberPhoneNumber.Text;
                string memberAddress = textBox_MemberAddress.Text;
                string memberEmail = textBox_MemberEmail.Text;
                string memberBenefit = textBox_MemberBenefit.Text;

                HoiVien hoiVien = new HoiVien(memberName , memberPhoneNumber, memberAddress , memberEmail , memberBenefit);

                if ( MemberDAO.GetInstance().InsertNewMember(hoiVien))
                {
                    LoadMemberList();
                    Clear();
                    MessageBox.Show("Thêm hội viên thành công !!!");
                } else
                {
                    MessageBox.Show("Thêm hội viên không thành công !!!");
                }
            }
        }
            
        private void listView_MemberList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_MemberList.SelectedItems.Count > 0)
            {
                button_AddMember.Enabled = false;
                button_UpdateMember.Enabled = true;

                HoiVien monAn = (HoiVien)listView_MemberList.SelectedItems[0].Tag;

                textBox_MemberName.Text = monAn.TenHoiVien;
                textBox_MemberPhoneNumber.Text = monAn.SoDienThoai;
                textBox_MemberPhoneNumber.Enabled = false;

                textBox_MemberAddress.Text = monAn.DiaChi;
                textBox_MemberEmail.Text = monAn.Email;
                textBox_MemberBenefit.Text = monAn.QuyenLoi;
             
            }
        }

        private void FormMemberMangement_Load(object sender, EventArgs e)
        {

        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        #endregion
    }
}
