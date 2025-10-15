using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private PlayerMovement PlayerMovement;
    private Player Player;
    [SerializeField] private RiverChunkManager riverChunkManager;
    [SerializeField] private int spawnedChunks;

    [SerializeField] private Slider fuelSlider;
    [SerializeField] private GameObject DeathUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        PlayerMovement.onSpeedChange += Player_YSpeedChange;
        Player = PlayerMovement.GetComponent<Player>();

    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        DeathUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void ChangeFuelSlider(float fuel)
    {
        fuelSlider.value = fuel;
    }

    public void IncreasePlayerFuel(float fuelRefillAmount)
    {
        Player.IncreaseFuel(fuelRefillAmount);
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

        if (chunkManager != null)
        {
            chunkManager.SpawnContent(fuelSpawnChance, tankSpawnChance, boatSpawnChance);
            spawnedChunks++;
        }
    }
    
    public void OnPlayerDead()
    {
        GameOver();
        Player.PlayerDeath();
    }





}
