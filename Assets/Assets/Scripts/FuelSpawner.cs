using Unity.VisualScripting;
using UnityEngine;

public class FuelSpawner : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            GameManager.instance.IncreasePlayerFuel();
        }
        if(collider.CompareTag("Bullet"))
        {
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
    }
    

}
