using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject playerRb;

    private void Start()
    {
    }

    private void Update()
    {
        MoveCamera();
    }

    public void MoveCamera()
    {
        if (playerRb.transform.position.y >= (mainCamera.transform.position.y + mainCamera.orthographicSize))
        {
            mainCamera.transform.Translate(0f, mainCamera.orthographicSize * 2, 0f);
        }

        if (playerRb.transform.position.y < (mainCamera.transform.position.y - mainCamera.orthographicSize))
        {
            mainCamera.transform.Translate(0f, -mainCamera.orthographicSize * 2, 0f);
        }      
    }
}
