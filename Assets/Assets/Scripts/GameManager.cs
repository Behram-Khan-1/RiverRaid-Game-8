using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private PlayerMovement PlayerMovement;
    [SerializeField] private RiverChunkManager riverChunkManager;
    [SerializeField] private int spawnedChunks;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        PlayerMovement.onSpeedChange += Player_YSpeedChange;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreasePlayerFuel()
    {

    }

    public void Player_YSpeedChange(float speed)
    {
        if (speed > 0)
        {
            riverChunkManager.IncreaseDownSpeed();
        }
        else
        {
            riverChunkManager.ResetDownSpeed();
        }
        
    }

    //Spawn Bridge
    //Setup Fuel system and UI

    public void OnChunkSpawn(GameObject chunk, float fuelSpawnChance, float tankSpawnChance, float boatSpawnChance)
    {
        var chunkManager = chunk.GetComponent<ChunkManager>();
        if (spawnedChunks % 7 == 0)
        {
            chunkManager.isBridge = true;
            spawnedChunks = 0;
        }
        
        if( chunkManager != null)
        {
            chunkManager.SpawnContent(fuelSpawnChance, tankSpawnChance, boatSpawnChance);
            spawnedChunks++;
        }
      
    }





}
