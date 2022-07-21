using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class Wander : MonoBehaviour
{
    public float pursuitSpeed;
    public float wanderSpeed;
    float currentSpeed;
    public float directionChangeInterval;
    public bool followPlayer;
    Coroutine moveCoroutine;
    Rigidbody2D rb2d;
    Animator animator;
    Transform targetTransform = null;
    Vector3 endPosition;
    float currentAngle = 0;
    CircleCollider2D circleCollider;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentSpeed = wanderSpeed;
        rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine(WanderRoutine());
        circleCollider = GetComponent<CircleCollider2D>();
    }

    public IEnumerator WanderRoutine() {
        while(true) {
            ChooseNewEndPoint();
            if(moveCoroutine != null) {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(Move(rb2d, currentSpeed));
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    void ChooseNewEndPoint() {
        currentAngle += Random.Range(0, 360);
        currentAngle = Mathf.Repeat(currentAngle, 360);
        endPosition += Vector3FromAngle(currentAngle);
    }

    Vector3 Vector3FromAngle(float inputAngleDegrees) {
        float inputAngleRadians = inputAngleDegrees * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians), 0);
    }

    public IEnumerator Move(Rigidbody2D rigidBodyToMove, float speed) {
        float remainingDistance = (transform.position - endPosition).sqrMagnitude;
        while(remainingDistance > float.Epsilon) {
            if(targetTransform != null) {
                endPosition = targetTransform.position;
            }

            if(rigidBodyToMove != null) {
                animator.SetBool("isWalking", true);
                Vector3 newPosition = Vector3.MoveTowards(rigidBodyToMove.position, endPosition, speed*Time.deltaTime);
                rb2d.MovePosition(newPosition);
                remainingDistance = (transform.position - endPosition).sqrMagnitude;
            }
            yield return new WaitForFixedUpdate();
        }

        animator.SetBool("isWalking", false);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")&&followPlayer) {
            currentSpeed = pursuitSpeed;
            targetTransform = collision.gameObject.transform;
            if(moveCoroutine != null) {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(Move(rb2d, currentSpeed));
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            animator.SetBool("isWalking", false);
            currentSpeed = wanderSpeed;
            if(moveCoroutine != null) {
                StopCoroutine(moveCoroutine);
            }
            targetTransform = null;
        }
    }

    void OnDrawGizmos() {
        if(circleCollider != null) {
            Gizmos.DrawWireSphere(transform.position, circleCollider.radius);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(rb2d.position, endPosition, Color.red);   
    }
}
