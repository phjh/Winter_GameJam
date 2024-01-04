using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering.Universal;

public class OnlyStartScene : MonoBehaviour
{
    public List<Light2D> lights;
    public HashSet<KeyBase> set = new HashSet<KeyBase>();
    float intencity = 1.4f;
    float outerRad = 1.5f;

    [SerializeField]
    PlayableDirector timeline;
    [SerializeField]
    GameObject effect;

    public PlayerMove move;

    bool isInvoked = true;

    private void FixedUpdate()
    {
        if(!move.isMoving && isInvoked)
        {
            int i = 0;
            isInvoked = false;
            foreach(var v in KeyManager.Instance.MainBoard)
            {
                if(!set.Contains(v) && v == GameManager.Instance.PlayerPos)
                {
                    set.Add(v);
                    lights[i].intensity = intencity;
                    lights[i + 1].intensity = intencity / 2;
                    lights[i].pointLightOuterRadius = outerRad;
                    if (i + 2 == lights.Count)
                        StartCoroutine(TimelineExcute());
                    break;
                }
                i++;
            }
        }
        else if (move.isMoving)
        {
            isInvoked = true;
        }
    }

    IEnumerator TimelineExcute()
    {
        timeline.Play();
        Destroy(Instantiate(effect,GameManager.Instance.PlayerPos.transform.position + new Vector3(0,0.5f,0), Quaternion.identity),2f);
        yield return new WaitForSeconds(3);
        SceneLoader.Instance.Tutorial();
    }


}
