using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Buff;
using Google.Protobuf;
using Google.Protobuf.Collections;
using TCPSocket;
using MySql.Data.MySqlClient;
using GameSpec;
using System.Speech.Synthesis;

namespace Game
{
    public partial class Form1 : Form
    {
        SpeechSynthesizer synth = new SpeechSynthesizer();

        public Form1()
        {
            InitializeComponent();
        }

        private SocketError socketError;
        private Buffers buff = new Buffers();
        private TCPSocketClient tcpSocket;
        private bool isLogined = false;
        private FileInfo[] fileInfos;
        private Dictionary<int, string> dicId2File;
        private Players player;

        private void SortAsFileName(ref FileInfo[] arrFi)
        {
            Array.Sort(arrFi, delegate (FileInfo x, FileInfo y) { return x.Name.CompareTo(y.Name); });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region

            dicId2File = new Dictionary<int, string>();
            listViewBagItem.Items.Clear();
            listViewBagItem.View = View.LargeIcon;
            string filePath = @"../../resource";
            DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
            imgList.ImageSize = new Size(32, 32);
            fileInfos = directoryInfo.GetFiles("*.png", SearchOption.TopDirectoryOnly);// GetFiles();
            SortAsFileName(ref fileInfos);

            for (int i = 0; i < fileInfos.Length; i++)
            {
                dicId2File.Add(i, fileInfos[i].FullName);
                imgList.Images.Add(Image.FromFile(fileInfos[i].FullName));
            }

            string ip = "10.0.150.52";
            int port = 3000;
            tcpSocket = new TCPSocketClient(ip, port);
            buff.Init(1024, 1024 * 1024);

            string picStartFile = @"../../picResource/RO.jpg";
            this.BackgroundImage = Image.FromFile(picStartFile);
            this.BackgroundImageLayout = ImageLayout.Stretch;

            #endregion


            SetControllerInvisible(false);

            #region Newdata

            //string connstr = "server=192.168.78.66;user id=mysql;password=mysql;database=game";
            //using (MySqlConnection connection = new MySqlConnection(connstr))
            //{
            //    using (MySqlCommand command = new MySqlCommand())
            //    {
            //        command.Connection = connection;
            //        connection.Open();
            //        for (int i = 0; i < 125; i++)
            //        {
            //            Players p = new Players();
            //            //p.Id = 1 + i;
            //            p.Exp = 2000 + i;
            //            p.Name = "jack" + (1 + i);
            //            p.Rank = 100 + i;
            //            p.Bags = new BagInfo();
            //            BagItem item = new BagItem();
            //            item.Count = 1 + i;
            //            item.Binded = false;
            //            item.ItemID = 8 + 1;
            //            item.Overly = 5;
            //            item.TypeID = 2;
            //            p.Bags.Bag.Add(item);
            //            BagItem item2 = new BagItem();
            //            item2.Count = 9 + i;
            //            item2.Binded = false;
            //            item2.ItemID = i + 5;
            //            item2.Overly = 5;
            //            item2.TypeID = 2;
            //            p.Bags.Bag.Add(item2);
            //            BagItem item3 = new BagItem();
            //            item3.Count = 7 + i;
            //            item3.Binded = false;
            //            item3.ItemID = i + 2;
            //            item3.Overly = 5;
            //            item3.TypeID = 2;
            //            p.Bags.Bag.Add(item3);
            //            BagItem item4 = new BagItem();
            //            item4.Count = 5 + i;
            //            item4.Binded = false;
            //            item4.ItemID = i + 3;
            //            item4.Overly = 5;
            //            item4.TypeID = 2;
            //            p.Bags.Bag.Add(item4);
            //            BagItem item5 = new BagItem();
            //            item5.Count = 2 + i;
            //            item5.Binded = false;
            //            item5.ItemID = i + 4;
            //            item5.Overly = 5;
            //            item5.TypeID = 2;
            //            p.Bags.Bag.Add(item5);
            //            CurrencyItem currencyItemGolden = new CurrencyItem();
            //            currencyItemGolden.Count = 100;
            //            currencyItemGolden.ItemID = 200 + i;
            //            currencyItemGolden.Overly = 5;
            //            currencyItemGolden.TypeID = 0;
            //            p.Bags.Currency.Add(currencyItemGolden);
            //            CurrencyItem currencyItem1 = new CurrencyItem();
            //            currencyItem1.Count = 100;
            //            currencyItem1.ItemID = 500 + i;
            //            currencyItem1.Overly = 5;
            //            currencyItem1.TypeID = 1;
            //            p.Bags.Currency.Add(currencyItem1);
            //            CurrencyItem currencyItem2 = new CurrencyItem();
            //            currencyItem2.Count = 100;
            //            currencyItem2.ItemID = 100 + i;
            //            currencyItem2.Overly = 5;
            //            currencyItem2.TypeID = 2;
            //            p.Bags.Currency.Add(currencyItem2);
            //            EquipItem equiptItem = new EquipItem();
            //            equiptItem.Count = 5;
            //            equiptItem.Attack = 500;
            //            equiptItem.Binded = false;
            //            equiptItem.Durability = 500;
            //            equiptItem.ItemID = 2 + i;
            //            equiptItem.Overly = 5;
            //            equiptItem.TypeID = 1;
            //            p.Bags.Equipment.Add(equiptItem);

            //            EquipItem equiptItem1 = new EquipItem();
            //            equiptItem1.Count = 5;
            //            equiptItem1.Attack = 500;
            //            equiptItem1.Binded = false;
            //            equiptItem1.Durability = 500;
            //            equiptItem1.ItemID = 1 + i;
            //            equiptItem1.Overly = 5;
            //            equiptItem1.TypeID = 1;
            //            p.Bags.Equipment.Add(equiptItem1);

            //            command.CommandText = @"insert into players(playerName, rank, exp, bagInfo) values(@Name" + i + ", @r" + i + ", @ex" + i + ", @bag" + i + ")";

            //            MySqlParameter nameParameter = new MySqlParameter("@Name" + i, MySqlDbType.String, 15);
            //            MySqlParameter rankParameter = new MySqlParameter("@r" + i, MySqlDbType.Int32);
            //            MySqlParameter expParameter = new MySqlParameter("@ex" + i, MySqlDbType.Int32);
            //            MySqlParameter bagInfoParameter = new MySqlParameter("@bag" + i, MySqlDbType.MediumBlob);


            //            nameParameter.Value = p.Name;
            //            rankParameter.Value = p.Rank;
            //            expParameter.Value = p.Exp;
            //            bagInfoParameter.Value = p.Bags.ToByteArray();
            //            command.Parameters.Add(nameParameter);
            //            command.Parameters.Add(rankParameter);
            //            command.Parameters.Add(expParameter);
            //            command.Parameters.Add(bagInfoParameter);


            //            command.ExecuteNonQuery();
            //        }


            //    }
            //}

            #endregion

            synth.SpeakAsync("欢迎登陆！");
        }

