using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;
// using System.Diagnostics;
// using System.Diagnostics;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject smallPrefab;
    [SerializeField] private GameObject mediumPrefab;

    [SerializeField] private float spawnerCooldown = 3f;
    [SerializeField] private int spawnBucket = 10;

    private int currentBucket;

    public int MAX_ROUND = 5;
    private int round = 1;

    [SerializeField]
    private List<GameObject> objectList;

    [SerializeField]
    GameObject enemyManager;

    void Start()
    {
        objectList = new List<GameObject> { smallPrefab, mediumPrefab };

        // Start the round system properly
        StartCoroutine(RoundLoop());
    }

    void Update()
    {
            for(int i=0; i<enemyManager.gameObject.transform.childCount; i++)
            {
                enemyManager.gameObject.transform.GetChild(i).gameObject.GetComponent<Enemy>().scale(round);
            }
    }

    private IEnumerator RoundLoop()
    {
        while (round <= MAX_ROUND)
        {
            Debug.Log("Round " + round);

            currentBucket = spawnBucket;

            Debug.Log("Spawning starts in 5 seconds...");
            yield return new WaitForSeconds(5f);

            roundStartText();

            // Start spawning enemies
            yield return StartCoroutine(SpawnEnemies(currentBucket));

            // Wait until all enemies are dead
            while (!IsRoundOver())
                yield return new WaitForSeconds(1f);

            Debug.Log("Round " + round + " complete!");

            spawnBucket += 5;
            round++;
        }
    }

    public void roundStartText()
    {
        Debug.Log("Begin round " + round + "!");
    }

    public bool IsRoundOver()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }

    private IEnumerator SpawnEnemies(int remainingBucket)
    {
        while (remainingBucket > 0)
        {
            GameObject prefab = objectList[UnityEngine.Random.Range(0, objectList.Count)];
            Enemy enemyScript = prefab.GetComponent<Enemy>();
            int cost = enemyScript.data.unitCost;

            // Spawn enemy
            Instantiate(
                prefab,
                new Vector3(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-6f, 6f), 0),
                Quaternion.identity,
                enemyManager.transform
            );

            remainingBucket -= cost;
            yield return new WaitForSeconds(spawnerCooldown);
        }
    }

}