using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class KeyBase : MonoBehaviour
{
    public KeyCode InputKeyCode;
    public List<KeyBase> connectedKeys;
    GameObject sprite;

    public void DamageEvent(int damage, float time = 1, float duration = 1) => StartCoroutine(DamageCode(damage, time, duration));

    //void DamageEffect(float duration)=> Destroy(Instantiate(DamageEffecter), duration);

    private void Start()
    {
        RefreshConnectedKey();
        sprite = transform.GetChild(0).gameObject;
    }

    public void RefreshConnectedKey()
    {
        connectedKeys = new List<KeyBase>();

        foreach (var key in KeyManager.Instance.Boards)
        {
            if (Vector3.Distance(this.transform.position, key.transform.position) < 2f && InputKeyCode != key.InputKeyCode && key.gameObject.activeInHierarchy)
            {
                connectedKeys.Add(key);
            }
        }
    }

    IEnumerator DamageCode(int damage,float time = 1,float duration=1)
    {
        SpriteRenderer sp = sprite.GetComponent<SpriteRenderer>();
        float t = 0;
        while (t <= 1)
        {
            float color = Mathf.Lerp(1, 0, t);
            sp.color = new Color(1, color, color);
            t += Time.deltaTime/time;
            Debug.Log(t);
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        sp.color = Color.white;
        EnemyAttackArea attackArea = PoolManager.Instance.Pop("EnemyAttackArea") as EnemyAttackArea;
        attackArea.transform.position = this.transform.position;
        yield return new WaitForSeconds(duration);
        PoolManager.Instance.Push(attackArea);
    }

    //±× ÀÌµ¿
    //public List<KeyBase> GetRoute(KeyBase nextKey)
    //{
    //    List<KeyBase> nav = new List<KeyBase>();
    //    foreach(var key in nextKey.connectedKeys)
    //    {
    //        if(key == KeyManager.Instance.AimKey)
    //        {
    //            nav.Add(key);
    //            return nav;
    //        }
    //        if (!KeyManager.Instance.set.Contains(key))
    //        {
    //            KeyManager.Instance.set.Add(key);
    //            nav = nextKey.GetRoute(key);
    //            nav.Add(key);
    //        }

    //    }

    //    return nav;
    //}

    
}
