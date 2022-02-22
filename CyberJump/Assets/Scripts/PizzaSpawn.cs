using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaSpawn : MonoBehaviour
{
    public GameObject[] pizzaList;

    private Vector3 spawnPos = new Vector3(-6, 241, 0);
    [SerializeField] private float startDelay = 2.0f;
    [SerializeField] private float repeatRate = 1.5f;
    public void SpawnStart()
    {
        InvokeRepeating("SpawnPizza", startDelay, repeatRate);
    }
    public void SpawnPizza()
    {
        spawnPos = new Vector3(Random.Range(-7, -4), 245, 0);
        int randIndex = Random.Range(0, pizzaList.Length - 1);
        Instantiate(pizzaList[randIndex], spawnPos, pizzaList[randIndex].transform.rotation);
    }
}
