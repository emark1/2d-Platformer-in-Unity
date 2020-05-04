using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    //Player Movement:
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float climbSpeed = 10f;
    [SerializeField] float jumpSpeed = 10f;

    //Variables
    Rigidbody2D myRigidBody;
    Animator animator;
    CapsuleCollider2D myCollider;
    BoxCollider2D feetCollider;
    LayerMask groundLayer;
    LayerMask ladderLayer;
    LayerMask enemyLayer;
    LayerMask hazardLayer;
    LayerMask interactableLayer;

    [SerializeField] bool alive = true;
    AudioSource playerAudio;


    //Functions
    void Start()
    {
        ladderLayer = LayerMask.GetMask("Ladder");
        groundLayer = LayerMask.GetMask("Ground");
        interactableLayer = LayerMask.GetMask("Interactable");
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        enemyLayer = LayerMask.GetMask("Enemy");
        hazardLayer = LayerMask.GetMask("Hazard");
        playerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        Movement();
        PlayerDeath();
        FlipSprite();
        UpdateAnimationState();
    }

    private void Movement() {
        if (alive) {
            Run();
            Jump();
            Climb();
        }
    }
    private void Run() {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow*runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        animator.SetBool("Running", true);
    }

    private void Jump() {
        if (feetCollider.IsTouchingLayers(groundLayer)) {
            if (Input.GetButtonDown("Jump")) {
                Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
                myRigidBody.velocity += jumpVelocity;
            }
        }
    }

    private void Climb() {
        if (myCollider.IsTouchingLayers(ladderLayer)) {
            animator.SetBool("Climbing", true);

            float controlThrow = Input.GetAxis("Vertical");
            Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow*climbSpeed);
            myRigidBody.velocity = climbVelocity;
            myRigidBody.gravityScale = 0;

        }
    }

    private void PlayerDeath() {
        if (myCollider.IsTouchingLayers(hazardLayer) || myCollider.IsTouchingLayers(enemyLayer)) {
            alive = false;
            myRigidBody.velocity = new Vector2(3f, 20f);
            playerAudio.Play();
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    private void UpdateAnimationState() {
        if (alive) {
            bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
            if (!playerHasHorizontalSpeed) {
                animator.SetBool("Running", false);
            }

            if (!myCollider.IsTouchingLayers(ladderLayer)) {
                animator.SetBool("Climbing", false);
                myRigidBody.gravityScale = 6.5f;
            }
        }

        if (!alive) {
            animator.SetBool("Dead", true);
            animator.SetBool("Running", false);
            animator.SetBool("Climbing", false);
        }
    }

    private void FlipSprite(){
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed) {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

}
