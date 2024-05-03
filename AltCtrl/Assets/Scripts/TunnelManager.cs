using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TunnelManager : MonoBehaviour
{

    public static TunnelManager TMinstance { get; private set; }

    public static GameManager gm;

    public float velocity;
    public GameObject LastTunnel;   
    public GameObject[] tunnelPrefabs;
    public GameObject[] obstaclePrefabs;
    public float chanceObstacle = 0.5f;


    private void Awake()
    {
        // Ensure this is the only instance. If not, destroy self.
        if (TMinstance != null && TMinstance != this) { Destroy(this); }
        else { TMinstance = this; }
        //Check that Last tunnel has been assigned
        if (LastTunnel == null) { Debug.LogError("Assign Last tunnel in inspector!!!"); }

    }

    // Start is called before the first frame update
    void Start()
    {
        //get reference to GameManager
        gm = GameManager.instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = gm.getCurrSpeed();
    }

    public void SpawnTunnel()
    {
        GameObject nextTunnel = Instantiate(tunnelPrefabs[Random.Range(0, tunnelPrefabs.Length)], LastTunnel.transform.position + (5 - Time.deltaTime * velocity) * Vector3.forward, LastTunnel.transform.rotation);

        if (Random.value < chanceObstacle)
        {
            Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)],
                LastTunnel.transform.position + new Vector3(-2.5f, -4f, 0) //center reference point
                + Random.Range(-5.0f, 5.0f) * Vector3.right + Random.Range(-5.0f, 5.0f) * Vector3.up, //repositioning logic
                LastTunnel.transform.rotation);
        }
        LastTunnel = nextTunnel;
    }
}
