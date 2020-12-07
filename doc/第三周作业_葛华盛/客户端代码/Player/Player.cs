using Google.Protobuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
//using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
public class Player //: MonoBehaviour
{
    public PlayerInfo m_playerinfo;
    private PlayerBag m_playerbag;
    private CharacterController p_cc;
    private Animator p_animator;
    private GameObject player = null;
    public float rotate_Y = 0f;
    private Button m_skillbtn1;
    private Button m_skillbtn2;

    public PlayerMoveStateHistory m_movehistory;

    /*
    private MSG_PLAYER_REGISTER msgRegister;
    private MSG_PLAYER_MOVE msgtemp;
    private MSG_PLAYER_REGISTER msgtemp_register;
    private MSG_PLAYER_REGISTER msgtemp_register_recv;
    */
    public float cameraDistance = 3.0f;
    public float cameraHight = 0.9f;
    private Vector3 cameraPosition;

    GameObject camera;

    public Player()
    {

    }

    public bool Init(int uid)
    {
        m_playerinfo = new PlayerInfo(uid);
       

        //PlayerMgr.singleton.RegistePlayerSelf(this);

        player = (GameObject)Resources.Load("Prefabs/Player_warrior_ghs");
        player = GameObject.Instantiate(player);
        //player = GameObject.Find("Player_warrior_ghs(Clone)");

        //player.transform.position = new Vector3(100f,0.11f,118f);



        //player = GameObject.Find("Player_warrior");
        p_cc = player.GetComponent<CharacterController>();
        p_animator = player.transform.GetComponent<Animator>();
        m_movehistory = new PlayerMoveStateHistory();


        return true;
    }

    public void Awake()
    {
        m_playerinfo = new PlayerInfo(UnityEngine.Random.Range(1, 1000000));

        m_playerbag = new PlayerBag();
        
        //p_cc = GetComponent<CharacterController>();
        player = GameObject.Find("Player_warrior");
        p_cc = player.GetComponent<CharacterController>();
        p_animator = player.transform.GetComponent<Animator>();

        m_skillbtn1 = GameObject.Find("/Canvas/Skill1").GetComponent<Button>();
        m_skillbtn2 = GameObject.Find("/Canvas/Skill2").GetComponent<Button>();
        if (m_skillbtn1 == null)
        {
            Debug.LogError("m_skillbtn1 is null");
        }
        if (m_skillbtn2 == null)
        {
            Debug.LogError("m_skillbtn2 is null");
        }
        m_skillbtn1.onClick.AddListener(OnClickSkill);
        m_skillbtn2.onClick.AddListener(OnClickSkill2);
        PlayerMgr.singleton.RegistePlayerSelf(this, m_playerinfo.uid, (Int32)(player.transform.rotation.y* 100000) , (Int32)(player.transform.position.x * 100000), (Int32)(player.transform.position.z* 100000) );

        camera = GameObject.Find("Main Camera");
        if (camera == null)
        {
            Debug.Log("camera  is   null");
        }

        cameraPosition = new Vector3(0, 0, 0);
        //放置相机的位置
        camera.transform.position = player.transform.position - player.transform.forward * cameraDistance;
        camera.transform.LookAt(player.transform.position);
        cameraPosition.x = camera.transform.position.x;
        cameraPosition.y = camera.transform.position.y + cameraHight;
        cameraPosition.z = camera.transform.position.z;
        camera.transform.position = cameraPosition;
    }



    // Start is called before the first frame update
    public void Start()
    {

        m_playerinfo.SetState(0);
        p_animator.SetBool(m_playerinfo.GetState(0), true);

    }

