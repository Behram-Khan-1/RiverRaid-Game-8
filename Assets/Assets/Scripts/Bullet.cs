using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Animator animator;

    [SerializeField] private float bulletSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(BulletLifetime(2.25f));
    }
    public void SetupBullet(float bulletSpeed)
    {
        this.bulletSpeed = bulletSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.Translate(Vector2.up * bulletSpeed * Time.fixedDeltaTime);
    }

    IEnumerator BulletLifetime(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        StopAndDestroyBullet();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ground"))
        {
            StopAndDestroyBullet();
        }
    }
    private void StopAndDestroyBullet()
    {
        animator.Play("BulletDestoryAnim");
    }
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
