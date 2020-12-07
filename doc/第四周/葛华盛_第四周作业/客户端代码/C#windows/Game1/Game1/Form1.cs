using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCPSocket;
using Buff;
using GameSpec;
using System.Net.Sockets;
using System.IO;
using Google.Protobuf;
using Google.Protobuf.Collections;
using MySql.Data.MySqlClient;
using Game;

namespace Game1
{
    public partial class GameForm : Form
    {
        private SocketError socketError;
        private Buffers buffers = new Buffers();
        private TCPSocketClient tcpSocket;
        private bool isLogined = false;
        private FileInfo[] fileInfos;
        private Dictionary<int, string> dicId2File;
        private Players player;
        private object chkBoxPws;
        ImageList imgList ;
        private object dataGridViewSearch;

        public GameForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dicId2File = new Dictionary<int, string>();
            BagList.Items.Clear();
            BagList.View = View.LargeIcon;
            string filePath = @"../../resource";
            DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
            imgList = new ImageList();
            imgList.ImageSize = new Size(32, 32);


            fileInfos = directoryInfo.GetFiles();// GetFiles();
            for (int i = 0; i < fileInfos.Length; i++)
            {
                dicId2File.Add(i, fileInfos[i].FullName);
                imgList.Images.Add(Image.FromFile(fileInfos[i].FullName));
            }

            string ip = "10.0.150.52";
            int port = 3000;
            tcpSocket = new TCPSocketClient(ip, port);
            buffers.Init(1024, 1024 * 1024);

            Password.PasswordChar = '*';

            this.BackgroundImage = Image.FromFile("../../bkground.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string name = Username.Text.Trim();
            string password = Password.Text.Trim();
            if(String.IsNullOrEmpty(name) || String.IsNullOrEmpty(password))
            {
                RemindLable.Text = "请输入合法的用户名密码！";
                RemindLable.ForeColor = Color.Red;
                return;
            }
            CtlMsgLoginReq login = new CtlMsgLoginReq();
            login.Name = name;
            login.Password = password;

            Byte[] temp = new byte[1024];
            byte[] data = new byte[1024];
            using (CodedOutputStream cos = new CodedOutputStream(temp))
            {
                login.Name = name;
                login.Password = password;
                login.WriteTo(cos);
            }

            Buffers.Encode(data, 9999, 1, login.CalculateSize());
            for (int i = 0; i < login.CalculateSize(); i++)
            {
                data[sizeof(Int32) * 3 + i] = temp[i];
            }

            if (tcpSocket.Connected())
            {
                tcpSocket.Send(data, 0, sizeof(Int32) * 3 + login.CalculateSize(), out socketError);
            }

            MsgInfo msg = null;
            while(true)
            {
                int ret = tcpSocket.Receive(buffers, out socketError);
                if (buffers.IsHeaderReadable(sizeof(Int32) * 3))
                {
                    msg = buffers.Decode();
                    if (buffers.IsHeaderAndPayloadReadable(msg.GetPacLen() + sizeof(Int32) * 3))
                    {
                        buffers.GetPayload(msg);
                        buffers.SetStartByLen(msg.GetPacLen() + sizeof(Int32) * 3);
                        break;
                    }
                }
            }

            CtlMsgLoginRsp loginRsp = CtlMsgLoginRsp.Parser.ParseFrom(msg.GetPayload());
            if(loginRsp.ErrCode != ErrorCode.ErrorNoError)
            {
                RemindLable.Text = "登录失败！";
                RemindLable.ForeColor = Color.Red;
                return;
            }
            RemindLable.Text = "登录成功！";
            RemindLable.ForeColor = Color.Green;
            isLogined = true;
            player = loginRsp.Player;
            LoginButton.Visible = false;
            checkBox.Visible = false;

            InitBagInfo();
            foreach (var curitem in player.Bags.Currency)
            {
                if(curitem.ItemID == -1)
                {
                    continue;
                }
                switch(curitem.TypeID)
                {
                    case 0:
                        label7.Text = "金币：" + curitem.Count;
                        break;
                    case 1:
                        label10.Text = "银币：" + curitem.Count;
                        break;
                    case 2:
                        label11.Text = "钻石：" + curitem.Count;
                        break;
                    default:
                        break;
                }

            }
            
        }
        private void InitBagInfo()
        {
            BagList.Items.Clear();
            EquipList.Items.Clear();
            this.BagList.BeginUpdate();
            this.EquipList.BeginUpdate();

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
                BagList.Items.Add(item);
            }
            BagList.LargeImageList = imgList;
            BagList.Show();
            

