using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// I wrote all this shit and then realized we are using the 2D scene and not the canvas :,)

/*
public class UIInteraction 
{

    /// Returns if the pointer is over any ui.
    public static bool IsPointerOverUI()
    {
        return IsPointerOverUI(GetEventSystemRaycastResults());
    }

    /// Returns if the pointer is over ui with a specified tag.
    public static bool IsPointerOverUI(string tag)
    {
        return IsPointerOverUI(GetEventSystemRaycastResults(), tag);
    }

    /// Returns if the pointer is over ui with a specified tag, except a specified object.
    public static bool IsPointerOverUINot(Vector2 pos, string tag, GameObject not)
    {
        return IsPointerOverUI(GetEventSystemRaycastResults(pos), tag, not);
    }

    /// Returns if the pointer is over the specific object.
    public static bool IsPointerOver(GameObject obj)
    {
        List<RaycastResult> eventSystemRaysastResults = GetEventSystemRaycastResults();
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject == obj)
                return true;
        }
        return false;
    }

    private static bool IsPointerOverUI(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                return true;
        }
        return false;
    }

    private static bool IsPointerOverUI(List<RaycastResult> eventSystemRaysastResults, string tag)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI") && curRaysastResult.gameObject.tag == tag)
                return true;
        }
        return false;
    }

    private static bool IsPointerOverUI(List<RaycastResult> eventSystemRaysastResults, string tag, GameObject not)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject != not && curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI") && curRaysastResult.gameObject.tag == tag)
                return true;
        }
        return false;
    }



    /// Returns if the pointer's closest interaction is an object with a specified tag.
    public static bool IsPointerTouchingUI(string tag)
    {
        return IsPointerOverUI(GetEventSystemRaycastResults(), tag);
    }

    /// Returns if the pointer is over ui with a specified tag, except a specified object.
    public static bool IsPointerTouchingUINot(Vector2 pos, string tag, GameObject not)
    {
        return IsPointerOverUI(GetEventSystemRaycastResults(pos), tag, not);
    }

    /// Returns if the pointer is touching the specific object.
    public static bool IsPointerTouching(GameObject obj)
    {
        List<RaycastResult> eventSystemRaysastResults = GetEventSystemRaycastResults();
        if (eventSystemRaysastResults.Count == 0) return false;
        return eventSystemRaysastResults[0].gameObject == obj;
    }

    private static bool IsPointerTouchingUI(List<RaycastResult> eventSystemRaysastResults)
    {
        if (eventSystemRaysastResults.Count == 0) return false;
        return eventSystemRaysastResults[0].gameObject.layer == LayerMask.NameToLayer("UI");
    }

    private static bool IsPointerTouchingUI(List<RaycastResult> eventSystemRaysastResults, string tag)
    {
        if (eventSystemRaysastResults.Count == 0) return false;
        return eventSystemRaysastResults[0].gameObject.layer == LayerMask.NameToLayer("UI") && eventSystemRaysastResults[0].gameObject.tag == tag;
    }

    private static bool IsPointerTouchingUI(List<RaycastResult> eventSystemRaysastResults, string tag, GameObject not)
    {
        if (eventSystemRaysastResults.Count == 0) return false;
        return eventSystemRaysastResults[0].gameObject != not && eventSystemRaysastResults[0].gameObject.layer == LayerMask.NameToLayer("UI") && eventSystemRaysastResults[0].gameObject.tag == tag;
    }



    public static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }

    public static List<RaycastResult> GetEventSystemRaycastResults(Vector2 pixelPos)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = pixelPos;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }

}
*/