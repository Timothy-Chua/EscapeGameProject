using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCR : Interactable
{
    private bool isOpen;
    private Animator animator;
    public Collider trigger;

    protected override void Start()
    {
        base.Start();

        isOpen = false;
        animator = GetComponent<Animator>();
        trigger.enabled = true;
    }

    protected override void Interact()
    {
        base.Interact();

        if (!isOpen && GameManager.instance.isKeyCRCollected)
        {
            animator.SetTrigger("isOpen");
            isOpen = true;

            GameManager.instance.txtReact.text = "";
            GameManager.instance.txtObjSub.text = "";
            trigger.enabled = false;
        }
        else
        {
            StartCoroutine(GameManager.instance.QuickReaction("The door is locked. I need to find the key."));
            GameManager.instance.txtObjSub.text = "Find the key to the restroom.";
        }
    }
}
