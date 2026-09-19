using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMaterial : MonoBehaviour
{

    public float Speed = 1.0f;
    MeshRenderer mr;

    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        mr.sharedMaterial.mainTextureOffset = new Vector2(0.0f, Speed * Time.time);
    }

}
