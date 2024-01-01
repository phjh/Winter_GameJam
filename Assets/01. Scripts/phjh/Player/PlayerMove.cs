using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                    KeyCode keycode = key.InputKeyCode;
                    isMoving = true;

                    
                    nowPos = key;
                }
            }
        }
    }

    void DoBFSWithMoving()
    {

    }


}
