using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /** The index of the current level, starts at 0 so the title screen can increment it to one */
    public static int currentLevel = 0;

    public Phone phoneController;

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
            SceneManager.LoadSceneAsync("GameOver");
        }
    }
    public void nextLevel()
    {
        currentLevel++;
        SceneManager.LoadScene(currentLevel);
    }

    public void retryLastLevel()
    {
        SceneManager.LoadScene(currentLevel);
    }

    public void winningScreen()
    {

    }
}
