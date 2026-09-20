using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{

    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        sr.color = new Color(
            sr.color.r,
            sr.color.g,
            sr.color.b,
            1.0f - LevelTime.time
        );
    }

}
