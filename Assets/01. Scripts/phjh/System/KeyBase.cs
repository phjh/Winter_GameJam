using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBase : MonoBehaviour
{
    public KeyCode InputKeyCode;
    public List<KeyBase> connectedKeys { get; private set; }
    GameObject sprite;
    public bool isImmunity = false;
    Material NormalMat;

    public void DamageEvent(float time = 1, float duration = 1, bool isPlayParticle = false, string particleName = "") => StartCoroutine(DamageCode(time, duration, isPlayParticle, particleName));

    //void DamageEffect(float duration)=> Destroy(Instantiate(DamageEffecter), duration);

    private void Start()
    {
        NormalMat = GetComponentInChildren<SpriteRenderer>().material;

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

    private void Update()
    {
        if (isImmunity)
        {
            GetComponentInChildren<SpriteRenderer>().material = KeyManager.Instance.immunityMat;
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().material = NormalMat;
        }
    }

    public void AddConnectedKey()
    {
        foreach (var keys in connectedKeys)
        {
            keys.connectedKeys.Add(this);
        }
    }

    public void DeleteConnectedKey()
    {
        foreach(var keys in connectedKeys)
        {
            keys.connectedKeys.Remove(this);
        }
    }

    IEnumerator DamageCode(float time = 1,float duration=1, bool isPlayParticle = false, string particleName = "")
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
        if (isPlayParticle)
        {
            AttackParticle attackParticle = PoolManager.Instance.Pop(particleName) as AttackParticle;
            attackParticle.transform.position = this.transform.position;
        }
        yield return new WaitForSeconds(duration);
        PoolManager.Instance.Push(attackArea);
    }

    public void CorruptedKey()
    {

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
