using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    private float repeatWidth;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider2D>().size.x;
    }

    private void FixedUpdate()
    {
        MoveLeft();
        RepeatBackground();
    }

    public void MoveLeft()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    public void RepeatBackground()
    {
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
