using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBag
{
    static int bag_size = 48;
    List<BagItem> m_bagitems; //items info
    private int[] m_bagcubes; //index -> itemid
    
    public PlayerBag()
    {
        m_bagitems = new List<BagItem>();
        m_bagcubes = new int[bag_size];
        m_bagcubes.Append(1);
        Init();
    }

    public bool Init()
    {
        for (int i = 0; i < bag_size; i++)
        {
            // todo: read data form databases;
            //m_bagitems.Add(new BagItem(0));
            m_bagcubes[i] = 0;
        }

        return true;
    }

    //todo 常量
    public int[] GetItems()
    {
        return m_bagcubes;
    }

    public int AddItem(int itemID)
    {
        //todo
        int pos = -1;
        for (int i=0;i<bag_size;i++)
        {
            if(m_bagcubes[i] == 0)
            {
                pos = i;
                break;
            }
        }
        if (pos != -1)
        {
            m_bagcubes[pos] = itemID;
            m_bagitems.Add(new BagItem(itemID));
        }
        return pos;
    }

    public int DelItem()
    {
        //todo,暂时这么写，后面加了选中就按照格子来
        if (m_bagitems.Count == 0)
        {
            return -1;
        }
        int pos = m_bagitems.Count - 1;
        m_bagcubes[pos] = 0;
        m_bagitems.RemoveAt(pos);

        return pos;
    }

}

public class BagItem
{
    private int itemID;
    
    public BagItem(int id)
    {
        itemID = id;
    }
    public int GetID()
    {
        return itemID;
    }


}