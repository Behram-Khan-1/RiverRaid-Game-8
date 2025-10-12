using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static Action<GameObject> OnBulletDestroyed;
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(BulletLifetime(8f));
    }

    // Update is called once per frame
    void Update()
    {

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
        OnBulletDestroyed?.Invoke(gameObject);
        animator.Play("BulletDestoryAnim");

    }
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
