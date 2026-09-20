using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEndScreen : MonoBehaviour
{

    bool called = false;

    Vector3 pos;
    Vector3 scale;
    float startTime = 0.0f;
    float soundTime = 0.92f;

    private void Awake()
    {
        pos = transform.position;
        scale = transform.localScale;
    }

    private void Update()
    {
        if (CarController.Crashed & !called)
        {
            called = true;
            startTime = Time.time;
            GameObject.Find("_Main").GetComponent<AudioSource>().volume = 0.0f;
            Destroy(GameObject.Find("_Main").GetComponent<CarController>());
            AudioSystem.PlaySound("SFX/crash");
            GetComponent<SpriteRenderer>().enabled = true;
            StartCoroutine("Popup");
        }

        if (called)
        {
            float since = Time.time - startTime;
            float normal = Mathf.Clamp01(since / soundTime);
            transform.position = pos + Vector3.up * Mathf.Sin(Mathf.PI * normal) * 4.0f;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, normal * 360 * 4.0f);
            transform.localScale = Vector3.Lerp(Vector3.zero, scale, Mathf.Pow(normal, 2.2f));
        }
    }



    IEnumerator Popup()
    {
        yield return new WaitForSeconds(soundTime);
    }

}
