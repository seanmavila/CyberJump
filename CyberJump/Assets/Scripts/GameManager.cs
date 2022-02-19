using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject playerRb;

    private GameObject exitMenu;
    private bool menuOpen = false;

    private void Start()
    {
        exitMenu = GameObject.Find("Exit Menu");
        exitMenu.SetActive(false);
    }

    private void Update()
    {
        MoveCamera();
        EscapeBtn();

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

    public void EscapeBtn()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuOpen = !menuOpen;
            exitMenu.SetActive(menuOpen);
        }
    }
}
