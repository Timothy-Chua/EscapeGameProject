using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionKey : Interactable
{
    public GameObject keyMesh;

    protected override void Interact()
    {
        base.Interact();

        if (GameManager.instance.isFlashlightCollected && GameManager.instance.isBatteryCollected)
        {
            GameManager.instance.isKeyCRCollected = true;
            GameManager.instance.txtObjSub.text = "Open the bathroom door.";

            StartCoroutine(ClearAfterCollect());
        }
        else if (GameManager.instance.isFlashlightCollected && !GameManager.instance.isBatteryCollected)
        {
            StartCoroutine(GameManager.instance.QuickReaction("I can't turn on the flashlight"));
        }
        else
        {
            StartCoroutine(GameManager.instance.QuickReaction("It's too dark. I need a light."));
        }
    }

    protected virtual IEnumerator ClearAfterCollect()
    {
        StartCoroutine(GameManager.instance.QuickReaction("Got the key."));
        keyMesh.SetActive(false);

        yield return new WaitForSeconds(GameManager.instance.reactClearDelay);

        this.gameObject.SetActive(false);
    }
}
