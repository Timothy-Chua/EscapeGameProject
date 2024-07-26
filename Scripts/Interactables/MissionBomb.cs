using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionBomb : Interactable
{
    public GameObject bombMesh;
    public Collider bombTrigger;

    protected override void Interact()
    {
        base.Interact();

        if (!GameManager.instance.isBombFound)
        {
            bombMesh.SetActive(true);

            StartCoroutine(GameManager.instance.QuickReaction("A bomb!? I need to find a way to disarm it."));
            GameManager.instance.isBombFound = true;
        }
        else if (GameManager.instance.isScissorsCollected && GameManager.instance.isBombFound && !GameManager.instance.isBombDefused)
        {
            bombTrigger.enabled = false;
            StartCoroutine(GameManager.instance.QuickReaction("I need to get out of here."));
            GameManager.instance.isBombDefused = true;
        }
        else
        {
            StartCoroutine(GameManager.instance.QuickReaction("I need to find something to disarm it."));
        }
        
    }
}
