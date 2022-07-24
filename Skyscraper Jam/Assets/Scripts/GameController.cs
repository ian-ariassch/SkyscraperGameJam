using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameController : MonoBehaviour
{
    public PlatformGenerator platformGenerator;
    public PolygonCollider2D cameraConfiner;
    public GameObject virtualCam;
    // Start is called before the first frame update
    public float xRange;
    void Awake()
    {
        xRange = Camera.main.orthographicSize * Camera.main.aspect + 0.1f;
        platformGenerator.xRange = xRange;
        Vector2[] cameraBoundsPoints = new Vector2[4];
        cameraBoundsPoints[0] = new Vector2(-xRange, 2000);
        cameraBoundsPoints[1] = new Vector2(xRange, 2000);
        cameraBoundsPoints[2] = new Vector2(xRange, -6);
        cameraBoundsPoints[3] = new Vector2(-xRange, -6);
        cameraConfiner.points = cameraBoundsPoints;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
