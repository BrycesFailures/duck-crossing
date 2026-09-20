using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seagull : Interactable
{

    public int NumClicks = 5;
    int clicks = 0;

    public Sprite regular, angry;

    Vector3 scale;
    Vector3 velocity = Vector3.zero;
    float angular = 0.0f;

    private void Awake()
    {
        scale = transform.localScale;
    }

    private void Update()
    {
        if (velocity.sqrMagnitude > 0.0001f)
        {
            velocity += Physics.gravity * Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime);
            transform.position += Vector3.forward * Time.deltaTime * 10.0f;
        }
        transform.position += velocity * Time.deltaTime;
        transform.rotation *= Quaternion.Euler(0.0f, 0.0f, angular * Time.deltaTime);
    }

    public override void OnInteraction()
    {
        transform.localScale *= 1.1f;
        clicks++;

        int numSquaks = 3;
        int index = Mathf.FloorToInt(Random.Range(0.0f, numSquaks - 0.51f));
        AudioSystem.PlaySound("SFX/squak" + index);

        GetComponent<SpriteRenderer>().sprite = angry;

        StartCoroutine("Shrink");
    }

    public override void OnRelease()
    {
    }

    IEnumerator Shrink()
    {
        yield return new WaitForSeconds(0.25f);
        transform.localScale = scale;
        GetComponent<SpriteRenderer>().sprite = regular;
        if (clicks >= NumClicks)
        {
            velocity.x = Random.Range(-10.0f, 10.0f);
            velocity.y = Random.Range(10.0f, 20.0f);
            angular = 360.0f;
            Destroy(gameObject, 3.0f);
        }
    }

}
