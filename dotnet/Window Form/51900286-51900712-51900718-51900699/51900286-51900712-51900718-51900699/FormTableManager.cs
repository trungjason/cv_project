using RestaurentDAO;
using RestaurentDTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

namespace _51900286_51900712_51900718_51900699
{
    public partial class FormTableManager : Form
    {
        private Dictionary<string, int> buttonList;
        public FormTableManager()
        {
            InitializeComponent();               
            // Load Table when form is loaded
            LoadTable();
            LoadFoodGroup();

            // Prevent user don't choose a table but press add food button
            listView_Bill.Tag = "";
        }
     
        #region Method
        private double caculateDiscount(double totalBill, double totalPaid)
        {
            double discountPercent = 0;
            if (totalPaid <= 3000000)
            {
                discountPercent = 0.02;
            }
            else if (totalPaid <= 5000000)
            {
                discountPercent = 0.05;
            }
            else if (totalPaid <= 10000000)
            {
                discountPercent = 0.08;
            }
            else if (totalPaid <= 15000000)
            {
                discountPercent = 0.1;
            }
            else if (totalPaid <= 20000000)
            {
                discountPercent = 0.12;
            }
            else
            {
                discountPercent = 0.15;
            }

            double discountAmount = totalBill * discountPercent;

            if (discountAmount > 1500000)
            {
                discountAmount = 1500000;
            }

            return discountAmount;
        }

        public void ReLoad()
        {
            LoadTable();
            LoadFoodGroup();

            // Prevent user don't choose a table but press add food button
            listView_Bill.Tag = "";
        }

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

        private void LoadTable()
        {
            this.buttonList = new Dictionary<string, int>();
            flowLayoutPanel_Table.Controls.Clear();
            List<BanAn> listTable = TableDAO.GetInstance().LoadTableList();
            int i = 0;
            foreach( BanAn banAn in listTable)
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
                if ( banAn.TinhTrang.Equals("Trống") )
                {
                    tableButton.BackColor = Color.FromArgb(32, 250, 83);
                } else if (banAn.TinhTrang.Equals("Đặt lịch hẹn"))
                {
                    tableButton.BackColor = Color.Aqua;
                } else
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

        private void ShowBillInformation(string tableID)
        {
            double totalBillPrice = 0;
            listView_Bill.Items.Clear();

            List<HoaDonTam> hoaDonTams = MixableDAO.GetInstance().GetHoaDonTamByTableID(tableID);

            foreach(HoaDonTam hoaDonTam in hoaDonTams)
            {
                totalBillPrice += hoaDonTam.TotalPrice;

                ListViewItem listViewItem = new ListViewItem(hoaDonTam.MaMonAn.ToString());
                listViewItem.SubItems.Add(hoaDonTam.TenMonAn.ToString());
                listViewItem.SubItems.Add(hoaDonTam.SoLuongMonAn.ToString());
                listViewItem.SubItems.Add(hoaDonTam.DonGia.ToString());
                listViewItem.SubItems.Add(hoaDonTam.DonVi.ToString());
                listViewItem.SubItems.Add(hoaDonTam.MaBanAn.ToString());
                listViewItem.SubItems.Add(hoaDonTam.TotalPrice.ToString());

                listView_Bill.Items.Add(listViewItem);
            };

            textBox_TotalBillPrice.Text = totalBillPrice.ToString("c",new CultureInfo("vi-VN"));
        }

        private void LoadFoodByFoodGroupID(string foodGroupID)
        {
            List<MonAn> monAns = FoodDAO.GetInstance().GetFoodListByFoodGroupID(foodGroupID);
            comboBox_Food.DataSource = monAns;
            comboBox_Food.DisplayMember = "TenMonAn";
        }

        private void LoadFoodGroup()
        {
            List<NhomMonAn> nhomMonAns = FoodGroupDAO.GetInstance().GetFoodGroupList();
            comboBox_FoodGroup.DataSource = nhomMonAns;
            comboBox_FoodGroup.DisplayMember = "TenNhom";
        }
        #endregion

        #region Events
        private void FormTableManager_Load(object sender, EventArgs e)
        {
            // Phân quyền
            var role = (this.Tag as NhanVien).ChucVu;

            if (role == "Quản Lý Nhà Hàng")
            {
                return;
            }
            if (role != "Lễ Tân")
            {
                LeTanMenuItem.Visible = false;
            }
            if (role != "Phục Vụ" && role != "Lễ Tân")
            {
                flowLayoutPanel_Table.Enabled = false;
                comboBox_FoodGroup.Enabled = false;
                comboBox_Food.Enabled = false;
                button_AddFood.Enabled = false;
                button_Checkout.Enabled = false;
                numericUpDown_FoodCount.Enabled = false;
            }
        }
        
        // When user click an table we will save the tableID into listView_Bill Tag Attribute for using 
        // in some opeartion
        private void tableButtonClick(object sender, EventArgs e)
        {
            string tableID = ((sender as Button).Tag as BanAn).MaBanAn;
            // Save table ID to list view tag to check which table is clicked
            listView_Bill.Tag = tableID;
            ShowBillInformation(tableID);

            button_DeleteFood.Tag = "";
        }
  
        private void comboBox_FoodGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            if ( comboBox.SelectedItem == null)
            {
                return;
            }

            NhomMonAn nhomMonAn = comboBox.SelectedItem as NhomMonAn;

            LoadFoodByFoodGroupID(nhomMonAn.MaNhom);
        }

