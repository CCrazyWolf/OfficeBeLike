using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour, IInteractable, IBeingSick
{
    public GameObject maskPoint;
    public GameObject workplace;

    NavMeshAgent agent;
    GameObject currentTarget;

    bool isSick = false;
    public float deseaseLevel = 0f;
    float timerForNewVirus = 0f;
    bool exausted = false;
    bool withMask = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        currentTarget = workplace;
    }
    public void Interact(GameObject obj)
    {
        currentTarget = maskPoint;
        agent.SetDestination(maskPoint.transform.position);
    }

    private void Update()
    {
        if (!exausted)
        {
            setTarget(workplace);
        }
        else if (!withMask && deseaseLevel > 70f)
        {
            setTarget(GameManager.instance.washers[Random.Range(0, 1)]);
        }
        else
        {
            setTarget(GameManager.instance.coffeeCups[Random.Range(0, 1)]);
        }

    }

    void setTarget(GameObject obj)
    {
        if (currentTarget == obj)
            return;
        currentTarget = obj;
        agent.SetDestination(currentTarget.transform.position);
    }

    public void gettingSick()
    {
        if (!isSick)
        {
            isSick = true;
            StartCoroutine("beingSick");
        }
    }

    public void Cured()
    {
        isSick = false;
        StopCoroutine("beingSick");
        deseaseLevel = 0f;
    }

    public void spawnVirus()
    {
        GameManager.instance.SpawnVirus(transform.position);
    }

    public void Quarantine()
    {
        Debug.Log("NPC is sick. He can not do work!");
    }

    IEnumerator beingSick()
    {
        while (deseaseLevel <= 20)
        {
            deseaseLevel += 2f;
            if (Random.value < 0.3f)
                spawnVirus();
            yield return new WaitForSeconds(1.5f);
        }
        if (isSick && deseaseLevel == 100f)
            Quarantine();
    }
}
