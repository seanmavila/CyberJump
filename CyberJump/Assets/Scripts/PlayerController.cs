using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public bool isGrounded;
    public LayerMask groundMask;
    public bool canJump = true;
    public float jumpForce = 0.0f;
    public PhysicsMaterial2D bounceMat, normalMat;
    public AudioClip jumpSound;
    public AudioClip bounceSound;
    public GameObject dialogueContainer;


    private float moveInput;
    private Rigidbody2D playerRb;
    private Animator playerAnim;
    private bool isMoving = false;
    private Vector2 lookDirection = new Vector2(1, 0);
    [SerializeField] private float jumpLimit = 20f;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerJump();
        LookForNPC();
    }

    void PlayerJump()
    {
        // Get input axis
        moveInput = Input.GetAxisRaw("Horizontal");
        Vector2 move = new Vector2(moveInput, 0f);
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        // Checks if the character is grounded
        isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x - 0.095f, gameObject.transform.position.y - 1f), new Vector2(0.65f, 0.1f), 0f, groundMask);

        playerAnim.SetBool("isJumping", !isGrounded);
        playerAnim.SetFloat("yVelocity", playerRb.velocity.y);

        if (playerRb.velocity.y == 0 + Mathf.Epsilon)
        {
            playerAnim.SetBool("isFalling", false);
        }

        if (isMoving)
        {
            playerAnim.SetBool("isRunning", true);
        }
        else
        {
            playerAnim.SetBool("isRunning", false);
        }

        // Stops player from moving while pressing space or while in the air
        if (jumpForce == 0.0f && isGrounded)
        {
            playerRb.velocity = new Vector2(moveInput * walkSpeed, playerRb.velocity.y);
            if (moveInput < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                isMoving = true;
            }
            else if (moveInput > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
            
        }

        // Switches the players material to bouncy when jumping
        if (jumpForce > 0)
        {
            playerRb.sharedMaterial = bounceMat;
        }
        else
        {
            playerRb.sharedMaterial = normalMat;
        }

        // Charges player jump
        if (Input.GetKey(KeyCode.Space) && isGrounded && canJump)
        {
            playerRb.velocity = new Vector2(0f, 0f);
            jumpForce += 15f * Time.deltaTime;
            playerAnim.SetBool("isCrouching", true);
            playerAnim.SetBool("isRunning", false);
        }

        // Player jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            playerRb.velocity = new Vector2(0.0f, playerRb.velocity.y);
        }

        // Automatic jump once players jump charge reaches 20
        if (jumpForce >= jumpLimit && isGrounded)
        {
            float tempX = moveInput * walkSpeed;
            float tempY = jumpForce;
            playerRb.velocity = new Vector2(tempX, tempY);
            Invoke("ResetJump", 0.1f);
            playerAnim.SetBool("isCrouching", false);
            playerAnim.SetBool("isRunning", false);
            SoundManager.instance.PlaySingle(jumpSound);
        }

        // Player jumps with current jumpForce value
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isGrounded)
            {
                playerRb.velocity = new Vector2(moveInput * walkSpeed, jumpForce);
                jumpForce = 0.0f;
                playerAnim.SetBool("isCrouching", false);
                playerAnim.SetBool("isRunning", false);
                SoundManager.instance.PlaySingle(jumpSound);

            }
            canJump = true;
        }

    }

    // Resets players jump state
    void ResetJump()
    {
        canJump = false;
        jumpForce = 0;
    }

    public void LookForNPC()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(playerRb.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
            if (hit.collider != null)
            {
                character.DisplayDialog(1);
                StartCoroutine(dialogueTimer(character));
            }
        }
    }

    IEnumerator dialogueTimer(NonPlayerCharacter character)
    {
        yield return new WaitForSeconds(4);
        character.DisplayDialog(2);
    }

    //Draws the isGrounded overlapbox shape and position
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x - 0.095f, gameObject.transform.position.y - 1f), new Vector2(0.65f, 0.1f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isGrounded)
        {
            SoundManager.instance.PlaySingle(bounceSound);
        }

        if(collision.tag == "Dialogue")
        {
            dialogueContainer.SetActive(true);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Dialogue")
        {
            dialogueContainer.SetActive(false);
        }
    }




}
