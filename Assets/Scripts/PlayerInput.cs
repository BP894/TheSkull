using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string moveAxisName = "Vertical";
    public string rotateAxisName = "Horizontal";
    public string fireButtonName = "Fire1";
    public string reloadButtonName = "Reload";
    public float move { get; private set; }
    public float rotate { get; private set; }
    public bool fire { get; private set; }
    public bool reload { get; private set; }
    public bool keynum1 { get; private set; }
    public bool keynum2 { get; private set; }
    public bool keynum3 { get; private set; }
    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis(moveAxisName);
        rotate = Input.GetAxis(rotateAxisName);
        fire = Input.GetButton(fireButtonName);
        reload = Input.GetButton(reloadButtonName);
        keynum1 = Input.GetKeyDown(KeyCode.Keypad1);
        keynum2 = Input.GetKeyDown(KeyCode.Keypad2);
        keynum3 = Input.GetKeyDown(KeyCode.Keypad3);
    }
}
