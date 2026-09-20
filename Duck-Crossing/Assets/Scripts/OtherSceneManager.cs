using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OtherSceneManager : MonoBehaviour
{

    public static void ReloadScene()
    {
        CarController.Crashed = false;
        Phone.Finished = false;
        LevelTime.time = 0.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public static void NextScene()
    {
        CarController.Crashed = false;
        Phone.Finished = false;
        LevelTime.time = 0.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void FirstScene()
    {
        CarController.Crashed = false;
        Phone.Finished = false;
        LevelTime.time = 0.0f;
        SceneManager.LoadScene(0);
    }

    public static void LastScene()
    {
        CarController.Crashed = false;
        Phone.Finished = false;
        LevelTime.time = 0.0f;
        SceneManager.LoadScene(SceneManager.sceneCount - 1);
    }

}
