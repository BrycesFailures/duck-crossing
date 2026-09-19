using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jiggle : MonoBehaviour
{

    public float Strength = 1.0f;
    public float Dampening = 0.5f;

    Vector3 velocity = Vector3.zero;
    Vector3 offset = Vector3.zero;

    Vector3 pos;


    private void Awake()
    {
        pos = transform.position;
    }


    private void Update()
    {
        velocity += CarController.Force;
        velocity -= offset * Strength * Time.deltaTime;
        velocity -= velocity * Dampening * Time.deltaTime;
        offset += velocity * Time.deltaTime;

        transform.position = pos + offset;
    }


}
