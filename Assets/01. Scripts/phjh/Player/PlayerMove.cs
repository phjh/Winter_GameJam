using UnityEngine;
using DG.Tweening;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    KeyBase StartKey;
    [SerializeField]
    float movetime = 0.5f;

    KeyBase nowPos;
    bool isMoving = false;

    float movesecond => 1 - Mathf.Log(movetime)/2;

    private void Start()
    {
        nowPos = StartKey;
        transform.position = StartKey.transform.position;
    }

    void Update()
    {
        if (Input.anyKeyDown && !isMoving)
        {
            foreach (var key in KeyManager.Instance.Boards)
            {
                if (Input.GetKeyDown(key.InputKeyCode) && Input.GetKey(KeyCode.LeftControl) && key.gameObject.activeInHierarchy)
                {
                    StartCoroutine(TelePort(key));
                }
                else if (Input.GetKeyDown(key.InputKeyCode) && nowPos.connectedKeys.Contains(key))
                {
                    StartCoroutine(Moving(key));
                }
            }
        }
    }

    IEnumerator Moving(KeyBase key)
    {
        isMoving = true;
        transform.DOMove(key.transform.position, movesecond);
        yield return new WaitForSeconds(movesecond);
        nowPos = key;
        isMoving = false;
    }

    IEnumerator TelePort(KeyBase key)
    {
        isMoving = true;
        transform.position = key.transform.position;
        yield return new WaitForSeconds(0.5f);
        nowPos = key;
        isMoving = false;
    }

    //±× ÀÌµ¿22
    //void GetRouteAndMoving(KeyBase key)
    //{
    //    KeyCode keycode = key.InputKeyCode;

    //    //KeyManager.Instance.AimKey = key;
    //    //foreach(var v in key.)
    //    //List<KeyBase> route = key.GetRoute(key);



    //    nowPos = key;
    //}

}
