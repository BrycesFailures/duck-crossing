using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterEndScreen : MonoBehaviour
{

    bool called = false;
    bool retried = false;

    Vector3 pos;
    float startTime = 0.0f;
    float soundTime = 0.92f;
    float retryTime = 0.0f;

    private void Awake()
    {
        pos = transform.position;
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
            transform.localScale = Vector3.Lerp(Vector3.zero, new Vector3(1.5f, 1.5f, 1.0f), Mathf.Pow(normal, 2.2f));
        }

        if (retried)
        {
            Destroy(GameObject.Find("FadeIn").GetComponent<FadeIn>());
            SpriteRenderer sr = GameObject.Find("FadeIn").GetComponent<SpriteRenderer>();
            sr.color = new Color(
                sr.color.r,
                sr.color.g,
                sr.color.b,
                Time.time - retryTime
            );
        }
    }



    public void Retry()
    {
        if (!retried) StartCoroutine("ReloadScene");
        retried = true;
        retryTime = Time.time;
    }



    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(1.0f);
        CarController.Crashed = false;
        Phone.Finished = false;
        LevelTime.time = 0.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



    IEnumerator Popup()
    {
        yield return new WaitForSeconds(soundTime);
    }

}
