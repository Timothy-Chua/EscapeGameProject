using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected virtual void OnTriggerEnter(Collider actor)
    {
        if (GameManager.instance.state == GameState.isResume)
        {
            if (actor.gameObject.CompareTag("Player"))
            {
                GameManager.instance.txtReact.text = "Press [E] to interact";
            }
        }
    }

    protected virtual void OnTriggerStay(Collider actor)
    {
        if (GameManager.instance.state == GameState.isResume)
        {
            if (actor.gameObject.CompareTag("Player"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Interact();
                }
            }
        }
    }

    protected virtual void OnTriggerExit(Collider actor)
    {
        if (GameManager.instance.state == GameState.isResume)
        {
            if (actor.gameObject.CompareTag("Player"))
            {
                GameManager.instance.txtReact.text = "";
            }
        }
    }

    protected virtual void Interact()
    {

    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
