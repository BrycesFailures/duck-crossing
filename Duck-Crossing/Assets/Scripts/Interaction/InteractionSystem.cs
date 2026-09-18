using UnityEngine;

public class InteractionSystem : MonoBehaviour
{

    /// The sprites used for the cursor
    [Header("Cursor Stuff")]
    public GameObject CursorObject;
    public Sprite defaultCursor, hoverCursor, clickCursor, grabCursor;



    /// A list of all of the things in front of the mouse, in order.
    public static Collider2D[] MouseCollisions = {};
    private static GameObject lastInteractedObject = null;

    /// Returns if the object is overlapping the mouse.
    public static bool TouchingMouse(GameObject obj)
    {
        for (int i = 0; i < MouseCollisions.Length; i++)
            if (MouseCollisions[i].gameObject == obj) return true;
        return false;
    }

    /// Releases the current interaction and invokes the release method.
    public static void Release()
    {
        lastInteractedObject.GetComponent<Interactable>().OnRelease();
        lastInteractedObject = null;
    }



    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }



    private void Update()
    {
        MouseCollisions = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        // begin an interaction with the closest thing
        if (Input.GetMouseButtonDown(0) && MouseCollisions.Length > 0)
        {
            Interactable interactable = MouseCollisions[0].gameObject.GetComponent<Interactable>();
            if (interactable)
            {
                lastInteractedObject = MouseCollisions[0].gameObject;
                interactable.OnInteraction();
            }
        }

        // if you release, release the interaction
        if (Input.GetMouseButtonUp(0) && lastInteractedObject)
            Release();

        // cursor whatnot
        CursorStuff();

    }



    void CursorStuff()
    {
        SpriteRenderer cursorSprite = CursorObject.GetComponent<SpriteRenderer>();
        Vector3 wmp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CursorObject.transform.position = new Vector3(
            wmp.x + cursorSprite.bounds.size.x * 0.5f,
            wmp.y - cursorSprite.bounds.size.y * 0.5f,
            CursorObject.transform.position.z
        );

        cursorSprite.sprite = defaultCursor;

        if (Input.GetMouseButton(0))
            cursorSprite.sprite = clickCursor;

        if (MouseCollisions.Length > 0 && MouseCollisions[0].gameObject.tag == "Interactable")
            cursorSprite.sprite = hoverCursor;

        if (lastInteractedObject && lastInteractedObject.tag == "Interactable")
            cursorSprite.sprite = grabCursor;
    }

}
