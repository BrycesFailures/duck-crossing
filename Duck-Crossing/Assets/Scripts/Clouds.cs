using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{

    public float Speed = 1.0f;
    public float RightLimit = 12.0f;

    private void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.position += Vector3.right * Speed * Time.deltaTime;

            if (child.position.x >= RightLimit)
                child.position += Vector3.left * RightLimit * 2.0f;
        }
    }

}
