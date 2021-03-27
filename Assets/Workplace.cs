using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workplace : MonoBehaviour, IInteractable
{
    public Transform offset;
    GameObject interactingObject;

    private void Update()
    {
        if (interactingObject != null && (interactingObject.transform.position-offset.position).magnitude > 0.5f)
        {
            ClearPlace();
        }
    }
    public void Interact(GameObject obj)
    {
        if (interactingObject == null)
        {
            interactingObject = obj;
            interactingObject.transform.position = offset.position;
            interactingObject.transform.rotation = offset.rotation;
            Debug.Log(interactingObject + " is working.");
        }
    }

    public void ClearPlace()
    {
        if (interactingObject != null)
        {
            Debug.Log(interactingObject + " ends his work.");
            interactingObject = null;
        }
    }

}
