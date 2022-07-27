using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{

    public EnemiesSpawner _enemiesSpawner;
    public BuildingGenerator _buildingGenerator;
    public int buildingHeight = 1;
    public int platformsPerLevel = 2;
    public float heightBetweenLevels = 5f;
    public float xRange = 7f;
    public GameObject platform;
    public GameObject platformParents;
    public List<GameObject> platformsList;
    private float platformX, lastPlatformPos = 0f;

    private float yRange = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        int iterations = 0;
        for(int i = -1; i < buildingHeight; i++)
        {
            Dictionary<float, bool> takenPositions = new Dictionary<float, bool>();
            for(int j = 0; j < platformsPerLevel; j++)
            {
                var randomYOffset = Random.Range(0, yRange);
                do
                {
                    platformX = Random.Range(-xRange,xRange);
                    iterations++;
                    if(iterations > 100) break;
                }while(checkIfTooClose(takenPositions, platformX));
                Vector3 position = new Vector3(platformX, i*heightBetweenLevels+randomYOffset, 0);
                takenPositions.Add(platformX, true);
                GameObject tempPlatform = Instantiate(platform, position, Quaternion.identity,platformParents.transform);
                platformsList.Add(tempPlatform);
            }
            iterations = 0;
        }
        _enemiesSpawner.spawnEnemies();
        _buildingGenerator.generateBuilding(platformsList[platformsList.Count-1].transform.position.y);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool checkIfTooClose(Dictionary<float,bool> takenPositions, float newX)
    {
        foreach(var pos in takenPositions)
        {
            if(Mathf.Abs(pos.Key - newX) < 4f)
            {
                return true;
            }
        }
        return false;
    }
}
