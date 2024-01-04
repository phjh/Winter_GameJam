using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneLoader.Instance.Tutorial();
        }       
    }
}
