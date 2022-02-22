using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryPizza : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DestroyPizzaObject());
    }

    IEnumerator DestroyPizzaObject()
    {
        yield return new WaitForSeconds(15);
        Destroy(gameObject);
    }
}
