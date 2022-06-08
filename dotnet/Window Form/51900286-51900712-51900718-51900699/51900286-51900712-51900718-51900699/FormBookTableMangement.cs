using RestaurentDAO;
using RestaurentDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace _51900286_51900712_51900718_51900699
{
    public partial class FormBookTableMangement : Form
    {
        private Dictionary<string, int> buttonList;

        public FormBookTableMangement(NhanVien nhanvien)
        {
            InitializeComponent();
            this.buttonList = new Dictionary<string, int>();
            this.Tag = nhanvien;
            dataGridView_BookTableRequest.Tag = new BanAn();
            // Load Table when form is loaded
            LoadTable();
            LoadBookList();
        }


        #region Method

        private void LoadBookList()
        {
            // Load lịch hẹn lên data grid view
            BindingList<LichHenTam> bi = new BindingList<LichHenTam>();

            foreach ( LichHenTam lichhentam in MixableDAO.GetInstance().GetLichHenTam() ){
                bi.Add(lichhentam);
            }
            
            dataGridView_BookTableRequest.DataSource = bi;
        }

        private void LoadTable()
        {
            List<BanAn> listTable = TableDAO.GetInstance().LoadTableList();
            int i = 0;
            foreach (BanAn banAn in listTable)
            {
                // Lấy từng bàn trong danh sách bàn ra và tạo button dựa trên thông tin từ bàn ăn đó
                Button tableButton = new Button()
                {
                    Width = TableDAO.TABLE_WIDTH,
                    Height = TableDAO.TABLE_HEIGHT
                };

                // Show text button table
                tableButton.Text = banAn.MaBanAn + '\n' + banAn.TinhTrang + '\n' + "Sức Chứa: " + banAn.SucChua;
                // Add click event to button
                tableButton.Click += tableButtonClick;
                // Save an object into button throught tag attribute
                tableButton.Tag = banAn;

                // Set màu cho button bàn ăn dựa trên tình trạng
                if (banAn.TinhTrang.Equals("Trống"))
                {
                    tableButton.BackColor = Color.FromArgb(32, 250, 83);
                }
                else if (banAn.TinhTrang.Equals("Đặt lịch hẹn"))
                {
                    tableButton.BackColor = Color.Aqua;
                }
                else
                {
                    tableButton.BackColor = Color.FromArgb(245, 56, 56);
                }

                // Lưu mã bàn vào dictionary với value là i
                this.buttonList.Add(banAn.MaBanAn, i);
                i += 1;
                // Add button to flow layout panel
                flowLayoutPanel_Table.Controls.Add(tableButton);
            }
        }

        // Đặt trạng thái bàn
        private void SettingTableState(string tableID)
        {
            // Lấy bàn ăn theo mã bàn
            BanAn banAn = TableDAO.GetInstance().GetTableByID(tableID);
            // Set lại Tag cho bàn ăn đó dựa trên value được lấy ra từ dictionary
            flowLayoutPanel_Table.Controls[this.buttonList[tableID]].Tag = banAn;
            // cập nhật lại text của button đó
            flowLayoutPanel_Table.Controls[this.buttonList[tableID]].Text = banAn.MaBanAn + '\n' + banAn.TinhTrang + '\n' + "Sức Chứa: " + banAn.SucChua;

            // cập nhật lại màu cho button tương ứng với bàn ăn
            if (banAn.TinhTrang.Equals("Trống"))
            {
                flowLayoutPanel_Table.Controls[this.buttonList[tableID]].BackColor = Color.FromArgb(32, 250, 83);
            }
            else if (banAn.TinhTrang.Equals("Đặt lịch hẹn"))
            {
                flowLayoutPanel_Table.Controls[this.buttonList[tableID]].BackColor = Color.Aqua;
            }
            else
            {
                flowLayoutPanel_Table.Controls[this.buttonList[tableID]].BackColor = Color.FromArgb(245, 56, 56);
            }
        }
        #endregion

        #region Events

        private void tableButtonClick(object sender, EventArgs e)
        {
            if (((sender as Button).Tag as BanAn).TinhTrang.Equals("Đang phục vụ"))
            {
                MessageBox.Show("Bàn ăn đang phục vụ không thể đặt lịch hẹn !!!", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            string tableID = ((sender as Button).Tag as BanAn).MaBanAn;
            textBox_TableID.Text = tableID;
            dataGridView_BookTableRequest.Tag = ((sender as Button).Tag as BanAn);

            if (((sender as Button).Tag as BanAn).TinhTrang.Equals("Đặt lịch hẹn"))
            {
                LichHen lichHen = BookTableDAO.GetInstance().GetBookTableTime(tableID);

                if ( lichHen.MaLichHen.Equals(""))
                {
                    MessageBox.Show("Không tìm thấy lịch hẹn !!!", "Thông báo", MessageBoxButtons.OK);
                    if (TableDAO.GetInstance().SetTableState(tableID, "Trống"))
                    {
                        SettingTableState(tableID);
                    }

                } else
                {
                    string message = string.Format("Bàn ăn {0} được đặt lịch vào thời gian: {1}, ngày: {2}", tableID, lichHen.ThoiGian.ToString(), lichHen.NgayHen.ToString());
                    MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK);
                }
            }         
        }

        private void button_BookTable_Click(object sender, EventArgs e)
        {
            string tableID = (dataGridView_BookTableRequest.Tag as BanAn).MaBanAn;
            string bookID = textBox_currentBookID.Text;
            string employeeID = ((this.Tag) as NhanVien).MaNhanVien;

            if (tableID.Equals("") || bookID.Equals("0") )
            {
                MessageBox.Show("Bạn cần chọn một bàn ăn và lịch hẹn trước khi nhấn đặt lịch !!!", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            if ((dataGridView_BookTableRequest.Tag as BanAn).SucChua < (int)textBox_currentBookID.Tag)
            {
                MessageBox.Show("Bàn ăn vừa chọn có sức chứa nhỏ hơn số lượng khách trong lịch hẹn  !!!", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            // Update mã bàn ăn + mã nhân viên vào lịch hẹn
            if (BookTableDAO.GetInstance().UpdateBookTable(bookID, tableID, employeeID))
            {
                if (TableDAO.GetInstance().SetTableState(tableID, "Đặt lịch hẹn"))
                {
                    if (!button_BookTable.Tag.ToString().Equals(""))
                    {
                        string previous_TableID = button_BookTable.Tag.ToString();
                        if (TableDAO.GetInstance().SetTableState(previous_TableID, "Trống"))
                        {
                            SettingTableState(previous_TableID);
                        }
                        else
                        {
                            MessageBox.Show("Đặt lại trạng thái bàn trước không thành công !!!", "Thông báo", MessageBoxButtons.OK);
                        }

                    }

                    SettingTableState(tableID);
                    LoadBookList();
                }
            }
            else
            {
                MessageBox.Show("Đặt lịch không thành công !!!", "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void dataGridView_BookTableRequest_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            if (index < 0 || index >= dataGridView_BookTableRequest.RowCount)
            {
                return;
            }

            DataGridViewRow row = dataGridView_BookTableRequest.Rows[index];
    
            // Save book ID, number of customer and table ID if exists
            // Số lượng khách
            textBox_currentBookID.Tag = (int)(row.Cells[2].Value);
            // Mã lịch hẹn
            textBox_currentBookID.Text = Convert.ToString(row.Cells[0].Value);
            // Mã bàn ăn
            button_BookTable.Tag = Convert.ToString(row.Cells[7].Value);
        }

        private void button_ShowBookList_Click(object sender, EventArgs e)
        {
            LoadBookList();
        }

        private void button_DeleteBook_Click(object sender, EventArgs e)
        {
            if ( textBox_currentBookID.Text.Equals("") || textBox_currentBookID.Text == null)
            {
                MessageBox.Show("Vui lòng chọn lịch hẹn cần xóa !!!", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            if (!BookTableDAO.GetInstance().DeleteBookTable(textBox_currentBookID.Text))
            {
                MessageBox.Show("Xóa lịch hẹn không thành công !!!", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            MessageBox.Show("Xóa lịch hẹn thành công !!!", "Thông báo", MessageBoxButtons.OK);

            foreach(DataGridViewRow dataGridViewRow in this.dataGridView_BookTableRequest.Rows)
            {
                if ( dataGridViewRow.Cells[0].Value.Equals(textBox_currentBookID.Text))
                {
                    this.dataGridView_BookTableRequest.Rows.Remove(dataGridViewRow);
                }
            }

            // TODO [Xử lý nếu lịch hẹn đã có bàn ăn thì cập nhật lại tình trạng bàn ăn hoặc không cho xóa]
        }

        #endregion
    }
}
