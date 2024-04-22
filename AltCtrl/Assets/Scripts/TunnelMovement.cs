using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelMovement : MonoBehaviour
{
    public static GameManager gm;

    public float velocity;
    public float endTunnelZ = -40;
    public GameObject LastTunnel;
    public static GameObject LastTunnelInstance;
    private float trackMotion;
    public GameObject[] tunnelPrefabs;
    public GameObject[] obstaclePrefabs;
    public float chanceObstacle = 0.5f;

    private void Start()
    {
        //get reference to GameManager
        gm = GameManager.instance;
        if(LastTunnel != null && LastTunnelInstance == null)
        {
            LastTunnelInstance = LastTunnel;
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
            GameObject nextTunnel = Instantiate(tunnelPrefabs[Random.Range(0, tunnelPrefabs.Length)], LastTunnelInstance.transform.position + (5-Time.deltaTime*velocity) *Vector3.forward, LastTunnelInstance.transform.rotation);
                if (Random.value < chanceObstacle)
                {
                Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)],
                    LastTunnelInstance.transform.position + new Vector3(-2.5f, -4f, 0) //center reference point
                    + Random.Range(-5.0f, 5.0f) * Vector3.right + Random.Range(-5.0f, 5.0f) * Vector3.up, //repositioning logic
                    LastTunnelInstance.transform.rotation);
                }
                LastTunnelInstance = nextTunnel;
            }
            Destroy(gameObject);

        }
    }
}
