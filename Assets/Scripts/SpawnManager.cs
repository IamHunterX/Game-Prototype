using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstacles;
    private Vector3 pos = new Vector3(25, 0, 0);
    private float startDelay = 1.5f;
    private float spawnInterval = 1.0f;
    private PlayerController myPlayer;
    private float leftRange = -10.0f;
    // Start is called before the first frame update
    void Start()
    {
        myPlayer = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("spawnManage", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void spawnManage()
    {
        int randomObstacle = Random.Range(0,4);
        if (myPlayer.gameOver == false)
        {
            Instantiate(obstacles[randomObstacle], pos, obstacles[randomObstacle].transform.rotation);

        }
        
    }
}
