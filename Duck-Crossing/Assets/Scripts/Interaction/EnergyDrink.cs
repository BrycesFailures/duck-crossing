using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrink : Interactable
{

    public override void OnInteraction()
    {
    }

    public override void OnRelease()
    {
        Camera.main.GetComponent<VisualEffect>().Target = -1.0f;
        AudioSystem.PlaySound("SFX/drink");
        Destroy(gameObject);
    }
}
