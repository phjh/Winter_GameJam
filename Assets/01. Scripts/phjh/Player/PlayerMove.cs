using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    KeyBase nowPos;
    bool isMoving = false;

    void Update()
    {
        if (Input.anyKeyDown && !isMoving)
        {
            foreach (var key in KeyManager.Instance.Boards)
            {
                if (Input.GetKeyDown(key.InputKeyCode))
                {
                    GetRouteAndMoving(key);
                }
            }
        }
    }

    void GetRouteAndMoving(KeyBase key)
    {
        KeyCode keycode = key.InputKeyCode;
        KeyManager.Instance.AimKey = key;

        //foreach(var v in key.)
        List<KeyBase> route = key.GetRoute(key);



        nowPos = key;
    }
}
