using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{

    public abstract void OnInteraction();
    public abstract void OnRelease();

}
