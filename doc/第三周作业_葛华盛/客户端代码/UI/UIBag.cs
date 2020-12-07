using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIBag  //UI Bag管理类
{
    Button m_BagButton;
    GameObject m_Bag;
    GameObject m_scrollview;
    Button m_BagADD;
    Button m_BagDEL;

    List<GameObject> m_Bagimgs;
    private int m_BagImgsLen;
    

    public UIBag()
    {
        m_Bagimgs = new List<GameObject>();
    }

    public void OnMainCity()
    {
        m_BagButton = GameObject.Find("/Canvas/Bag").GetComponent<Button>();
        m_Bag = GameObject.Find("/Canvas/Scroll View/Viewport/Content/");
        m_scrollview = GameObject.Find("/Canvas/Scroll View");
        m_BagADD = GameObject.Find("/Canvas/Scroll View/ADDButton").GetComponent<Button>();
        m_BagDEL = GameObject.Find("/Canvas/Scroll View/DELButton").GetComponent<Button>();
        
        if (m_BagButton == null)
        {
            Debug.LogError("m_BagButton is null");
            return;
        }
        if (m_Bag == null)
        {
            Debug.LogError("m_Bag is null");
            return;
        }
        if (m_scrollview == null)
        {
            Debug.LogError("m_scrollview is null");
            return;
        }
        if (m_BagADD == null)
        {
            Debug.LogError("m_BagADD is null");
            return;
        }
        if (m_BagDEL == null)
        {
            Debug.LogError("m_BagDEL is null");
            return;
        }
        m_BagButton.onClick.AddListener(onBagClick);
        m_BagADD.onClick.AddListener(onBagADD);
        m_BagDEL.onClick.AddListener(onBagDEL);
        m_scrollview.SetActive(false);





        int num = m_Bag.transform.GetChildCount();
        for (int i = 0; i < num; i++)
        {
            m_Bagimgs.Add(m_Bag.transform.GetChild(i).gameObject);
        }

        if (m_Bagimgs == null)
        {
            Debug.LogError("imgs is null");
            return;
        }
        m_BagImgsLen = m_Bagimgs.Count;

  
    }


    public void onBagClick()
    {
        if (m_scrollview.active)
        {
            m_scrollview.SetActive(false);
        }
        else
        {
            m_scrollview.SetActive(true);
            int[] items = PlayerMgr.singleton.GetBagItems();
            int itemslen = items.Length;

            for (int i = 0; i < m_BagImgsLen; i++)
            {
                //Debug.LogError("imgs index = " + i);
                if (i > itemslen)
                {
                    continue;
                }
                //todo
                if (items[i] != 0)
                {
                    GameObject pot = m_Bagimgs[i].transform.Find("Image").gameObject;
                    pot.SetActive(true);
                    //m_BagImgs[i].gameObject.SetActive(true);
                }
                else
                {
                    GameObject pot = m_Bagimgs[i].transform.Find("Image").gameObject;
                    pot.SetActive(false);
                    //m_BagImgs[i].gameObject.SetActive(false);
                }
            }
        }

    }

    public void onBagADD()
    {
        //tmp
        int pos = PlayerMgr.singleton.AddBagItem(321);
        
        if (pos != -1)
        {
            m_Bagimgs[pos].transform.Find("Image").gameObject.SetActive(true);
        }
    }

    public void onBagDEL()
    {
        int pos = PlayerMgr.singleton.DelBagItem();
        if (pos != -1)
        {
            m_Bagimgs[pos].transform.Find("Image").gameObject.SetActive(false);
        }
    }
}
