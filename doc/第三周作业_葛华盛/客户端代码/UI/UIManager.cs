using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager
{
    private Dictionary<String, GameObject> objUIList;
    Button m_LoginButton;
    Button m_SettingButton;
    Button m_InfoButton;
    UIBag m_uibag;
    Slider m_hpline;


    public UIManager()
    {
        objUIList = new Dictionary<string, GameObject>();
        m_uibag = new UIBag();
    }

    public void OnLogin()
    {

        m_LoginButton = GameObject.Find("LoginCanvas/Button").GetComponent<Button>();
        if (m_LoginButton == null)
        {
            Debug.LogError("m_LoginButton is null");
        }
        m_LoginButton.onClick.AddListener(onLoginClick);
        

    }

    public void OnMainCity()
    {


        m_SettingButton = GameObject.Find("/Canvas/Image/Button").GetComponent<Button>();
        m_InfoButton = GameObject.Find("/Canvas/Info").GetComponent<Button>();
        m_hpline = GameObject.Find("/Canvas/Slider").GetComponent<Slider>();

      

        if (m_SettingButton == null)
        {
            Debug.LogError("m_LoginButton is null");
        }
        if (m_InfoButton == null)
        {
            Debug.LogError("m_InfoButton is null");
        }
        if (m_hpline == null)
        {
            Debug.LogError("m_hpline is null");
        }

        m_uibag.OnMainCity();


        m_SettingButton.onClick.AddListener(onSettingClick);
        m_InfoButton.onClick.AddListener(onInfoClick);
      
    }

    public void onLoginClick()
    {
        SceneManager.LoadScene("DN_maincity_sstt");
    }

    public void onSettingClick()
    {

    }

    public void onInfoClick()
    {

    }

    public void UpdateHpLine(int HP)
    {
        //Debug.Log("HP = "+HP);
        m_hpline.value = (float)HP / 100f;
    }
}
