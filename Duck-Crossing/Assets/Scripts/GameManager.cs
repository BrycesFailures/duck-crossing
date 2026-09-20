using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /** The index of the current level, starts at 0 so the title screen can increment it to one */
    public static int currentLevel = 0;

    public Phone phoneController;

    bool faded = false;

    //public bool isFinished;

    // Start is called before the first frame update
    void Start()
    {

        //phoneController = GetComponent<Phone>();
        //isFinished = phoneController.Finished;

        /*if (currentScene == 1)
        {
            phoneController.MessageListName = "First";
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if ((!Phone.Finished) & CarController.Finished)
        {
            CarController.Finished = false;
            Fade();
            StartCoroutine("LevelLose");
        }

        if (Phone.Finished)
        {
            Phone.Finished = false;
            Fade();
            StartCoroutine("LevelWin");
        }
    }
    public void nextLevel()
    {
        currentLevel++;
        CarController.Crashed = false;
        Phone.Finished = false;
        LevelTime.time = 0.0f;
        Fade();
        SceneManager.LoadScene(currentLevel);
    }

    public void retryLastLevel()
    {
        CarController.Crashed = false;
        Phone.Finished = false;
        LevelTime.time = 0.0f;
        Console.WriteLine("Current level: " +  currentLevel);
        Fade();
        SceneManager.LoadScene(currentLevel);
    }

    IEnumerator LevelWin()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("LevelWinScreen");
    }

    IEnumerator LevelLose()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("GameOver");
    }

    void Fade()
    {
        if (!faded) GameObject.Find("FadeIn").GetComponent<FadeIn>().StartFadeOut();
        faded = true;
    }
}
