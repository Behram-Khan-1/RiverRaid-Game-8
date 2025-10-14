using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnLeft, bulletSpawnRight;
    bool isShootingLeft = true;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] float originalFireRate = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (fireRate >= originalFireRate)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject bullet;
                if (isShootingLeft)
                {
                    bullet = Instantiate(bulletPrefab, bulletSpawnLeft.position, Quaternion.identity);
                }
                else
                {
                    bullet = Instantiate(bulletPrefab, bulletSpawnRight.position, Quaternion.identity);
                }
                isShootingLeft = !isShootingLeft;

                bullet.GetComponent<Bullet>().SetupBullet(bulletSpeed);
                fireRate = 0;
            }
        }
        else
        {
            fireRate = fireRate + Time.deltaTime;
        }
    }



    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ground"))
        {
            Debug.Log("Collided with ground");
        }
    }
}
