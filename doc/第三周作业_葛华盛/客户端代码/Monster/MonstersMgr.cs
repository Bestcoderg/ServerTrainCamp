using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonstersMgr : MSingleton<MonstersMgr>
{
    public List<MonsterInfo> m_MonsterList;
    public GameObject[] m_Monsters;
 
    public override bool Init()
    {
        m_MonsterList = new List<MonsterInfo>();
        InitAllMonster();
        return base.Init();
    }

    public override void Uninit()
    {
        base.Uninit();
    }

    public void InitAllMonster()
    {
        m_MonsterList = new List<MonsterInfo>();
        m_MonsterList.Clear();
        

        m_Monsters = GameObject.FindGameObjectsWithTag("Monster");
        int t_len = m_Monsters.Length;
        for (int i = 0; i< t_len; i++)
        {
            m_MonsterList.Add(new MonsterInfo(m_Monsters[i]));
        }
        
        return;
    }

    //保持看向人物
    public void lookatPlayer(Vector3 pos)
    {
        for (int index = 0; index<m_MonsterList.Count; index++)
        {
            m_MonsterList[index].lookatPlayer(pos);
        }
        return;
    }
    
    //看有没有monster被攻击到，死了的monster从list删除
    public void CheckEnemy(int des, float px, float pz, float player_R, float forward, float player_angle)
    {
        for (int index = m_MonsterList.Count-1; index >= 0; index--)
        {
            if (m_MonsterList[index].Getblood() == 0)
            {
                continue;
            }
            m_MonsterList[index].CheckEnemy(des, px, pz, player_R, forward, player_angle);
        }
        return;
    }

    public void Update()
    {
        lookatPlayer(PlayerMgr.singleton.GetPlayerPosition());
        int t_len = m_MonsterList.Count;

        for (int i=0;i<t_len;i++ )
        {

            m_MonsterList[i].SetSlider();
        }
        for (int index = m_MonsterList.Count - 1; index >= 0; index--)
        {
            if (m_MonsterList[index].GetisDead())
            {
                m_MonsterList.RemoveAt(index);
                Debug.LogError("Monster has been removed. index = " + index);
            }
        }

        return;
    }
}
