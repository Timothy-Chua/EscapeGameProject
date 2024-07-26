using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraInteractable : Interactable
{
    protected override void Interact()
    {
        base.Interact();

        StartCoroutine(GameManager.instance.QuickReaction("There is nothing here"));
    }
}
