using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionFlashlight : Interactable
{
    protected override void Interact()
    {
        base.Interact();

        GameManager.instance.isFlashlightCollected = true;
        GameManager.instance.txtReact.text = "";
        this.gameObject.SetActive(false);
    }
}