        private void SetControllerInvisible(bool status)
        {
            this.lblGold.Visible = status;
            this.lblSilver.Visible = status;
            this.lblDiamond.Visible = status;
            this.lblAddItem.Visible = status;
            this.lblAddItemHints.Visible = status;
            this.lblAddItemID.Visible = status;
            this.txtBoxItemID.Visible = status;
            this.txtBoxItemCount.Visible = status;
            this.btnAddItem.Visible = status;
            this.lblConsumhints.Visible = status;
            this.lblConsumeHints.Visible = status;
            this.lblConsumeCnt.Visible = status;
            this.txtBoxConsumeCnt.Visible = status;
            this.txtBoxConsumeID.Visible = status;
            this.btnConsume.Visible = status;
            this.lblSearch.Visible = status;
            this.txtBoxSearchName.Visible = status;
            this.txtBoxRankSearchstart.Visible = status;
            this.txtBoxRankSearchEnd.Visible = status;
            this.lblRankSearchHints.Visible = status;
            this.lblItemCount.Visible = status;
            this.lblSearchName.Visible = status;
            this.btnSearch.Visible = status;
            this.btnRankSearch.Visible = status;
            this.lblRankSearchEnd.Visible = status;
            this.lblBag.Visible = status;
            this.lblEquipmentBag.Visible = status;
            this.listViewBagItem.Visible = status;
            this.listViewEquipmentBag.Visible = status;
            this.dataGridViewSearch.Visible = status;
            this.lblEquipment.Visible = status;
            this.lblequipmentCnt.Visible = status;
            this.txtBoxEquipmentID.Visible = status;
            this.txtBoxEquipmentCnt.Visible = status;
            this.lblUnequipCnt.Visible = status;
            this.lblUnequipment.Visible = status;
            this.txtBoxUnequipID.Visible = status;
            this.txtBoxUnequipCnt.Visible = status;
            this.btnequipment.Visible = status;
            this.btnUnequip.Visible = status;
            this.lblexp.Visible = status;
            this.lblRank.Visible = status;
            this.lablPlayerRank.Visible = status;
        }

