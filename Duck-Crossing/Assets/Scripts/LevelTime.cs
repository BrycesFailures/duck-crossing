using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTime : MonoBehaviour
{

    public static float time = 0.0f;

    private void Update()
    {
        time += Time.deltaTime;
    }

}
