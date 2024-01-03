using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBase : MonoBehaviour
{
    public KeyCode InputKeyCode;
    public RowKey BaseKey;
    public List<KeyBase> connectedKeys { get; private set; }
	private SpriteRenderer sp;

	public void DamageEvent(float time = 1, float duration = 1, bool isPlayParticle = false, string particleName = "") => StartCoroutine(DamageCode(time, duration, isPlayParticle, particleName));

	//void DamageEffect(float duration)=> Destroy(Instantiate(DamageEffecter), duration);

	private void Awake()
	{
		sp = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
	}

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
        float t = 0;
		while (t < 1)
        {
            sp.color = new Color(1, Mathf.Lerp(1, 0, t), Mathf.Lerp(1, 0, t));
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
