using UnityEngine;

public class Phone : Interactable
{

    [System.Serializable]
    public struct TextMessageList
    {
        public TextMessage[] messages;
    }

    [System.Serializable]
    public struct TextMessage
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
    TextMessage[] messages = null;

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

    public GameObject messagePrefab;
    public GameObject responsePrefab;

    TextMessageBox previousBox = null;

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

        return obj;
    }



    private void Awake()
    {
        try { messages = LoadMessages(MessageListName); }
        catch { Debug.LogError("Failed to Load Text Message JSON: Resourses/TextMessages/" + MessageListName); Destroy(this); }

        content = GetChild("Scroll View");
        content = GetChild("Content");
    }



    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string str = "";
            while (Random.value < 0.95f) str += (char)Mathf.FloorToInt(Random.value * 255.0f);
            AddMessage(str, Random.value > 0.5f);
        }

    }


    public override void OnInteraction()
    {
    }

    public override void OnRelease()
    {
    }


}
