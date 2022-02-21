using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTreeLeft : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float leftBound = -15;

    private void Update()
    {
        CheckDist();
    }

    private void FixedUpdate()
    {
        MoveLeft();
    }

    public void MoveLeft()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    public void CheckDist()
    {
        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
}
