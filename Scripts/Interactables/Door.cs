using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
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

        if (!isOpen)
        {
            animator.SetTrigger("isOpen");
            isOpen = true;

            GameManager.instance.txtReact.text = "";
            trigger.enabled = false;
        }
    }
}
