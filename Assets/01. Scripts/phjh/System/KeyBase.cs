using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBase : MonoBehaviour
{
    public KeyCode InputKeyCode;
    public List<KeyBase> connectedKeys { get; private set; }
    GameObject sprite;

    public void DamageEvent(int damage, float time = 1, float duration = 1) => StartCoroutine(DamageCode(damage, time, duration));

    //void DamageEffect(float duration)=> Destroy(Instantiate(DamageEffecter), duration);

    private void Start()
    {
        connectedKeys = new List<KeyBase>();

        foreach (var key in KeyManager.Instance.MainBoard)
        {
            if (Vector3.Distance(this.transform.position, key.transform.position) < 1.8f && InputKeyCode != key.InputKeyCode && key.gameObject.activeInHierarchy)
            {
                connectedKeys.Add(key);
            }
        }

        sprite = transform.GetChild(0).gameObject;
    }

    public void AddConnectedKey()
    {
        foreach (var keys in connectedKeys)
        {
            keys.connectedKeys.Remove(this);
        }
    }
    public void DeleteConnectedKey()
    {
        foreach(var keys in connectedKeys)
        {
            keys.connectedKeys.Remove(this);
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
