using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickObject : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject btnObj;
    Button btn;
    void Start()
    {
        //获取按钮游戏对象
         btnObj = GameObject.Find("LoginCanvas/Button");
        //获取按钮脚本组件
        btnObj.GetComponent<Button>();
        btn = (Button)btnObj.GetComponent<Button>();
        //添加点击侦听
 
        //btn.onClick.AddListener(onClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onClick()
    {
        Debug.LogError("click!");
        SceneManager.LoadScene("DN_maincity_sstt");//level1为我们要切换到的场景

    }
}
