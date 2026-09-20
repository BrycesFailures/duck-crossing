using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /** The index of the current scene */
    private int currentScene;    
    // Start is called before the first frame update
    void Start()
    {
        //Get current scene index
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void nextScene()
    {
        SceneManager.LoadScene(currentScene + 1);
    }
}
