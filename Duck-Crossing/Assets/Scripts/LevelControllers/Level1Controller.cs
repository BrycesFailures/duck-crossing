using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Controller : MonoBehaviour
{

    bool ended = false;

    public GameObject seagull;

    private void Update()
    {
        if (!ended && LevelTime.time > 5.0f)
        {
            if (seagull) seagull.SetActive(true);
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
