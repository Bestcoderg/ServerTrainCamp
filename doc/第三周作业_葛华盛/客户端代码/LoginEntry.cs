using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginEntry : MonoBehaviour
{
    UIManager ui_manager_;

    private void Awake()
    {
        ui_manager_ = new UIManager();
    }
    // Start is called before the first frame update
    void Start()
    {
        ui_manager_.OnLogin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
