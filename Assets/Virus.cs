using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var sickMan = collision.gameObject.GetComponent<IBeingSick>();
        if (sickMan != null)
        {
            if (Random.Range(0f, 1f) < 1f)
                sickMan.gettingSick();
        }
    }

}
