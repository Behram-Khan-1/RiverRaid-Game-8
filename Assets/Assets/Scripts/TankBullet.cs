using System.Collections;
using UnityEngine;

public class TankBullet : MonoBehaviour
{
    [SerializeField] private float timeToLive = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(BulletLifetime(timeToLive));
    }
    
    private IEnumerator BulletLifetime(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.transform.root.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameManager.instance.OnPlayerDead();
            //Player Killed
        }
    }
}
