using Buff;
using Google.Protobuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TCPSocket;
//using System.Numerics;
using UnityEngine;


public class PlayerMgr : MSingleton<PlayerMgr>
{
    //public List<Player> m_PlayerList;
    private Player m_PlayerSelf;
    public Dictionary<int, Player> m_PlayerList;

    Buffers buffers;
    SocketError se;
    private TCPSocketClient net_manager;
    private Buffers buffer_recv;
    private MsgInfo msgInfo;
    private MSG_PLAYER_MOVE msgtemp;
    private MSG_PLAYER_REGISTER msgtemp_register;
    private MSG_PLAYER_REGISTER msgtemp_register_recv;
    private MSG_PLAYER_REGISTER msgRegister;
    private Byte[] data = new byte[1024 * 1024];
    private PlayerStateManager psm;

    DateTime dtUpdateRun;


    public override bool Init()
    {
        dtUpdateRun = DateTime.Now;
        m_PlayerList = new Dictionary<int, Player>();
        m_PlayerList.Clear();
        psm = new PlayerStateManager();
        net_manager = new TCPSocketClient("10.0.150.52", 3000);
        buffer_recv = new Buffers();
        buffer_recv.Init(128, 1024 * 1024); //small buffer & big buffer





        return base.Init();
    }

    public override void Uninit()
    {
        base.Uninit();
    }

    public void OnLogout(bool clearAccountData = true)
    {

    }

    public void Update()
    {
        foreach (var item in m_PlayerList)
        {
            if(item.Value.m_movehistory.isPredictable())
            {
                Vector3 vec = item.Value.GetSelf().transform.position;
                float ry = item.Value.GetSelf().transform.rotation.y;

                MoveState ms = item.Value.m_movehistory.getMoveDeltaPrediction(vec.x, vec.y, vec.z, ry);
                item.Value.pos_update(ms.x, ms.z, ms.get_facing_direc());
            }

        }

    }

    public void FixedUpdate()
    {
        if (net_manager.Connected()) net_manager.Receive(buffer_recv, out se);
        //Debug.Log("!!!!!!" + buffer_recv.GetDataSize() + " " + buffer_recv.GetStart() + " " + buffer_recv.GetEnd());
        while (true)
        {
            if (buffer_recv.IsHeaderReadable(3 * sizeof(Int32)) == false)
            {
                break;
            }

            msgInfo = buffer_recv.Decode();

            if (buffer_recv.IsHeaderAndPayloadReadable(3 * sizeof(Int32) + msgInfo.GetPacLen()) == false)
            {
                break;
            }


            buffer_recv.GetPayload(msgInfo);
            switch (msgInfo.GetMsgID())
            {
                case 1006:
                    msgtemp = MSG_PLAYER_MOVE.Parser.ParseFrom(msgInfo.GetPayload());
                    Debug.LogError("Recv mov: x = " + msgtemp.X + ", z = " + msgtemp.Z + ".    ry = " + msgtemp.Ry);

                    update_player(msgtemp.Playerid, ((float)msgtemp.X) / 100000, ((float)msgtemp.Z) / 100000, ((float)msgtemp.Ry) / 100000, msgtemp.State);
                    break;
                case 1008:
                    msgtemp_register_recv = MSG_PLAYER_REGISTER.Parser.ParseFrom(msgInfo.GetPayload());
                    Debug.LogError("Recv reg: x = " + msgtemp_register_recv.X + ", z = " + msgtemp_register_recv.Z + ".    Ry = ");

                    update_player(msgtemp_register_recv.Playerid, ((float)msgtemp_register_recv.X) / 100000, ((float)msgtemp_register_recv.Z) / 100000, ((float)msgtemp_register_recv.Ry) / 100000, 0);
                    break;
            }
        }



        m_PlayerSelf.FixedUpdate();
    }

    public void LateUpdate()
    {
        m_PlayerSelf.LateUpdate();
    }
    


    public void RegistePlayerSelf(Player player, Int32 uid, Int32 ry, Int32 x, Int32 y)
    {
        if (player == null)
        {
            return;
        }
        m_PlayerSelf = player;

        // 客户端发送register信息
        msgRegister = new MSG_PLAYER_REGISTER();
        msgtemp_register = new MSG_PLAYER_REGISTER();
        Byte[] temp = new byte[1024];
        using (CodedOutputStream cos = new CodedOutputStream(temp))
        {
            msgRegister.Playerid = uid;
            msgRegister.Ry = ry;
            msgRegister.Professsional = 0;
            msgRegister.X = x;
            msgRegister.Z = y;
            msgRegister.Speed = 0;
            msgRegister.WriteTo(cos);
            //data = cos.to.ToArray();
        }
        Buffers.Encode(data, 1008, uid, msgRegister.CalculateSize()); 
        for (int i = 0; i < msgRegister.CalculateSize(); i++)
        {
            data[sizeof(Int32) * 3 + i] = temp[i];
        }
        net_manager.Send(data, 0, sizeof(Int32) * 3 + msgRegister.CalculateSize(), out se);

    }

