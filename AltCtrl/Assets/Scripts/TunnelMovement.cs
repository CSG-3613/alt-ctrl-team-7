using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelMovement : MonoBehaviour
{
<<<<<<< Updated upstream
    public float velocity = 2f;
    public float endTunnelZ = -40;
    public GameObject SpawnPosition;
    public static Transform startingPosition;
    private float trackMotion;
=======
    public static GameManager gm;
    public static TunnelManager tm;

    public float velocity;
    public float endTunnelZ = -40;
    public GameObject LastTunnel;
    public static GameObject LastTunnelInstance;
>>>>>>> Stashed changes
    public GameObject[] tunnelPrefabs;
    public GameObject[] obstaclePrefabs;
    public float chanceObstacle = 0.5f;

    private void Start()
    {
<<<<<<< Updated upstream
        if(SpawnPosition != null && startingPosition == null)
=======
        //get reference to GameManager
        gm = GameManager.instance;
        //get reference to TunnelManager
        tm = TunnelManager.TMinstance;
        if(LastTunnel != null && LastTunnelInstance == null)
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
            Instantiate(tunnelPrefabs[Random.Range(0, tunnelPrefabs.Length)], startingPosition.position, startingPosition.rotation);
            if (Random.value < chanceObstacle)
            {
                Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)],
                    startingPosition.position + new Vector3(-2.5f, 1.5f, 0) //center reference point
                    + Random.Range(-5.0f, 5.0f) * Vector3.right + Random.Range(-5.0f, 5.0f) * Vector3.up, //repositioning logic
                    startingPosition.rotation);
            }
=======
            
>>>>>>> Stashed changes
            Destroy(gameObject);

        }
    }
}
