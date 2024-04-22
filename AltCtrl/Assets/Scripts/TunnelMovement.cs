using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelMovement : MonoBehaviour
{
    public static GameManager gm;

    public float velocity;
    public float endTunnelZ = -40;
    public GameObject SpawnPosition;
    public static Transform startingPosition;
    private float trackMotion;
    public GameObject[] tunnelPrefabs;
    public GameObject[] obstaclePrefabs;
    public float chanceObstacle = 0.5f;

    private void Start()
    {
        //get reference to GameManager
        gm = GameManager.instance;

        if(SpawnPosition != null && startingPosition == null)
        {
            startingPosition = SpawnPosition.transform;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //get current velocity from GM
        velocity = gm.getCurrSpeed();

        gameObject.transform.position = gameObject.transform.position + (Vector3.back)*velocity*Time.deltaTime;
        if (this.gameObject.transform.position.z<=endTunnelZ)
        {
            if(gameObject.layer == 3){
            Instantiate(tunnelPrefabs[Random.Range(0, tunnelPrefabs.Length)], startingPosition.position, startingPosition.rotation);
                if (Random.value < chanceObstacle)
                {
                Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)],
                    startingPosition.position + new Vector3(-2.5f, 1.5f, 0) //center reference point
                    + Random.Range(-5.0f, 5.0f) * Vector3.right + Random.Range(-5.0f, 5.0f) * Vector3.up, //repositioning logic
                    startingPosition.rotation);
                }
            }
            Destroy(gameObject);

        }
    }
}
