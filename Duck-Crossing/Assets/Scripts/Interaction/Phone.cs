using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phone : Interactable
{

    // Message Stuff ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    [System.Serializable] public struct TextMessageList { public TextMessage[] messages; }

    [System.Serializable] public struct TextMessage
    {
        public string type, sender, message, response;
        public bool hint;
    }

    /// Loads text message data from a given Resource. ex: "Example"
    TextMessage[] LoadMessages(string path)
    {
        return JsonUtility.FromJson<TextMessageList>("{\"messages\":" + Resources.Load<TextAsset>("TextMessages/" + path).text + "}").messages;
    }

    /// The Name of the JSON file in Resources/TextMessages/
    public string MessageListName = "Example";

    /// The list of messages deserialized from the JSON.
    /// This is NOT the list of UI elements.
    List<TextMessage> messages = null;

    /// Utility function for lazy referencing.
    private RectTransform GetChild(string name)
    {
        RectTransform[] children = GetComponentsInChildren<RectTransform>();
        foreach (RectTransform child in children)
            if (child.name == name) return child;
        return null;
    }

    /// The Rect Transform of the scroll view Object in the Area Field.
    RectTransform scrollView;
    /// The Rect Transform of the Content Object in the Area Field.
    RectTransform content;

    /// The prefab of the message box.
    public GameObject messagePrefab;
    /// The prefab of the response message box.
    public GameObject responsePrefab;
    /// The last box added, used for positioning.
    TextMessageBox previousBox = null;

    /// Adds a new message to the scroll area and forces the view to the bottom.
    GameObject AddMessage(string message, bool response)
    {
        GameObject obj;
        if (!response) obj = Instantiate(messagePrefab, content);
        else obj = Instantiate(responsePrefab, content);

        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.localScale = messagePrefab.GetComponent<RectTransform>().localScale;
        if (previousBox) rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, previousBox.GetComponent<RectTransform>().anchoredPosition.y - (previousBox.Height + 1.0f) * rect.localScale.y);
        previousBox = obj.GetComponent<TextMessageBox>();
        previousBox.SetText(message);
        previousBox.Shrink();

        float height = (previousBox.Height + 1.0f) * rect.localScale.y;
        content.sizeDelta += new Vector2(0.0f, height);

        for (int i = 0; i < content.childCount; i++)
            content.GetChild(i).GetComponent<RectTransform>().anchoredPosition += new Vector2(0.0f, height * 0.5f);

        scrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = 0.0f;

        if (!response) AudioSystem.PlaySound("SFX/message");

        return obj;
    }

    /// Attemps to send the current response, fails if it doesn't match.
    void SendResponse()
    {
        if (messages.Count > 0 && messages[0].type == "response" && currentResponse == messages[0].response.ToUpper())
        {
            AddMessage(messages[0].response, true);
            messages.RemoveAt(0);
            currentResponse = "";
            fixedCounter = 0;
        } else
        {
            AudioSystem.PlaySound("SFX/wrong");
        }
    }

    int fixedCounter = 0;
    // End of Message Stuff ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~



    // Typing Stuff :,( ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Text hint, response;
    string currentResponse = "";

    RectTransform keyboard;
    //bool capsLock = true;
    static readonly string[] defaultButtons = new[] {
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0",
        "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P",
        "A", "S", "D", "F", "G", "H", "J", "K", "L",
        "Z", "X", "C", "V", "B", "N", "M", " ",
        "Send", "Del"
    };
    static readonly string[] defaultButtonsLower = new[] {
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0",
        "q", "w", "e", "r", "t", "y", "u", "i", "o", "p",
        "a", "s", "d", "f", "g", "h", "j", "k", "l",
        "z", "x", "c", "v", "b", "n", "m", " ",
        "Send", "Del"
    };
    List<string> buttonValues = new List<string>(defaultButtons);

    void UpdateButtons()
    {
        //if (!capsLock) buttonValues = new List<string>(defaultButtonsLower);
        //else buttonValues = new List<string>(defaultButtons);
        for (int i = 0; i < keyboard.childCount; i++) keyboard.GetChild(i).GetChild(0).GetComponent<Text>().text = buttonValues[i];
    }

    void ShuffleButtons()
    {
        string[] shuffle = new string[37];
        for (int i = 0; i < shuffle.Length; i++)
            shuffle[i] = defaultButtons[i];

        for (int n = 0; n < 10; n++)
        for (int i = 0; i < shuffle.Length - 1; i++)
            if (UnityEngine.Random.value > 0.5f)
            {
                string temp = shuffle[i];
                shuffle[i] = shuffle[i + 1];
                shuffle[i + 1] = temp;
            }

        for (int i = 0; i < shuffle.Length; i++)
            buttonValues[i] = shuffle[i];

        UpdateButtons();
    }

    void UpdateResponse()
    {
        if (messages.Count > 0) hint.text = messages[0].response.ToUpper();
        response.text = currentResponse;
        marker.anchoredPosition = new Vector2(
            markerStart + response.preferredWidth,
            marker.anchoredPosition.y
        );
    }

    RectTransform marker;
    float markerStart = 0.0f;
    // End Typing Stuff ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~



    public static bool Finished = false;



    private void Awake()
    {
        try { messages = new List<TextMessage>(LoadMessages(MessageListName)); }
        catch (Exception e) {
            Debug.LogError(e.Message);
            Debug.LogError("Failed to Load Text Message JSON: Resourses/TextMessages/" + MessageListName);
            Destroy(this); 
        }

        scrollView = GetChild("Scroll View");
        content = GetChild("Content");

        hint = GetChild("Hint").GetComponent<Text>();
        response = GetChild("Response").GetComponent<Text>();
        marker = GetChild("Marker").GetComponent<RectTransform>();
        markerStart = marker.anchoredPosition.x;

        keyboard = GetChild("Keyboard").GetComponent<RectTransform>();
        UpdateButtons();
        for (int i = 0; i < keyboard.childCount; i++)
        {
            Transform but = keyboard.GetChild(i);
            but.GetComponent<Button>().onClick.AddListener(() => ButtonPressed(but));
        }

        UpdateResponse();

        AudioSystem.PlaySound("SFX/vibrate");
    }



    private void Update()
    {

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            string str = "";
            while (Random.value < 0.95f) str += (char)Mathf.FloorToInt(Random.value * 255.0f);
            AddMessage(str, Random.value > 0.5f);
        }*/

        if (messages.Count > 0 && messages[0].type == "response")
            UpdateResponse();

        Finished = messages.Count == 0;

        RawImage mi = marker.GetComponent<RawImage>();
        mi.color = new Color(
            mi.color.r,
            mi.color.g,
            mi.color.b,
            Mathf.Sin(Time.time * 6.0f) * 0.5f + 0.5f
        );

    }

    private void FixedUpdate()
    {

        if ((fixedCounter += Mathf.RoundToInt(UnityEngine.Random.value)) >= 50 && messages.Count > 0 && messages[0].type == "message")
        {
            AddMessage(messages[0].message, false);
            messages.RemoveAt(0);
            fixedCounter = 0;
        }

    }

    void ButtonPressed(Transform button)
    {
        switch (button.name)
        {
            case "Send":
                SendResponse();
                UpdateResponse();
                break;

            case "Del":
                if (currentResponse.Length > 0)
                    currentResponse = currentResponse.Substring(0, currentResponse.Length - 1);
                break;

            default:
                if (response.preferredWidth < 28.0f) currentResponse += buttonValues[int.Parse(button.name)];
                if (UnityEngine.Random.value < 0.15f && response.preferredWidth < 28.0f)
                    currentResponse += buttonValues[int.Parse(button.name)];
                UpdateResponse();
                break;
        }
        ShuffleButtons();
        AudioSystem.PlaySound("SFX/type");
    }


    public override void OnInteraction()
    {
    }

    public override void OnRelease()
    {
    }


}
