using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithMouse : MonoBehaviour
{

    Vector3 pos = Vector3.zero;

    public Vector2 Movement = Vector2.one;

    private void Awake()
    {
        pos = transform.position;
    }

    private void Update()
    {
        Vector3 mp = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mp = mp * 2.0f - Vector3.one;

        transform.position = new Vector3(
            pos.x - mp.x * Movement.x,
            pos.x - mp.y * Movement.y,
            pos.z
        );
    }

}
