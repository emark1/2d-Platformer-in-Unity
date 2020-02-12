using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    //Player Movement:
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 10f;

    //Variables
    Rigidbody2D myRigidBody;
    Animator animator;

    //Functions
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Jump();
        Run();
        FlipSprite();
        UpdateAnimationState();
    }

    private void Run() {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow*runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        animator.SetBool("Running", true);
    }

    private void Jump() {
        if (Input.GetButtonDown("Jump")) {
            Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocity;
        }
    }

    private void UpdateAnimationState() {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (!playerHasHorizontalSpeed) {
            animator.SetBool("Running", false);
        }
    }

    private void FlipSprite(){
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed) {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }


}
