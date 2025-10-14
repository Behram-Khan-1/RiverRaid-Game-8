using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public GameObject fuelPrefab;
    public GameObject tankPrefab;
    public GameObject boatPrefab;
    public GameObject bridgePrefab;

    public bool isBridge;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void SpawnContent(float fuelSpawnChance, float tankSpawnChance, float boatSpawnChance)
    {
        // Debug.Log("Spawning content");
        if (isBridge)
        {
            // Debug.Log("Spawning bridge");
            SpawnBridge();
            return;
        }

        if (Random.value < fuelSpawnChance)
        {
            SpawnFuel();
        }
        if (Random.value < boatSpawnChance)
        {
            SpawnBoat();
        }
        if (Random.value < tankSpawnChance)
        {
            SpawnTank();
        }
    }
    void SpawnBridge()
    {
        Instantiate(bridgePrefab, gameObject.transform.position, Quaternion.identity, gameObject.transform);
    }

    void SpawnFuel()
    {
        Transform waterArea = transform.Find("WaterArea");
        var spawnPoint = GetSpawnPoint(waterArea);
        GameObject fuel = Instantiate(fuelPrefab, spawnPoint, Quaternion.identity, gameObject.transform);
        fuel.gameObject.SetActive(true);
    }

    void SpawnBoat()
    {
        Transform waterArea = transform.Find("WaterArea");
        var spawnPoint = GetSpawnPoint(waterArea);
        GameObject boat = Instantiate(boatPrefab, spawnPoint, Quaternion.identity, gameObject.transform);
        boat.gameObject.SetActive(true);
    }

    void SpawnTank()
    {
        Transform groundArea = transform.Find("GroundArea");
        var spawnArea = groundArea.GetChild(Random.Range(0, groundArea.childCount));
        if (spawnArea.childCount > 0)
        {
            spawnArea = spawnArea.GetChild(Random.Range(0, spawnArea.childCount));
        }
        else
        {
            spawnArea = spawnArea.GetChild(0);
        }

        var spawnPoint = GetSpawnPoint(spawnArea);
        GameObject tank = Instantiate(tankPrefab, spawnPoint, Quaternion.identity, gameObject.transform);
        tank.gameObject.SetActive(true);
    }



    private Vector2 GetSpawnPoint(Transform area)
    {
        var collider = area.GetComponent<Collider2D>();
        Bounds bounds = collider.bounds;
        Vector2 point;

        do
        {
            // pick random point inside the polygon’s bounding box
            point = new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y)
            );
        }
        while (!IsPointInPolygon(collider, point));

        return point;
    }

    bool IsPointInPolygon(Collider2D collider, Vector2 point)
    {
        return collider.OverlapPoint(point);
    }
}