    public void PlayerMove(Int32 x, Int32 z, Int32 ry ,Int32 uid, Int32 state)
    {
        
        // 客户端发送move信息
        msgtemp = new MSG_PLAYER_MOVE();
        Byte[] temp = new byte[1024];
        using (CodedOutputStream cos = new CodedOutputStream(temp))
        {
            //Debug.LogError("Send mov: x = " + x + ", z = " + z + ".");

            msgtemp.Ry = ry;
            msgtemp.X = x;
            msgtemp.Z = z;
            msgtemp.Playerid = uid;
            msgtemp.State = state;
            msgtemp.WriteTo(cos);
            //Debug.LogError("-------------------------state   " + state);
            
            //data = cos.to.ToArray();
        }
        Buffers.Encode(data, 1006, uid, msgtemp.CalculateSize()); // 1006 -> player move
        for (int i = 0; i < msgtemp.CalculateSize(); i++)
        {
            data[sizeof(Int32) * 3 + i] = temp[i];
        }
        net_manager.Send(data, 0, sizeof(Int32) * 3 + msgtemp.CalculateSize(), out se);
        
    }

    public void MovePlayer(int uif, Vector3 pos, Vector3 rot)
    {
        
    }

    public void BroadMsg(int uif, string msg)
    {

    }

    public int[] GetBagItems()
    {
        return m_PlayerSelf.GetBagItems();
    }

    public int AddBagItem(int itemID)
    {
        return m_PlayerSelf.AddBagItem(itemID);
    }

    public int DelBagItem()
    {
        return m_PlayerSelf.DelBagItem();
    }

    public Vector3 GetPlayerPosition()
    {
        return m_PlayerSelf.GetSelf().transform.position;
    }

    public void DecPlayerHP(int damage)
    {
        m_PlayerSelf.DecHP(damage);
    }
    public void update_player(int player_id, float X, float Z, float R, Int32 state)
    {
        if(player_id == m_PlayerSelf.GetUID())
        {
            return;
        }
        //Debug.LogError("player_id:" + player_id + " updated: (" + X + ", " + Z + "）, facing：" + R + "");
        
        
        // 新的player id就新建玩家
        
        if (!m_PlayerList.ContainsKey(player_id))
        {
            Player t_player = new Player();
            t_player.Init(player_id);

            t_player.pos_update();
            m_PlayerList.Add(player_id, t_player);

        }
        else
        {   //已有玩家的话就更新玩家位置
            Player temp;
            if (m_PlayerList.TryGetValue(player_id, out temp))
            {
                temp.pos_update(X, Z, R);
            }
            
        }
        UpdatePlayerState(player_id, state);
    }

    private void UpdatePlayerState(int playerID, int state)
    {
        GameObject playerTemp;
        if (playerID == m_PlayerSelf.GetUID())
        {
            return;
        }
        else
        {
            if (!psm.Contains(playerID))
            {
                psm.AddPlayerState(playerID, new PlayerState(0.9));
            }

            playerTemp = m_PlayerList[playerID].GetSelf();//psm.GetPlayer(playerID).GetPlayer();
            PlayerState playerState = psm.GetPlayerState(playerID);
            Animator playerAnimator = playerTemp.GetComponent<Animator>();

            if ((state & (int)KeyboardState.Up) != 0 || (state & (int)KeyboardState.Down) != 0 || (state & (int)KeyboardState.Left) != 0 || (state & (int)KeyboardState.Right) != 0)
            {

                playerAnimator.SetBool("walk", false);

                if((state & (int)KeyboardState.Up) != 0)
                {
                    //=============================== change =================================//
                    playerAnimator.SetBool("walk", true);
                }

            }
            else
            {

                playerAnimator.SetBool("walk", false);
                playerAnimator.SetBool("stand", true);
            }

            if ((state & (int)KeyboardState.E) != 0)
            {
                playerAnimator.SetTrigger("attackT2");
            }
            else if ((state & (int)KeyboardState.Q) != 0)
            {
                playerAnimator.SetTrigger("attackT1");
            }

        }
    }
}

