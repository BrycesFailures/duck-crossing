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
            Camera.main.gameObject.GetComponent<VisualEffect>().Active = false;
            Camera.main.gameObject.GetComponent<VisualEffect>().Target = 0.0f;
            CarController.Force = Vector3.zero;
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
    }



    public void Retry()
    {
        if (!retried) StartCoroutine("ReloadScene");
        AudioSystem.PlaySound("SFX/type");
        retried = true;
        GameObject.Find("FadeIn").GetComponent<FadeIn>().StartFadeOut();
    }



    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(1.0f);
        OtherSceneManager.ReloadScene();
    }



    IEnumerator Popup()
    {
        yield return new WaitForSeconds(soundTime);
    }

}
