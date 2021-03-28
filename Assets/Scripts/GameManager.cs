using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    List<GameObject> viruses;

    public GameObject virusPrefab;
    public GameObject[] coffeeCups;
    public GameObject[] washers;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;

        viruses = new List<GameObject>();

        StartCoroutine("virusSpawner");
    }

    public void SpawnVirus()
    {
        GameObject virus = Instantiate(virusPrefab, new Vector3(Random.Range(-5.4f, 7.3f), Random.Range(-6.1f, 5.9f), 0f), Quaternion.identity);
        viruses.Add(virus);
    }
    public void SpawnVirus(Vector3 pos)
    {
        if (viruses.Count <= 50)
        {
            GameObject virus = Instantiate(virusPrefab, pos, Quaternion.identity);
            viruses.Add(virus);
        }
    }

    IEnumerator virusSpawner()
    {
        while (true)
        {
            if (viruses.Count <= 20)
            {
                SpawnVirus();
            }
            yield return new WaitForSeconds(Random.Range(6f, 10f));
        }
    }
}
