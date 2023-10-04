using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpawnManager : MonoBehaviour
{
    private float spawnRange = 5.0f;
    private Transform playerTrans;
    private int spawnCount = 3;
    public bool isSpawn;

    public GameObject[] powerupPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        playerTrans = GameManager.Instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isSpawn)
        {
            SpawnPowerup(spawnCount);
        }
    }

    private IEnumerator PowerupSpawnCountdownRoutine()
    {
        yield return new WaitForSeconds(10);
        isSpawn = false;
    } 

    private void SpawnPowerup(int count)
    {
        if (GameManager.Instance.player.hp < 3 || GameManager.Instance.manager.dashCollision)
        {
            isSpawn = true;
            for (int i = 0; i < count; i++)
            {
                int randomPowerup = Random.Range(0, powerupPrefabs.Length);
                Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPosition(), powerupPrefabs[randomPowerup].transform.rotation);
                StartCoroutine(PowerupSpawnCountdownRoutine());
            }
        }

    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = playerTrans.position.x + Random.Range(-spawnRange, spawnRange);
        float spawnPosY = playerTrans.position.z + Random.Range(0, spawnRange * 2);

        Vector3 randomPos = new Vector3(spawnPosX, 1.5f, spawnPosY);

        return randomPos;
    }
}
