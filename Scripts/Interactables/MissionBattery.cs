using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionBattery : Interactable
{
    protected override void Interact()
    {
        base.Interact();

        GameManager.instance.isBatteryCollected = true;
        GameManager.instance.txtReact.text = "";
        this.gameObject.SetActive(false);
    }
}
