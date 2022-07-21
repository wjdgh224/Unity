using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    Vector2 movement = new Vector2();

    Animator animator;

    Rigidbody2D rb2D;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateState();
    }

    void FixedUpdate() {
        MoveCharacter();
    }

    private void MoveCharacter() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();
        rb2D.velocity = movement * movementSpeed;
    }

    private void UpdateState() {
        // if(movement.x > 0) {
        //     animator.SetInteger(animationState, (int)CharStates.walkEast);
        // }
        // else if(movement.x < 0) {
        //     animator.SetInteger(animationState, (int)CharStates.walkWest);
        // }
        // else if(movement.y > 0) {
        //     animator.SetInteger(animationState, (int)CharStates.walkNorth);
        // }
        // else if(movement.y < 0) {
        //     animator.SetInteger(animationState, (int)CharStates.walkSouth);
        // }
        // else {
        //     animator.SetInteger(animationState, (int)CharStates.idleSouth);
        // }

        if(Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0)) {
            animator.SetBool("isWalking", false);
        }
        else {
            animator.SetBool("isWalking", true);
        }

        animator.SetFloat("xDir", movement.x);
        animator.SetFloat("yDir", movement.y);
    }
}
