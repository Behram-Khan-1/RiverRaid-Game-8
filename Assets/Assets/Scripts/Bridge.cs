using Unity.VisualScripting;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] private int health;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (health <= 0)
        {
            BridgeDestroyed();
        }
        if (collider.CompareTag("Bullet"))
        {
            health--;
            Destroy(collider.gameObject);
        }
    }
    
    void BridgeDestroyed()
    {
        //Play Bridge Destory Animation   
        Destroy(gameObject);
    }
 
}