        private void chkBoxPws_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxPws.Checked)
            {
                //复选框被勾选，明文显示
                txtBoxPws.PasswordChar = new char();
            }
            else
            {
                //复选框被取消勾选，密文显示
                txtBoxPws.PasswordChar = '*';
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string password = txtBoxPws.Text.Trim();
            string name = txtboxName.Text.Trim();
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(password))
            {
                lblHints.Text = "Please Enter Name or Password!";
                lblHints.ForeColor = Color.DarkRed;
            }
            else
            {
                #region Login

                //Byte[] temp = new byte[1024];
                //byte[] data = new byte[1024];
                //CtlMsgLoginReq login = new CtlMsgLoginReq();
                //using (CodedOutputStream cos = new CodedOutputStream(temp))
                //{
                //    login.Name = name;
                //    login.Password = password;
                //    login.WriteTo(cos);
                //}

                //Buffers.Encode(data, 9999, 1, login.CalculateSize());
                //for (int i = 0; i < login.CalculateSize(); i++)
                //{
                //    data[sizeof(Int32) * 3 + i] = temp[i];
                //}

                //if (tcpSocket.Connected())
                //{
                //    tcpSocket.Send(data, 0, sizeof(Int32) * 3 + login.CalculateSize(), out socketError);
                //}

                //DateTime dateTime = DateTime.Now;
                //MsgInfo msg = null;
                //while (true)
                //{
                //    int ret = tcpSocket.Receive(buff, out socketError);
                //    if (buff.IsHeaderReadable(sizeof(Int32) * 3))
                //    {
                //        msg = buff.Decode();
                //        if (buff.IsHeaderAndPayloadReadable(msg.GetPacLen() + sizeof(Int32) * 3))
                //        {
                //            buff.GetPayload(msg);
                //            buff.SetStartByLen(msg.GetPacLen() + sizeof(Int32) * 3);
                //            break;
                //        }
                //    }

                //    if ((DateTime.Now - dateTime).Milliseconds > 1000)
                //    {
                //        lblHints.Text = "网络异常！登录失败！";
                //        lblHints.ForeColor = Color.Red;
                //        dateTime = DateTime.Now;
                //        return;
                //    }
                //}

                //CtlMsgLoginRsp loginRsp = CtlMsgLoginRsp.Parser.ParseFrom(msg.GetPayload());
                //if (loginRsp.ErrCode != ErrorCode.ErrorNoError)
                //{
                //    lblHints.Text = "登录失败，请输入正确的用户名和密码！";
                //    return;
                //}

                //lblHints.Text = "登录成功！";
                //lblHints.ForeColor = Color.Green;

                //lblHints.Visible = false;
                //lblName.Visible = false;
                //lblPsw.Visible = false;
                //txtBoxPws.Visible = false;
                //txtboxName.Visible = false;
                //btnLogin.Visible = false;
                //chkBoxPws.Visible = false;
                //isLogined = true;
                //lblUIName.Text = txtboxName.Text.Trim();
                //string picFile = @"../../picResource/figpic.jpg";
                //picBoxFig.Image = Image.FromFile(picFile);
                //picBoxFig.Visible = true;
                //if ((player = loginRsp.Player) == null)
                //{
                //    return;
                //}

                //InitBagInfo();


                #endregion

                #region LoginTest

                string sql = "select * from players where playerName = @Name";

                
                MySqlParameter[] nameParameter = { new MySqlParameter("@Name", MySqlDbType.String, 15) };
                nameParameter[0].Value = name;
                DataSet dataSet = SqlHelper.GetDataSet(SqlHelper.Conn, CommandType.Text, sql, nameParameter);
                DataTable dataTable = dataSet.Tables[0];
                DataRowCollection rows = dataTable.Rows;


                if (rows.Count > 0)
                {
                    player = new Players();
                    player.Id = (Int32)rows[0]["id"];
                    player.Exp = (Int32)rows[0]["exp"];
                    player.Name = (string)rows[0]["playerName"];
                    player.Rank = (Int32)rows[0]["rank"];
                    player.Bags = BagInfo.Parser.ParseFrom((Byte[])rows[0]["bagInfo"]);
                    InitBagInfo();
                    lblHints.Text = "登录成功！";
                    lblHints.ForeColor = Color.Green;
                    lblHints.Visible = false;
                    lblName.Visible = false;
                    lblPsw.Visible = false;
                    txtBoxPws.Visible = false;
                    txtboxName.Visible = false;
                    btnLogin.Visible = false;
                    chkBoxPws.Visible = false;
                    isLogined = true;
                    lblUIName.Text = (string)rows[0]["playerName"];
                    string picFile = @"../../picResource/figpic.jpg";
                    picBoxFig.Image = Image.FromFile(picFile);
                    picBoxFig.Visible = true;
                    SetControllerInvisible(true);
                }
                else
                {
                    lblHints.Text = "用户名或者密码错误！";
                    lblHints.ForeColor = Color.Red;
                    return;
                }

                lblHints.Text = "";
                int start = 0;
                int end = 100;
                string sqlstr = "select id, playerName, rank, exp from players order by exp desc limit @offset, @count";
                int offset = start;

                int count = end - start;
                count = count > 0 ? count : 0;
                //MySqlParameter idParameter = new MySqlParameter("@i", MySqlDbType.Int32);
                MySqlParameter[] ranksParameters =
                {
                    new MySqlParameter("@offset", MySqlDbType.Int32),
                    new MySqlParameter("@count", MySqlDbType.Int32)
                };
                ranksParameters[0].Value = offset;
                ranksParameters[1].Value = count;
                MySqlDataReader dataReader = SqlHelper.ExecuteReader(SqlHelper.Conn, CommandType.Text, sqlstr, ranksParameters);

                DataTable dt = new DataTable();
                dt.Load(dataReader);
                dataGridViewSearch.DataSource = dt;
                #endregion
                synth.SpeakAsync("欢迎" + player.Name + "回到Game!");

            }
        }

        private void InitBagInfo()
        {
            #region 更新背包
            listViewBagItem.Items.Clear();
            this.listViewBagItem.BeginUpdate();
            RepeatedField<BagItem> bags = player.Bags.Bag;
            for (int i = 0; i < bags.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                //imgList.Images.Add(Image.FromFile(dicId2File[bags[i].ItemID]));
                if (!dicId2File.ContainsKey(bags[i].ItemID))
                {
                    continue;
                }
                item.Text = Path.GetFileNameWithoutExtension(dicId2File[bags[i].ItemID]) + ":" + bags[i].Count;
                item.ImageIndex = bags[i].ItemID;
                listViewBagItem.Items.Add(item);
            }
            listViewBagItem.LargeImageList = imgList;
            listViewBagItem.Show();
            this.listViewBagItem.EndUpdate();
            #endregion


            #region 更新装备背包
            listViewEquipmentBag.Items.Clear();
            this.listViewEquipmentBag.BeginUpdate();
            RepeatedField<EquipItem> eqQags = player.Bags.Equipment;
            for (int i = 0; i < eqQags.Count; i++)
            {
                ListViewItem eqItem = new ListViewItem();
                //imgList.Images.Add(Image.FromFile(dicId2File[bags[i].ItemID]));
                if (!dicId2File.ContainsKey(eqQags[i].ItemID))
                {
                    continue;
                }
                eqItem.Text = Path.GetFileNameWithoutExtension(dicId2File[eqQags[i].ItemID]) + ":" + eqQags[i].Count;
                eqItem.ImageIndex = eqQags[i].ItemID;
                listViewEquipmentBag.Items.Add(eqItem);
            }
            listViewEquipmentBag.LargeImageList = imgList;
            listViewEquipmentBag.Show();
            this.listViewEquipmentBag.EndUpdate();
            #endregion

            #region 更新钱包

            RepeatedField<CurrencyItem> currencyBag = player.Bags.Currency;
            foreach (CurrencyItem item in currencyBag)
            {
                if (item.ItemID == -1)
                {
                    continue;
                }
                switch (item.TypeID)
                {
                    case 0:
                        lblGold.Text = "金币：" + item.Count;
                        lblGold.ForeColor = Color.Gold;
                        lblGold.BackColor = Color.Purple;
                        break;
                    case 1:
                        lblSilver.Text = "银币：" + item.Count;
                        lblSilver.ForeColor = Color.Silver;
                        break;
                    case 2:
                        lblDiamond.Text = "钻石:" + item.Count;
                        lblDiamond.ForeColor = Color.Purple;

                        break;
                    default:
                        break;

                }

            }

            #endregion

            #region 更新基本信息

            lblexp.Text = "经验：" + player.Exp;
            lablPlayerRank.Text = "Rank：" + player.Rank;

            #endregion

        }
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (!isLogined)
            {
                lblAddItemHints.Text = "请登录账户和密码！";
                txtboxName.Focus();
            }
            int id;
            int count;
            if (!Int32.TryParse(txtBoxItemID.Text.Trim(), out id) || !Int32.TryParse(txtBoxItemCount.Text.Trim(), out count))
            {
                lblAddItemHints.Text = "请输入正确的id和count!";
                lblAddItemHints.ForeColor = Color.Red;
                return;
            }

            #region 增加物品

            //Byte[] temp = new byte[1024];
            //byte[] data = new byte[1024];
            //AddItemReq item = new AddItemReq();
            //item.ItemID = id;
            //item.Count = count;
            //using (CodedOutputStream cos = new CodedOutputStream(temp))
            //{
            //    item.WriteTo(cos);
            //}
            //Buffers.Encode(data, 10000, player.Id, item.CalculateSize());
            //for (int i = 0; i < item.CalculateSize(); i++)
            //{
            //    data[sizeof(Int32) * 3 + i] = temp[i];
            //}

            //if (tcpSocket.Connected())
            //{
            //    tcpSocket.Send(data, 0, sizeof(Int32) * 3 + item.CalculateSize(), out socketError);
            //}
            //DateTime dateTime = DateTime.Now;
            //MsgInfo msg;
            //while (true)
            //{
            //    int ret = tcpSocket.Receive(buff, out socketError);
            //    if (buff.IsHeaderReadable(sizeof(Int32) * 3))
            //    {
            //        msg = buff.Decode();
            //        if (buff.IsHeaderAndPayloadReadable(msg.GetPacLen() + sizeof(Int32) * 3))
            //        {
            //            buff.GetPayload(msg);
            //            buff.SetStartByLen(msg.GetPacLen() + sizeof(Int32) * 3);
            //            break;
            //        }
            //    }

            //    if ((DateTime.Now - dateTime).Milliseconds > 1000)
            //    {
            //        lblAddItemHints.Text = "网络异常！物品添加成功失败！";
            //        lblAddItemHints.ForeColor = Color.Red;
            //        return;
            //    }
            //}
            //AddItemRsp item1 = AddItemRsp.Parser.ParseFrom(msg.GetPayload());
            //if (item1.ErrCode != ErrorCode.ErrorNoError)
            //{
            //    lblAddItemHints.Text = "物品添加失败!";
            //    lblAddItemHints.ForeColor = Color.Red;
            //    return;
            //}

            //lblAddItemHints.Text = "物品添加成功!";
            //lblAddItemHints.ForeColor = Color.Green;
            //int itemIdx = FindBagItemByID(item.ItemID);
            //if (itemIdx != -1)
            //{
            //    player.Bags.Bag[itemIdx].Count += item.Count;
            //    if (player.Bags.Bag[itemIdx].Count <= 0)
            //    {
            //        player.Bags.Bag.RemoveAt(itemIdx);
            //    }
            //}
            //else
            //{
            //    BagItem bgitem = new BagItem();
            //    bgitem.Count = item.Count;
            //    bgitem.Binded = false;
            //    bgitem.ItemID = item.ItemID;
            //    bgitem.TypeID = 1;
            //    player.Bags.Bag.Add(bgitem);
            //}
            #endregion

            #region Test

            int idx = FindBagItemByID(id);
            if (idx != -1)
            {
                player.Bags.Bag[idx].Count += count;
            }
            else
            {
                BagItem bgitem = new BagItem();
                bgitem.Count = count;
                bgitem.Binded = false;
                bgitem.ItemID = id;
                bgitem.TypeID = 1;
                player.Bags.Bag.Add(bgitem);
            }
            player.Bags.Currency[0].Count -= count;
            #endregion



            InitBagInfo();
        }

        private int FindBagItemByID(int id)
        {
            int idx = -1;
            RepeatedField<BagItem> bags = player.Bags.Bag;
            for (int i = 0; i < bags.Count; i++)
            {
                if (bags[i] == null)
                {
                    continue;
                }
                if (bags[i].ItemID == id)
                {
                    idx = i;
                    break;
                }
            }

            return idx;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!isLogined)
            {
                lblAddItemHints.Text = "请登录账户和密码！";
                txtboxName.Focus();
            }
            int id = -1;
            string name = txtBoxSearchName.Text.Trim();

            if (!Int32.TryParse(txtBoxSearchID.Text.Trim(), out id) && String.IsNullOrEmpty(name))
            {
                lblSearchHints.Text = "请输入正确的id或者请输入非空的用户名！";
                lblSearchHints.ForeColor = Color.Red;
                return;
            }

            #region search
            //Byte[] temp = new byte[1024];
            //byte[] data = new byte[1024];
            //CtlMsgSearchReq search = new CtlMsgSearchReq();
            //using (CodedOutputStream cos = new CodedOutputStream(temp))
            //{
            //    search.Id = id;
            //    search.Name = name;
            //    search.WriteTo(cos);
            //}
            //Buffers.Encode(data, 10002, player.Id, search.CalculateSize());
            //for (int i = 0; i < search.CalculateSize(); i++)
            //{
            //    data[sizeof(Int32) * 3 + i] = temp[i];
            //}

            //if (tcpSocket.Connected())
            //{
            //    tcpSocket.Send(data, 0, sizeof(Int32) * 3 + search.CalculateSize(), out socketError);
            //}
            //DateTime dateTime = DateTime.Now;
            //MsgInfo msg;
            //while (true)
            //{
            //    int ret = tcpSocket.Receive(buff, out socketError);

            //    if (buff.IsHeaderReadable(sizeof(Int32) * 3))
            //    {
            //        msg = buff.Decode();
            //        if (buff.IsHeaderAndPayloadReadable(msg.GetPacLen() + sizeof(Int32) * 3))
            //        {
            //            buff.GetPayload(msg);
            //            buff.SetStartByLen(msg.GetPacLen() + sizeof(Int32) * 3);
            //            break;
            //        }
            //    }
            //    if ((DateTime.Now - dateTime).Milliseconds > 1000)
            //    {
            //        lblSearchHints.Text = "网络异常！检索失败！";
            //        lblSearchHints.ForeColor = Color.Red;
            //        return;
            //    }
            //}

            //CtlMsgSearchRsp searchrsp = CtlMsgSearchRsp.Parser.ParseFrom(msg.GetPayload());
            //if (searchrsp.ErrCode != ErrorCode.ErrorNoError)
            //{
            //    lblSearchHints.Text = "查无此人！";
            //    lblSearchHints.ForeColor = Color.Red;
            //    buff.SetStartByLen(sizeof(Int32) * 3 + msg.GetPacLen());
            //    return;
            //}
            //lblSearchHints.Text = "ID:" + searchrsp.Player.Id + "Name:" + searchrsp.Player.Name + "Rank:" + searchrsp.Player.Rank + "Exp:" + searchrsp.Player.Exp;
            //lblSearchHints.ForeColor = Color.Green;


            #endregion


            #region Test

            string sql = "select id, playerName, rank, exp from players where playerName= @Name";

            //MySqlParameter idParameter = new MySqlParameter("@i", MySqlDbType.Int32);
            MySqlParameter[] nameParameter =
            {
                new MySqlParameter("@Name", MySqlDbType.String, 15)
            };
            nameParameter[0].Value = name;
            MySqlDataReader dataReader = SqlHelper.ExecuteReader(SqlHelper.Conn, CommandType.Text, sql, nameParameter);

            DataTable dt = new DataTable();
            dt.Load(dataReader);

            dataGridViewSearch.DataSource = dt;
            //dataGridViewSearch.;
            #endregion
        }

        private void btnRankSearch_Click(object sender, EventArgs e)
        {
            if (!isLogined)
            {
                lblRankSearchHints.Text = "请登录账户和密码！";
                txtboxName.Focus();
            }

            int start;
            int end;
            if (!Int32.TryParse(txtBoxRankSearchstart.Text.Trim(), out start) || !Int32.TryParse(txtBoxRankSearchEnd.Text.Trim(), out end))
            {
                lblRankSearchHints.Text = "请输入正确的排名开始和结束位置！";
                return;
            }
            start = (start -= 1) > 0 ? start : 0;
            end = (end > 100) ? 100 : end;
            #region 排行榜查询
            //string sql = "select id, playerName, rank, exp from players order by exp desc limit @offset, @count";
            //int offset = start;

            //int count = end - start;
            //count = count > 0 ? count : 0;
            ////MySqlParameter idParameter = new MySqlParameter("@i", MySqlDbType.Int32);
            //MySqlParameter[] nameParameter =
            //{
            //    new MySqlParameter("@offset", MySqlDbType.Int32),
            //    new MySqlParameter("@count", MySqlDbType.Int32)
            //};
            //nameParameter[0].Value = offset;
            //nameParameter[1].Value = count;
            //MySqlDataReader dataReader = SqlHelper.ExecuteReader(SqlHelper.Conn, CommandType.Text, sql, nameParameter);

            //DataTable dt = new DataTable();
            //dt.Load(dataReader);
            //dataGridViewSearch.DataSource = dt;
            #endregion

            #region Test

            //Byte[] temp = new byte[1024];
            //byte[] data = new byte[1024];
            //RankReq item = new RankReq();
            //item.Start = start;
            //item.End = end;
            //using (CodedOutputStream cos = new CodedOutputStream(temp))
            //{

            //    item.WriteTo(cos);
            //}
            //Buffers.Encode(data, 10003, 88, item.CalculateSize());
            //for (int i = 0; i < item.CalculateSize(); i++)
            //{
            //    data[sizeof(Int32) * 3 + i] = temp[i];
            //}

            //if (tcpSocket.Connected())
            //{
            //    tcpSocket.Send(data, 0, sizeof(Int32) * 3 + item.CalculateSize(), out socketError);
            //}

            //MsgInfo msg = null;
            //while (true)
            //{
            //    int ret = tcpSocket.Receive(buff, out socketError);
            //    if (buff.IsHeaderReadable(sizeof(Int32) * 3))
            //    {
            //        msg = buff.Decode();
            //        if (buff.IsHeaderAndPayloadReadable(msg.GetPacLen() + sizeof(Int32) * 3))
            //        {
            //            buff.GetPayload(msg);
            //            buff.SetStartByLen(msg.GetPacLen() + sizeof(Int32) * 3);
            //            break;
            //        }
            //    }
            //}

            //RankRsp rankRsp = RankRsp.Parser.ParseFrom(msg.GetPayload());
            //if (rankRsp.ErrCode != ErrorCode.ErrorNoError)
            //{
            //    return;
            //}
            //for (int i = 0; i < rankRsp.Name.Count; i++)
            //{
            //    int index = this.dataGridViewSearch.Rows.Add(rankRsp.Name[i], rankRsp.Exp[i]);

            //}

            #endregion
        }

        private void dataGridViewSearch_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            //e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
        }

        private int FindEquipBagItemByID(int id)
        {
            int idx = -1;
            RepeatedField<EquipItem> bags = player.Bags.Equipment;
            for (int i = 0; i < bags.Count; i++)
            {
                if (bags[i] == null)
                {
                    continue;
                }
                if (bags[i].ItemID == id)
                {
                    idx = i;
                    break;
                }
            }

            return idx;
        }
        private void btnConsume_Click(object sender, EventArgs e)
        {
            if (!isLogined)
            {
                lblConsumhints.Text = "请登录账户和密码！";
                txtboxName.Focus();
            }
            int id;
            int count;
            if (!Int32.TryParse(txtBoxConsumeID.Text.Trim(), out id) || !Int32.TryParse(txtBoxConsumeCnt.Text.Trim(), out count))
            {
                lblConsumhints.Text = "请输入正确的id和count!";
                lblConsumhints.ForeColor = Color.Red;
                return;
            }

            #region 消耗物品

            //Byte[] temp = new byte[1024];
            //byte[] data = new byte[1024];
            //RemoveItemReq item = new RemoveItemReq();
            //item.Count = count;
            //item.ItemID = id;
            //item.Pos = 0;
            //using (CodedOutputStream cos = new CodedOutputStream(temp))
            //{
            //    item.WriteTo(cos);
            //}
            //Buffers.Encode(data, 10001, player.Id, item.CalculateSize());
            //for (int i = 0; i < item.CalculateSize(); i++)
            //{
            //    data[sizeof(Int32) * 3 + i] = temp[i];
            //}

            //if (tcpSocket.Connected())
            //{
            //    tcpSocket.Send(data, 0, sizeof(Int32) * 3 + item.CalculateSize(), out socketError);
            //}
            //DateTime dateTime = DateTime.Now;
            //MsgInfo msg;
            //while (true)
            //{
            //    int ret = tcpSocket.Receive(buff, out socketError);
            //    if (buff.IsHeaderReadable(sizeof(Int32) * 3))
            //    {
            //        msg = buff.Decode();
            //        if (buff.IsHeaderAndPayloadReadable(msg.GetPacLen() + sizeof(Int32) * 3))
            //        {
            //            buff.GetPayload(msg);
            //            buff.SetStartByLen(msg.GetPacLen() + sizeof(Int32) * 3);
            //            break;
            //        }
            //    }

            //    if ((DateTime.Now - dateTime).Milliseconds > 1000)
            //    {
            //        lblAddItemHints.Text = "网络异常！物品添加成功失败！";
            //        lblAddItemHints.ForeColor = Color.Red;
            //        return;
            //    }
            //}
            //RemoveItemRsp item1 = RemoveItemRsp.Parser.ParseFrom(msg.GetPayload());
            //if (item1.ErrCode != ErrorCode.ErrorNoError)
            //{
            //    lblAddItemHints.Text = "物品消耗失败!";
            //    lblAddItemHints.ForeColor = Color.Red;
            //    return;
            //}
            //lblConsumhints.Text = "物品消耗成功!";
            //lblConsumhints.ForeColor = Color.Green;
            //int itemIdx = FindBagItemByID(item.ItemID);
            //if (itemIdx != -1)
            //{
            //    player.Bags.Bag[itemIdx].Count -= item.Count;
            //    if (player.Bags.Bag[itemIdx].Count <= 0)
            //    {
            //        player.Bags.Bag.RemoveAt(itemIdx);
            //    }
            //}
            //else
            //{
            //    lblConsumhints.Text = "物品消耗失败!";
            //    lblConsumhints.ForeColor = Color.Red;
            //}

            #endregion

            #region Test

            int idx = FindEquipBagItemByID(id);
            if (idx != -1)
            {
                player.Bags.Equipment[idx].Count -= count;
                if (player.Bags.Equipment[idx].Count <= 0)
                {
                    player.Bags.Equipment.RemoveAt(idx);
                }

            }
            else
            {
                return;
            }
            #endregion

            InitBagInfo();
        }

        private void dataGridViewSearch_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);

        }

        private void btnequipment_Click(object sender, EventArgs e)
        {
            int id;
            int count;
            if (!isLogined)
            {
                return;
            }

            if (!Int32.TryParse(txtBoxEquipmentID.Text.Trim(), out id) ||
                !Int32.TryParse(txtBoxEquipmentCnt.Text.Trim(), out count))
            {
                return;
            }

            int bagIdx = FindBagItemByID(id);
            if (bagIdx == -1)
            {
                return;
            }

            int equipIdx = FindEquipBagItemByID(id);
            int cnt;

            cnt = (player.Bags.Bag[bagIdx].Count -= count);
            if (cnt <= 0)
            {
                player.Bags.Bag.RemoveAt(bagIdx);
            }
            EquipItem equipItem = new EquipItem();
            equipItem.Count = count;
            equipItem.ItemID = id;
            equipItem.TypeID = 1;
            equipItem.Attack = 500;
            equipItem.Binded = false;
            equipItem.Durability = 500;
            player.Bags.Equipment.Add(equipItem);

            InitBagInfo();
        }

        private void btnUnequip_Click(object sender, EventArgs e)
        {
            int id;
            int count;
            if (!isLogined)
            {
                return;
            }

            if (!Int32.TryParse(txtBoxUnequipID.Text.Trim(), out id) ||
                !Int32.TryParse(txtBoxUnequipCnt.Text.Trim(), out count))
            {
                return;
            }

            int equipIdx = FindEquipBagItemByID(id);
            if (equipIdx == -1)
            {
                return;
            }

            int bagIdx = FindBagItemByID(id);
            int cnt;
            cnt = (player.Bags.Equipment[equipIdx].Count -= count);
            if (bagIdx == -1)
            {
                BagItem item = new BagItem();
                item.Count = count;
                item.ItemID = id;
                player.Bags.Bag.Add(item);
            }
            else
            {
                player.Bags.Bag[bagIdx].Count += count;
            }
            if (cnt <= 0)
            {
                player.Bags.Equipment.RemoveAt(equipIdx);
            }

            InitBagInfo();
        }
    }
}
