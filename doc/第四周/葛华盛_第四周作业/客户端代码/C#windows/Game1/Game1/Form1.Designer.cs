namespace Game1
{
    partial class GameForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Username = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.LoginButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.RemindLable = new System.Windows.Forms.Label();
            this.AddItemText = new System.Windows.Forms.TextBox();
            this.AddItemCount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.AddItemButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.BagList = new System.Windows.Forms.ListView();
            this.RemoveItemText = new System.Windows.Forms.TextBox();
            this.RemoveItemCount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.RankList = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.RankListStart = new System.Windows.Forms.TextBox();
            this.RankListEnd = new System.Windows.Forms.TextBox();
            this.RankListButton = new System.Windows.Forms.Button();
            this.RankListHints = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.EquipList = new System.Windows.Forms.ListView();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.RankList)).BeginInit();
            this.SuspendLayout();
            // 
            // Username
            // 
            this.Username.AcceptsReturn = true;
            this.Username.Location = new System.Drawing.Point(98, 35);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(100, 21);
            this.Username.TabIndex = 0;
            this.Username.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(276, 35);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(100, 21);
            this.Password.TabIndex = 1;
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(477, 33);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(75, 23);
            this.LoginButton.TabIndex = 2;
            this.LoginButton.Text = "登录";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "用户名：";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(229, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "密码：";
            // 
            // checkBox
            // 
            this.checkBox.AutoSize = true;
            this.checkBox.Location = new System.Drawing.Point(399, 38);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(72, 16);
            this.checkBox.TabIndex = 5;
            this.checkBox.Text = "显示密码";
            this.checkBox.UseVisualStyleBackColor = true;
            this.checkBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // RemindLable
            // 
            this.RemindLable.AutoSize = true;
            this.RemindLable.Location = new System.Drawing.Point(609, 38);
            this.RemindLable.Name = "RemindLable";
            this.RemindLable.Size = new System.Drawing.Size(0, 12);
            this.RemindLable.TabIndex = 6;
            // 
            // AddItemText
            // 
            this.AddItemText.Location = new System.Drawing.Point(98, 71);
            this.AddItemText.Name = "AddItemText";
            this.AddItemText.Size = new System.Drawing.Size(100, 21);
            this.AddItemText.TabIndex = 7;
            // 
            // AddItemCount
            // 
            this.AddItemCount.Location = new System.Drawing.Point(276, 71);
            this.AddItemCount.Name = "AddItemCount";
            this.AddItemCount.Size = new System.Drawing.Size(100, 21);
            this.AddItemCount.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "增加物品ID：";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(229, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "数量：";
            // 
            // AddItemButton
            // 
            this.AddItemButton.Location = new System.Drawing.Point(428, 71);
            this.AddItemButton.Name = "AddItemButton";
            this.AddItemButton.Size = new System.Drawing.Size(75, 23);
            this.AddItemButton.TabIndex = 11;
            this.AddItemButton.Text = "添加物品";
            this.AddItemButton.UseVisualStyleBackColor = true;
            this.AddItemButton.Click += new System.EventHandler(this.AddItemButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "消耗物品ID：";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // BagList
            // 
            this.BagList.HideSelection = false;
            this.BagList.Location = new System.Drawing.Point(52, 226);
            this.BagList.Name = "BagList";
            this.BagList.Size = new System.Drawing.Size(218, 242);
            this.BagList.TabIndex = 13;
            this.BagList.UseCompatibleStateImageBehavior = false;
            this.BagList.SelectedIndexChanged += new System.EventHandler(this.BagList_SelectedIndexChanged);
            // 
            // RemoveItemText
            // 
            this.RemoveItemText.Location = new System.Drawing.Point(98, 109);
            this.RemoveItemText.Name = "RemoveItemText";
            this.RemoveItemText.Size = new System.Drawing.Size(100, 21);
            this.RemoveItemText.TabIndex = 14;
            // 
            // RemoveItemCount
            // 
            this.RemoveItemCount.Location = new System.Drawing.Point(276, 109);
            this.RemoveItemCount.Name = "RemoveItemCount";
            this.RemoveItemCount.Size = new System.Drawing.Size(100, 21);
            this.RemoveItemCount.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(229, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "数量：";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(428, 107);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "消耗物品";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // RankList
            // 
            this.RankList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RankList.Location = new System.Drawing.Point(595, 74);
            this.RankList.Name = "RankList";
            this.RankList.RowTemplate.Height = 23;
            this.RankList.Size = new System.Drawing.Size(370, 364);
            this.RankList.TabIndex = 18;
            this.RankList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RankList_CellContentClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 149);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 20;
            this.label8.Text = "排行榜起始：";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(205, 149);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 21;
            this.label9.Text = "排行榜尾部：";
            // 
            // RankListStart
            // 
            this.RankListStart.Location = new System.Drawing.Point(98, 146);
            this.RankListStart.Name = "RankListStart";
            this.RankListStart.Size = new System.Drawing.Size(100, 21);
            this.RankListStart.TabIndex = 22;
            // 
            // RankListEnd
            // 
            this.RankListEnd.Location = new System.Drawing.Point(276, 146);
            this.RankListEnd.Name = "RankListEnd";
            this.RankListEnd.Size = new System.Drawing.Size(100, 21);
            this.RankListEnd.TabIndex = 23;
            // 
            // RankListButton
            // 
            this.RankListButton.Location = new System.Drawing.Point(428, 143);
            this.RankListButton.Name = "RankListButton";
            this.RankListButton.Size = new System.Drawing.Size(75, 23);
            this.RankListButton.TabIndex = 24;
            this.RankListButton.Text = "排行榜";
            this.RankListButton.UseVisualStyleBackColor = true;
            this.RankListButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // RankListHints
            // 
            this.RankListHints.AutoSize = true;
            this.RankListHints.Location = new System.Drawing.Point(593, 59);
            this.RankListHints.Name = "RankListHints";
            this.RankListHints.Size = new System.Drawing.Size(0, 12);
            this.RankListHints.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(50, 188);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 12);
            this.label7.TabIndex = 26;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(162, 188);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(0, 12);
            this.label10.TabIndex = 27;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(274, 188);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(0, 12);
            this.label11.TabIndex = 28;
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // EquipList
            // 
            this.EquipList.HideSelection = false;
            this.EquipList.Location = new System.Drawing.Point(302, 226);
            this.EquipList.Name = "EquipList";
            this.EquipList.Size = new System.Drawing.Size(222, 242);
            this.EquipList.TabIndex = 29;
            this.EquipList.UseCompatibleStateImageBehavior = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(52, 208);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 12);
            this.label12.TabIndex = 30;
            this.label12.Text = "物品";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(300, 211);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 12);
            this.label13.TabIndex = 31;
            this.label13.Text = "装备";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1013, 488);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.EquipList);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.RankListHints);
            this.Controls.Add(this.RankListButton);
            this.Controls.Add(this.RankListEnd);
            this.Controls.Add(this.RankListStart);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.RankList);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.RemoveItemCount);
            this.Controls.Add(this.RemoveItemText);
            this.Controls.Add(this.BagList);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.AddItemButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.AddItemCount);
            this.Controls.Add(this.AddItemText);
            this.Controls.Add(this.RemindLable);
            this.Controls.Add(this.checkBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.Username);
            this.Name = "GameForm";
            this.Text = "葛华盛_数据落地";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RankList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox;
        private System.Windows.Forms.Label RemindLable;
        private System.Windows.Forms.TextBox AddItemText;
        private System.Windows.Forms.TextBox AddItemCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button AddItemButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListView BagList;
        private System.Windows.Forms.TextBox RemoveItemText;
        private System.Windows.Forms.TextBox RemoveItemCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView RankList;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox RankListStart;
        private System.Windows.Forms.TextBox RankListEnd;
        private System.Windows.Forms.Button RankListButton;
        private System.Windows.Forms.Label RankListHints;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListView EquipList;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
    }
}

