using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float direction = 0;
    [SerializeField] private int turnRotationAngleZ;
    [SerializeField] private int turnRotationAngleY;
    [SerializeField] private int rotationSpeed;
    [SerializeField] private float turnSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InputHandler();

    }

    private void InputHandler()
    {
        direction = Input.GetAxisRaw("Horizontal");
        if (direction > 0)
        {
            direction = 1;
        }
        else if (direction < 0)
        {
            direction = -1;
        }
    }

    void FixedUpdate()
    {
        Movement();
    }


    private void Movement()
    {
        transform.Translate(new Vector2(direction * turnSpeed, 0) * Time.fixedDeltaTime, Space.World);
        Quaternion newAngle = Quaternion.Euler(0, turnRotationAngleY * -direction, turnRotationAngleZ * -direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, newAngle, Time.fixedDeltaTime * rotationSpeed);
    }
}
