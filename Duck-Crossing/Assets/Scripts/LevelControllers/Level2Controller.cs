using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Controller : MonoBehaviour
{

    bool ended = false;

    public GameObject seagull1;
    public GameObject seagull2;

    private void Update()
    {
        if (!ended && LevelTime.time > 40.0f)
        {
            if (seagull1) seagull1.SetActive(true);
        }

        if (!ended && LevelTime.time > 80.0f)
        {
            if (seagull2) seagull2.SetActive(true);
        }

        if (Phone.Finished && !ended)
        {
            StartCoroutine("Fade");
        }
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1.0f);
        GameManager.currentLevel++;
        OtherSceneManager.NextScene();
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(2.0f);
        GameObject.Find("FadeIn").GetComponent<FadeIn>().StartFadeOut();
        StartCoroutine("NextLevel");
    }

}
