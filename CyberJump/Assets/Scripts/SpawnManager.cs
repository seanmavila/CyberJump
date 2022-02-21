using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] palmTrees;

    private Vector3 spawnPos = new Vector3(25, -3, 0);
    [SerializeField] private float startDelay = 2.0f;
    [SerializeField] private float repeatRate = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObject", startDelay, repeatRate);
    }

    private void SpawnObject()
    {
        int randIndex = Random.Range(0, palmTrees.Length - 1);
        Instantiate(palmTrees[randIndex], spawnPos, palmTrees[randIndex].transform.rotation);
    }
}
