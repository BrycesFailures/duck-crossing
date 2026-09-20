using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class LevelWin : MonoBehaviour
{
    //public GameObject textBox = GameObject.Find("Text (TMP)");
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Text>().text = "You Yielded in " + Mathf.Round(Phone.FinishedTime) + " Seconds!";
    }
}
