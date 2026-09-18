using UnityEngine;
using UnityEngine.UI;

public class TextMessageBox : MonoBehaviour
{
    Text textRenderer;
    RectTransform rectTransform;

    public Vector2 Padding = Vector2.zero;
    public float Height { get; private set; } = 0.0f;

    private void Awake()
    {
        textRenderer = GetChild("Text").GetComponent<Text>();
        rectTransform = GetChild("Background").GetComponent<RectTransform>();
    }

    private void Update()
    {
        Shrink();
    }

    public void Shrink()
    {
        textRenderer.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectTransform.rect.width - Padding.x * 2.0f);
        Height = textRenderer.preferredHeight + Padding.y * 2.0f;
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Height);
        textRenderer.rectTransform.anchoredPosition = new Vector2(Padding.x * textRenderer.rectTransform.localScale.x, -Padding.y * textRenderer.rectTransform.localScale.y);
    }

    private RectTransform GetChild(string name)
    {
        RectTransform[] children = GetComponentsInChildren<RectTransform>();
        foreach (RectTransform child in children)
            if (child.name == name) return child;
        return null;
    }

    public void SetText(string str)
    {
        textRenderer.text = str;
    }
}
