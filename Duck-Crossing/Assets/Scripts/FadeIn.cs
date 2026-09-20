using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{

    SpriteRenderer sr;

    float time = 0.0f;
    bool fadeOut = true;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        StartFadeIn();
    }

    private void Update()
    {
        if (!fadeOut)
            sr.color = new Color(
                sr.color.r,
                sr.color.g,
                sr.color.b,
                1.0f - (Time.time - time)
            );
        else
            sr.color = new Color(
                sr.color.r,
                sr.color.g,
                sr.color.b,
                (Time.time - time)
            );
    }

    public void StartFadeOut()
    {
        time = Time.time;
        fadeOut = true;
    }

    public void StartFadeIn()
    {
        time = Time.time;
        fadeOut = false;
    }

}
