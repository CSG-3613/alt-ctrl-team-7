using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelMovement : MonoBehaviour
{
    public float velocity = 2f;
    public float endTunnelZ = -40;
    public GameObject SpawnPosition;
    public static Transform startingPosition;
    private float trackMotion;
    public GameObject[] tunnelPrefabs;
    public GameObject[] obstaclePrefabs;
    public float chanceObstacle = 0.5f;

    private void Start()
    {
        if(SpawnPosition != null && startingPosition == null)
        {
            startingPosition = SpawnPosition.transform;
        }
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = gameObject.transform.position + (Vector3.back)*velocity*Time.deltaTime;
        if (this.gameObject.transform.position.z<=endTunnelZ)
        {
            Instantiate(tunnelPrefabs[Random.Range(0, tunnelPrefabs.Length)], startingPosition.position, startingPosition.rotation);
            if (Random.value < chanceObstacle)
            {
                Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)],
                    startingPosition.position //center reference point
                    + Random.Range(-1, 1) * 5 * Vector3.right 
                    + Random.Range(-1, 1) * 5 * Vector3.up, //repositioning logic
                    startingPosition.rotation);
            }
            Destroy(gameObject);

        }
    }
}
