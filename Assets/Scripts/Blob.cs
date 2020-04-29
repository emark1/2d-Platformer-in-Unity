using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    [SerializeField] float moveSpeed = 1f;
    BoxCollider2D feetCollider;
    CapsuleCollider2D bodyCollider;
    LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        groundLayer = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        FlipSprite();
    }



    private void Move() {
        myRigidBody.velocity = new Vector2(moveSpeed, 0);        
    }


    private void FlipSprite(){
        // if (!feetCollider.IsTouchingLayers(groundLayer)) {
        //     // transform.localScale = new Vector2 (Mathf.Sign(myRigidBody.velocity.x), 1f*-1f);
        //     moveSpeed = moveSpeed * -1f;
        // }
        // // bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        // // if (playerHasHorizontalSpeed) {
        // //     transform.localScale = new Vector2 (Mathf.Sign(myRigidBody.velocity.x), 1f);
        // // }

        if (IsFacingRight()) {
            myRigidBody.velocity = new Vector2(moveSpeed, 0f);
        } else {
            myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    bool IsFacingRight() {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        transform.localScale = new Vector2 (-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }
}
