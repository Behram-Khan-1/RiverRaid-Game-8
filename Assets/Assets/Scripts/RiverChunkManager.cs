using System.Collections.Generic;
using UnityEngine;

public class RiverChunkManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> chunks;
    [SerializeField] private List<GameObject> activeChunks;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform despawnPoint;
    [SerializeField] private Transform startingPoint;

    [SerializeField] private float downwardSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(activeChunks[0].transform.position.y <= despawnPoint.position.y)
        {
            Destroy(activeChunks[0]);
            activeChunks.RemoveAt(0);
            activeChunks.Add(Instantiate(chunks[Random.Range(0, chunks.Count)], spawnPoint.position, Quaternion.identity));
        }
    }

    void FixedUpdate()
    {
        foreach (var chunk in activeChunks)
        {
            chunk.transform.Translate(Vector3.down * downwardSpeed * Time.fixedDeltaTime);
        }


        
    }

    private void SpawnChunk()
    {
        
    }
}
