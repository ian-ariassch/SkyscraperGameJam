using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{
    public GameObject topOfBuilding;
    public GameObject middleOfBuilding;
    public GameObject firstMiddlePiece;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateBuilding(float lastPlatformHeight)
    {
        topOfBuilding = Instantiate(topOfBuilding, new Vector3(topOfBuilding.transform.position.x, lastPlatformHeight-3, 0), Quaternion.identity);
        topOfBuilding.SetActive(true);
        var middleOfBuildingHeight = middleOfBuilding.GetComponent<SpriteRenderer>().bounds.size.y;
        var startPosition = firstMiddlePiece.transform.position;
        var position = new Vector3(startPosition.x, startPosition.y - 1, 0);
        GameObject lastMiddlePiece = firstMiddlePiece;
        int i = 0;
        while(position.y + middleOfBuildingHeight/2 < lastPlatformHeight)
        {
            position = new Vector3(startPosition.x, startPosition.y + i * (middleOfBuildingHeight-2f), 0);
            if(position.y < topOfBuilding.transform.position.y)
            {
                lastMiddlePiece = Instantiate(middleOfBuilding, position, Quaternion.identity);
            }
            i++;
        }
    }
}