            RepeatedField<EquipItem> equips = player.Bags.Equipment;
            for (int i = 0; i < equips.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                //imgList.Images.Add(Image.FromFile(dicId2File[bags[i].ItemID]));
                if (!dicId2File.ContainsKey(equips[i].ItemID))
                {
                    continue;
                }
                item.Text = Path.GetFileNameWithoutExtension(dicId2File[equips[i].ItemID]) + ":" + equips[i].Count;
                item.ImageIndex = equips[i].ItemID;
                EquipList.Items.Add(item);
            }
            EquipList.LargeImageList = imgList;
            EquipList.Show();


            this.BagList.EndUpdate();
            this.EquipList.EndUpdate();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox.Checked)
            {
                //复选框被勾选，明文显示
                Password.PasswordChar = new char();
            }
            else
            {
                //复选框被取消勾选，密文显示
                Password.PasswordChar = '*';
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void AddItemButton_Click(object sender, EventArgs e)
        {
            if(isLogined == false)
            {
                RemindLable.Text = "请正确登陆后操作！";
                RemindLable.ForeColor = Color.Red;
                return;
            }


            int id;
            int count;
            if (!Int32.TryParse(AddItemText.Text.Trim(), out id) || !Int32.TryParse(AddItemCount.Text.Trim(), out count))
            {
                RemindLable.Text = "请输入正确的id和count!";
                RemindLable.ForeColor = Color.Red;
                return;
            }
            RemindLable.Text = "添加成功！";
            RemindLable.ForeColor = Color.Green;
            Byte[] temp = new byte[1024];
            byte[] data = new byte[1024];
            AddItemReq item = new AddItemReq();
            item.ItemID = id;
            item.Count = count;
            using (CodedOutputStream cos = new CodedOutputStream(temp))
            {
                item.WriteTo(cos);
            }
            Buffers.Encode(data, 10000, player.Id, item.CalculateSize());
            for (int i = 0; i < item.CalculateSize(); i++)
            {
                data[sizeof(Int32) * 3 + i] = temp[i];
            }

            if (tcpSocket.Connected())
            {
                tcpSocket.Send(data, 0, sizeof(Int32) * 3 + item.CalculateSize(), out socketError);
            }

            MsgInfo msg = null;
            while (true)
            {
                int ret = tcpSocket.Receive(buffers, out socketError);
                if (buffers.IsHeaderReadable(sizeof(Int32) * 3))
                {
                    msg = buffers.Decode();
                    if (buffers.IsHeaderAndPayloadReadable(msg.GetPacLen() + sizeof(Int32) * 3))
                    {
                        buffers.GetPayload(msg);
                        buffers.SetStartByLen(msg.GetPacLen() + sizeof(Int32) * 3);
                        break;
                    }
                }
            }

            AddItemRsp addRsp = AddItemRsp.Parser.ParseFrom(msg.GetPayload());
            if (addRsp.ErrCode != ErrorCode.ErrorNoError)
            {
                RemindLable.Text = "添加失败！";
                RemindLable.ForeColor = Color.Red;
                return;
            }

            BagInfo bag = player.Bags;
            for(int i=0;i<bag.Bag.Count;i++)
            {
                if(bag.Bag[i] == null)
                {
                    continue;
                }
                if(bag.Bag[i].ItemID == item.ItemID)
                {
                    bag.Bag[i].Count += item.Count;  // 我发送的请求生效，则在客户端同步
                    break;
                }
            }

            InitBagInfo();



        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void BagList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isLogined == false)
            {
                RemindLable.Text = "请正确登陆后操作！";
                RemindLable.ForeColor = Color.Red;
                return;
            }

            int id;
            int count;
            if (!Int32.TryParse(RemoveItemText.Text.Trim(), out id) || !Int32.TryParse(RemoveItemCount.Text.Trim(), out count))
            {
                RemindLable.Text = "请输入正确的id和count!";
                RemindLable.ForeColor = Color.Red;
                return;
            }
            RemindLable.Text = "删除成功！";
            RemindLable.ForeColor = Color.Green;
            Byte[] temp = new byte[1024];
            byte[] data = new byte[1024];
            AddItemReq item = new AddItemReq();
            item.ItemID = id;
            item.Count = count;
            using (CodedOutputStream cos = new CodedOutputStream(temp))
            {
                item.WriteTo(cos);
            }
            Buffers.Encode(data, 10001, player.Id, item.CalculateSize());
            for (int i = 0; i < item.CalculateSize(); i++)
            {
                data[sizeof(Int32) * 3 + i] = temp[i];
            }

            if (tcpSocket.Connected())
            {
                tcpSocket.Send(data, 0, sizeof(Int32) * 3 + item.CalculateSize(), out socketError);
            }

            MsgInfo msg = null;
            while (true)
            {
                int ret = tcpSocket.Receive(buffers, out socketError);
                if (buffers.IsHeaderReadable(sizeof(Int32) * 3))
                {
                    msg = buffers.Decode();
                    if (buffers.IsHeaderAndPayloadReadable(msg.GetPacLen() + sizeof(Int32) * 3))
                    {
                        buffers.GetPayload(msg);
                        buffers.SetStartByLen(msg.GetPacLen() + sizeof(Int32) * 3);
                        break;
                    }
                }
            }

            RemoveItemRsp addRsp = RemoveItemRsp.Parser.ParseFrom(msg.GetPayload());
            if (addRsp.ErrCode != ErrorCode.ErrorNoError)
            {
                RemindLable.Text = "删除失败！";
                RemindLable.ForeColor = Color.Red;
                return;
            }

            BagInfo bag = player.Bags;
            for (int i = 0; i < bag.Bag.Count; i++)
            {
                if (bag.Bag[i] == null)
                {
                    continue;
                }
                if (bag.Bag[i].ItemID == item.ItemID)
                {
                    bag.Bag[i].Count -= item.Count;  // 我发送的请求生效，则在客户端同步
                    if(bag.Bag[i].Count <= 0)
                    {
                        bag.Bag.RemoveAt(i);
                    }
                    break;
                }
            }
            InitBagInfo();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (!isLogined)
            {
                RankListHints.Text = "请登录账户和密码！";
                return;
            }
            
            string connstr = "server=10.0.150.52;user id=root;password=;database=game";
            using (MySqlConnection connection = new MySqlConnection(connstr))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    int start = Int32.Parse(RankListStart.Text.Trim());
                    int end = Int32.Parse(RankListEnd.Text.Trim());
                    command.Connection = connection;
                    connection.Open();
                    string sql = "select id, playerName, rank, exp from players order by exp desc limit @offset, @count";
                    int offset = start;

                    int count = end - start;
                    count = count > 0 ? count : 0;
                    //MySqlParameter idParameter = new MySqlParameter("@i", MySqlDbType.Int32);
                    MySqlParameter[] nameParameter =
                    {
                        new MySqlParameter("@offset", MySqlDbType.Int32),
                        new MySqlParameter("@count", MySqlDbType.Int32)
                    };
                    nameParameter[0].Value = offset;
                    nameParameter[1].Value = count;
                    MySqlDataReader dataReader = SqlHelper.ExecuteReader(SqlHelper.Conn, CommandType.Text, sql, nameParameter);

                    DataTable dt = new DataTable();
                    dt.Load(dataReader);
                    RankList.DataSource = dt;
                }
            }
            /*
            if (isLogined == false)
            {
                RemindLable.Text = "请正确登陆后操作！";
                RemindLable.ForeColor = Color.Red;
                return;
            }*/

            /*
            int start = Int32.Parse(RankListStart.Text.Trim());
            int end = Int32.Parse(RankListEnd.Text.Trim());

            if (!Int32.TryParse(RankListStart.Text.Trim(), out start) || !Int32.TryParse(RankListEnd.Text.Trim(), out end))
            {
                return;
            }

            Byte[] temp = new byte[1024];
            byte[] data = new byte[1024];
            RankReq item = new RankReq();
            item.Start = start;
            item.End = end;
            using (CodedOutputStream cos = new CodedOutputStream(temp))
            {
                
                item.WriteTo(cos);
            }
            Buffers.Encode(data, 10003, 88, item.CalculateSize());
            for (int i = 0; i < item.CalculateSize(); i++)
            {
                data[sizeof(Int32) * 3 + i] = temp[i];
            }

            if (tcpSocket.Connected())
            {
                tcpSocket.Send(data, 0, sizeof(Int32) * 3 + item.CalculateSize(), out socketError);
            }

            MsgInfo msg = null;
            while (true)
            {
                int ret = tcpSocket.Receive(buffers, out socketError);
                if (buffers.IsHeaderReadable(sizeof(Int32) * 3))
                {
                    msg = buffers.Decode();
                    if (buffers.IsHeaderAndPayloadReadable(msg.GetPacLen() + sizeof(Int32) * 3))
                    {
                        buffers.GetPayload(msg);
                        buffers.SetStartByLen(msg.GetPacLen() + sizeof(Int32) * 3);
                        break;
                    }
                }
            }

            RankRsp rankRsp = RankRsp.Parser.ParseFrom(msg.GetPayload());
            if (rankRsp.ErrCode != ErrorCode.ErrorNoError)
            {
                return;
            }
            for (int i = 0; i < rankRsp.Name.Count; i++)
            {
                int index = this.RankList.Rows.Add(rankRsp.Name[i],rankRsp.Exp[i]);

            }*/



        }

        private void label8_Click(object sender, EventArgs e)
        {
          
            

        }

        private void RankList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
