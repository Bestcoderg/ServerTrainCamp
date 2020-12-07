using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCPSocket;
using Buff;
using System.Net.Sockets;
using System;

public class GameEntry : MonoBehaviour
{
    public UIManager ui_manager_;
    public List<MBaseSingleton> mSingletons;
    public Player m_PlayerSelf;

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        ui_manager_ = new UIManager();
        //Debug.LogError("GameEntity awake !!!");
        
        mSingletons = new List<MBaseSingleton>();
        mSingletons.Add(MonstersMgr.singleton);
        mSingletons.Add(PlayerMgr.singleton);

        
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i=0;i<mSingletons.Count;i++)
        {
            mSingletons[i].Init();
        }
        m_PlayerSelf = new Player();
        m_PlayerSelf.Awake();
        m_PlayerSelf.Start();

        ui_manager_.OnMainCity();

    }

    // Update is called once per frame
    void Update()
    {

        m_PlayerSelf.Update();
        PlayerMgr.singleton.Update();
        //Debug.Log();
        MonstersMgr.singleton.Update();
        ui_manager_.UpdateHpLine(m_PlayerSelf.GetHP());


    }

    private void FixedUpdate()
    {
        PlayerMgr.singleton.FixedUpdate();
    }

    private void LateUpdate()
    {
        PlayerMgr.singleton.LateUpdate();
    }
    
    private void OnDestroy()
    {
        for (int i = 0; i < mSingletons.Count; i++)
        {
            mSingletons[i].Uninit();

        }
    }

}
