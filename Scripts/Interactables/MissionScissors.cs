using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionScissors : Interactable
{
    public GameObject scissorMesh;

    protected override void Interact()
    {
        base.Interact();

        if (GameManager.instance.isBombFound)
        {
            GameManager.instance.isScissorsCollected = true;
            StartCoroutine(ClearAfterCollect());
        }
        else
        {
            StartCoroutine(GameManager.instance.QuickReaction("There is nothing here."));
        }
    }

    protected virtual IEnumerator ClearAfterCollect()
    {
        StartCoroutine(GameManager.instance.QuickReaction("I think I can use this to cut the wires."));
        scissorMesh.SetActive(false);

        yield return new WaitForSeconds(GameManager.instance.reactClearDelay);

        this.gameObject.SetActive(false);
    }
}
