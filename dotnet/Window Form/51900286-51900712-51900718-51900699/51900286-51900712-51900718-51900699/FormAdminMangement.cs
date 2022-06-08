using RestaurentDAO;
using RestaurentDTO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Linq;
using System.Globalization;

namespace _51900286_51900712_51900718_51900699
{
    public partial class FormAdminMangement : Form
    {
        public FormAdminMangement()
        {
            InitializeComponent();
            button_AddEmployee.Enabled = false;
            button_UpdateEmployee.Enabled = false;
            button_DeleteEmployee.Enabled = false;
            button_AddFoodGroup.Enabled = false;
            button_UpdateFoodGroup.Enabled = false;
            button_DeleteFoodGroup.Enabled = false;
            ClearMaterials();

            ShowMaterialList();
            ShowTableList();

            dataGridView_EmployeeList.DataSource = EmployeeDAO.GetInstance().GetEmployeeList();
            dataGridView_FoodGroup.DataSource = FoodGroupDAO.GetInstance().GetFoodGroupList();           
        }

        // FORM LOAD
        private void FormAdminMangement_Load(object sender, EventArgs e)
        {
            string role = (this.Tag as NhanVien).ChucVu;

            if (role == "Quản Lý Nhà Hàng")
            {
                return;
            }

            // Ẩn các chức năng -> phân quyền
            tabControl_Admin.TabPages.Remove(tabControl_Admin.TabPages["tabPage_Menu"]);
            tabControl_Admin.TabPages.Remove(tabControl_Admin.TabPages["tabPage_Employee"]);
            tabControl_Admin.TabPages.Remove(tabControl_Admin.TabPages["tabPage_Table"]);
            tabControl_Admin.TabPages.Remove(tabControl_Admin.TabPages["tabPage_FoodGroup"]);

            if (role != "Thu Ngân")
            {
                tabControl_Admin.TabPages.Remove(tabControl_Admin.TabPages["tabPage_Bill"]);
            }
            if (role != "Quản Lý Kho")
            {
                tabControl_Admin.TabPages.Remove(tabControl_Admin.TabPages["tabPage_Materials"]);
                tabControl_Admin.TabPages.Remove(tabControl_Admin.TabPages["tabPage_Import"]);
                tabControl_Admin.TabPages.Remove(tabControl_Admin.TabPages["tabPage_Supplier"]);
            }
        }

        private void FormAdminMangement_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormTableManager form = Application.OpenForms.OfType<FormTableManager>().FirstOrDefault();
            form.ReLoad();
        }

