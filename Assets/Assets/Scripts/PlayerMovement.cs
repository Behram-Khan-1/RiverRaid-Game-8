using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float directionX = 0;
    [SerializeField] private float directionY = 0;
    [SerializeField] private float oldDirectionY = 0;
    [SerializeField] private int turnRotationAngleZ;
    [SerializeField] private int turnRotationAngleY;
    [SerializeField] private int rotationSpeed;
    [SerializeField] private float turnSpeed;
    public event Action<float> onSpeedChange;
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
        directionX = Input.GetAxisRaw("Horizontal");
        directionY = Input.GetAxisRaw("Vertical");
        if (directionX > 0)
        {
            directionX = 1;
        }
        else if (directionX < 0)
        {
            directionX = -1;
        }


        if (directionY != oldDirectionY)
        {
            if (directionY > 0)
            {
                oldDirectionY = directionY;
                onSpeedChange?.Invoke(directionY);
            }
            else if (directionY < 0 || directionY == 0)
            {
                oldDirectionY = directionY;
                onSpeedChange?.Invoke(directionY);
            }
        }
    }

    void FixedUpdate()
    {
        Movement();
    }


    private void Movement()
    {
        transform.Translate(new Vector2(directionX * turnSpeed, 0) * Time.fixedDeltaTime, Space.World);
        Quaternion newAngle = Quaternion.Euler(0, turnRotationAngleY * -directionX, turnRotationAngleZ * -directionX);
        transform.rotation = Quaternion.Slerp(transform.rotation, newAngle, Time.fixedDeltaTime * rotationSpeed);
    }
}
