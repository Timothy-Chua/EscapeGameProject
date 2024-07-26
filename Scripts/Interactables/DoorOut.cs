using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOut : Interactable
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Interact()
    {
        base.Interact();

        if (GameManager.instance.isBombDefused)
        {
            StartCoroutine(GameManager.instance.GameOverTrigger());
        }
        else
        {
            StartCoroutine(GameManager.instance.QuickReaction("I can't go out yet, I still need to do something"));
        }
    }
}