        private void tabControl_Admin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl_Admin.SelectedTab == tabControl_Admin.TabPages["tabPage_Supplier"])
            {
                ShowNCC();
            }
            else if (tabControl_Admin.SelectedTab == tabControl_Admin.TabPages["tabPage_Menu"])
            {
                ShowMenu();
                LoadFoodGroup();
                ResetInputFoodMenu();
            } else if (tabControl_Admin.SelectedTab == tabControl_Admin.TabPages["tabPage_Materials"])
            {
                ShowMaterialList();
            }
            else if (tabControl_Admin.SelectedTab == tabControl_Admin.TabPages["tabPage_Import"])
            {
                tb_NVNhap.Text = (this.Tag as NhanVien).MaNhanVien;
                LoadSupplier();
                ShowMaterialImport();
            }
        }
      
        #region FUNCTION THỐNG KÊ DOANH THU
        private void CalculateTotal(List<HoaDon> bills)
        {
            double total = 0;
            dataListView_Bill.Items.Clear();
            foreach (var bill in bills)
            {
                ListViewItem listViewItem = new ListViewItem(bill.MaHoaDon.ToString());

                double price = bill.TongTien - bill.ChietKhau;
                if (bill.TinhTrang == 1)
                {
                    listViewItem.SubItems.Add("Đã thanh toán");
                    total += price;
                }
                else
                {
                    listViewItem.SubItems.Add("Chưa thanh toán");
                }
                listViewItem.SubItems.Add(bill.ThoiGianThanhToan.ToString());
                listViewItem.SubItems.Add(bill.TongTien.ToString());
                listViewItem.SubItems.Add(bill.ChietKhau.ToString());

                listViewItem.SubItems.Add(price.ToString());
                dataListView_Bill.Items.Add(listViewItem);
            }

            lb_income.Text = total.ToString("c", new CultureInfo("vi-VN"));
        }

        // THỐNG KÊ DOANH THU - form control
        private void button_byYear_Click(object sender, EventArgs e)
        {
            List<HoaDon> bills = BillDAO.GetInstance().GetBillByYear(dateTimePicker_date.Value.Year);
            CalculateTotal(bills);
        }

        private void button_byMonth_Click(object sender, EventArgs e)
        {
            List<HoaDon> bills = BillDAO.GetInstance().GetBillByMonth(dateTimePicker_date.Value.Month, dateTimePicker_date.Value.Year);
            CalculateTotal(bills);
        }

        #endregion

        #region FUNCTION QUẢN LÝ THỰC ĐƠN

        private void ShowMenu(List<MonAn> monAns = null)
        {
            List<MonAn> food = monAns;
            if ( food == null)
            {
                food = FoodDAO.GetInstance().GetAllFood();
            }

            listView_food.Items.Clear();

            foreach (var monAn in food)
            {
                ListViewItem listViewItem = new ListViewItem(monAn.MaMonAn);
                listViewItem.Tag = monAn;
                listViewItem.SubItems.Add(monAn.TenMonAn);
                listViewItem.SubItems.Add(monAn.MaNhom);
                listViewItem.SubItems.Add(monAn.DonGia.ToString());

                listView_food.Items.Add(listViewItem);
            }
        }

        private void LoadFood(MonAn monAn)
        {
            // Load thông tin món ăn
            textBox_FoodID.Text = monAn.MaMonAn;
            textBox_FoodName.Text = monAn.TenMonAn;
            numericUpDown_FoodPrice.Value = (int)monAn.DonGia;
            textBox_FoodUnit.Text = monAn.DonVi;
            textBox_Desc.Text = monAn.MoTaNgan;
            textBox_FoodDescription.Text = monAn.MoTa;
            foreach (NhomMonAn group in comboBox_FoodGroup.Items)
            {
                if (group.MaNhom.Equals(monAn.MaNhom))
                {
                    comboBox_FoodGroup.SelectedItem = group;
                    break;
                }
            }

            // load hình ảnh món ăn
            LoadImage(monAn);
        }

        private void LoadImage(MonAn monAn)
        {
            try
            {
                WebRequest req = WebRequest.Create("https://localhost:7219/images/food-images/" + monAn.HinhAnh);

                var res = req.GetResponse();
                var imgStream = res.GetResponseStream();

                foodImage.Image = Image.FromStream(imgStream, false);
                lb_hinhAnh.Text = monAn.HinhAnh;

                res.Close();
                imgStream.Close();
            }
            catch (ArgumentException)
            {
                foodImage.Image = Image.FromFile(Application.StartupPath + "/Image/stolen.jpg");
                lb_hinhAnh.Text = monAn.HinhAnh;
            }
        }

        private async Task<bool> UploadImage(string filename)
        {
            var client = new HttpClient();

            var content = new MultipartFormDataContent();
            FileStream fileContent = File.OpenRead(filename);

            content.Add(new StreamContent(fileContent), "file", openFileDialog.SafeFileName);

            var response = await client.PostAsync("https://localhost:7219/ComNha/api/upload-image", content);

            var responseString = response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseString.Result);
            
            if( !(bool)json["status"] )
            {
                MessageBox.Show("" + json["message"], "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private void LoadFoodGroup()
        {
            List<NhomMonAn> nhomMonAns = FoodGroupDAO.GetInstance().GetFoodGroupList();
            comboBox_FoodGroup.DataSource = nhomMonAns;
            comboBox_FoodGroup.DisplayMember = "TenNhom";
        }

        private void ResetInputFoodMenu()
        {
            listView_food.SelectedIndices.Clear();

            foodImage.Image = null;
            lb_hinhAnh.Text = "";
            textBox_FoodID.Text = "";
            textBox_FoodName.Text = "";
            numericUpDown_FoodPrice.Value = 0;
            textBox_FoodUnit.Text = "";
            textBox_Desc.Text = "";
            textBox_FoodDescription.Text = "";

            btn_Save.Tag = null;
            btn_Save.Enabled = false;
            button_DeleteFood.Enabled = false;
        }

        private List<MonAn> FilterFood(string filter)
        {
            var list = new List<MonAn>();

            foreach (ListViewItem food in listView_food.Items)
            {
                if( food.ToString().Contains(filter))
                {
                    list.Add(food.Tag as MonAn);
                    continue;
                }
                foreach (var subFood in food.SubItems)
                {
                    if(subFood.ToString().Contains(filter))
                    {
                        list.Add(food.Tag as MonAn);
                        continue;
                    }
                }
            }

            if(list.Count > 0 && list.Count!=listView_food.Items.Count)
            {
                ShowMenu(list);
            }
            if(list.Count==0)
            {
                listView_food.Items.Clear();
            }
            
            return list;
        }

        // QUẢN LÝ THỰC ĐƠN - form control
        private void listView_food_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_food.SelectedItems.Count > 0)
            {
                button_AddFood.Enabled = true;
                button_DeleteFood.Enabled = true;
                btn_Save.Text = "Lưu";
                btn_Save.Enabled = true;

                MonAn monAn = (MonAn)listView_food.SelectedItems[0].Tag;
                LoadFood(monAn);

                // Gắn tag button save
                btn_Save.Tag = monAn;
            }
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                lb_hinhAnh.Text = openFileDialog.SafeFileName;
                foodImage.Image = Image.FromFile(openFileDialog.FileName);
            }
            else
            {
                // Hủy chọn file -> load lại file hình cũ trước đó nếu đang sửa món ăn
                if (btn_Save.Tag != null)
                {
                    openFileDialog.FileName = "";
                    var monAn = (MonAn)btn_Save.Tag;
                    lb_hinhAnh.Text = monAn.HinhAnh;
                    LoadImage(monAn);
                }
            }
        }

        private void button_AddFood_Click(object sender, EventArgs e)
        {
            ResetInputFoodMenu();
            button_AddFood.Enabled = false;
            btn_Save.Enabled = true;
            btn_Save.Text = "Thêm";
        }

        private void button_DeleteFood_Click(object sender, EventArgs e)
        {
            var id = (btn_Save.Tag as MonAn).MaMonAn;
            var result = FoodDAO.GetInstance().DeleteFood(id);

            if (!result)
            {
                MessageBox.Show("Xóa món ăn thất bại", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            MessageBox.Show("Xóa món ăn thành công", "Thông báo", MessageBoxButtons.OK);

            // Reload menu
            ShowMenu();
            ResetInputFoodMenu();
        }

        private async void btn_Save_ClickAsync(object sender, EventArgs e)
        {
            if (lb_hinhAnh.Text == "")
            {
                MessageBox.Show("Vui lòng chọn hình ảnh", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            if (openFileDialog.FileName != "")
            {
                var res = await UploadImage(openFileDialog.FileName);
                // Upload ảnh vô db bị lỗi
                if (!res)
                {
                    return;
                }
            }
            // Upload thành công lên db

            var result = false;
            string action = "Thêm";
            // Insert db
            if (btn_Save.Text.Equals("Lưu"))
            {
                // Sửa món ăn
                action = "Sửa";
                result = FoodDAO.GetInstance().UpdateFood(
                    ((MonAn)btn_Save.Tag).MaMonAn,
                    textBox_FoodName.Text,
                    (double)numericUpDown_FoodPrice.Value,
                    textBox_FoodUnit.Text,
                    textBox_FoodDescription.Text,
                    textBox_Desc.Text,
                    lb_hinhAnh.Text,
                    (comboBox_FoodGroup.SelectedItem as NhomMonAn).MaNhom
                    );
            }
            else
            {
                // Thêm món ăn mới
                result = FoodDAO.GetInstance().AddFood(
                    textBox_FoodName.Text,
                    (double)numericUpDown_FoodPrice.Value,
                    textBox_FoodUnit.Text,
                    textBox_FoodDescription.Text,
                    textBox_Desc.Text,
                    lb_hinhAnh.Text,
                    (comboBox_FoodGroup.SelectedItem as NhomMonAn).MaNhom
                    );
            }
            if (!result)
            {
                MessageBox.Show(action + " món ăn thất bại!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            MessageBox.Show(action + " món ăn thành công!", "Thông báo", MessageBoxButtons.OK);
            openFileDialog.FileName = "";
            ResetInputFoodMenu();

            // Reload menu
            ShowMenu();
        }

        private void textBox_FoodFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if(textBox_FoodFilter.Text.Length<2)
            {
                if( textBox_FoodFilter.Tag != null )
                {
                    textBox_FoodFilter.Tag = null;
                    ShowMenu();
                }
                return;
            }
            textBox_FoodFilter.Tag = FilterFood(textBox_FoodFilter.Text);
        }

        #endregion

        #region FUNCTION QUẢN LÝ NHÂN VIÊN

        // QUẢN LÝ NHÂN VIÊN
        private void button_ShowEmployeeList_Click(object sender, EventArgs e)
        {
            dataGridView_EmployeeList.DataSource = EmployeeDAO.GetInstance().GetEmployeeList();
        }

        private void dataGridView_EmployeeList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index < 0 || index >= dataGridView_EmployeeList.RowCount)
            {
                return;
            }

            DataGridViewRow row = dataGridView_EmployeeList.Rows[index];
            string employeeID = Convert.ToString(row.Cells[0].Value);
            string employeeName = Convert.ToString(row.Cells[1].Value);
            string employeePhoneNumber = Convert.ToString(row.Cells[2].Value);
            double employeeSalary = Convert.ToDouble(row.Cells[3].Value);
            string employeeRole = Convert.ToString(row.Cells[4].Value);
            string employeeAddress = Convert.ToString(row.Cells[5].Value);
            string employeeEmail = Convert.ToString(row.Cells[6].Value);

            // update UI
            textBox_EmployeeID.Text = employeeID;
            textBox_EmployeeName.Text = employeeName;
            textBox_EmployeePhoneNumber.Text = employeePhoneNumber;
            numericUpDown_EmployeeSalary.Value = (decimal)employeeSalary;
            comboBox_EmployeeRole.Text = employeeRole;
            textBox_EmployeeAddress.Text = employeeAddress;
            textBox_EmployeeEmail.Text = employeeEmail;

            SetEditingEmployeeMode(true);
        }

        private void ClearEmployeeInput()
        {
            // update UI
            textBox_EmployeeID.Text = "";
            textBox_EmployeeName.Text = "";
            textBox_EmployeePhoneNumber.Text = "";
            numericUpDown_EmployeeSalary.Value = 0;
            comboBox_EmployeeRole.Text = "";
            textBox_EmployeeAddress.Text = "";
            textBox_EmployeeEmail.Text = "";
            SetEditingEmployeeMode(false);
        }

        private void SetEditingEmployeeMode(bool enable)
        {
            if (!enable)
            {
                dataGridView_EmployeeList.ClearSelection();
            }
            button_AddEmployee.Enabled = !enable;
            button_UpdateEmployee.Enabled = enable;
            button_DeleteEmployee.Enabled = enable;
            textBox_EmployeeEmail.ReadOnly = enable;
        }

        private void button_ClearEmployee_Click(object sender, EventArgs e)
        {
            ClearEmployeeInput();
        }

        private void button_DeleteEmployee_Click(object sender, EventArgs e)
        {
            string employeeID = textBox_EmployeeID.Text;
            
            if ( employeeID != "")
            {
                if (EmployeeDAO.GetInstance().DeleteEmployeeByID(employeeID))
                {
                    ClearEmployeeInput();
                    dataGridView_EmployeeList.DataSource = EmployeeDAO.GetInstance().GetEmployeeList();
                    MessageBox.Show("Xóa nhân viên thành công !!!", "Thông báo", MessageBoxButtons.OK);
                } else
                {
                    MessageBox.Show("Xóa nhân viên không thành công !!!", "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        private void button_UpdateEmployee_Click(object sender, EventArgs e)
        {
            // Validate user input
            if (!ValidateEmployeeInput())
            {
                return;
            }
            string employeeID = textBox_EmployeeID.Text;

            if (employeeID != "")
            {
                string employeeName = textBox_EmployeeName.Text;
                string employeePhoneNumber = textBox_EmployeePhoneNumber.Text;
                double employeeSalary = (double)numericUpDown_EmployeeSalary.Value;
                string employeeRole = comboBox_EmployeeRole.Text;
                string employeeAddress = textBox_EmployeeAddress.Text;
                string employeeEmail = textBox_EmployeeEmail.Text;

                NhanVien nhanVien = new NhanVien(employeeID, employeeName, employeePhoneNumber, employeeSalary, employeeRole , employeeAddress, employeeEmail);
                if (EmployeeDAO.GetInstance().UpdateEmployeeByID(nhanVien))
                {
                    ClearEmployeeInput();
                    dataGridView_EmployeeList.DataSource = EmployeeDAO.GetInstance().GetEmployeeList();
                    MessageBox.Show("Sửa thông tin nhân viên thành công !!!", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Sửa thông tin nhân viên không thành công !!!", "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        private void button_AddEmployee_Click(object sender, EventArgs e)
        {
            // Validate user input
            if (!ValidateEmployeeInput())
            {
                return;
            }

            string employeeName = textBox_EmployeeName.Text;
            string employeePhoneNumber = textBox_EmployeePhoneNumber.Text;
            double employeeSalary = (double)numericUpDown_EmployeeSalary.Value;
            string employeeRole = comboBox_EmployeeRole.Text;
            string employeeAddress = textBox_EmployeeAddress.Text;
            string employeeEmail = textBox_EmployeeEmail.Text;

            // Generate id
            string employeeID = EmployeeDAO.GetInstance().GetMaxEmployeeID();
            employeeID = DatabaseIDSupport.CreateNewMaxID(employeeID);

            // Insert
            NhanVien nhanVien = new NhanVien(employeeID, employeeName, employeePhoneNumber, employeeSalary, employeeRole, employeeAddress, employeeEmail);
            if (EmployeeDAO.GetInstance().AddEmployee(nhanVien))
            {
                ClearEmployeeInput();
                dataGridView_EmployeeList.DataSource = EmployeeDAO.GetInstance().GetEmployeeList();
                MessageBox.Show("Thêm nhân viên thành công !!!", "Thông báo", MessageBoxButtons.OK);

                if (AccountDAO.GetInstance().CreateEmployeeAccount(nhanVien))
                {
                    MessageBox.Show("Tài khoản nhân viên đã được tạo - Tên tài khoản = Email nhân viên - Mật khẩu = Số điện thoại nhân viên !!!", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Tạo tài khoản thất bại !!!", "Thông báo", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Thêm nhân viên không thành công !!!", "Thông báo", MessageBoxButtons.OK);
            }
        }

        private bool ValidateEmployeeInput()
        {
            string employeeName = textBox_EmployeeName.Text;
            string employeePhoneNumber = textBox_EmployeePhoneNumber.Text;
            double employeeSalary = (double)numericUpDown_EmployeeSalary.Value;
            string employeeAddress = textBox_EmployeeAddress.Text;
            string employeeEmail = textBox_EmployeeEmail.Text;

            if (employeeName == "" || employeeEmail == "" || employeeAddress == "" || employeeSalary.ToString() == "" || employeePhoneNumber == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK);
                return false;
            }

            try
            {
                MailAddress validEmail = new MailAddress(employeeEmail);
            }
            catch (FormatException)
            {
                MessageBox.Show("Email không hợp lệ.", "Thông báo", MessageBoxButtons.OK);
                return false;
            }

            if ( !textBox_EmployeeEmail.ReadOnly )
            {
                if (EmployeeDAO.GetInstance().CheckExistEmployeeEmail(employeeEmail))
                {
                    MessageBox.Show("Email nhân viên đã tồn tại.", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }
            }

            if (!long.TryParse(employeePhoneNumber, out long result))
            {
                MessageBox.Show("Số điện thoại không hợp lệ", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        #endregion

        #region FUNCTION QUẢN LÝ NHÓM MÓN ĂN

        private void ClearFoodGroupInput()
        {
            textBox_FoodGroupID.Text = "";
            textBox_FoodGroupName.Text = "";
            SetEditingFoodGroupMode(false);
        }

        private void SetEditingFoodGroupMode(bool enable)
        {
            if (!enable)
            {
                dataGridView_FoodGroup.ClearSelection();
            }
            button_AddFoodGroup.Enabled = !enable;
            button_UpdateFoodGroup.Enabled = enable;
            button_DeleteFoodGroup.Enabled = enable;
        }

        private void button_ShowFoodGroup_Click(object sender, EventArgs e)
        {
            dataGridView_FoodGroup.DataSource = FoodGroupDAO.GetInstance().GetFoodGroupList();
        }

        private void button_ClearFoodGroup_Click(object sender, EventArgs e)
        {
            ClearFoodGroupInput();
        }

        private void dataGridView_FoodGroup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            if (index < 0 || index >= dataGridView_FoodGroup.RowCount)
            {
                return;
            }

            DataGridViewRow row = dataGridView_FoodGroup.Rows[index];
            string foodGroupID = Convert.ToString(row.Cells[0].Value);
            string foodGroupName = Convert.ToString(row.Cells[1].Value);
            

            // update UI
            textBox_FoodGroupID.Text = foodGroupID;
            textBox_FoodGroupName.Text = foodGroupName;

            SetEditingFoodGroupMode(true);
        }

        private void button_AddFoodGroup_Click(object sender, EventArgs e)
        {
            // Validate user input
            string foodGroupName = textBox_FoodGroupName.Text;
            if (foodGroupName.Length<1)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            string foodGroupID = FoodGroupDAO.GetInstance().GetMaxFoodGroupID();
            foodGroupID = DatabaseIDSupport.CreateNewMaxID(foodGroupID);
            
            
            NhomMonAn nhomMonAn = new NhomMonAn(foodGroupID, foodGroupName);
            if (FoodGroupDAO.GetInstance().AddFoodGroup(nhomMonAn))
            {
                ClearFoodGroupInput();
                dataGridView_FoodGroup.DataSource = FoodGroupDAO.GetInstance().GetFoodGroupList();
                MessageBox.Show("Thêm nhóm món ăn thành công !!!", "Thông báo", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Thêm nhóm món ăn không thành công !!!", "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void button_DeleteFoodGroup_Click(object sender, EventArgs e)
        {
            string foodGroupID = textBox_FoodGroupID.Text;
            
            if (FoodGroupDAO.GetInstance().DeleteFoodGroup(foodGroupID))
            {
                ClearFoodGroupInput();
                dataGridView_FoodGroup.DataSource = FoodGroupDAO.GetInstance().GetFoodGroupList();
                MessageBox.Show("Xóa nhóm món ăn thành công !!!", "Thông báo", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Xóa nhóm món ăn không thành công !!!", "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void button_UpdateFoodGroup_Click(object sender, EventArgs e)
        {
            string foodGroupName = textBox_FoodGroupName.Text;
            // Validate user input
            if (foodGroupName.Length < 1)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            string foodGroupID = textBox_FoodGroupID.Text;

            NhomMonAn nhomMonAn = new NhomMonAn(foodGroupID, foodGroupName);
            if (FoodGroupDAO.GetInstance().UpdateFoodGroup(nhomMonAn))
            {
                ClearFoodGroupInput();
                dataGridView_FoodGroup.DataSource = FoodGroupDAO.GetInstance().GetFoodGroupList();
                MessageBox.Show("Sửa thông tin nhóm món ăn thành công !!!", "Thông báo", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Sửa thông tin nhóm món ăn không thành công !!!", "Thông báo", MessageBoxButtons.OK);
            }
        }

        #endregion

        #region FUNCTION QUẢN LÝ BÀN ĂN

        private void ClearTableInput()
        {
            textBox_TableID.Text = "";
            numericUpDown_TableSeat.Value = 0;
            comboBox_TableStatus.SelectedIndex = 0;
            textBox_TableDescription.Text = "";
            SetEditingTableMode(false);
        }

        private void SetEditingTableMode(bool enable)
        {
            button_AddTable.Enabled = !enable;
            button_UpdateTable.Enabled = enable;
            button_DeleteTable.Enabled = enable;
        }

        private void ShowTableList()
        {
            SetEditingTableMode(false);
            var tables = TableDAO.GetInstance().LoadTableList();

            dataGridView_TableList.DataSource = tables;
            dataGridView_TableList.Columns[0].HeaderText = "Mã Bàn ăn";
            dataGridView_TableList.Columns[1].HeaderText = "Tình trạng";
            dataGridView_TableList.Columns[2].HeaderText = "Sức chứa";
            dataGridView_TableList.Columns[3].HeaderText = "Ghi chú";
        }

        private bool ValidateTable()
        {
            if (comboBox_TableStatus.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            if (numericUpDown_TableSeat.Value == 0)
            {
                MessageBox.Show("Sức chứa bàn ăn phải lớn hơn 0.", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            
            return true;
        }

        // QUẢN LÝ BÀN ĂN - form control
        private void button_AddTable_Click(object sender, EventArgs e)
        {
            if (!ValidateTable())
            {
                return;
            }
            var status = comboBox_TableStatus.SelectedItem.ToString();
            var seat = (int)numericUpDown_TableSeat.Value;
            var desc = textBox_TableDescription.Text;

            var result = TableDAO.GetInstance().AddTable(seat, status, desc);
            if(!result)
            {
                MessageBox.Show("Thêm bàn ăn thất bại", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            MessageBox.Show("Thêm bàn ăn thành công", "Thông báo", MessageBoxButtons.OK);

            ClearTableInput();
            ShowTableList();
        }

        private void button_DeleteTable_Click(object sender, EventArgs e)
        {
            var id = textBox_TableID.Text;
            var result = TableDAO.GetInstance().DeleteTable(id);

            if (!result)
            {
                MessageBox.Show("Xóa bàn ăn thất bại", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            MessageBox.Show("Xóa bàn ăn thành công", "Thông báo", MessageBoxButtons.OK);

            ClearTableInput();
            ShowTableList();
        }

        private void button_UpdateTable_Click(object sender, EventArgs e)
        {
            if (!ValidateTable())
            {
                return;
            }
            var id = textBox_TableID.Text;
            var status = comboBox_TableStatus.SelectedItem.ToString();
            var seat = (int)numericUpDown_TableSeat.Value;
            var desc = textBox_TableDescription.Text;

            var result = TableDAO.GetInstance().UpdateTable(id, seat, status, desc);

            if (!result)
            {
                MessageBox.Show("Sửa bàn ăn thất bại", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            MessageBox.Show("Sửa bàn ăn thành công", "Thông báo", MessageBoxButtons.OK);

            ClearTableInput();
            ShowTableList();
        }

        private void button_ShowTableList_Click(object sender, EventArgs e)
        {
            ShowTableList();
        }

        private void btn_clearTable_Click(object sender, EventArgs e)
        {
            ClearTableInput();
        }

        private void dataGridView_TableList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            if (index < 0 || index >= dataGridView_TableList.RowCount)
            {
                return;
            }

            DataGridViewRow row = dataGridView_TableList.Rows[index];
            string tableID = Convert.ToString(row.Cells[0].Value);
            string status = Convert.ToString(row.Cells[1].Value);
            int capacity = (int)row.Cells[2].Value;
            string desc = Convert.ToString(row.Cells[3].Value);

            // update UI
            textBox_TableID.Text = tableID;
            numericUpDown_TableSeat.Value = capacity;
            comboBox_TableStatus.SelectedIndex = comboBox_TableStatus.FindStringExact(status);
            textBox_TableDescription.Text = desc;

            SetEditingTableMode(true);
        }

        #endregion

        #region FUNCTION QUẢN LÝ NHÀ CUNG CẤP

        private void ClearNCCInput()
        {
            tb_idNCC.Text = "";
            tb_nameNCC.Text = "";
            tb_descNCC.Text = "";
            tb_addressNCC.Text = "";
            tb_phoneNCC.Text = "";
            SetEditingNCCMode(false);
        }

        private void SetEditingNCCMode(bool enable)
        {
            btn_saveSupplier.Text = !enable ? "Thêm" : "Lưu";
            btn_deleteSupplier.Enabled = enable;

            btn_actionNCC.Text = !enable ? "Thêm" : "Sửa";
            btn_actionNCC.Text = btn_actionNCC.Text + " thông tin Nhà cung cấp";
        }

        private bool ValidateNCCInput()
        {
            string name = tb_nameNCC.Text;
            string address = tb_addressNCC.Text;
            string phone = tb_phoneNCC.Text;
            string desc = tb_descNCC.Text;

            if (name == "" || address == "" || phone == "" || desc == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            if (!long.TryParse(phone, out long result))
            {
                MessageBox.Show("Số điện thoại không hợp lệ.", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private void ShowNCC()
        {
            ClearNCCInput();
            dataGridView_SupplierList.DataSource = SupplierDAO.GetInstance().GetAllSuppliers();

            dataGridView_SupplierList.Columns[0].HeaderText = "Mã NCC";
            dataGridView_SupplierList.Columns[1].HeaderText = "Tên NCC";
            dataGridView_SupplierList.Columns[2].HeaderText = "Mô tả";
            dataGridView_SupplierList.Columns[3].HeaderText = "Địa chỉ";
            dataGridView_SupplierList.Columns[4].HeaderText = "Số điện thoại";
        }

        // QUẢN LÝ NHÀ CUNG CẤP - form control
        private void btn_saveSupplier_Click(object sender, EventArgs e)
        {
            // Validate before save
            if (!ValidateNCCInput())
            {
                return;
            }

            var action = btn_saveSupplier.Text;

            // get data input
            string name = tb_nameNCC.Text;
            string address = tb_addressNCC.Text;
            string phone = tb_phoneNCC.Text;
            string desc = tb_descNCC.Text;

            var result = false;

            if ( action=="Thêm")
            {
                // Thêm nhà cung cấp mới
                result = SupplierDAO.GetInstance().AddSupplier(name, desc, address, phone);
            } else
            {
                // Update nhà cung cấp
                string id = tb_idNCC.Text;
                result = SupplierDAO.GetInstance().UpdateSupplier(id, name, desc, address, phone);
            }

            if (!result)
            {
                MessageBox.Show(action + " Nhà cung cấp thất bại.", "Thông báo", MessageBoxButtons.OK);
            } else
            {
                MessageBox.Show(action + " Nhà cung cấp thành công.", "Thông báo", MessageBoxButtons.OK);
                ShowNCC();
                ClearNCCInput();
            }
        }

        private void btn_deleteSupplier_Click(object sender, EventArgs e)
        {
            var id = tb_idNCC.Text;
            var result = SupplierDAO.GetInstance().DeleteSupplier(id);

            if (!result)
            {
                MessageBox.Show("Xóa Nhà cung cấp thất bại.", "Thông báo", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Xóa Nhà cung cấp thành công.", "Thông báo", MessageBoxButtons.OK);
                ShowNCC();
                ClearNCCInput();
            }
        }

        private void btn_clearSupplier_Click(object sender, EventArgs e)
        {
            ClearNCCInput();
        }

        private void dataGridView_SupplierList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            if (index < 0 || index >= dataGridView_SupplierList.RowCount)
            {
                return;
            }

            DataGridViewRow row = dataGridView_SupplierList.Rows[index];
            string idNCC = Convert.ToString(row.Cells[0].Value);
            string nameNCC = Convert.ToString(row.Cells[1].Value);
            string desc = Convert.ToString(row.Cells[2].Value);
            string address = Convert.ToString(row.Cells[3].Value);
            string phone = Convert.ToString(row.Cells[4].Value);

            // update UI
            tb_idNCC.Text = idNCC;
            tb_nameNCC.Text = nameNCC;
            tb_addressNCC.Text = address;
            tb_phoneNCC.Text = phone;
            tb_descNCC.Text = desc;

            SetEditingNCCMode(true);
        }


        #endregion

        #region FUNCTION QUẢN LÝ NGUYÊN VẬT LIỆU
        private void button_ClearMaterials_Click(object sender, EventArgs e)
        {
            ClearMaterials();
        }

        private void ClearMaterials()
        {
            textBox_MaterialID.Text = "";
            textBox_MaterialName.Text = "";
            numericUpDown_MaterialPrice.Value = 0;
            textBox_MaterialUnit.Text = "";
            textBox_MaterialState.Text = "";
            numericUpDown_MaterialQuantityLeft.Value = 0;

            numericUpDown_MaterialQuantityLeft.Enabled = false;
            button_AddMaterials.Enabled = true;
            button_DeleteMaterials.Enabled = false;
            button_UpdateMaterials.Enabled = false;
        }

        private bool ValidateMaterialInput()
        {           
            string materialName = textBox_MaterialName.Text;
            double materialPrice = (double)numericUpDown_MaterialPrice.Value;
            string materialUnit = textBox_MaterialUnit.Text;
            string materialState = textBox_MaterialState.Text;

            if ( materialName.Equals("") || materialPrice == 0 || materialUnit.Equals("") || materialState.Equals(""))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin nguyên vật liệu !!!", "Thông báo", MessageBoxButtons.OK);
                return false;
            }

            return true;
        }

        private void button_AddMaterials_Click(object sender, EventArgs e)
        {
            if( !ValidateMaterialInput())
            {
                return;
            }

            string materialID = DatabaseIDSupport.CreateNewMaxID(MaterialDAO.GetInstance().GetMaxMaterialID());
            string materialName = textBox_MaterialName.Text;
            double materialPrice = (double)numericUpDown_MaterialPrice.Value;
            string materialUnit = textBox_MaterialUnit.Text;
            string materialState = textBox_MaterialState.Text;

            NguyenVatLieu nguyenVatLieu = new NguyenVatLieu(materialID, materialName, materialPrice, materialUnit, 0, materialState);

            if ( MaterialDAO.GetInstance().InsertNewMaterial(nguyenVatLieu))
            {
                MessageBox.Show("Thêm nguyên vật liệu thành công !!!", "Thông báo", MessageBoxButtons.OK);
                ClearMaterials();
                ShowMaterialList();
            } else
            {
                MessageBox.Show("Thêm nguyên vật liệu không thành công !!!", "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void button_DeleteMaterials_Click(object sender, EventArgs e)
        {
            string materialID = textBox_MaterialID.Text;

            if (MaterialDAO.GetInstance().DeleteMaterial(materialID))
            {
                MessageBox.Show("Xóa nguyên vật liệu thành công !!!", "Thông báo", MessageBoxButtons.OK);
                ClearMaterials();
                ShowMaterialList();
            }
            else
            {
                MessageBox.Show("Xóa nguyên vật liệu không thành công !!!", "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void button_UpdateMaterials_Click(object sender, EventArgs e)
        {
            string materialID = textBox_MaterialID.Text;
            string materialName = textBox_MaterialName.Text;
            double materialPrice = (double)numericUpDown_MaterialPrice.Value;
            string materialUnit = textBox_MaterialUnit.Text;
            string materialState = textBox_MaterialState.Text;
            int materialQuantityLeft = (int)numericUpDown_MaterialQuantityLeft.Value;

            NguyenVatLieu nguyenVatLieu = new NguyenVatLieu(materialID, materialName, materialPrice, materialUnit, materialQuantityLeft, materialState);

            if (MaterialDAO.GetInstance().UpdateMaterial(nguyenVatLieu))
            {
                MessageBox.Show("Sửa thông tin nguyên vật liệu thành công !!!", "Thông báo", MessageBoxButtons.OK);
                ClearMaterials();
                ShowMaterialList();
            }
            else
            {
                MessageBox.Show("Sửa thông tin nguyên vật liệu không thành công !!!", "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void button_ShowMaterialList_Click(object sender, EventArgs e)
        {
            ShowMaterialList();
        }

        private void ShowMaterialList()
        {
            List<NguyenVatLieu> materials = MaterialDAO.GetInstance().GetAllMaterials();

            listView_MaterialList.Items.Clear();

            foreach (NguyenVatLieu material in materials)
            {
                ListViewItem listViewItem = new ListViewItem(material.MaNguyenVatLieu);
                listViewItem.Tag = material;
                listViewItem.SubItems.Add(material.TenNguyenVatLieu);
                listViewItem.SubItems.Add(material.DonGia.ToString());
                listViewItem.SubItems.Add(material.DonVi);
                listViewItem.SubItems.Add(material.TinhTrang);
                listViewItem.SubItems.Add(material.SoLuongTonKho.ToString());

                listView_MaterialList.Items.Add(listViewItem);
            }
        }


        private void listView_MaterialList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_MaterialList.SelectedItems.Count > 0)
            {
                button_AddMaterials.Enabled = false;
                button_UpdateMaterials.Enabled = true;
                button_DeleteMaterials.Enabled = true;
                numericUpDown_MaterialQuantityLeft.Enabled = true;

                NguyenVatLieu nguyenVatLieu = (NguyenVatLieu)listView_MaterialList.SelectedItems[0].Tag;

                textBox_MaterialID.Text = nguyenVatLieu.MaNguyenVatLieu;
                textBox_MaterialName.Text = nguyenVatLieu.TenNguyenVatLieu;
                numericUpDown_MaterialPrice.Value = (decimal)nguyenVatLieu.DonGia;
                textBox_MaterialUnit.Text = nguyenVatLieu.DonVi;
                textBox_MaterialState.Text = nguyenVatLieu.TinhTrang;
                numericUpDown_MaterialQuantityLeft.Value = nguyenVatLieu.SoLuongTonKho;
            }
        }

        #endregion

        #region FUNCTION QUẢN LÝ PHIẾU NHẬP HÀNG

        private void ClearImportInput()
        {
            tb_idPhieuNhap.Text = "";
            comboBox_NCC.CreateControl();
            comboBox_NCC.SelectedIndex = 0;
            dateTimePicker_ngayNhap.Value = DateTime.Now;
            tb_noteImport.Text = "";
            tb_tongGia.Text = "";
            tb_nameNVL.Text = "";
            numeric_soLuong.Enabled = false;

            SetEditingImportMode(false);
        }

        private void SetEditingImportMode(bool enable)
        {
            btn_saveImport.Text = !enable ? "Thêm" : "Lưu";
            btn_deleteImport.Enabled = enable;

            btn_actionImport.Text = !enable ? "Thêm" : "Sửa thông tin";
            btn_actionImport.Text = btn_actionImport.Text + " Phiếu nhập hàng";

            ShowMaterialImport();
        }

        private void LoadSupplier()
        {
            comboBox_NCC.DataSource = SupplierDAO.GetInstance().GetAllSuppliers();
            comboBox_NCC.DisplayMember = "TenNCC";
        }

        private void ShowImportList()
        {
            btn_infoList.Text = "Danh sách Phiếu nhập hàng";

            dataGridView_Import.Tag = "Import";
            dataGridView_Import.Columns.Clear();
            dataGridView_Import.DataSource = ImportDAO.GetInstance().GetAllImports();

            dataGridView_Import.Columns[0].HeaderText = "Mã Phiếu nhập";
            dataGridView_Import.Columns[1].HeaderText = "Ngày nhập";
            dataGridView_Import.Columns[2].HeaderText = "Tổng tiền";
            dataGridView_Import.Columns[3].HeaderText = "Ghi chú";
            dataGridView_Import.Columns[4].HeaderText = "Mã NCC";
            dataGridView_Import.Columns[5].HeaderText = "Mã NV";
        }

        private void ShowMaterialImport()
        {
            btn_infoList.Text = "Danh sách Nguyên vật liệu";

            dataGridView_Import.Tag = "Material";
            tb_idPhieuNhap.Tag = null;

            dataGridView_Import.Columns.Clear();

            dataGridView_Import.DataSource = MaterialDAO.GetInstance().GetAllMaterials();

            dataGridView_Import.Columns[0].HeaderText = "Mã NVL";
            dataGridView_Import.Columns[1].HeaderText = "Tên";
            dataGridView_Import.Columns[2].HeaderText = "Giá";
            dataGridView_Import.Columns[3].HeaderText = "Đơn vị";
            dataGridView_Import.Columns[4].HeaderText = "Tình trạng";

            dataGridView_Import.Columns.RemoveAt(5);

            var soLuongCol = new DataGridViewColumn();
            soLuongCol.CellTemplate = new DataGridViewTextBoxCell();
            soLuongCol.HeaderText = "Số lượng";
            dataGridView_Import.Columns.Insert(5, soLuongCol);

            if (tb_idPhieuNhap.Text != "")
            {
                // Mode chỉnh sửa phiếu nhập
                var list = DetailImportDAO.GetInstance().GetAllDetailImportByID(tb_idPhieuNhap.Text);

                tb_idPhieuNhap.Tag = list;  // Gắn tag để tiện update trường hợp xóa vật liệu khỏi phiếu nhập

                foreach(var item in list)
                {
                    // Tìm và update số lượng nvl trong phiếu nhập
                    foreach(DataGridViewRow row in dataGridView_Import.Rows)
                    {
                        if(row.Cells[0].Value.ToString() == item.MaNVL)
                        {
                            row.Cells[5].Value = item.SoLuong;
                            break;
                        }
                    }
                }
            }
        }

        private void ShowTotalPrice()
        {
            double total = 0;
            foreach (DataGridViewRow row in dataGridView_Import.Rows)
            {
                if (row.Cells[5].Value != null && Convert.ToInt32(row.Cells[5].Value) > 0)
                {
                    total += Convert.ToDouble(row.Cells[2].Value) * Convert.ToInt32(row.Cells[5].Value);
                }
            }
            tb_tongGia.Text = total.ToString();
        }

        private bool ValidateImportInput()
        {
            if (tb_tongGia.Text == "0") {
                MessageBox.Show("Vui lòng thêm số lượng Nguyên liệu cần nhập!", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        // QUẢN LÝ PHIẾU NHẬP HÀNG - FORM CONTROL
        private void dataGridView_Import_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            if (index < 0 || index >= dataGridView_Import.RowCount)
            {
                return;
            }
            DataGridViewRow row = dataGridView_Import.Rows[index];

            if (dataGridView_Import.Tag.ToString() == "Material")
            {
                // Thêm số lượng nguyên vật liệu
                numeric_soLuong.Tag = row.Cells[5];
                tb_nameNVL.Text = row.Cells[1].Value.ToString();
                
                if ( row.Cells[5].Value != null)
                {
                    numeric_soLuong.Value = Convert.ToInt32(row.Cells[5].Value);
                } 
                else
                {
                    numeric_soLuong.Value = 0;
                }
                numeric_soLuong.Enabled = true;
                return;
            }

            string importID = Convert.ToString(row.Cells[0].Value);
            DateTime date = Convert.ToDateTime(row.Cells[1].Value);
            double price = Convert.ToDouble(row.Cells[2].Value);
            string note = Convert.ToString(row.Cells[3].Value);
            string supplierID = Convert.ToString(row.Cells[4].Value);

            // update UI
            tb_idPhieuNhap.Text = importID;
            dateTimePicker_ngayNhap.Value = date;
            tb_noteImport.Text = note;
            tb_tongGia.Text = price.ToString();
            foreach(NhaCungCap supplier in comboBox_NCC.Items)
            {
                if( supplier.MaNCC == supplierID)
                {
                    comboBox_NCC.SelectedItem = supplier;
                    break;
                }
            }

            SetEditingImportMode(true);
        }

        private void numeric_soLuong_ValueChanged(object sender, EventArgs e)
        {
            var cell = numeric_soLuong.Tag as DataGridViewCell;
            cell.Value = numeric_soLuong.Value;
            ShowTotalPrice();
        }

        private void btn_clearImport_Click(object sender, EventArgs e)
        {
            ClearImportInput();
        }

        private void btn_deleteImport_Click(object sender, EventArgs e)
        {
            string id = tb_idPhieuNhap.Text;
            var result = ImportDAO.GetInstance().DeleteImport(id);
            if (result)
            {
                MessageBox.Show("Xóa Phiếu nhập hàng thành công.", "Thông báo", MessageBoxButtons.OK);
                ClearImportInput();
                ShowImportList();
            }
            else
            {
                MessageBox.Show("Xóa Phiếu nhập hàng thất bại!", "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void btn_saveImport_Click(object sender, EventArgs e)
        {
            if (!ValidateImportInput())
            {
                return;
            }

            var mats = new Dictionary<string, int>();

            // Thông tin phiếu nhập
            string maPhieuNhap = tb_idPhieuNhap.Text;
            string maNCC = (comboBox_NCC.SelectedItem as NhaCungCap).MaNCC;
            DateTime ngayNhap = dateTimePicker_ngayNhap.Value;
            string maNV = tb_NVNhap.Text;
            string ghiChu = tb_noteImport.Text;
            double tongGia = Convert.ToDouble(tb_tongGia.Text);

            // Đang chọn 1 phiếu nhập để sửa
            if (tb_idPhieuNhap.Text != "")
            {
                var list = tb_idPhieuNhap.Tag as List<ChiTietPhieuNhap>;
                foreach (var nvl in list)
                {
                    mats.Add(nvl.MaNVL, nvl.SoLuong);
                }
            }

            foreach (DataGridViewRow row in dataGridView_Import.Rows)
            {
                string maNVL = row.Cells[0].Value.ToString();

                if (mats.ContainsKey(maNVL) || (row.Cells[5].Value != null && Convert.ToInt32(row.Cells[5].Value) > 0))
                {
                    // Thông tin chi tiết nguyên liệu nhập
                    int soLuong = Convert.ToInt32(row.Cells[5].Value);
                    mats[maNVL] = soLuong;
                }
            }

            if ( btn_saveImport.Text == "Thêm")
            {
                // Thêm phiếu nhập
                if (!ImportDAO.GetInstance().AddImport(ngayNhap, tongGia, ghiChu, maNCC, maNV))
                {
                    MessageBox.Show("Có lỗi xảy ra khi thêm Phiếu nhập hàng!", "Thông báo", MessageBoxButtons.OK);
                    return;
                }

                // Lấy id phiếu nhập mới tạo
                maPhieuNhap = ImportDAO.GetInstance().GetMaxImportID();

                // Thêm thông tin vật liệu
                foreach (var nvl in mats)
                {
                    if (!DetailImportDAO.GetInstance().AddDetailImport(nvl.Key, maPhieuNhap, nvl.Value))
                    {
                        MessageBox.Show("Có lỗi xảy ra khi thêm Chi tiết Phiếu nhập hàng!", "Thông báo", MessageBoxButtons.OK);
                        return;
                    }
                }
                MessageBox.Show("Thêm Phiếu nhập hàng thành công", "Thông báo", MessageBoxButtons.OK);
                ClearImportInput();
                ShowImportList();
            } 
            else
            {
                // Sửa thông tin phiếu nhập hàng
                if(!ImportDAO.GetInstance().UpdateImport(maPhieuNhap, ngayNhap, tongGia, ghiChu, maNCC, maNV))
                {
                    MessageBox.Show("Sửa thông tin Phiếu nhập hàng thất bại.", "Thông báo", MessageBoxButtons.OK);
                    return;
                }

                // Update thông tin chi tiết Phiếu nhập hàng
                // Thêm thông tin vật liệu
                var list = tb_idPhieuNhap.Tag as List<ChiTietPhieuNhap>;
                foreach (var nvl in mats)
                {
                    if(nvl.Value == 0)
                    {
                        // Trường hợp nvl đã tồn tại trong PN trước nhưng số lượng update = 0 -> Xóa NVL ra khỏi PN
                        if (!DetailImportDAO.GetInstance().DeleteDetailImport(nvl.Key, maPhieuNhap))
                        {
                            MessageBox.Show("Có lỗi xảy ra khi xóa Nguyên liệu khỏi Phiếu nhập hàng!", "Thông báo", MessageBoxButtons.OK);
                            return;
                        }
                        continue;
                    }

                    // Trường hợp kiểm tra xem nvl đã có trong PN chưa
                    // Có -> Update 
                    bool found = false;
                    foreach(var mat in list)
                    {
                        if(mat.MaNVL == nvl.Key)
                        {
                            if (!DetailImportDAO.GetInstance().UpdateDetailImport(mat.MaNVL, maPhieuNhap, nvl.Value))
                            {
                                MessageBox.Show("Có lỗi xảy ra khi cập nhật Nguyên liệu khỏi Phiếu nhập hàng!", "Thông báo", MessageBoxButtons.OK);
                                return;
                            }
                            found = true;
                            break;
                        }
                    }
                    // Chưa -> Insert mới
                    if (!found && !DetailImportDAO.GetInstance().AddDetailImport(nvl.Key, maPhieuNhap, nvl.Value))
                    {
                        MessageBox.Show("Có lỗi xảy ra khi sửa Thông tin Chi tiết Phiếu nhập hàng!", "Thông báo", MessageBoxButtons.OK);
                        return;
                    }
                }
                MessageBox.Show("Sửa Phiếu nhập hàng thành công", "Thông báo", MessageBoxButtons.OK);
                ClearImportInput();
                ShowImportList();
            }
        }

        private void btn_viewImport_Click(object sender, EventArgs e)
        {
            ShowImportList();
        }

        #endregion
    }
}
