using UnityEngine;
using DG.Tweening;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    KeyBase StartKey;
    [SerializeField]
    float movetime = 0.5f;

    public KeyBase nowPos;
    public bool isMoving = false;

    int nowSkill = 0;

    float movesecond => 1 - Mathf.Log(movetime)/2;

	private void Awake()
	{
		nowPos = StartKey;
		GameManager.Instance.PlayerPos = nowPos;
	}

	private void Start()
    {
		transform.position = StartKey.transform.position;
    }

    void Update()
    {
		GameManager.Instance.PlayerPos = nowPos;
		if (Input.anyKeyDown && !isMoving)
        {
            foreach (var key in KeyManager.Instance.MainBoard)
            {
                if (Input.GetKeyDown(key.InputKeyCode) && Input.GetKey(KeyCode.LeftControl) && key.gameObject.activeInHierarchy)
                {
                    StartCoroutine(TelePort(key));
                }
                else if (Input.GetKeyDown(key.InputKeyCode) && nowPos.connectedKeys.Contains(key))
                {
                    StartCoroutine(Moving(key));
                }
                else if (Input.GetKeyDown(key.InputKeyCode) && 
                    (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.Space))) 
                {
                    switch(key.InputKeyCode)
                    {
                        case KeyCode.LeftShift:
                            nowSkill += 2;
                            break;
                        case KeyCode.RightShift:
                            nowSkill++;
                            break;
                        case KeyCode.Space:
                            
                            break;
                    }
                    nowSkill = nowSkill % 3;
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
