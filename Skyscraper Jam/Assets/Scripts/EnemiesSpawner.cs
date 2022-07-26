using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public PlatformGenerator _platformGenerator;
    public GameObject _enemy;
    public float spawnPercentage = 0.1f;
    public float finalSpawnPercentage = 0.9f;
    private float spawnPercentageIncrease;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnEnemies()
    {
        int platformCounter = 0;
        spawnPercentageIncrease =  Mathf.Abs(finalSpawnPercentage-spawnPercentage)/(_platformGenerator.platformsList.Count/_platformGenerator.platformsPerLevel);

        foreach(var platform in _platformGenerator.platformsList)
        {
            var enemyPosition = platform.transform.position + new Vector3(Random.Range(-1f,1f),0.5f,0);
            if(Random.Range(0f,1f) < spawnPercentage)
            {
                Instantiate(_enemy, enemyPosition, Quaternion.identity);
            }
            if(platformCounter%_platformGenerator.platformsPerLevel == 0)
            {
                spawnPercentage += spawnPercentageIncrease;
            }
            platformCounter++;
        }
    }
}
