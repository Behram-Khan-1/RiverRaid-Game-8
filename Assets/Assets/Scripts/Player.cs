using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnLeft, bulletSpawnRight;
    [SerializeField] private List<GameObject> activeBullets;
    bool isShootingLeft = true;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] float originalFireRate = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Bullet.OnBulletDestroyed += RemoveBullet;
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

                activeBullets.Add(bullet);
                fireRate = 0;
            }
        }
        else
        {
            fireRate = fireRate + Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (activeBullets == null) return;

        foreach (var bullet in activeBullets)
        {
            bullet.transform.Translate(Vector2.up * bulletSpeed * Time.fixedDeltaTime);
        }
    }
    
    public void RemoveBullet(GameObject bullet)
    {
        activeBullets.Remove(bullet);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ground"))
        {
            Debug.Log("Collided with ground");
        }
    }
}