        private void button_AddFood_Click(object sender, EventArgs e)
        {
            // Để thêm một món ăn vào bàn trước tiên kiểm tra
            // Có tồn tại hóa đơn nào đang phục vụ mã bàn được click hay chưa ?
            // Trường hợp 1: Nếu tồn tại gọi thì thêm món theo 2 trường hợp : Món đã nằm trong phiếu gọi món và món chưa nằm trong phiếu gọi món
            // Trường hợp 1.1: Nếu món đã nằm trong phiếu gọi món thì tăng hoặc giảm số lượng - nếu số lượng giảm <= 0 thì xóa món ăn khỏi phiếu gọi món
            // Trường hợp 1.2: Nếu món chưa nằm trong phiếu gọi món thì insert món vào chitietgoimon.
            // Trường hợp 2: Nếu chưa tồn tại hóa đơn thì tạo mới hóa đơn theo trình tự :
            // Thêm phiếu gọi món -> thêm chi tiết gọi món dựa trên mã món ăn và mã phiếu gọi món vừa tạo -> thêm hóa đơn
            // Thêm hóa đơn dựa trên mã phiếu gọi món, mã nhân viên hiện đang đăng nhập,
            // tình trạng 0 là chưa thanh toán , 1 là đã thanh toán 

            // Get table id
            string tableID = listView_Bill.Tag.ToString();

            if (tableID.Equals(""))
            {
                MessageBox.Show("Bạn cần chọn một bàn ăn trước khi thêm món !!!", "Thông báo", MessageBoxButtons.OK);
                return;               
            }

            // Lấy hóa đơn theo mã bàn
            string billID = BillDAO.GetInstance().GetBillIDByTableID(tableID);

            // TODO: Fix bug trường hợp chọn bàn nhưng không chọn món [Đã fix]
            string foodID = (comboBox_Food.SelectedItem as MonAn) == null ? null : (comboBox_Food.SelectedItem as MonAn).MaMonAn;

            if (foodID == null)
            {
                // Chọn bàn ăn nhưng chưa chọn món [TRONG TRƯỜNG HỢP DATABASE có nhóm món ăn nhưng chưa có món ăn nào nằm trong nhóm]
                MessageBox.Show("Bạn cần chọn món ăn trước khi thêm món !!!", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            // if that table does not exist an bill then create it
            if (billID.Equals("-1"))
            {
                // TODO: Thêm chức năng ghi chú món ăn vào phiếu gọi món
                // Gọi món trực tiếp tại nhà hàng cần có mã bàn + mã nhân viên
                // mã nhân viên được lưu vào tag của FormTableManager dưới dạng Object NhanVien khi đăng nhập thành công
                // mã nhân viên của tài khoản đang đăng nhập sẽ được insert vào phieugoimon
                // mã order sẽ được tạo tự động 
                          
                // XỬ LÝ TRƯỜNG HỢP: Tạo mới hóa đơn
                int foodQuantity = (int)numericUpDown_FoodCount.Value;

                if (foodQuantity <= 0)
                {
                    // Tạo chi tiết gọi món nhưng số lượng âm 
                    MessageBox.Show("Số lượng món cần là số dương !!!", "Thông báo", MessageBoxButtons.OK);
                    return;
                }

                // Tạo phiếu gọi món
                string employeeID = (this.Tag as NhanVien).MaNhanVien;
                // TODO: Thêm ghi chú [CHƯA HOÀN THÀNH]
                OrderDAO.GetInstance().InsertNewOrder("", tableID, employeeID);
                // Tạo chi tiết gọi món
                DetailOrderDAO.GetInstance().InsertNewDetailOrder(foodID, foodQuantity);
                // Chuyển trạng thái bàn sang Đang phục vụ và cập nhật trên listview                    
                TableDAO.GetInstance().SetTableState(tableID, "Đang phục vụ");
                SettingTableState(tableID);
                // Tạo hóa đơn mới
                BillDAO.GetInstance().InsertNewBill(OrderDAO.GetInstance().GetMaxOrderID());
            }
            else
            {
                // Nếu hóa đơn đã tồn tại
                // chia 2 trường hợp : món được chọn đã có trong hóa đơn và món được chọn chưa có trong hóa đơn

                // Trường hợp đã có trong hóa đơn

                int foodQuantity = (int)numericUpDown_FoodCount.Value;
                // Lấy mã phiếu gọi món theo mã hóa đơn
                string existDetailOrderID = BillDAO.GetInstance().GetOrderIDByBillID(billID);

                if (existDetailOrderID.Equals("-1"))
                {
                    return;
                }

                // Kiểm tra món ăn có tồn tại trong phiếu gọi món vừa tìm được chưa ?
                if (DetailOrderDAO.GetInstance().CheckExistFoodInDetailOrderByOrderID(foodID, existDetailOrderID))
                {
                    // Đã tồn tại
                    // Điều chỉnh số lượng
                    // Nếu số lượng âm thì xóa
                    // TODO: Check nếu số lượng âm thì xóa nếu hóa đơn không còn món nào thì xóa hóa đơn + set lại trạng thái bàn
                    DetailOrderDAO.GetInstance().UpdateFoodQuantityByFoodIDAndDetailOrderID(foodID, existDetailOrderID, foodQuantity);

                    if (!DetailOrderDAO.GetInstance().CheckExistDetailOrderByOrderID(existDetailOrderID))
                    {
                        if (BillDAO.GetInstance().DeleteBillByOrderID(existDetailOrderID))
                        {
                            // Chuyển trạng thái bàn sang Đang phục vụ và cập nhật trên listview                    
                            if (TableDAO.GetInstance().SetTableState(tableID, "Trống"))
                            {
                                SettingTableState(tableID);
                                MessageBox.Show("Do hóa đơn không còn món ăn nào nên sẽ bị xóa !!!", "Thông báo", MessageBoxButtons.OK);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Xóa hóa đơn không thành công !!!", "Thông báo", MessageBoxButtons.OK);
                            return;
                        }
                    }
                }
                else
                {
                    // Chưa tồn tại
                    // Thêm món vào
                    if (foodQuantity > 0)
                    {
                        DetailOrderDAO.GetInstance().InsertNewFoodByFoodIDAndDetailOrderID(foodID, existDetailOrderID, foodQuantity);
                    }
                    else
                    {
                        MessageBox.Show("Số lượng món cần là số dương !!!", "Thông báo", MessageBoxButtons.OK);
                        return;
                    }
                }
            }

            // Cập nhật lại list view hiện hóa đơn
            ShowBillInformation(tableID);
            return;
        }
        
        private void button_Checkout_Click(object sender, EventArgs e)
        {
            // TODO: Thêm chức năng chọn khách hàng thường hay hội viên sau đó thay Null = mã hội viên khi thực hiện thanh toán
            // Get table id
            string tableID = listView_Bill.Tag.ToString();

            if (tableID.Equals(""))
            {
                MessageBox.Show("Bạn cần chọn một bàn ăn trước khi thanh toán !!!", "Thông báo", MessageBoxButtons.OK);
                return;               
            }

            // Lấy hóa đơn theo mã bàn
            string billID = BillDAO.GetInstance().GetBillIDByTableID(tableID);

            // Nếu tồn tại hóa đơn thì cập nhật trạng thái hóa đơn sang đã thanh toán
            if (billID.Equals("-1"))
            {
                MessageBox.Show("Bàn ăn này hiện không có hóa đơn hoặc hóa đơn đã thanh toán !!!", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn thanh toán bàn " + tableID + " ?", "Thanh toán", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                // TODO: Thêm chức năng kiểm tra khách hàng thường hay hội viên
                string memberPhoneNumber = textBox_MemberPhoneNumber.Text;

                double chietKhau = 0;
                double tongTien = BillDAO.GetInstance().GetTotalBillPrice(billID);

                // Nếu có mã hội viên thì check ngược lại không có thì chiết khấu là 0
                if (!memberPhoneNumber.Equals(""))
                {
                    if (!MemberDAO.GetInstance().CheckExistsMemberByPhoneNumber(memberPhoneNumber))
                    {
                        MessageBox.Show("Mã hội viên không tồn tại vui lòng kiểm tra lại !!!", "Thông báo", MessageBoxButtons.OK);
                        return;
                    }

                    // Nếu mã hội viên tồn tại thì tính chiết khấu
                    double totalPaidByMember = MemberDAO.GetInstance().GetTotalPaidByPhoneNumber(memberPhoneNumber);
                    chietKhau = caculateDiscount(tongTien, totalPaidByMember);
                }

                string employeeID = (this.Tag as NhanVien).MaNhanVien;

                if (BillDAO.GetInstance().Payment(billID, tongTien, chietKhau, memberPhoneNumber, employeeID))
                {
                    MessageBox.Show("Thanh toán thành công !!!", "Thông báo", MessageBoxButtons.OK);
                    textBox_MemberPhoneNumber.Text = "";
                    // Cập nhật số tiền thanh toán cho hội viên
                    ShowBillInformation(tableID);

                    if (TableDAO.GetInstance().SetTableState(tableID, "Trống"))
                    {
                        SettingTableState(tableID);
                    }
                }
                else
                {
                    MessageBox.Show("Thanh toán không thành công !!!", "Thông báo", MessageBoxButtons.OK);
                }
            }
        }
   
        private void listView_Bill_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView_Bill.SelectedItems.Count == 0)
                return;

            button_DeleteFood.Tag = this.listView_Bill.SelectedItems[0].Text;
        }

        private void button_DeleteFood_Click(object sender, EventArgs e)
        {
            // Get table id
            string tableID = listView_Bill.Tag.ToString();

            if (tableID.Equals(""))
            {
                MessageBox.Show("Bạn cần chọn một bàn ăn trước khi xóa món !!!", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            // Lấy hóa đơn theo mã bàn
            string billID = BillDAO.GetInstance().GetBillIDByTableID(tableID);

            // if that table does not exist an bill then create it
            if (billID.Equals("-1"))
            {
                MessageBox.Show("Bàn ăn chưa có hóa đơn không thể xóa món !!!", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            string foodID = button_DeleteFood.Tag.ToString();
            if (foodID.Equals(""))
            {
                MessageBox.Show("Cần chọn món ăn cần xóa !!!", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            // Lấy mã phiếu gọi món theo mã hóa đơn
            string existDetailOrderID = BillDAO.GetInstance().GetOrderIDByBillID(billID);

            if (existDetailOrderID.Equals("-1"))
            {
                MessageBox.Show("Không tồn tại phiếu gọi món !!!", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            // Kiểm tra món ăn có tồn tại trong phiếu gọi món vừa tìm được chưa ?
            if (!DetailOrderDAO.GetInstance().CheckExistFoodInDetailOrderByOrderID(foodID, existDetailOrderID))
            {
                MessageBox.Show("Món ăn không tồn tại trong phiếu gọi món !!!", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            if (!DetailOrderDAO.GetInstance().DeleteFoodInDetailOrder(foodID, existDetailOrderID))
            {
                MessageBox.Show("Xóa món ăn không thành công !!!", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            MessageBox.Show("Xóa món ăn thành công !!!", "Thông báo", MessageBoxButtons.OK);

            if (!DetailOrderDAO.GetInstance().CheckExistDetailOrderByOrderID(existDetailOrderID))
            {
                if (!BillDAO.GetInstance().DeleteBillByOrderID(existDetailOrderID))
                {
                    MessageBox.Show("Xóa hóa đơn không thành công !!!", "Thông báo", MessageBoxButtons.OK);
                    return;                   
                }

                // Chuyển trạng thái bàn sang Đang phục vụ và cập nhật trên listview                    
                TableDAO.GetInstance().SetTableState(tableID, "Trống");
                SettingTableState(tableID);
                MessageBox.Show("Do hóa đơn không còn món ăn nào nên sẽ bị xóa !!!", "Thông báo", MessageBoxButtons.OK);
            }

            // Cập nhật lại list view hiện hóa đơn
            ShowBillInformation(tableID);
            return;
        }

        private void xemLịchĐặtBànToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FormBookTableMangement formBookTableMangement = new FormBookTableMangement((NhanVien) this.Tag);
            formBookTableMangement.ShowDialog();

            // Sau khi đặt lịch xong thì load lại table
            LoadTable();
        }

        private void quảnLýHộiViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMemberMangement formMemberMangement = new FormMemberMangement((NhanVien)this.Tag);
            formMemberMangement.ShowDialog();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUserProfile formUserProfile = new FormUserProfile((NhanVien)this.Tag);
            formUserProfile.ShowDialog();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAdminMangement formAdminMangement = new FormAdminMangement();
            formAdminMangement.Tag = this.Tag;
            formAdminMangement.ShowDialog();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        
    }
}
