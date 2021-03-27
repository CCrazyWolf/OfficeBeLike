using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour, IInteractable, IBeingSick
{
    NavMeshAgent agent;
    GameObject currentTarget;

    bool isSick = false;
    public float deseaseLevel = 0f;



    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    public void Interact(GameObject obj)
    {
        currentTarget = null; ////////////////////////////////////
    }

    public void gettingSick()
    {
        isSick = true;
        StartCoroutine(beingSick());
    }

    public void Cured()
    {
        isSick = false;
        deseaseLevel = 0f;
    }

    public void Quarantine()
    {
        Debug.Log("NPC is sick. He can not do work!");
    }

    IEnumerator beingSick()
    {
        while (deseaseLevel <= 98f)
        {
            deseaseLevel += 2f;
            yield return new WaitForSeconds(2);
        }
        if (isSick)
            Quarantine();
    }
}
