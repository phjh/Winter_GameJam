using UnityEngine;
using DG.Tweening;
using System.Collections;
using Cinemachine;
using System.Collections.Generic;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    KeyBase StartKey;
    [SerializeField]
    float movetime = 0.5f;

    public bool isTutorial = false;

    public KeyBase nowPos;
    public bool isMoving = false;

    Animator animator;

    float movesecond => 1 - Mathf.Log(movetime)/2;

	private void Awake()
	{
		nowPos = StartKey;
		GameManager.Instance.PlayerPos = nowPos;
		transform.position = StartKey.transform.position;
        animator = GetComponentInChildren<Animator>();
	}

	private void Start()
    {
    }

    void Update()
    {
		if (Input.anyKeyDown && !isMoving)
        {
            foreach (var key in KeyManager.Instance.MainBoard)
            {
                if((Input.GetKeyDown(key.InputKeyCode) && !key.Corrupted)){
                    if (Input.GetKey(KeyCode.LeftControl) && key.gameObject.activeInHierarchy && KeyManager.Instance.TeleportCooltime == 0)
                    {
                        StartCoroutine(TelePort(key));
                    }
                    else if ( nowPos.connectedKeys.Contains(key))
                    {
                        StartCoroutine(Moving(key));
                    }
                    else if ( Input.GetKeyDown(KeyCode.Space) && KeyManager.Instance.ImmunityCooltime == 0)
                    {
                        StartCoroutine(InvalidationArea());
                    }
                }
            }
        }
    }

    IEnumerator Moving(KeyBase key)
    {
        if (isTutorial)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = (bool)(nowPos.transform.position.x >       key.transform.position.x);
        }
        isMoving = true;
        animator.SetBool("Move", isMoving);
        yield return new WaitForSeconds(0.1f);
        transform.DOMove(key.transform.position, movesecond);
        yield return new WaitForSeconds(movesecond);
        nowPos = key;
        isMoving = false;
        animator.SetBool("Move", isMoving);
		GameManager.Instance.PlayerPos = nowPos;
    }

    IEnumerator TelePort(KeyBase key)
    {
        isMoving = true;
        transform.position = key.transform.position;
        yield return new WaitForSeconds(0.1f);
        nowPos = key;
        KeyManager.Instance.TeleportCooltime = 5;
        isMoving = false;
	    GameManager.Instance.PlayerPos = nowPos;
    }


    #region 플레이어 스킬

    IEnumerator InvalidationArea()   //그면상 (그냥 무적상태)
    {
        KeyBase ImmunityKey;
        ImmunityKey = nowPos;
        ImmunityKey.isImmunity = true;
        foreach(var key in ImmunityKey.connectedKeys)
        {
            key.isImmunity = true;
        }
        KeyManager.Instance.ImmunityCooltime = 20;
        yield return new WaitForSeconds(2f);
        ImmunityKey.isImmunity = false;
        foreach (var key in ImmunityKey.connectedKeys)
        {
            key.isImmunity = false;
        }
    }

    public IEnumerator CorruptedKey()
    {
        KeyBase ImmunityKey;
        ImmunityKey = nowPos;
        ImmunityKey.Corrupted = true;
        KeyManager.Instance.DeleteConnectkeys(ImmunityKey);
        yield return new WaitForSeconds(3);

        ImmunityKey.Corrupted = false;
        KeyManager.Instance.AddConnectKeys(ImmunityKey);
    }

    //IEnumerator CollectAtk()
    //{
    //    InstanceCollectobj(5);
    //    yield return collectObj.Count != 0;
    //    Debug.Log("skill Actived");
    //    collectObjInfo.
    //}

    //void InstanceCollectobj(int n)
    //{
    //    for(;collectObj.Count < n;)
    //    {
    //        int rand = Random.Range((int)RowKey.one, (int)RowKey.Period + 1);
    //        if (!collectObj.Contains(rand))
    //        {
    //            GameObject obj = Instantiate(collectiveobj);
    //            collectObj.Add(rand);
    //            collectObjInfo.Add(obj);
    //        }
    //    }
    //}







    #endregion


    //그 이동22
    //void GetRouteAndMoving(KeyBase key)
    //{
    //    KeyCode keycode = key.InputKeyCode;

    //    //KeyManager.Instance.AimKey = key;
    //    //foreach(var v in key.)
    //    //List<KeyBase> route = key.GetRoute(key);



    //    nowPos = key;
    //}

}
