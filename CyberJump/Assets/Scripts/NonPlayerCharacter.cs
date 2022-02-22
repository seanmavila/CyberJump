using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    public GameObject dialogBox2;

    private Rigidbody2D npcRb;
    private Animator npcAnim;
    private float timerDisplay;
    private bool dialogueEnd = false;
    private bool gameEnd = false;
    [SerializeField] private float speed = 1.0f;

    private void Start()
    {
        npcRb = GetComponent<Rigidbody2D>();
        npcAnim = GetComponent<Animator>();
        dialogBox.SetActive(false);
        dialogBox2.SetActive(false);
        timerDisplay = -1.0f;
        
    }
    private void Update()
    {
        CheckIfDisplayed();
        if (dialogueEnd)
        {
            MoveNpcLeft();
        }
        if (gameEnd)
        {
            npcAnim.SetBool("gameOver", true);
        }
    }

    public void CheckIfDisplayed()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {

                dialogBox.SetActive(false);
                dialogBox2.SetActive(false);
            }
        }
    }

    public void DisplayDialog(int dialogue)
    {
        timerDisplay = displayTime;

        switch (dialogue)
        {
            case 1:
                dialogBox.SetActive(true);
                break;
            case 2:
                dialogBox2.SetActive(true);
                StartCoroutine(WaitToExit());
                break;
        }

    }

    public void MoveNpcLeft()
    {
        transform.localScale = new Vector3(-1, 1, 1);
        npcAnim.SetBool("isRunning", true);
        transform.Translate(Vector3.left * Time.deltaTime * speed);
        if(transform.position.x <= -15)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator WaitToExit()
    {
        yield return new WaitForSeconds(4);
        dialogueEnd = true;
    }

    public void NpcGameEnd(int dialogue)
    {
        timerDisplay = displayTime;

        switch (dialogue)
        {
            case 1:
                dialogBox.SetActive(true);
                break;
            case 2:
                dialogBox2.SetActive(true);
                gameEnd = true;
                break;
        }
    }


}
