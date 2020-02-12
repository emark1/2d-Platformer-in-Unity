using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float runSpeed = 10f;
    float xMinimum;
    float xMaximum;

    Rigidbody2D myRigidBody;


    // private void Move() {
    //     var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * runSpeed;
    //     var newXPosition = Mathf.Clamp(transform.position.x + deltaX, xMinimum, xMaximum);
    //     transform.position = new Vector2(newXPosition, transform.position.y);
    // }

    private void Run() {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow*runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }
}
