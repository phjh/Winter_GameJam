using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoSingleton<KeyManager>
{
    //public bool isFound = false;
    //public KeyBase AimKey;
    //public SortedSet<KeyBase> set;
    public List<KeyBase> Boards;

    public void RefreshConnectKeys()
    {
        foreach (var key in Boards)
        {
            key.RefreshConnectedKey();
        }
    }


}
