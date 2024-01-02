using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBase : MonoBehaviour
{
    public KeyCode InputKeyCode;
    public List<KeyBase> connectedKeys;

    private void Start()
    {
        RefreshConnectedKey();
    }

    public void RefreshConnectedKey()
    {
        connectedKeys = new List<KeyBase>();

        foreach (var key in KeyManager.Instance.Boards)
        {
            if (Vector3.Distance(this.transform.position, key.transform.position) < 2f && InputKeyCode != key.InputKeyCode)
            {
                connectedKeys.Add(key);
            }
        }
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
