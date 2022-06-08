
namespace _51900286_51900712_51900718_51900699
{
    partial class FormMemberMangement
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
            this.panel21 = new System.Windows.Forms.Panel();
            this.listView_MemberList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox_MemberBenefit = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel39 = new System.Windows.Forms.Panel();
            this.textBox_MemberAddress = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBox_MemberName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel33 = new System.Windows.Forms.Panel();
            this.textBox_MemberEmail = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.panel40 = new System.Windows.Forms.Panel();
            this.textBox_MemberPhoneNumber = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.button_AddMember = new System.Windows.Forms.Button();
            this.button_UpdateMember = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button_Clear = new System.Windows.Forms.Button();
            this.button_Exit = new System.Windows.Forms.Button();
            this.panel21.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel39.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel33.SuspendLayout();
            this.panel40.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel21
            // 
            this.panel21.Controls.Add(this.listView_MemberList);
            this.panel21.Location = new System.Drawing.Point(12, 36);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(776, 203);
            this.panel21.TabIndex = 11;
            // 
            // listView_MemberList
            // 
            this.listView_MemberList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listView_MemberList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView_MemberList.FullRowSelect = true;
            this.listView_MemberList.HideSelection = false;
            this.listView_MemberList.Location = new System.Drawing.Point(4, 3);
            this.listView_MemberList.Name = "listView_MemberList";
            this.listView_MemberList.Size = new System.Drawing.Size(769, 195);
            this.listView_MemberList.TabIndex = 0;
            this.listView_MemberList.UseCompatibleStateImageBehavior = false;
            this.listView_MemberList.View = System.Windows.Forms.View.Details;
            this.listView_MemberList.SelectedIndexChanged += new System.EventHandler(this.listView_MemberList_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Tên hội viên";
            this.columnHeader1.Width = 110;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Số điện thoại";
            this.columnHeader2.Width = 116;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Tổng số tiền thanh toán";
            this.columnHeader3.Width = 186;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Email";
            this.columnHeader4.Width = 124;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Địa chỉ";
            this.columnHeader5.Width = 120;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Quyền lợi";
            this.columnHeader6.Width = 104;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.label1.Location = new System.Drawing.Point(262, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 24);
            this.label1.TabIndex = 12;
            this.label1.Text = "DANH SÁCH HỘI VIÊN";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel39);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel33);
            this.panel1.Controls.Add(this.panel40);
            this.panel1.Location = new System.Drawing.Point(12, 246);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(360, 299);
            this.panel1.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Bisque;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.textBox_MemberBenefit);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(4, 225);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5);
            this.panel2.Size = new System.Drawing.Size(350, 66);
            this.panel2.TabIndex = 5;
            // 
            // textBox_MemberBenefit
            // 
            this.textBox_MemberBenefit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_MemberBenefit.Location = new System.Drawing.Point(126, 10);
            this.textBox_MemberBenefit.Multiline = true;
            this.textBox_MemberBenefit.Name = "textBox_MemberBenefit";
            this.textBox_MemberBenefit.Size = new System.Drawing.Size(212, 44);
            this.textBox_MemberBenefit.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkCyan;
            this.label3.Location = new System.Drawing.Point(8, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Quyền lợi:";
            // 
            // panel39
            // 
            this.panel39.BackColor = System.Drawing.Color.Bisque;
            this.panel39.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel39.Controls.Add(this.textBox_MemberAddress);
            this.panel39.Controls.Add(this.label11);
            this.panel39.Location = new System.Drawing.Point(4, 153);
            this.panel39.Name = "panel39";
            this.panel39.Padding = new System.Windows.Forms.Padding(5);
            this.panel39.Size = new System.Drawing.Size(350, 66);
            this.panel39.TabIndex = 4;
            // 
            // textBox_MemberAddress
            // 
            this.textBox_MemberAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_MemberAddress.Location = new System.Drawing.Point(126, 10);
            this.textBox_MemberAddress.Multiline = true;
            this.textBox_MemberAddress.Name = "textBox_MemberAddress";
            this.textBox_MemberAddress.Size = new System.Drawing.Size(212, 44);
            this.textBox_MemberAddress.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.DarkCyan;
            this.label11.Location = new System.Drawing.Point(8, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 20);
            this.label11.TabIndex = 0;
            this.label11.Text = "Địa chỉ:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Bisque;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.textBox_MemberName);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(4, 53);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5);
            this.panel3.Size = new System.Drawing.Size(350, 44);
            this.panel3.TabIndex = 2;
            // 
            // textBox_MemberName
            // 
            this.textBox_MemberName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox_MemberName.Location = new System.Drawing.Point(126, 5);
            this.textBox_MemberName.Multiline = true;
            this.textBox_MemberName.Name = "textBox_MemberName";
            this.textBox_MemberName.Size = new System.Drawing.Size(212, 25);
            this.textBox_MemberName.TabIndex = 1;
            this.textBox_MemberName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkCyan;
            this.label2.Location = new System.Drawing.Point(6, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tên hội viên:";
            // 
            // panel33
            // 
            this.panel33.BackColor = System.Drawing.Color.Bisque;
            this.panel33.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel33.Controls.Add(this.textBox_MemberEmail);
            this.panel33.Controls.Add(this.label20);
            this.panel33.Location = new System.Drawing.Point(4, 103);
            this.panel33.Name = "panel33";
            this.panel33.Padding = new System.Windows.Forms.Padding(5);
            this.panel33.Size = new System.Drawing.Size(350, 44);
            this.panel33.TabIndex = 3;
            // 
            // textBox_MemberEmail
            // 
            this.textBox_MemberEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_MemberEmail.Location = new System.Drawing.Point(126, 5);
            this.textBox_MemberEmail.Multiline = true;
            this.textBox_MemberEmail.Name = "textBox_MemberEmail";
            this.textBox_MemberEmail.Size = new System.Drawing.Size(212, 25);
            this.textBox_MemberEmail.TabIndex = 1;
            this.textBox_MemberEmail.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.DarkCyan;
            this.label20.Location = new System.Drawing.Point(9, 9);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(58, 20);
            this.label20.TabIndex = 0;
            this.label20.Text = "Email:";
            // 
            // panel40
            // 
            this.panel40.BackColor = System.Drawing.Color.Bisque;
            this.panel40.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel40.Controls.Add(this.textBox_MemberPhoneNumber);
            this.panel40.Controls.Add(this.label19);
            this.panel40.Location = new System.Drawing.Point(4, 3);
            this.panel40.Name = "panel40";
            this.panel40.Padding = new System.Windows.Forms.Padding(5);
            this.panel40.Size = new System.Drawing.Size(350, 44);
            this.panel40.TabIndex = 1;
            // 
            // textBox_MemberPhoneNumber
            // 
            this.textBox_MemberPhoneNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_MemberPhoneNumber.Location = new System.Drawing.Point(126, 5);
            this.textBox_MemberPhoneNumber.Multiline = true;
            this.textBox_MemberPhoneNumber.Name = "textBox_MemberPhoneNumber";
            this.textBox_MemberPhoneNumber.Size = new System.Drawing.Size(212, 25);
            this.textBox_MemberPhoneNumber.TabIndex = 1;
            this.textBox_MemberPhoneNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.DarkCyan;
            this.label19.Location = new System.Drawing.Point(8, 10);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(120, 20);
            this.label19.TabIndex = 0;
            this.label19.Text = "Số điện thoại:";
            // 
            // button_AddMember
            // 
            this.button_AddMember.BackColor = System.Drawing.Color.LightGreen;
            this.button_AddMember.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_AddMember.Location = new System.Drawing.Point(237, 16);
            this.button_AddMember.Name = "button_AddMember";
            this.button_AddMember.Size = new System.Drawing.Size(157, 69);
            this.button_AddMember.TabIndex = 6;
            this.button_AddMember.Text = "THÊM HỘI VIÊN";
            this.button_AddMember.UseVisualStyleBackColor = false;
            this.button_AddMember.Click += new System.EventHandler(this.button_AddMember_Click);
            // 
            // button_UpdateMember
            // 
            this.button_UpdateMember.BackColor = System.Drawing.Color.Gold;
            this.button_UpdateMember.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_UpdateMember.Location = new System.Drawing.Point(237, 114);
            this.button_UpdateMember.Name = "button_UpdateMember";
            this.button_UpdateMember.Size = new System.Drawing.Size(157, 70);
            this.button_UpdateMember.TabIndex = 7;
            this.button_UpdateMember.Text = "SỬA THÔNG TIN HỘI VIÊN";
            this.button_UpdateMember.UseVisualStyleBackColor = false;
            this.button_UpdateMember.Click += new System.EventHandler(this.button_UpdateMember_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel4.Controls.Add(this.button_Clear);
            this.panel4.Controls.Add(this.button_Exit);
            this.panel4.Controls.Add(this.button_AddMember);
            this.panel4.Controls.Add(this.button_UpdateMember);
            this.panel4.Location = new System.Drawing.Point(379, 246);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(409, 299);
            this.panel4.TabIndex = 16;
            // 
            // button_Clear
            // 
            this.button_Clear.BackColor = System.Drawing.Color.MediumAquamarine;
            this.button_Clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Clear.Location = new System.Drawing.Point(16, 115);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(157, 69);
            this.button_Clear.TabIndex = 9;
            this.button_Clear.Text = "LÀM MỚI";
            this.button_Clear.UseVisualStyleBackColor = false;
            this.button_Clear.Click += new System.EventHandler(this.button_Clear_Click);
            // 
            // button_Exit
            // 
            this.button_Exit.BackColor = System.Drawing.Color.LightCoral;
            this.button_Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Exit.Location = new System.Drawing.Point(237, 221);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(157, 70);
            this.button_Exit.TabIndex = 8;
            this.button_Exit.Text = "THOÁT";
            this.button_Exit.UseVisualStyleBackColor = false;
            this.button_Exit.Click += new System.EventHandler(this.button_Exit_Click);
            // 
            // FormMemberMangement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 553);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel21);
            this.Name = "FormMemberMangement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý hội viên";
            this.panel21.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel39.ResumeLayout(false);
            this.panel39.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel33.ResumeLayout(false);
            this.panel33.PerformLayout();
            this.panel40.ResumeLayout(false);
            this.panel40.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel21;
        private System.Windows.Forms.ListView listView_MemberList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel40;
        private System.Windows.Forms.TextBox textBox_MemberPhoneNumber;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBox_MemberName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel33;
        private System.Windows.Forms.TextBox textBox_MemberEmail;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel panel39;
        private System.Windows.Forms.TextBox textBox_MemberAddress;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox_MemberBenefit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_AddMember;
        private System.Windows.Forms.Button button_UpdateMember;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button_Exit;
        private System.Windows.Forms.Button button_Clear;
    }
}