using Unity.VisualScripting;
using UnityEngine;

public class FuelSpawner : MonoBehaviour
{
    [SerializeField] int fuelRefillAmount;

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
        if (collider.transform.root.CompareTag("Player"))
        {
            GameManager.instance.IncreasePlayerFuel(fuelRefillAmount);
            Destroy(gameObject);
        }
        if (collider.CompareTag("Bullet"))
        {
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
    }


}
