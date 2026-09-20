using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTutorialController : MonoBehaviour
{

    bool ended = false;

    private void Update()
    {
        if (Phone.Finished && !ended)
        {
            ended = true;
            StartCoroutine("Fade");
        }
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1.0f);
        OtherSceneManager.NextScene();
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(2.0f);
        GameObject.Find("FadeIn").GetComponent<FadeIn>().StartFadeOut();
        StartCoroutine("NextLevel");
    }

}
