using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTunnel : MonoBehaviour
{
    public GameObject StartTunnelUnit;
    private float TrackMotion;
    private float velocity;
    public GameObject[] tunnelPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        velocity = StartTunnelUnit.GetComponent<TunnelMovement>().velocity;
    }

    // Update is called once per frame
    void Update()
    {
        TrackMotion = velocity * Time.deltaTime;
        if(TrackMotion == 5)
        {
            Object.Instantiate(tunnelPrefabs[Random.Range(0, tunnelPrefabs.Length)], StartTunnelUnit.transform);
        }
    }
}
