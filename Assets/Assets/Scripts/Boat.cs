using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    Animator animator;
    bool isTurning;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!isTurning)
        {
            transform.Translate(Vector2.right * speed * Time.fixedDeltaTime, Space.Self);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ground"))
        {
            // Debug.Log("Collided with ground");
            Turn();
            //Turn Direction
        }

        if (collider.CompareTag("Bullet"))
        {
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }

        if (collider.CompareTag("Player"))
        {
            GameManager.instance.OnPlayerDead();
            Destroy(gameObject);
            //Player Killed
        }
    }

    void Turn()
    {
        if (transform.right.x > 0) //Moving right, turn left
        {
            animator.SetTrigger("TurnLeft");
        }
        else
        {
            animator.SetTrigger("TurnRight");
        }
        isTurning = true;
    }

    public void StopTurn()
    {
        transform.Rotate(new Vector3(0f, 180f, 0f), Space.Self);
        isTurning = false;

    }
}
