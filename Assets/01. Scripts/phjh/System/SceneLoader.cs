using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class SceneLoader : MonoSingleton<SceneLoader>
{


    public void Tutorial()
    {
        SceneManager.LoadScene(1);
    }

    public void InGame()
    {

    }



}
