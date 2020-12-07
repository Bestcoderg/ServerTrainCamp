
namespace Game
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.txtboxName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPsw = new System.Windows.Forms.Label();
            this.txtBoxPws = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.chkBoxPws = new System.Windows.Forms.CheckBox();
            this.lblHints = new System.Windows.Forms.Label();
            this.txtBoxItemID = new System.Windows.Forms.TextBox();
            this.txtBoxItemCount = new System.Windows.Forms.TextBox();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.lblAddItem = new System.Windows.Forms.Label();
            this.lblAddItemID = new System.Windows.Forms.Label();
            this.lblItemCount = new System.Windows.Forms.Label();
            this.lblAddItemHints = new System.Windows.Forms.Label();
            this.lblUIName = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.lblSearchID = new System.Windows.Forms.Label();
            this.lblSearchName = new System.Windows.Forms.Label();
            this.txtBoxSearchID = new System.Windows.Forms.TextBox();
            this.txtBoxSearchName = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblSearchHints = new System.Windows.Forms.Label();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.listViewBagItem = new System.Windows.Forms.ListView();
            this.lblBag = new System.Windows.Forms.Label();
            this.dataGridViewSearch = new System.Windows.Forms.DataGridView();
            this.lblRank = new System.Windows.Forms.Label();
            this.txtBoxRankSearchstart = new System.Windows.Forms.TextBox();
            this.txtBoxRankSearchEnd = new System.Windows.Forms.TextBox();
            this.lblRankSearchEnd = new System.Windows.Forms.Label();
            this.btnRankSearch = new System.Windows.Forms.Button();
            this.lblRankSearchHints = new System.Windows.Forms.Label();
            this.picBoxFig = new System.Windows.Forms.PictureBox();
            this.txtBoxConsumeID = new System.Windows.Forms.TextBox();
            this.txtBoxConsumeCnt = new System.Windows.Forms.TextBox();
            this.lblConsumeHints = new System.Windows.Forms.Label();
            this.btnConsume = new System.Windows.Forms.Button();
            this.lblConsumhints = new System.Windows.Forms.Label();
            this.listViewEquipmentBag = new System.Windows.Forms.ListView();
            this.lblEquipmentBag = new System.Windows.Forms.Label();
            this.lblGold = new System.Windows.Forms.Label();
            this.lblSilver = new System.Windows.Forms.Label();
            this.lblDiamond = new System.Windows.Forms.Label();
            this.lblConsumeCnt = new System.Windows.Forms.Label();
            this.lblEquipment = new System.Windows.Forms.Label();
            this.txtBoxEquipmentID = new System.Windows.Forms.TextBox();
            this.txtBoxEquipmentCnt = new System.Windows.Forms.TextBox();
            this.lblequipmentCnt = new System.Windows.Forms.Label();
            this.btnequipment = new System.Windows.Forms.Button();
            this.lblexp = new System.Windows.Forms.Label();
            this.lablPlayerRank = new System.Windows.Forms.Label();
            this.lblUnequipment = new System.Windows.Forms.Label();
            this.txtBoxUnequipID = new System.Windows.Forms.TextBox();
            this.txtBoxUnequipCnt = new System.Windows.Forms.TextBox();
            this.lblUnequipCnt = new System.Windows.Forms.Label();
            this.btnUnequip = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxFig)).BeginInit();
            this.SuspendLayout();
            // 
            // txtboxName
            // 
            this.txtboxName.Location = new System.Drawing.Point(69, 9);
            this.txtboxName.Name = "txtboxName";
            this.txtboxName.Size = new System.Drawing.Size(118, 21);
            this.txtboxName.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 12);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(53, 12);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "用户名：";
            // 
            // lblPsw
            // 
            this.lblPsw.AutoSize = true;
            this.lblPsw.Location = new System.Drawing.Point(202, 12);
            this.lblPsw.Name = "lblPsw";
            this.lblPsw.Size = new System.Drawing.Size(41, 12);
            this.lblPsw.TabIndex = 2;
            this.lblPsw.Text = "密码：";
            // 
            // txtBoxPws
            // 
            this.txtBoxPws.Location = new System.Drawing.Point(240, 9);
            this.txtBoxPws.Name = "txtBoxPws";
            this.txtBoxPws.PasswordChar = '*';
            this.txtBoxPws.Size = new System.Drawing.Size(110, 21);
            this.txtBoxPws.TabIndex = 3;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(466, 7);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // chkBoxPws
            // 
            this.chkBoxPws.AutoSize = true;
            this.chkBoxPws.Location = new System.Drawing.Point(371, 12);
            this.chkBoxPws.Name = "chkBoxPws";
            this.chkBoxPws.Size = new System.Drawing.Size(72, 16);
            this.chkBoxPws.TabIndex = 5;
            this.chkBoxPws.Text = "显示密码";
            this.chkBoxPws.UseVisualStyleBackColor = true;
            this.chkBoxPws.CheckedChanged += new System.EventHandler(this.chkBoxPws_CheckedChanged);
            // 
            // lblHints
            // 
            this.lblHints.AutoSize = true;
            this.lblHints.Location = new System.Drawing.Point(558, 14);
            this.lblHints.Name = "lblHints";
            this.lblHints.Size = new System.Drawing.Size(0, 12);
            this.lblHints.TabIndex = 6;
            // 
            // txtBoxItemID
            // 
            this.txtBoxItemID.Location = new System.Drawing.Point(108, 82);
            this.txtBoxItemID.Name = "txtBoxItemID";
            this.txtBoxItemID.Size = new System.Drawing.Size(100, 21);
            this.txtBoxItemID.TabIndex = 7;
            // 
            // txtBoxItemCount
            // 
            this.txtBoxItemCount.Location = new System.Drawing.Point(257, 82);
            this.txtBoxItemCount.Name = "txtBoxItemCount";
            this.txtBoxItemCount.Size = new System.Drawing.Size(100, 21);
            this.txtBoxItemCount.TabIndex = 8;
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(417, 82);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(75, 23);
            this.btnAddItem.TabIndex = 9;
            this.btnAddItem.Text = "增减物品";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // lblAddItem
            // 
            this.lblAddItem.AutoSize = true;
            this.lblAddItem.Location = new System.Drawing.Point(13, 87);
            this.lblAddItem.Name = "lblAddItem";
            this.lblAddItem.Size = new System.Drawing.Size(53, 12);
            this.lblAddItem.TabIndex = 10;
            this.lblAddItem.Text = "增加物品";
            // 
            // lblAddItemID
            // 
            this.lblAddItemID.AutoSize = true;
            this.lblAddItemID.Location = new System.Drawing.Point(72, 87);
            this.lblAddItemID.Name = "lblAddItemID";
            this.lblAddItemID.Size = new System.Drawing.Size(23, 12);
            this.lblAddItemID.TabIndex = 11;
            this.lblAddItemID.Text = "ID:";
            // 
            // lblItemCount
            // 
            this.lblItemCount.AutoSize = true;
            this.lblItemCount.Location = new System.Drawing.Point(214, 87);
            this.lblItemCount.Name = "lblItemCount";
            this.lblItemCount.Size = new System.Drawing.Size(35, 12);
            this.lblItemCount.TabIndex = 12;
            this.lblItemCount.Text = "数量:";
            // 
            // lblAddItemHints
            // 
            this.lblAddItemHints.AutoSize = true;
            this.lblAddItemHints.Location = new System.Drawing.Point(511, 92);
            this.lblAddItemHints.Name = "lblAddItemHints";
            this.lblAddItemHints.Size = new System.Drawing.Size(0, 12);
            this.lblAddItemHints.TabIndex = 13;
            // 
            // lblUIName
            // 
            this.lblUIName.AutoSize = true;
            this.lblUIName.Location = new System.Drawing.Point(90, 49);
            this.lblUIName.Name = "lblUIName";
            this.lblUIName.Size = new System.Drawing.Size(0, 12);
            this.lblUIName.TabIndex = 14;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(23, 146);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(29, 12);
            this.lblSearch.TabIndex = 15;
            this.lblSearch.Text = "检索";
            // 
            // lblSearchID
            // 
            this.lblSearchID.AutoSize = true;
            this.lblSearchID.Location = new System.Drawing.Point(67, 146);
            this.lblSearchID.Name = "lblSearchID";
            this.lblSearchID.Size = new System.Drawing.Size(23, 12);
            this.lblSearchID.TabIndex = 16;
            this.lblSearchID.Text = "ID:";
            this.lblSearchID.Visible = false;
            // 
            // lblSearchName
            // 
            this.lblSearchName.AutoSize = true;
            this.lblSearchName.Location = new System.Drawing.Point(216, 146);
            this.lblSearchName.Name = "lblSearchName";
            this.lblSearchName.Size = new System.Drawing.Size(53, 12);
            this.lblSearchName.TabIndex = 17;
            this.lblSearchName.Text = "用户名：";
            // 
            // txtBoxSearchID
            // 
            this.txtBoxSearchID.Location = new System.Drawing.Point(108, 143);
            this.txtBoxSearchID.Name = "txtBoxSearchID";
            this.txtBoxSearchID.Size = new System.Drawing.Size(100, 21);
            this.txtBoxSearchID.TabIndex = 18;
            this.txtBoxSearchID.Visible = false;
            // 
            // txtBoxSearchName
            // 
            this.txtBoxSearchName.Location = new System.Drawing.Point(257, 143);
            this.txtBoxSearchName.Name = "txtBoxSearchName";
            this.txtBoxSearchName.Size = new System.Drawing.Size(100, 21);
            this.txtBoxSearchName.TabIndex = 19;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(417, 141);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 20;
            this.btnSearch.Text = "查找用户";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblSearchHints
            // 
            this.lblSearchHints.AutoSize = true;
            this.lblSearchHints.Location = new System.Drawing.Point(42, 205);
            this.lblSearchHints.Name = "lblSearchHints";
            this.lblSearchHints.Size = new System.Drawing.Size(0, 12);
            this.lblSearchHints.TabIndex = 21;
            // 
            // imgList
            // 
            this.imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imgList.ImageSize = new System.Drawing.Size(16, 16);
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listViewBagItem
            // 
            this.listViewBagItem.HideSelection = false;
            this.listViewBagItem.Location = new System.Drawing.Point(15, 293);
            this.listViewBagItem.Name = "listViewBagItem";
            this.listViewBagItem.Size = new System.Drawing.Size(292, 242);
            this.listViewBagItem.TabIndex = 22;
            this.listViewBagItem.UseCompatibleStateImageBehavior = false;
            // 
            // lblBag
            // 
            this.lblBag.AutoSize = true;
            this.lblBag.Location = new System.Drawing.Point(13, 265);
            this.lblBag.Name = "lblBag";
            this.lblBag.Size = new System.Drawing.Size(29, 12);
            this.lblBag.TabIndex = 23;
            this.lblBag.Text = "背包";
            // 
            // dataGridViewSearch
            // 
            this.dataGridViewSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSearch.Location = new System.Drawing.Point(108, 541);
            this.dataGridViewSearch.Name = "dataGridViewSearch";
            this.dataGridViewSearch.RowTemplate.Height = 23;
            this.dataGridViewSearch.Size = new System.Drawing.Size(468, 174);
            this.dataGridViewSearch.TabIndex = 24;
            this.dataGridViewSearch.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridViewSearch_RowPostPaint);
            this.dataGridViewSearch.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dataGridViewSearch_RowStateChanged);
            // 
            // lblRank
            // 
            this.lblRank.AutoSize = true;
            this.lblRank.Location = new System.Drawing.Point(1, 179);
            this.lblRank.Name = "lblRank";
            this.lblRank.Size = new System.Drawing.Size(95, 12);
            this.lblRank.TabIndex = 25;
            this.lblRank.Text = "排行榜查询:开始";
            // 
            // txtBoxRankSearchstart
            // 
            this.txtBoxRankSearchstart.Location = new System.Drawing.Point(108, 174);
            this.txtBoxRankSearchstart.Name = "txtBoxRankSearchstart";
            this.txtBoxRankSearchstart.Size = new System.Drawing.Size(100, 21);
            this.txtBoxRankSearchstart.TabIndex = 26;
            // 
            // txtBoxRankSearchEnd
            // 
            this.txtBoxRankSearchEnd.Location = new System.Drawing.Point(257, 170);
            this.txtBoxRankSearchEnd.Name = "txtBoxRankSearchEnd";
            this.txtBoxRankSearchEnd.Size = new System.Drawing.Size(100, 21);
            this.txtBoxRankSearchEnd.TabIndex = 27;
            // 
            // lblRankSearchEnd
            // 
            this.lblRankSearchEnd.AutoSize = true;
            this.lblRankSearchEnd.Location = new System.Drawing.Point(214, 175);
            this.lblRankSearchEnd.Name = "lblRankSearchEnd";
            this.lblRankSearchEnd.Size = new System.Drawing.Size(41, 12);
            this.lblRankSearchEnd.TabIndex = 28;
            this.lblRankSearchEnd.Text = "结束：";
            // 
            // btnRankSearch
            // 
            this.btnRankSearch.Location = new System.Drawing.Point(417, 170);
            this.btnRankSearch.Name = "btnRankSearch";
            this.btnRankSearch.Size = new System.Drawing.Size(75, 23);
            this.btnRankSearch.TabIndex = 29;
            this.btnRankSearch.Text = "排行榜";
            this.btnRankSearch.UseVisualStyleBackColor = true;
            this.btnRankSearch.Click += new System.EventHandler(this.btnRankSearch_Click);
            // 
            // lblRankSearchHints
            // 
            this.lblRankSearchHints.AutoSize = true;
            this.lblRankSearchHints.Location = new System.Drawing.Point(499, 174);
            this.lblRankSearchHints.Name = "lblRankSearchHints";
            this.lblRankSearchHints.Size = new System.Drawing.Size(0, 12);
            this.lblRankSearchHints.TabIndex = 30;
            // 
            // picBoxFig
            // 
            this.picBoxFig.Location = new System.Drawing.Point(4, 5);
            this.picBoxFig.Name = "picBoxFig";
            this.picBoxFig.Size = new System.Drawing.Size(82, 82);
            this.picBoxFig.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxFig.TabIndex = 31;
            this.picBoxFig.TabStop = false;
            this.picBoxFig.Visible = false;
            // 
            // txtBoxConsumeID
            // 
            this.txtBoxConsumeID.Location = new System.Drawing.Point(108, 113);
            this.txtBoxConsumeID.Name = "txtBoxConsumeID";
            this.txtBoxConsumeID.Size = new System.Drawing.Size(100, 21);
            this.txtBoxConsumeID.TabIndex = 32;
            // 
            // txtBoxConsumeCnt
            // 
            this.txtBoxConsumeCnt.Location = new System.Drawing.Point(257, 113);
            this.txtBoxConsumeCnt.Name = "txtBoxConsumeCnt";
            this.txtBoxConsumeCnt.Size = new System.Drawing.Size(100, 21);
            this.txtBoxConsumeCnt.TabIndex = 33;
            // 
            // lblConsumeHints
            // 
            this.lblConsumeHints.AutoSize = true;
            this.lblConsumeHints.Location = new System.Drawing.Point(13, 116);
            this.lblConsumeHints.Name = "lblConsumeHints";
            this.lblConsumeHints.Size = new System.Drawing.Size(77, 12);
            this.lblConsumeHints.TabIndex = 34;
            this.lblConsumeHints.Text = "消耗物品 ID:";
            // 
            // btnConsume
            // 
            this.btnConsume.Location = new System.Drawing.Point(417, 110);
            this.btnConsume.Name = "btnConsume";
            this.btnConsume.Size = new System.Drawing.Size(75, 23);
            this.btnConsume.TabIndex = 35;
            this.btnConsume.Text = "消耗物品";
            this.btnConsume.UseVisualStyleBackColor = true;
            this.btnConsume.Click += new System.EventHandler(this.btnConsume_Click);
            // 
            // lblConsumhints
            // 
            this.lblConsumhints.AutoSize = true;
            this.lblConsumhints.Location = new System.Drawing.Point(499, 121);
            this.lblConsumhints.Name = "lblConsumhints";
            this.lblConsumhints.Size = new System.Drawing.Size(0, 12);
            this.lblConsumhints.TabIndex = 36;
            // 
            // listViewEquipmentBag
            // 
            this.listViewEquipmentBag.HideSelection = false;
            this.listViewEquipmentBag.Location = new System.Drawing.Point(340, 293);
            this.listViewEquipmentBag.Name = "listViewEquipmentBag";
            this.listViewEquipmentBag.Size = new System.Drawing.Size(282, 242);
            this.listViewEquipmentBag.TabIndex = 37;
            this.listViewEquipmentBag.UseCompatibleStateImageBehavior = false;
            // 
            // lblEquipmentBag
            // 
            this.lblEquipmentBag.AutoSize = true;
            this.lblEquipmentBag.Location = new System.Drawing.Point(338, 265);
            this.lblEquipmentBag.Name = "lblEquipmentBag";
            this.lblEquipmentBag.Size = new System.Drawing.Size(53, 12);
            this.lblEquipmentBag.TabIndex = 38;
            this.lblEquipmentBag.Text = "装配背包";
            // 
            // lblGold
            // 
            this.lblGold.AutoSize = true;
            this.lblGold.Location = new System.Drawing.Point(145, 48);
            this.lblGold.Name = "lblGold";
            this.lblGold.Size = new System.Drawing.Size(0, 12);
            this.lblGold.TabIndex = 39;
            // 
            // lblSilver
            // 
            this.lblSilver.AutoSize = true;
            this.lblSilver.Location = new System.Drawing.Point(240, 48);
            this.lblSilver.Name = "lblSilver";
            this.lblSilver.Size = new System.Drawing.Size(0, 12);
            this.lblSilver.TabIndex = 40;
            // 
            // lblDiamond
            // 
            this.lblDiamond.AutoSize = true;
            this.lblDiamond.Location = new System.Drawing.Point(369, 49);
            this.lblDiamond.Name = "lblDiamond";
            this.lblDiamond.Size = new System.Drawing.Size(0, 12);
            this.lblDiamond.TabIndex = 41;
            // 
            // lblConsumeCnt
            // 
            this.lblConsumeCnt.AutoSize = true;
            this.lblConsumeCnt.Location = new System.Drawing.Point(214, 116);
            this.lblConsumeCnt.Name = "lblConsumeCnt";
            this.lblConsumeCnt.Size = new System.Drawing.Size(35, 12);
            this.lblConsumeCnt.TabIndex = 12;
            this.lblConsumeCnt.Text = "数量:";
            // 
            // lblEquipment
            // 
            this.lblEquipment.AutoSize = true;
            this.lblEquipment.Location = new System.Drawing.Point(12, 208);
            this.lblEquipment.Name = "lblEquipment";
            this.lblEquipment.Size = new System.Drawing.Size(77, 12);
            this.lblEquipment.TabIndex = 42;
            this.lblEquipment.Text = "装备物品 ID:";
            // 
            // txtBoxEquipmentID
            // 
            this.txtBoxEquipmentID.Location = new System.Drawing.Point(108, 205);
            this.txtBoxEquipmentID.Name = "txtBoxEquipmentID";
            this.txtBoxEquipmentID.Size = new System.Drawing.Size(100, 21);
            this.txtBoxEquipmentID.TabIndex = 43;
            // 
            // txtBoxEquipmentCnt
            // 
            this.txtBoxEquipmentCnt.Location = new System.Drawing.Point(257, 206);
            this.txtBoxEquipmentCnt.Name = "txtBoxEquipmentCnt";
            this.txtBoxEquipmentCnt.Size = new System.Drawing.Size(100, 21);
            this.txtBoxEquipmentCnt.TabIndex = 44;
            // 
            // lblequipmentCnt
            // 
            this.lblequipmentCnt.AutoSize = true;
            this.lblequipmentCnt.Location = new System.Drawing.Point(216, 210);
            this.lblequipmentCnt.Name = "lblequipmentCnt";
            this.lblequipmentCnt.Size = new System.Drawing.Size(41, 12);
            this.lblequipmentCnt.TabIndex = 45;
            this.lblequipmentCnt.Text = "数量：";
            // 
            // btnequipment
            // 
            this.btnequipment.Location = new System.Drawing.Point(417, 202);
            this.btnequipment.Name = "btnequipment";
            this.btnequipment.Size = new System.Drawing.Size(75, 23);
            this.btnequipment.TabIndex = 46;
            this.btnequipment.Text = "装备";
            this.btnequipment.UseVisualStyleBackColor = true;
            this.btnequipment.Click += new System.EventHandler(this.btnequipment_Click);
            // 
            // lblexp
            // 
            this.lblexp.AutoSize = true;
            this.lblexp.Location = new System.Drawing.Point(458, 49);
            this.lblexp.Name = "lblexp";
            this.lblexp.Size = new System.Drawing.Size(0, 12);
            this.lblexp.TabIndex = 47;
            // 
            // lablPlayerRank
            // 
            this.lablPlayerRank.AutoSize = true;
            this.lablPlayerRank.Location = new System.Drawing.Point(544, 49);
            this.lablPlayerRank.Name = "lablPlayerRank";
            this.lablPlayerRank.Size = new System.Drawing.Size(0, 12);
            this.lablPlayerRank.TabIndex = 48;
            // 
            // lblUnequipment
            // 
            this.lblUnequipment.AutoSize = true;
            this.lblUnequipment.Location = new System.Drawing.Point(9, 233);
            this.lblUnequipment.Name = "lblUnequipment";
            this.lblUnequipment.Size = new System.Drawing.Size(77, 12);
            this.lblUnequipment.TabIndex = 49;
            this.lblUnequipment.Text = "卸下物品 ID:";
            // 
            // txtBoxUnequipID
            // 
            this.txtBoxUnequipID.Location = new System.Drawing.Point(108, 233);
            this.txtBoxUnequipID.Name = "txtBoxUnequipID";
            this.txtBoxUnequipID.Size = new System.Drawing.Size(100, 21);
            this.txtBoxUnequipID.TabIndex = 50;
            // 
            // txtBoxUnequipCnt
            // 
            this.txtBoxUnequipCnt.Location = new System.Drawing.Point(257, 233);
            this.txtBoxUnequipCnt.Name = "txtBoxUnequipCnt";
            this.txtBoxUnequipCnt.Size = new System.Drawing.Size(100, 21);
            this.txtBoxUnequipCnt.TabIndex = 51;
            // 
            // lblUnequipCnt
            // 
            this.lblUnequipCnt.AutoSize = true;
            this.lblUnequipCnt.Location = new System.Drawing.Point(216, 241);
            this.lblUnequipCnt.Name = "lblUnequipCnt";
            this.lblUnequipCnt.Size = new System.Drawing.Size(35, 12);
            this.lblUnequipCnt.TabIndex = 52;
            this.lblUnequipCnt.Text = "数量:";
            // 
            // btnUnequip
            // 
            this.btnUnequip.Location = new System.Drawing.Point(417, 230);
            this.btnUnequip.Name = "btnUnequip";
            this.btnUnequip.Size = new System.Drawing.Size(75, 23);
            this.btnUnequip.TabIndex = 53;
            this.btnUnequip.Text = "卸下";
            this.btnUnequip.UseVisualStyleBackColor = true;
            this.btnUnequip.Click += new System.EventHandler(this.btnUnequip_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1047, 718);
            this.Controls.Add(this.btnUnequip);
            this.Controls.Add(this.lblUnequipCnt);
            this.Controls.Add(this.txtBoxUnequipCnt);
            this.Controls.Add(this.txtBoxUnequipID);
            this.Controls.Add(this.lblUnequipment);
            this.Controls.Add(this.lablPlayerRank);
            this.Controls.Add(this.lblexp);
            this.Controls.Add(this.btnequipment);
            this.Controls.Add(this.lblequipmentCnt);
            this.Controls.Add(this.txtBoxEquipmentCnt);
            this.Controls.Add(this.txtBoxEquipmentID);
            this.Controls.Add(this.lblEquipment);
            this.Controls.Add(this.lblDiamond);
            this.Controls.Add(this.lblSilver);
            this.Controls.Add(this.lblGold);
            this.Controls.Add(this.lblEquipmentBag);
            this.Controls.Add(this.listViewEquipmentBag);
            this.Controls.Add(this.lblConsumhints);
            this.Controls.Add(this.btnConsume);
            this.Controls.Add(this.lblConsumeHints);
            this.Controls.Add(this.txtBoxConsumeCnt);
            this.Controls.Add(this.txtBoxConsumeID);
            this.Controls.Add(this.picBoxFig);
            this.Controls.Add(this.lblRankSearchHints);
            this.Controls.Add(this.btnRankSearch);
            this.Controls.Add(this.lblRankSearchEnd);
            this.Controls.Add(this.txtBoxRankSearchEnd);
            this.Controls.Add(this.txtBoxRankSearchstart);
            this.Controls.Add(this.lblRank);
            this.Controls.Add(this.dataGridViewSearch);
            this.Controls.Add(this.lblBag);
            this.Controls.Add(this.listViewBagItem);
            this.Controls.Add(this.lblSearchHints);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtBoxSearchName);
            this.Controls.Add(this.txtBoxSearchID);
            this.Controls.Add(this.lblSearchName);
            this.Controls.Add(this.lblSearchID);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.lblUIName);
            this.Controls.Add(this.lblAddItemHints);
            this.Controls.Add(this.lblConsumeCnt);
            this.Controls.Add(this.lblItemCount);
            this.Controls.Add(this.lblAddItemID);
            this.Controls.Add(this.lblAddItem);
            this.Controls.Add(this.btnAddItem);
            this.Controls.Add(this.txtBoxItemCount);
            this.Controls.Add(this.txtBoxItemID);
            this.Controls.Add(this.lblHints);
            this.Controls.Add(this.chkBoxPws);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtBoxPws);
            this.Controls.Add(this.lblPsw);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtboxName);
            this.Name = "Form1";
            this.Text = "Game";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxFig)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtboxName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPsw;
        private System.Windows.Forms.TextBox txtBoxPws;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.CheckBox chkBoxPws;
        private System.Windows.Forms.Label lblHints;
        private System.Windows.Forms.TextBox txtBoxItemID;
        private System.Windows.Forms.TextBox txtBoxItemCount;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Label lblAddItem;
        private System.Windows.Forms.Label lblAddItemID;
        private System.Windows.Forms.Label lblItemCount;
        private System.Windows.Forms.Label lblAddItemHints;
        private System.Windows.Forms.Label lblUIName;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label lblSearchID;
        private System.Windows.Forms.Label lblSearchName;
        private System.Windows.Forms.TextBox txtBoxSearchID;
        private System.Windows.Forms.TextBox txtBoxSearchName;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblSearchHints;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.ListView listViewBagItem;
        private System.Windows.Forms.Label lblBag;
        private System.Windows.Forms.DataGridView dataGridViewSearch;
        private System.Windows.Forms.Label lblRank;
        private System.Windows.Forms.TextBox txtBoxRankSearchstart;
        private System.Windows.Forms.TextBox txtBoxRankSearchEnd;
        private System.Windows.Forms.Label lblRankSearchEnd;
        private System.Windows.Forms.Button btnRankSearch;
        private System.Windows.Forms.Label lblRankSearchHints;
        private System.Windows.Forms.PictureBox picBoxFig;
        private System.Windows.Forms.TextBox txtBoxConsumeID;
        private System.Windows.Forms.TextBox txtBoxConsumeCnt;
        private System.Windows.Forms.Label lblConsumeHints;
        private System.Windows.Forms.Button btnConsume;
        private System.Windows.Forms.Label lblConsumhints;
        private System.Windows.Forms.ListView listViewEquipmentBag;
        private System.Windows.Forms.Label lblEquipmentBag;
        private System.Windows.Forms.Label lblGold;
        private System.Windows.Forms.Label lblSilver;
        private System.Windows.Forms.Label lblDiamond;
        private System.Windows.Forms.Label lblConsumeCnt;
        private System.Windows.Forms.Label lblEquipment;
        private System.Windows.Forms.TextBox txtBoxEquipmentID;
        private System.Windows.Forms.TextBox txtBoxEquipmentCnt;
        private System.Windows.Forms.Label lblequipmentCnt;
        private System.Windows.Forms.Button btnequipment;
        private System.Windows.Forms.Label lblexp;
        private System.Windows.Forms.Label lablPlayerRank;
        private System.Windows.Forms.Label lblUnequipment;
        private System.Windows.Forms.TextBox txtBoxUnequipID;
        private System.Windows.Forms.TextBox txtBoxUnequipCnt;
        private System.Windows.Forms.Label lblUnequipCnt;
        private System.Windows.Forms.Button btnUnequip;
    }
}