    // Update is called once per frame
    public void Update()
    {
        //player.transform.Rotate(Vector3.up * 100);
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            Vector3 playerForward = player.transform.TransformDirection(Vector3.forward);
            Vector3 playerRight = player.transform.TransformDirection(Vector3.right);

            p_animator.SetBool(m_playerinfo.GetState(m_playerinfo.state), false);
           
            if (Input.GetKey("w") && Input.GetKey(KeyCode.LeftShift))
            {
                //=============================== change =================================//
                m_playerinfo.SetState(2);
                p_animator.SetBool(m_playerinfo.GetState(2), true);
                m_playerinfo.speed = 200f;
            }
            else
            {
                //=============================== change =================================//
                m_playerinfo.SetState(1);
                p_animator.SetBool(m_playerinfo.GetState(1), true);
                m_playerinfo.speed = 100f;
            }

            if (Input.GetAxis("Horizontal") != 0)
            {
                rotate_Y += Input.GetAxis("Horizontal") * m_playerinfo.rotateSpeed[1];
                if (rotate_Y < 0)
                {
                    rotate_Y += 360;
                }
                if (rotate_Y > 360) 
                {
                    rotate_Y -= 360;
                }

            }
        }
        else if (Input.GetKeyDown("e"))
        {
            p_animator.SetTrigger("attackT2");
        }
        else if (Input.GetKeyDown("q"))
        {
            p_animator.SetTrigger("attackT1");
        }
        else
        {
            int t_s = m_playerinfo.GetStateNow();
            if (t_s == 0)
            {
                return;
            }
            p_animator.SetBool(m_playerinfo.GetState(t_s), false);
            m_playerinfo.SetState(0);
            p_animator.SetBool(m_playerinfo.GetState(0), true);
        }
    }

    public void SetStand()
    {
        int t_s = m_playerinfo.GetStateNow();
        if (t_s == 0)
        {
            return;
        }
        p_animator.SetBool(m_playerinfo.GetState(t_s), false);
        m_playerinfo.SetState(0);
        p_animator.SetBool(m_playerinfo.GetState(0), true);
    }

    public void FixedUpdate()
    {

        //移动玩家角色
        Vector3 player_forward = player.transform.forward.normalized;
        // 发送玩家z轴上想到达的下一个位置
        Vector3 player_position = player.transform.position;

        // 客户端预计达到的位置
        float level_movement = Input.GetAxis("Vertical")/5; // * player_forward_speed * Time.deltaTime;
        player.transform.position = new Vector3(player_position.x + level_movement * player_forward.x / 15, player_position.y, player_position.z + level_movement * player_forward.z / 15);
        Vector3 desired_pos = new Vector3(player_position.x + level_movement * player_forward.x ,
            player_position.y, player_position.z + level_movement * player_forward.z );

        // 客户端预计的朝向

        player.transform.localEulerAngles = new Vector3(0, rotate_Y, 0);


        Int32 p_state = GetKeyBoardState();
        //客户端发送move
        Debug.Log("rotate keybodstate = "+ p_state);
        PlayerMgr.singleton.PlayerMove( (Int32)(player.transform.position.x *100000), (Int32)(player.transform.position.z * 100000), (Int32)(player.transform.eulerAngles.y * 100000) , m_playerinfo.uid, p_state);

    }

    public void LateUpdate()
    {
        //放置相机的位置

            camera.transform.position = player.transform.position - player.transform.forward * cameraDistance;
            camera.transform.LookAt(player.transform.position);
            cameraPosition.x = camera.transform.position.x;
            cameraPosition.y = camera.transform.position.y + cameraHight;
            cameraPosition.z = camera.transform.position.z;
            camera.transform.position = cameraPosition;
        
    }

    private void CheckEnemy(int des)
    {
        //Debug.LogError("CheckEnemy   !!!!");
        if (MonstersMgr.singleton == null)
        {
            Debug.LogError("singleton is null   !!!!");
        }
        MonstersMgr.singleton.CheckEnemy(des, player.transform.position.x, player.transform.position.z, m_playerinfo.player_R, player.transform.localEulerAngles.y, m_playerinfo.player_angle);
    }  

    public int GetUID()
    {
        return m_playerinfo.uid;
    }

    public int[] GetBagItems()
    {
        return m_playerbag.GetItems();
    }

    public int AddBagItem(int itemID)
    {
        return m_playerbag.AddItem(itemID);

    }

    public int DelBagItem()
    {
        return m_playerbag.DelItem();

    }
    
    public GameObject GetSelf()
    {
        return player;
    }

    public int GetHP()
    {
        return m_playerinfo.GetHP();
    }

    public void DecHP(int damage)
    {
        m_playerinfo.DecHP(damage);
    }

    public void OnClickSkill()
    {
        p_animator.SetTrigger("attackT1");
        CheckEnemy(30);
    }
    public void OnClickSkill2()
    {
        p_animator.SetTrigger("attackT2");
        CheckEnemy(50);
    }


    // 更新玩家操控发的GameObject的位置
    public void pos_update()
    {
        if (player)
        {
            //player.transform.rotation.
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            player.transform.forward = facer_direc_to_forward_vecter3(player.transform.rotation.y);
            player.GetComponent<Animator>().SetBool("stand", true);
        }
        else
        {
            Debug.LogError("没有玩家的GameObject");
        }
    }

    // 更新玩家操控发的GameObject的位置
    public void pos_update(float X,float Z, float face_direc)
    {
        if (player)
        {
            //Debug.LogError("@@@@@"+player.transform.position.x+",....." + player.transform.position.z);
            //Debug.LogError("__________________________________________________________________________________player move" + player.name +"    "+  m_playerinfo.uid);
            //Debug.LogError("   --  "+player.name);
            //player = GameObject.Find("Player_warrior_ghs(Clone)");
            player.transform.position = new Vector3(X, player.transform.position.y, Z);
            
            //player.transform.Translate(Vector3.forward * 5);

            //GameObject test = (GameObject)Resources.Load("Prefabs/Cube");
            //GameObject.Instantiate(test, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), Quaternion.identity);


            //Debug.LogError("@@@@@" + player.transform.position.x+",....." + player.transform.position.z);


           
            //player.transform.forward = facer_direc_to_forward_vecter3(face_direc);
            player.transform.localEulerAngles = new Vector3( 0, face_direc, 0);
            m_movehistory.add_to_history(X, player.transform.position.y, Z, face_direc);


        }
        else
        {
            Debug.LogError("没有玩家的GameObject");
        }
    }
    public static Vector3 facer_direc_to_forward_vecter3(float face_direc)
    {
        return new Vector3(Mathf.Cos(face_direc), 0, Mathf.Sin(face_direc));
        //return new Vector3(Mathf.Sin(face_direc), 0, Mathf.Cos(face_direc));
    }

    private Int32 GetKeyBoardState()
    {
        Int32 state = 0;
        bool behaviorUpDown = false;
        bool behaviorRightLeft = false;
        if (Input.GetKey("w"))
        {
            state |= (Int32)KeyboardState.Up;
            behaviorUpDown = true;
        }

        if (Input.GetKey("s") && !behaviorUpDown)
        {
            state |= (Int32)KeyboardState.Down;
            behaviorUpDown = true;
        }

        if (Input.GetKey("a"))
        {
            behaviorRightLeft = true;
            state |= (Int32)KeyboardState.Left;
        }

        if (Input.GetKey("d") && !behaviorRightLeft)
        {
            state |= (Int32)KeyboardState.Right;
            behaviorRightLeft = true;
        }

        if (Input.GetKey("q"))
        {
            state |= (Int32)KeyboardState.Q;
        }

        if (Input.GetKey("e"))
        {
            state |= (Int32)KeyboardState.E;
        }

        if (Input.GetKey("r"))
        {
            state |= (Int32)KeyboardState.R;
        }

        if (Input.GetKey("space"))
        {
            state |= (Int32)KeyboardState.Jump;
        }

        return state;
    }
}
