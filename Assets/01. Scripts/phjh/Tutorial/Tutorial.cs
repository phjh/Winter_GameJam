using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{


    void Start()
    {
        StartCoroutine(TutorialSequence());
    }

    IEnumerator TutorialSequence()
    {
        //대충 스토리 설명



        yield return null;

    }

}
