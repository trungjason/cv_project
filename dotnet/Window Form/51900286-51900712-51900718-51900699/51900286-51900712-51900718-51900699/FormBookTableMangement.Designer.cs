
namespace _51900286_51900712_51900718_51900699
{
    partial class FormBookTableMangement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView_BookTableRequest = new System.Windows.Forms.DataGridView();
            this.MaLichHen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NhuCau = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoLuongKhach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThoiGian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayHen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TinhTrang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaNhanVien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaBanAn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoDienThoai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenKhachHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiaChi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel_Table = new System.Windows.Forms.FlowLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button_DeleteBook = new System.Windows.Forms.Button();
            this.textBox_TableID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_currentBookID = new System.Windows.Forms.TextBox();
            this.labelcurrentid = new System.Windows.Forms.Label();
            this.button_ShowBookList = new System.Windows.Forms.Button();
            this.button_BookTable = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_BookTableRequest)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView_BookTableRequest
            // 
            this.dataGridView_BookTableRequest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_BookTableRequest.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaLichHen,
            this.NhuCau,
            this.SoLuongKhach,
            this.ThoiGian,
            this.NgayHen,
            this.TinhTrang,
            this.MaNhanVien,
            this.MaBanAn,
            this.SoDienThoai,
            this.TenKhachHang,
            this.DiaChi,
            this.Email});
            this.dataGridView_BookTableRequest.Location = new System.Drawing.Point(367, 120);
            this.dataGridView_BookTableRequest.Name = "dataGridView_BookTableRequest";
            this.dataGridView_BookTableRequest.Size = new System.Drawing.Size(752, 338);
            this.dataGridView_BookTableRequest.TabIndex = 0;
            this.dataGridView_BookTableRequest.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_BookTableRequest_CellClick);
            // 
            // MaLichHen
            // 
            this.MaLichHen.DataPropertyName = "MaLichHen";
            this.MaLichHen.HeaderText = "Mã lịch hẹn";
            this.MaLichHen.Name = "MaLichHen";
            this.MaLichHen.ReadOnly = true;
            // 
            // NhuCau
            // 
            this.NhuCau.DataPropertyName = "NhuCau";
            this.NhuCau.HeaderText = "Nhu cầu";
            this.NhuCau.Name = "NhuCau";
            this.NhuCau.ReadOnly = true;
            // 
            // SoLuongKhach
            // 
            this.SoLuongKhach.DataPropertyName = "SoLuongKhach";
            this.SoLuongKhach.HeaderText = "Số lượng khách";
            this.SoLuongKhach.Name = "SoLuongKhach";
            this.SoLuongKhach.ReadOnly = true;
            // 
            // ThoiGian
            // 
            this.ThoiGian.DataPropertyName = "ThoiGian";
            this.ThoiGian.HeaderText = "Thời gian";
            this.ThoiGian.Name = "ThoiGian";
            this.ThoiGian.ReadOnly = true;
            // 
            // NgayHen
            // 
            this.NgayHen.DataPropertyName = "NgayHen";
            this.NgayHen.HeaderText = "Ngày hẹn";
            this.NgayHen.Name = "NgayHen";
            this.NgayHen.ReadOnly = true;
            // 
            // TinhTrang
            // 
            this.TinhTrang.DataPropertyName = "TinhTrang";
            this.TinhTrang.HeaderText = "Tình trạng";
            this.TinhTrang.Name = "TinhTrang";
            this.TinhTrang.ReadOnly = true;
            // 
            // MaNhanVien
            // 
            this.MaNhanVien.DataPropertyName = "MaNhanVien";
            this.MaNhanVien.HeaderText = "Mã nhân viên";
            this.MaNhanVien.Name = "MaNhanVien";
            this.MaNhanVien.ReadOnly = true;
            // 
            // MaBanAn
            // 
            this.MaBanAn.DataPropertyName = "MaBanAn";
            this.MaBanAn.HeaderText = "Mã bàn ăn";
            this.MaBanAn.Name = "MaBanAn";
            this.MaBanAn.ReadOnly = true;
            // 
            // SoDienThoai
            // 
            this.SoDienThoai.DataPropertyName = "SoDienThoai";
            this.SoDienThoai.HeaderText = "Số điện thoại";
            this.SoDienThoai.Name = "SoDienThoai";
            this.SoDienThoai.ReadOnly = true;
            // 
            // TenKhachHang
            // 
            this.TenKhachHang.DataPropertyName = "TenKhachHang";
            this.TenKhachHang.HeaderText = "Tên khách hàng";
            this.TenKhachHang.Name = "TenKhachHang";
            this.TenKhachHang.ReadOnly = true;
            // 
            // DiaChi
            // 
            this.DiaChi.DataPropertyName = "DiaChi";
            this.DiaChi.HeaderText = "Địa chỉ";
            this.DiaChi.Name = "DiaChi";
            this.DiaChi.ReadOnly = true;
            // 
            // Email
            // 
            this.Email.DataPropertyName = "Email";
            this.Email.HeaderText = "Email";
            this.Email.Name = "Email";
            this.Email.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flowLayoutPanel_Table);
            this.panel1.Location = new System.Drawing.Point(12, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(352, 415);
            this.panel1.TabIndex = 2;
            // 
            // flowLayoutPanel_Table
            // 
            this.flowLayoutPanel_Table.AutoScroll = true;
            this.flowLayoutPanel_Table.BackColor = System.Drawing.SystemColors.Info;
            this.flowLayoutPanel_Table.ForeColor = System.Drawing.Color.Black;
            this.flowLayoutPanel_Table.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel_Table.Name = "flowLayoutPanel_Table";
            this.flowLayoutPanel_Table.Size = new System.Drawing.Size(345, 409);
            this.flowLayoutPanel_Table.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkGreen;
            this.label6.Location = new System.Drawing.Point(35, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(299, 31);
            this.label6.TabIndex = 6;
            this.label6.Text = "DANH SÁCH BÀN ĂN";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button_DeleteBook);
            this.panel2.Controls.Add(this.textBox_TableID);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.textBox_currentBookID);
            this.panel2.Controls.Add(this.labelcurrentid);
            this.panel2.Controls.Add(this.button_ShowBookList);
            this.panel2.Controls.Add(this.button_BookTable);
            this.panel2.Location = new System.Drawing.Point(367, 43);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(752, 71);
            this.panel2.TabIndex = 7;
            // 
            // button_DeleteBook
            // 
            this.button_DeleteBook.BackColor = System.Drawing.Color.IndianRed;
            this.button_DeleteBook.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_DeleteBook.Location = new System.Drawing.Point(112, 3);
            this.button_DeleteBook.Name = "button_DeleteBook";
            this.button_DeleteBook.Size = new System.Drawing.Size(103, 65);
            this.button_DeleteBook.TabIndex = 6;
            this.button_DeleteBook.Text = "XÓA LỊCH HẸN";
            this.button_DeleteBook.UseVisualStyleBackColor = false;
            this.button_DeleteBook.Click += new System.EventHandler(this.button_DeleteBook_Click);
            // 
            // textBox_TableID
            // 
            this.textBox_TableID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_TableID.Location = new System.Drawing.Point(681, 33);
            this.textBox_TableID.Name = "textBox_TableID";
            this.textBox_TableID.ReadOnly = true;
            this.textBox_TableID.Size = new System.Drawing.Size(64, 26);
            this.textBox_TableID.TabIndex = 5;
            this.textBox_TableID.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(570, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "MÃ BÀN ĂN";
            // 
            // textBox_currentBookID
            // 
            this.textBox_currentBookID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_currentBookID.Location = new System.Drawing.Point(681, 6);
            this.textBox_currentBookID.Name = "textBox_currentBookID";
            this.textBox_currentBookID.ReadOnly = true;
            this.textBox_currentBookID.Size = new System.Drawing.Size(64, 26);
            this.textBox_currentBookID.TabIndex = 3;
            this.textBox_currentBookID.Text = "0";
            // 
            // labelcurrentid
            // 
            this.labelcurrentid.AutoSize = true;
            this.labelcurrentid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelcurrentid.Location = new System.Drawing.Point(552, 6);
            this.labelcurrentid.Name = "labelcurrentid";
            this.labelcurrentid.Size = new System.Drawing.Size(123, 20);
            this.labelcurrentid.TabIndex = 2;
            this.labelcurrentid.Text = "MÃ LỊCH HẸN";
            // 
            // button_ShowBookList
            // 
            this.button_ShowBookList.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button_ShowBookList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_ShowBookList.ForeColor = System.Drawing.Color.PapayaWhip;
            this.button_ShowBookList.Location = new System.Drawing.Point(221, 6);
            this.button_ShowBookList.Name = "button_ShowBookList";
            this.button_ShowBookList.Size = new System.Drawing.Size(255, 65);
            this.button_ShowBookList.TabIndex = 1;
            this.button_ShowBookList.Text = "XEM DANH SÁCH LỊCH HẸN";
            this.button_ShowBookList.UseVisualStyleBackColor = false;
            this.button_ShowBookList.Click += new System.EventHandler(this.button_ShowBookList_Click);
            // 
            // button_BookTable
            // 
            this.button_BookTable.BackColor = System.Drawing.Color.Turquoise;
            this.button_BookTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_BookTable.Location = new System.Drawing.Point(3, 3);
            this.button_BookTable.Name = "button_BookTable";
            this.button_BookTable.Size = new System.Drawing.Size(103, 65);
            this.button_BookTable.TabIndex = 0;
            this.button_BookTable.Text = "ĐẶT LỊCH HẸN";
            this.button_BookTable.UseVisualStyleBackColor = false;
            this.button_BookTable.Click += new System.EventHandler(this.button_BookTable_Click);
            // 
            // FormBookTableMangement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1131, 465);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dataGridView_BookTableRequest);
            this.Controls.Add(this.panel1);
            this.Name = "FormBookTableMangement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xem lịch đặt bàn";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_BookTableRequest)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_BookTableRequest;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_Table;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button_BookTable;
        private System.Windows.Forms.Button button_ShowBookList;
        private System.Windows.Forms.TextBox textBox_currentBookID;
        private System.Windows.Forms.Label labelcurrentid;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaLichHen;
        private System.Windows.Forms.DataGridViewTextBoxColumn NhuCau;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoLuongKhach;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThoiGian;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayHen;
        private System.Windows.Forms.DataGridViewTextBoxColumn TinhTrang;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaNhanVien;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaBanAn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoDienThoai;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenKhachHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiaChi;
        private System.Windows.Forms.DataGridViewTextBoxColumn Email;
        private System.Windows.Forms.TextBox textBox_TableID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_DeleteBook;
    }
}