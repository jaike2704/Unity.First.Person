using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string promtMassage;

    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {
        //No code written here in this fucntion.
    }
}
