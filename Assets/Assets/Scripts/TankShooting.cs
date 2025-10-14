using Unity.Burst.Intrinsics;
using UnityEngine;

public class TankShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject aimPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Transform turret;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float aimSpeed;
    [SerializeField] private float turretRotationSpeed;
    [SerializeField] private float shootingTimer = 1f;
    [SerializeField] private TankState state = TankState.Idle;

    public float t = 0;
    private Player player;
    GameObject spawnedBullet;
    GameObject spawnedAim;
    Vector3 randomAimPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    //Rotate the tank to face the ahead player position
    //shoot after some time
    //bullet goes ahead of player so a smart bullet.

    //Have an aim visual showing aroudn player to show where bullet is shot.

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {

            case TankState.Idle:
            if(Vector2.Distance(transform.position, player.transform.position) < 10f)
                ChangeState(TankState.Turning);
                break;
            case TankState.Turning:
                FacePlayer();
                break;

            case TankState.Aiming:
                Aim();
                break;


        }


    }
    Vector3 direction;
    void FixedUpdate()
    {
        switch (state)
        {
            case TankState.FixingAim:
                FixingAim();
                break;

            case TankState.Shooting:
                Shoot();
                break;

            case TankState.BulletShooting:
            if(spawnedBullet != null)
                spawnedBullet.transform.Translate(direction.normalized * bulletSpeed * Time.fixedDeltaTime, Space.World);
                break;
        }
    }

    private void FixingAim()
    {
        spawnedAim.transform.position = Vector3.MoveTowards(spawnedAim.transform.position, randomAimPosition,
         aimSpeed * Time.fixedDeltaTime);

        if (Vector2.Distance(spawnedAim.transform.position, randomAimPosition) < 0.1f)
        {
            var randomCircle = Random.insideUnitCircle * 3f;
            randomAimPosition = player.transform.position + new Vector3(randomCircle.x, randomCircle.y, 0);
        }
        t += Time.deltaTime;
        if (t > shootingTimer)
        {
            ChangeState(TankState.Shooting);
        }
    }

    void Shoot()
    {
        spawnedBullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
        direction = new Vector3(spawnedAim.transform.position.x, spawnedAim.transform.position.y) - spawnedBullet.transform.position;
        Destroy(spawnedAim);
        ChangeState(TankState.BulletShooting);
    }

    void Aim()
    {
        var randomCircle = Random.insideUnitCircle * 3f;
        randomAimPosition = player.transform.position + new Vector3(randomCircle.x, randomCircle.y, 0);
        spawnedAim = Instantiate(aimPrefab, randomAimPosition, Quaternion.identity);
        state = TankState.FixingAim;
        t = 0;
    }

    void FacePlayer()
    {
        t += Time.deltaTime;
        if (t > shootingTimer)
        {
            ChangeState(TankState.Aiming);
        }

        Vector2 direction = player.transform.position - turret.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle + 20);
        turret.rotation = Quaternion.RotateTowards(turret.rotation, targetRotation, turretRotationSpeed * Time.deltaTime);
    }

    private void ChangeState(TankState newState)
    {
        state = newState;
    }

}
