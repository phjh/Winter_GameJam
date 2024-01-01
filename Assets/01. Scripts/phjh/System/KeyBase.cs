using System.Collections.Generic;
using UnityEngine;

public class KeyBase : MonoBehaviour
{
    public KeyCode InputKeyCode;
    [SerializeField]
    List<KeyBase> connectedKeys;

    private void Start()
    {
        connectedKeys = new List<KeyBase>();

        foreach(var key in KeyManager.Instance.Boards)
        {
            if (Vector3.Distance(this.transform.position, key.transform.position) < 2f && InputKeyCode != key.InputKeyCode)
            {
                connectedKeys.Add(key);
            }
        }
    }
}
