using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        int minutes = Mathf.FloorToInt(LevelTime.time / 60.0f);
        int seconds = Mathf.FloorToInt(LevelTime.time % 60.0f);

        string str = "";

        if (minutes < 10) str += "0";
        str += minutes;

        str += ":";

        if (seconds < 10) str += "0";
        str += seconds;

        text.text = str;
    }

}
