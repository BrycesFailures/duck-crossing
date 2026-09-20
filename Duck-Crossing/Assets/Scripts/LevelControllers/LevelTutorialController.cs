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
            EndLevel();
        }
    }

    public void EndLevel()
    {
        if (!ended) StartCoroutine("Fade", false);
        ended = true;
    }

    public void FadeOut()
    {
        SpriteRenderer sr = GameObject.Find("FadeIn").GetComponent<SpriteRenderer>();
        sr.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        if (!ended) StartCoroutine("Fade", true);
        ended = true;
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1.0f);
        GameManager.currentLevel++;
        OtherSceneManager.NextScene();
    }

    IEnumerator Fade(bool title)
    {
        if (!title) yield return new WaitForSeconds(2.0f);
        else yield return new WaitForEndOfFrame();
        GameObject.Find("FadeIn").GetComponent<FadeIn>().StartFadeOut();
        StartCoroutine("NextLevel");
    }

}
